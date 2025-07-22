using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flujo
{
    public class MarcaFlujo : IMarcaFlujo
    {
        private readonly IMarcaDA _marcaDA;

        public MarcaFlujo(IMarcaDA marcaDA)
        {
            _marcaDA = marcaDA;
        }

        public Task<Guid> Agregar(MarcaRequest marca)
        {
            return _marcaDA.Agregar(marca);
        }

        public Task<Guid> Editar(Guid id, MarcaRequest marca)
        {
            return _marcaDA.Editar(id, marca);
        }

        public Task<Guid> Eliminar(Guid id)
        {
            return _marcaDA.Eliminar(id);
        }

        public Task<IEnumerable<MarcaResponse>> Obtener()
        {
            return _marcaDA.Obtener();
        }

        public Task<MarcaResponse> Obtener(Guid id)
        {
            return _marcaDA.Obtener(id);
        }
    }
}
