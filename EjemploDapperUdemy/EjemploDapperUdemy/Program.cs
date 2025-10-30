using Dapper;
using EjemploDapperUdemy;
using Microsoft.Data.SqlClient;
using System.Data;

using (SqlConnection sql = new SqlConnection("Data Source=SDESSQLSRV01.SYC.LOC;Initial Catalog=DB_GPD_CONTRATACION;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
{
    /*
    //Insertar
    Prueba prueba = new Prueba();
    prueba.Nombre = "Antonio";
    var insertar = "INSERT INTO dbo.Prueba_cfescobar(Nombre) values (@Nombre) ";
    var res = sql.Execute(insertar, prueba);

    //Actualizar
    prueba.Nombre = "Carlos";
    var actualizar = "UPDATE dbo.Prueba_cfescobar SET Nombre=@Nombre where Nombre = 'Antonio' ";
    res = sql.Execute(actualizar, prueba);

    //Delete
    var borrar = "DELETE FROM dbo.Prueba_cfescobar WHERE nombre=@nombre";
    res = sql.Execute(borrar, prueba);

    //Listado
    var consulta = "SELECT * FROM Prueba_cfescobar";
    var lista = sql.Query<Prueba>(consulta);

    foreach (var p in lista)
    {
        Console.WriteLine(p.Id + "   " + p.Nombre);
    }
    Console.WriteLine("Pulsa para continuar");
    Console.ReadLine();
    */

    //P.A Alta Nueva
    Prueba p = new Prueba();
    p.Nombre = "XXXXXXXXXX";
    var param = new DynamicParameters();
    param.Add("@Nombre", p.Nombre);
    sql.ExecuteScalar("EjemploInsert", param, commandType:CommandType.StoredProcedure);

    //P.A SIN PARAMETROS
    var r = sql.ExecuteReader("Ejemplo", commandType:CommandType.StoredProcedure);
    while (r.Read())
        Console.WriteLine(r["Nombre"]);

    r.Close();

    Console.WriteLine("-------------");
    var parametro = new DynamicParameters();
    parametro.Add("@id", 1);
    r = sql.ExecuteReader("Ejemplo", parametro, commandType: CommandType.StoredProcedure);
    while (r.Read())
        Console.WriteLine(r["Nombre"]);

    r.Close();
}