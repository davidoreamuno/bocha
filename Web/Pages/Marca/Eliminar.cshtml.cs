using System.Text.Json;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Marcas
{
    public class EliminarModel : PageModel
    {
        private readonly IConfigurationApp _configuration;

        public MarcaResponse marca { get; set; } = default!;

        public EliminarModel(IConfigurationApp configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGet(Guid? id)
        {
            if (id == null || id == Guid.Empty)
                return NotFound();

            string endpoint = _configuration.ObtenerMetodo("ApiEndPoints", "ObtenerMarca");

            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));
            var respuesta = await cliente.SendAsync(solicitud);

            if (!respuesta.IsSuccessStatusCode)
                return NotFound();

            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            marca = JsonSerializer.Deserialize<MarcaResponse>(resultado, opciones)!;

            return Page();
        }

        public async Task<IActionResult> OnPost(Guid? id)
        {
            if (id == null || id == Guid.Empty)
                return NotFound();

            string endpoint = _configuration.ObtenerMetodo("ApiEndPoints", "EliminarMarca");

            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Delete, string.Format(endpoint, id));
            var respuesta = await cliente.SendAsync(solicitud);

            if (respuesta.IsSuccessStatusCode)
                return RedirectToPage("Index");

            ModelState.AddModelError(string.Empty, "Error al eliminar la marca.");
            return Page();
        }
    }
}
