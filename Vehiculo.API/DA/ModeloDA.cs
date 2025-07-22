using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DA
{
    public class ModeloDA : IModeloDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public ModeloDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        #region Operaciones

        public async Task<Guid> Agregar(ModeloRequest modelo)
        {
            string query = "sp_CrearModelo";
            Guid nuevoId = Guid.NewGuid();

            var parametros = new DynamicParameters();
            parametros.Add("Id", nuevoId, direction: System.Data.ParameterDirection.Output);
            parametros.Add("IdMarca", modelo.IdMarca);
            parametros.Add("Nombre", modelo.Nombre);

            await _sqlConnection.ExecuteAsync(query, parametros, commandType: System.Data.CommandType.StoredProcedure);

            return parametros.Get<Guid>("Id");
        }

        public async Task<Guid> Editar(Guid id, ModeloRequest modelo)
        {
            await VerificarModeloExiste(id);

            string query = "sp_ActualizarModelo";

            await _sqlConnection.ExecuteAsync(query, new
            {
                Id = id,
                IdMarca = modelo.IdMarca,
                Nombre = modelo.Nombre
            }, commandType: System.Data.CommandType.StoredProcedure);

            return id;
        }

        public async Task<Guid> Eliminar(Guid id)
        {
            await VerificarModeloExiste(id);

            string query = "sp_EliminarModelo";

            await _sqlConnection.ExecuteAsync(query, new { Id = id }, commandType: System.Data.CommandType.StoredProcedure);

            return id;
        }

        public async Task<IEnumerable<ModeloResponse>> Obtener()
        {
            string query = "sp_ObtenerModelos";
            var resultados = await _sqlConnection.QueryAsync<ModeloResponse>(query, commandType: System.Data.CommandType.StoredProcedure);
            return resultados;
        }

        public async Task<ModeloResponse> Obtener(Guid id)
        {
            string query = "sp_ObtenerModeloPorId";
            var resultado = await _sqlConnection.QueryFirstOrDefaultAsync<ModeloResponse>(
                query,
                new { Id = id },
                commandType: System.Data.CommandType.StoredProcedure);

            return resultado;
        }

        #endregion

        #region Helpers

        private async Task VerificarModeloExiste(Guid id)
        {
            var modelo = await Obtener(id);
            if (modelo == null)
                throw new Exception("No se encontró el modelo especificado.");
        }

        #endregion
    }
}
