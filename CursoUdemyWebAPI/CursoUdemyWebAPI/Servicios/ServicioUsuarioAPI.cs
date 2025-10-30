using CursoUdemyWebAPI.Modelo;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CursoUdemyWebAPI.Servicios
{
    public class ServicioUsuarioAPI(ConexionBaseDatos conex, ILogger<ServicioUsuarioAPI> l) : IServicioUsuarioAPI
    {
        private readonly string CadenaConexion = conex.CadenaConexionSQL;

        private readonly ILogger<ServicioUsuarioAPI> log = l;

        private SqlConnection Conexion()
        {
            return new SqlConnection(CadenaConexion);
        }
        public async Task<UsuarioAPI> DameUsuario(LoginAPI login)
        {
            SqlConnection sqlConexion = Conexion();
            UsuarioAPI u = null;

            try
            {
                sqlConexion.Open();
                var param = new DynamicParameters();
                param.Add("@UsuarioAPI", login.usuarioAPI, DbType.String, ParameterDirection.Input, 500);
                param.Add("@PassApi", login.passAPI, DbType.String, ParameterDirection.Input, 500);

                u = await sqlConexion.QueryFirstOrDefaultAsync<UsuarioAPI>("UsuarioAPIObtener", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                log.LogError("ERROR: " + ex.ToString());
                throw new Exception("Se produjo un error obtener datos del usuario de login" + ex.Message);
            }
            finally
            {
                sqlConexion.Close();
                sqlConexion.Dispose();
            }

            return u;
        }
    }
}
