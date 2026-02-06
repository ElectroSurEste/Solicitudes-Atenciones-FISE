using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using M0109100164666.Models;


namespace M0109100164666.Services
{
    public static class CentroAtencionService
    {
        public static Coordenadas ObtenerCoordenadas(
                     clsAccesoDatos objAccesoDatosComercial,
                     byte codigoSucursal,
                     int codigoCentroAtencion
                   )
        {
            var datosRetorno = (clsDatosRetorno)null;

            try
            {
                datosRetorno = objAccesoDatosComercial.fnObtenerDatosProcedimiento(
                    "[dbo].[gnCentroAtencionUbicacionPorCodigo]", new SqlParameter[] {
              new SqlParameter("@CodigoSucursal", SqlDbType.TinyInt) { Value = codigoSucursal },
              new SqlParameter("@CodigoCentroAtencion", SqlDbType.TinyInt) { Value = codigoCentroAtencion },
                    }
                  );

                if (datosRetorno.IdRetorno != 0)
                    throw new Exception(datosRetorno.Mensaje);

                if (datosRetorno.Datos.Tables.Count == 0)
                    throw new Exception("El origen de datos no devolvió el resultado esperado");

                var obj = (Coordenadas)null;
                foreach (DataRow dr in datosRetorno.Datos.Tables[0].Rows)
                    obj = new Coordenadas(dr);

                return obj;
            }
            finally
            {
                datosRetorno?.Dispose();
            }

        }
    }
}
