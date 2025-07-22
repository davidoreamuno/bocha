using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase, IMarcaController
    {
        private readonly IMarcaFlujo _marcaFlujo;
        private readonly ILogger<MarcaController> _logger;

        public MarcaController(IMarcaFlujo marcaFlujo, ILogger<MarcaController> logger)
        {
            _marcaFlujo = marcaFlujo;
            _logger = logger;
        }

        #region Operaciones

        [HttpPost]
        public async Task<IActionResult> Agregar(MarcaRequest marca)
        {
            var resultado = await _marcaFlujo.Agregar(marca);
            return CreatedAtAction(nameof(Obtener), new { id = resultado }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(Guid id, MarcaRequest marca)
        {
            if (!await VerificarMarcaExiste(id))
                return NotFound("La marca no existe");

            var resultado = await _marcaFlujo.Editar(id, marca);
            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            if (!await VerificarMarcaExiste(id))
                return NotFound("La marca no existe");

            var resultado = await _marcaFlujo.Eliminar(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _marcaFlujo.Obtener();
            if (!resultado.Any())
                return NoContent();
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener(Guid id)
        {
            var resultado = await _marcaFlujo.Obtener(id);
            if (resultado == null)
                return NotFound("Marca no encontrada");
            return Ok(resultado);
        }

        #endregion

        #region Helpers

        private async Task<bool> VerificarMarcaExiste(Guid id)
        {
            var marca = await _marcaFlujo.Obtener(id);
            return marca != null;
        }

        #endregion
    }
}
