using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos.Servicios.Registros;
using Abstracciones.Interfaces.Reglas;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Servicios
{
  
    public class RegistroServicio : IRegistroServicio
    {
        
        private readonly IConfiguracion _configuration;
        private readonly IHttpClientFactory _httpclient;

        public RegistroServicio(IConfiguracion configuration, IHttpClientFactory httpclient)
        {
            _configuration = configuration;
            _httpclient = httpclient;
        }

        public async Task <Propietario> Obtener(string placa)
        {
            var endPoint = _configuration.ObtenerMetodo("ApiEndPointsRegistro",
                "ObtenerRegistro");
            var servicioRegistro = _httpclient.CreateClient("ServicioRegistro");
            var respuesta = await servicioRegistro.GetAsync(string.Format(endPoint,placa));
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var resultadoDeserializado = JsonSerializer.Deserialize<List<Propietario>>(resultado, opciones);
            return resultadoDeserializado.FirstOrDefault();
        }

    }
}
