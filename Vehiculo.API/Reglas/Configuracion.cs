using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Servicios;
using Microsoft.Extensions.Configuration;




namespace Reglas
{
    public class Configuracion : IConfiguracion

    {
        private IConfiguration _configuration;

        public Configuracion(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ObtenerMetodo(string seccion, string nombre)
        {
            var apiEndPoint = _configuration.GetSection(seccion).Get<APIEndPoint>();

            if (apiEndPoint?.Metodos == null)
                throw new InvalidOperationException($"No se encontraron métodos en la sección '{seccion}'.");

            var metodo = apiEndPoint.Metodos.FirstOrDefault(m => m.Nombre == nombre);

            if (metodo == null)
                throw new InvalidOperationException($"Método con nombre '{nombre}' no encontrado en la sección '{seccion}'.");

            return $"{apiEndPoint.UrlBase}/{metodo.Url}";
        }


        private string? ObtenerUrlBase(string seccion)
        {
            return _configuration.GetSection(seccion).Get<APIEndPoint>
                            ().UrlBase;
        }

        public string ObtenerValor(string llave)
        {
            return _configuration.GetSection(llave).Value;
        }
    }
}
