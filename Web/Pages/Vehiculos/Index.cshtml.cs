using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Threading.Tasks;

namespace Web.Pages.Vehiculos
{
    public class IndexModel : PageModel
    {
        private readonly IConfigurationApp _configuration;
        public IList<VehiculoResponse> vehiculos { get; set; } = default!;
        public IndexModel(IConfigurationApp Configuration)
        {
            _configuration = Configuration;
        }

        public async Task OnGet()
        {
            string endpoint = _configuration.ObtenerMetodo("ApiEndPoints", "ObtenerVehiculos");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            var resultado= await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions
            { PropertyNameCaseInsensitive = true };
            vehiculos = JsonSerializer.Deserialize<List<VehiculoResponse>>
                (resultado, opciones);
        }
    }
}
