using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.DA
{
    public interface IMarcaDA
    {
        Task<IEnumerable<MarcaResponse>> Obtener();
        Task<MarcaResponse> Obtener(Guid id);
        Task<Guid> Agregar(MarcaRequest request);
        Task<Guid> Editar(Guid id, MarcaRequest request);
        Task<Guid> Eliminar(Guid id);
    }
}
