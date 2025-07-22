using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Marcas
{
    public class IndexModel : PageModel
    {
        private IConfigurationApp _configuration;
        public IList<MarcaResponse> marcas { get; set; } = default!;

        public IndexModel(IConfigurationApp configuration)
        {
            _configuration = configuration;
        }

        public async Task OnGet()
        {
            string endpoint = _configuration.ObtenerMetodo("ApiEndPoints", "ObtenerMarcas");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                marcas = JsonSerializer.Deserialize<List<MarcaResponse>>(resultado, opciones);
            }
        }
    }
}