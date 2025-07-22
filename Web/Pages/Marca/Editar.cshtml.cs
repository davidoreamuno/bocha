using System.Net.Http.Json;
using System.Text.Json;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Marcas
{
    public class EditarModel : PageModel
    {
        private readonly IConfigurationApp _configuration;

        public EditarModel(IConfigurationApp configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public MarcaRequest marca { get; set; } = new();

        public async Task<IActionResult> OnGet(Guid? id)
        {
            if (!id.HasValue || id == Guid.Empty)
                return NotFound();

            string endpoint = _configuration.ObtenerMetodo("ApiEndPoints", "ObtenerMarca");

            using var cliente = new HttpClient();
            var respuesta = await cliente.GetAsync($"{endpoint}/{id}");

            if (!respuesta.IsSuccessStatusCode)
                return NotFound();

            var json = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var marcaRespuesta = JsonSerializer.Deserialize<MarcaResponse>(json, opciones);

            if (marcaRespuesta == null)
                return NotFound();

            marca = new MarcaRequest
            {
                IdMarca = marcaRespuesta.Id,
                Nombre = marcaRespuesta.Nombre
            };

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid || marca.IdMarca == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "La información de la marca es inválida.");
                return Page();
            }

            string endpoint = _configuration.ObtenerMetodo("ApiEndPoints", "EditarMarca");

            using var cliente = new HttpClient();
            var respuesta = await cliente.PutAsJsonAsync($"{endpoint}/{marca.IdMarca}", marca);

            if (respuesta.IsSuccessStatusCode)
                return RedirectToPage("./Index");

            ModelState.AddModelError(string.Empty, "Error al actualizar la marca.");
            return Page();
        }
    }
}
