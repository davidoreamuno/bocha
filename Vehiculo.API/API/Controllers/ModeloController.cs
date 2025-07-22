using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModeloController : ControllerBase, IModeloController
    {
        private readonly IModeloFlujo _modeloFlujo;
        private readonly ILogger<ModeloController> _logger;

        public ModeloController(IModeloFlujo modeloFlujo, ILogger<ModeloController> logger)
        {
            _modeloFlujo = modeloFlujo;
            _logger = logger;
        }

        #region Operaciones

        [HttpPost]
        public async Task<IActionResult> Agregar(ModeloRequest modelo)
        {
            var resultado = await _modeloFlujo.Agregar(modelo);
            return CreatedAtAction(nameof(Obtener), new { id = resultado }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(Guid id, ModeloRequest modelo)
        {
            if (!await VerificarModeloExiste(id))
                return NotFound("El modelo no existe");

            var resultado = await _modeloFlujo.Editar(id, modelo);
            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            if (!await VerificarModeloExiste(id))
                return NotFound("El modelo no existe");

            var resultado = await _modeloFlujo.Eliminar(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _modeloFlujo.Obtener();
            if (!resultado.Any())
                return NoContent();
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener(Guid id)
        {
            var resultado = await _modeloFlujo.Obtener(id);
            if (resultado == null)
                return NotFound("Modelo no encontrado");
            return Ok(resultado);
        }

        #endregion

        #region Helpers

        private async Task<bool> VerificarModeloExiste(Guid id)
        {
            var modelo = await _modeloFlujo.Obtener(id);
            return modelo != null;
        }

        #endregion
    }
}
