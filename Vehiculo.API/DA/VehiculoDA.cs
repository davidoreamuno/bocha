﻿using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Data.SqlClient;

namespace DA
{
    public class VehiculoDA : IVehiculoDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public VehiculoDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
            
        }

       
        #region Operaciones
        public async Task<Guid> Agregar(VehiculoRequest vehiculo)
        {
            string query = @"AgregarVehiculo";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new 
            {
                Id = Guid.NewGuid()
                ,
                IdModelo = vehiculo.IdModelo
                ,
                Placa = vehiculo.Placa
                ,
                Color = vehiculo.Color
                ,
                Anio = vehiculo.Anio
                ,
                Precio = vehiculo.Precio
                ,
                CorreoPropietario = vehiculo.CorreoPropietario
                ,
                TelefonoPropietario = vehiculo.TelefonoPropietario

            });
            return resultadoConsulta;
        }

        public async Task<Guid> Editar(Guid Id, VehiculoRequest vehiculo)
        {
            string query = @"EditarVehiculo";
            await verificarVehiculoExiste(Id);
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Id
                ,
                IdModelo = vehiculo.IdModelo
                ,
                Placa = vehiculo.Placa
                ,
                Color = vehiculo.Color
                ,
                Anio = vehiculo.Anio
                ,
                Precio = vehiculo.Precio
                ,
                CorreoPropietario = vehiculo.CorreoPropietario
                ,
                TelefonoPropietario = vehiculo.TelefonoPropietario

            });
            return resultadoConsulta;
        }

      

        public async Task<Guid> Eliminar(Guid Id)
        {
            await verificarVehiculoExiste(Id);
            string query = @"EminicarVehiculo";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Id

            });
            return resultadoConsulta;
        }

        public async Task<IEnumerable<VehiculoResponse>> Obtener()
        {
            string query = @"ObtenerVehiculos";
            var resultadosConsulta = await _sqlConnection.QueryAsync<VehiculoResponse>(query);
            return resultadosConsulta;
        }

        public async Task<VehiculoDetalle> Obtener(Guid Id)
        {
            string query = @"ObtenerVehiculo";
            var resultadosConsulta = await _sqlConnection.QueryAsync<VehiculoDetalle>(query, new
            {Id=Id});
            return resultadosConsulta.FirstOrDefault();
        }
        #endregion

        #region Helpers
        private async Task verificarVehiculoExiste(Guid Id)
        {
            VehiculoDetalle? resultadoConsultaVehiculo = await Obtener(Id);
            if (resultadoConsultaVehiculo == null)
                throw new Exception("No se Encontro el vehiculo");
        }
        #endregion
    }
}
