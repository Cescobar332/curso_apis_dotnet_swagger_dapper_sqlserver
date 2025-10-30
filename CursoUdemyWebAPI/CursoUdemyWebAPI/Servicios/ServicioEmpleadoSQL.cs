
using CursoUdemyWebAPI.Modelo;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CursoUdemyWebAPI.Servicios
{
    public class ServicioEmpleadoSQL : IServicioEmpleadoSQL
    {
        private readonly string CadenaConexion;

        private readonly ILogger<ServicioEmpleadoSQL> log;

        public ServicioEmpleadoSQL(ConexionBaseDatos conex, ILogger<ServicioEmpleadoSQL> l)
        {
            CadenaConexion = conex.CadenaConexionSQL;
            this.log = l;
        }

        private SqlConnection conexion()
        {
            return new SqlConnection(CadenaConexion);
        }
        public async Task BajaEmpleado(string codEmpleado)
        {
            SqlConnection sqlConexion = conexion();
            try
            {
                sqlConexion.Open();
                var param = new DynamicParameters();
                param.Add("@CodEmpleado", codEmpleado, DbType.String, ParameterDirection.Input, 4);

                await sqlConexion.ExecuteScalarAsync("EmpleadoBorrar", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                log.LogError("ERROR: " + ex.ToString());
                throw new Exception("Se produjo un error al borrar un empleado" + ex.Message);
            }
            finally
            {
                sqlConexion.Close();
                sqlConexion.Dispose();
            }
        }

        public async Task<Empleado> DameEmpleado(string codEmpleado)
        {
            SqlConnection sqlConexion = conexion();
            Empleado? e = null;
            try
            {
                sqlConexion.Open();
                var param = new DynamicParameters();
                param.Add("@CodEmpleado", codEmpleado, DbType.String, ParameterDirection.Input, 4);

                e = await sqlConexion.QueryFirstOrDefaultAsync<Empleado>("EmpleadoObtener", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                log.LogError("ERROR: " + ex.ToString());
                throw new Exception("Se produjo un error al obtener un empleado" + ex.Message);
            }
            finally
            {
                sqlConexion.Close();
                sqlConexion.Dispose();
            }

            return e;
        }

        public async Task<IEnumerable<Empleado>> DameEmpleados()
        {
            SqlConnection sqlConexion = conexion();
            List<Empleado> empleados = new List<Empleado>();
            try
            {
                sqlConexion.Open();

                var r = await sqlConexion.QueryAsync<Empleado>("EmpleadoObtener", commandType: CommandType.StoredProcedure);
                empleados = [.. r];
            }
            catch (Exception ex)
            {
                log.LogError("ERROR: " + ex.ToString());
                throw new Exception("Se produjo un error al obtener los empleados" + ex.Message);
            }
            finally
            {
                sqlConexion.Close();
                sqlConexion.Dispose();
            }

            return empleados;
        }

        public async Task ModificarEmpleado(Empleado e)
        {
            SqlConnection sqlConexion = conexion();
            try
            {
                sqlConexion.Open();
                var param = new DynamicParameters();
                param.Add("@Nombre", e.Nombre, DbType.String, ParameterDirection.Input, 500);
                param.Add("@CodEmpleado", e.CodEmpleado, DbType.String, ParameterDirection.Input, 4);
                param.Add("@Email", e.Email, DbType.String, ParameterDirection.Input, 255);
                param.Add("@Edad", e.Edad, DbType.Int32, ParameterDirection.Input);

                await sqlConexion.ExecuteScalarAsync("EmpleadoModificar", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                log.LogError("ERROR: " + ex.ToString());
                throw new Exception("Se produjo un error al modificar empleado" + ex.Message);
            }
            finally
            {
                sqlConexion.Close();
                sqlConexion.Dispose();
            }
        }

        public async Task NuevoEmpleado(Empleado e)
        {
            SqlConnection sqlConexion = conexion();
            try
            {
                sqlConexion.Open();
                var param = new DynamicParameters();
                param.Add("@Nombre", e.Nombre, DbType.String, ParameterDirection.Input, 500);
                param.Add("@CodEmpleado", e.CodEmpleado, DbType.String, ParameterDirection.Input, 4);
                param.Add("@Email", e.Email, DbType.String, ParameterDirection.Input, 255);
                param.Add("@Edad", e.Edad, DbType.Int32, ParameterDirection.Input);
                param.Add("@FechaAlta", e.FechaAlta, DbType.DateTime, ParameterDirection.Input);

                await sqlConexion.ExecuteScalarAsync("EmpleadoAlta", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                log.LogError("ERROR: " + ex.ToString());
                throw new Exception("Se produjo un error al dar de alta" + ex.Message);
            }
            finally
            {
                sqlConexion.Close();
                sqlConexion.Dispose();
            }
        }
    }
}
