using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flujo
{
    public class ModeloFlujo : IModeloFlujo
    {
        private readonly IModeloDA _modeloDA;

        public ModeloFlujo(IModeloDA modeloDA)
        {
            _modeloDA = modeloDA;
        }

        public Task<Guid> Agregar(ModeloRequest modelo)
        {
            return _modeloDA.Agregar(modelo);
        }

        public Task<Guid> Editar(Guid id, ModeloRequest modelo)
        {
            return _modeloDA.Editar(id, modelo);
        }

        public Task<Guid> Eliminar(Guid id)
        {
            return _modeloDA.Eliminar(id);
        }

        public Task<IEnumerable<ModeloResponse>> Obtener()
        {
            return _modeloDA.Obtener();
        }

        public Task<ModeloResponse> Obtener(Guid id)
        {
            return _modeloDA.Obtener(id);
        }
    }
}
