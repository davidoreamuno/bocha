using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DA
{
    public class MarcaDA : IMarcaDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public MarcaDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        #region Operaciones

        public async Task<Guid> Agregar(MarcaRequest marca)
        {
            string procedimiento = "CrearMarca";
            var id = Guid.NewGuid();

            var resultado = await _sqlConnection.ExecuteScalarAsync<Guid>(
                procedimiento,
                new { Id = id, Nombre = marca.Nombre },
                commandType: CommandType.StoredProcedure
            );

            return resultado;
        }

        public async Task<Guid> Editar(Guid id, MarcaRequest marca)
        {
            await VerificarMarcaExiste(id);

            string procedimiento = "EditarMarca";

            var resultado = await _sqlConnection.ExecuteScalarAsync<Guid>(
                procedimiento,
                new { Id = id, Nombre = marca.Nombre },
                commandType: CommandType.StoredProcedure
            );

            return resultado;
        }

        public async Task<Guid> Eliminar(Guid id)
        {
            await VerificarMarcaExiste(id);

            string procedimiento = "EliminarMarca";

            var resultado = await _sqlConnection.ExecuteScalarAsync<Guid>(
                procedimiento,
                new { Id = id },
                commandType: CommandType.StoredProcedure
            );

            return resultado;
        }

        public async Task<IEnumerable<MarcaResponse>> Obtener()
        {
            string procedimiento = "ObtenerMarcas";

            var resultado = await _sqlConnection.QueryAsync<MarcaResponse>(
                procedimiento,
                commandType: CommandType.StoredProcedure
            );

            return resultado;
        }

        public async Task<MarcaResponse> Obtener(Guid id)
        {
            string procedimiento = "ObtenerMarca";

            var resultado = await _sqlConnection.QueryAsync<MarcaResponse>(
                procedimiento,
                new { Id = id },
                commandType: CommandType.StoredProcedure
            );

            return resultado.FirstOrDefault();
        }

        #endregion

        #region Helpers

        private async Task VerificarMarcaExiste(Guid id)
        {
            var marca = await Obtener(id);
            if (marca == null)
                throw new Exception("No se encontró la marca.");
        }

        #endregion
    }
}
