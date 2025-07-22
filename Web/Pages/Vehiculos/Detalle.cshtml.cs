using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Web.Pages.Vehiculos
{
    public class DetalleModel : PageModel
    {
        private readonly IConfigurationApp _configuration;

        public VehiculoResponse Vehiculo { get; set; } = default!;

        public DetalleModel(IConfigurationApp configuration)
        {
            _configuration = configuration;
        }

        public async Task OnGet(Guid? id)
        {
            if (id == null)
            {
                
                return;
            }

            string endpoint = string.Format(
                _configuration.ObtenerMetodo("ApiEndPoints", "ObtenerVehiculo"), id);

            var cliente = new HttpClient();

            
            cliente.DefaultRequestHeaders.Accept.Clear();
            cliente.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var respuesta = await cliente.GetAsync(endpoint);
            respuesta.EnsureSuccessStatusCode();

            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Vehiculo = JsonSerializer.Deserialize<VehiculoResponse>(resultado, opciones)!;
        }
    }
}
