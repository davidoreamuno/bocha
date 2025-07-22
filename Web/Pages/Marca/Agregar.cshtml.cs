using System.Net.Http.Json;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Marcas
{
    public class AgregarModel : PageModel
    {
        private readonly IConfigurationApp _configuration;

        public AgregarModel(IConfigurationApp configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public MarcaRequest marca { get; set; } = new();

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            string endpoint = _configuration.ObtenerMetodo("ApiEndPoints", "AgregarMarca");
            marca.IdMarca = Guid.NewGuid();

            using var cliente = new HttpClient();
            var respuesta = await cliente.PostAsJsonAsync(endpoint, marca);

            if (respuesta.IsSuccessStatusCode)
                return RedirectToPage("./Index");

            ModelState.AddModelError(string.Empty, "Error al agregar la marca.");
            return Page();
        }
    }
}
