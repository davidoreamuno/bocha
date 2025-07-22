using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos.Servicios.Revision;
using Abstracciones.Interfaces.Reglas;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Servicios
{

    public class RevisionServicio : IRevisionServicio
    {

        private readonly IConfiguracion _configuration;
        private readonly IHttpClientFactory _httpclient;

        public RevisionServicio(IConfiguracion configuration, IHttpClientFactory httpclient)
        {
            _configuration = configuration;
            _httpclient = httpclient;
        }

        public async Task<Revision> Obtener(string placa)
        {
            var endPoint = _configuration.ObtenerMetodo("ApiEndPointsRevision",
                "ObtenerRevision");
            var servicioRegistro = _httpclient.CreateClient("ServicioRevision");
            var respuesta = await servicioRegistro.GetAsync(string.Format(endPoint, placa));
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var resultadoDeserializado = JsonSerializer.Deserialize<List<Revision>>(resultado, opciones);
            return resultadoDeserializado.FirstOrDefault();
        }

    }
}
