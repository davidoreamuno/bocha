using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Servicios;
using Microsoft.Extensions.Configuration;

namespace Reglas
{
    public class Configuration : IConfigurationApp
    {
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;

        public Configuration(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ObtenerMetodo(string seccion, string nombre)
        {
            string? UrlBase = ObtenerUrlBase(seccion);

            var apiEndPoint = _configuration.GetSection(seccion).Get<APIEndPoint>();

            if (apiEndPoint?.Metodos == null)
            {
                throw new InvalidOperationException($"No se encontraron métodos en la sección '{seccion}'.");
            }

            var metodo = apiEndPoint.Metodos.FirstOrDefault(m => m.Nombre == nombre);

            if (metodo == null)
            {
                throw new InvalidOperationException($"Método con nombre '{nombre}' no encontrado en la sección '{seccion}'.");
            }

            return $"{UrlBase}/{metodo.Url}";
        }

        private string? ObtenerUrlBase(string seccion)
        {
            var apiEndPoint = _configuration.GetSection(seccion).Get<APIEndPoint>();
            return apiEndPoint?.UrlBase;
        }

        public string ObtenerUrl(string llave)
        {
            return _configuration.GetSection(llave).Value ?? throw new InvalidOperationException($"No se encontró el Url para la llave '{llave}'.");
        }
    }
}
