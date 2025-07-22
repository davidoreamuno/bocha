using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.DA
{
    public interface IModeloDA
    {
        Task<IEnumerable<ModeloResponse>> Obtener();
        Task<ModeloResponse> Obtener(Guid id);
        Task<Guid> Agregar(ModeloRequest request);
        Task<Guid> Editar(Guid id, ModeloRequest request);
        Task<Guid> Eliminar(Guid id);
    }
}
