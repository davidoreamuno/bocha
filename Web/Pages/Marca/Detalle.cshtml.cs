using System.Text.Json;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Marcas
{
    public class DetalleModel : PageModel
    {
        private readonly IConfigurationApp _configuration;

        public MarcaResponse marca { get; set; } = default!;

        public DetalleModel(IConfigurationApp configuration)
        {
            _configuration = configuration;
        }

        public async Task OnGetAsync(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                marca = null!;
                return;
            }

            string endpoint = _configuration.ObtenerMetodo("ApiEndPoints", "ObtenerMarca");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();

            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            marca = JsonSerializer.Deserialize<MarcaResponse>(resultado, opciones)!;
        }
    }
}
