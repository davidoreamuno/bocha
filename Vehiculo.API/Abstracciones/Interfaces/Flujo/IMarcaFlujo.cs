using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IMarcaFlujo
    {
        Task<IEnumerable<MarcaResponse>> Obtener();
        Task<MarcaResponse> Obtener(Guid id);
        Task<Guid> Agregar(MarcaRequest request);
        Task<Guid> Editar(Guid id, MarcaRequest marca);
        Task<Guid> Eliminar(Guid id);
    }
}
