namespace CursoUdemyWebAPI
{
    public class ConexionBaseDatos(string ConexionSql)
    {
        private readonly string cadenaConexionSql = ConexionSql;

        public string CadenaConexionSQL { get => cadenaConexionSql; }
    }
}
