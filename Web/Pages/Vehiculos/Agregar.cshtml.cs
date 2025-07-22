using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OpenXmlPowerTools;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Web.Pages.Vehiculos
{
    public class AgregarModel : PageModel
    {


        private readonly IConfigurationApp _configuration;
        
        public AgregarModel(IConfigurationApp Configuration)
        {
            _configuration = Configuration;
        }
        [BindProperty]
        public VehiculoRequest vehiculos { get; set; }

        [BindProperty]
        public List<SelectListItem> marcas { get; set; }

        [BindProperty]
        public List<SelectListItem> modelos { get; set; }
        [BindProperty]
        public Guid marcaseleccionada { get; set; }

        public async Task<ActionResult> OnGet()
        {
            await ObtenerMarcas();
            return Page();

        }

        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            string endpoint = _configuration.ObtenerMetodo("ApiEndPoints", "AgregarVehiculos");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Post,endpoint);
            var respuesta = await cliente.PostAsJsonAsync(endpoint,vehiculos);
            respuesta.EnsureSuccessStatusCode();
            return RedirectToPage("./Index");
        }


        private async Task ObtenerMarcas()
        {
            string endpoint = _configuration.ObtenerMetodo("ApiEndPoints", "ObtenerMarcas");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions
            { PropertyNameCaseInsensitive = true };
            var resultadodeserializado = JsonSerializer.Deserialize<List<MarcaResponse>>
                (resultado, opciones);
            marcas = resultadodeserializado.Select(m=>
            new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Nombre,
            }
            ).ToList();
        }
        private async Task<List<ModeloBase>> ObtenerModelos(Guid marcaId)
        {
            string endpoint = _configuration.ObtenerMetodo("ApiEndPoints", "ObtenerModelos");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, marcaId));

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions
                { PropertyNameCaseInsensitive = true };
                return  JsonSerializer.Deserialize<List<ModeloBase>>
                    (resultado, opciones);
            }
            
                return new List<ModeloBase>();
                
            
            
        }
        public async Task<JsonResult> OnGetObtenerModelos(Guid marcaId)
        {
            var modelos = await ObtenerModelos(marcaId);
            return new JsonResult(modelos);
        }
    }
}
