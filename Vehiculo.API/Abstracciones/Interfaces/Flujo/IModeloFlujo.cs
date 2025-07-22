using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IModeloFlujo
    {
        Task<IEnumerable<ModeloResponse>> Obtener();
        Task<ModeloResponse> Obtener(Guid id);
        Task<Guid> Agregar(ModeloRequest request);
        Task<Guid> Editar(Guid id, ModeloRequest modelo);
        Task<Guid> Eliminar(Guid id);
    }
}

