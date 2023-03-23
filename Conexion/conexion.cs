using System;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Extensions.Configuration;

public class conexion
{
    
    private readonly string DBtest;
    private readonly string DBProd;


    public conexion()
    {
         DBtest = "Data Source=; Initial Catalog=; User=; Password=";
         DBProd = "Data Source=; Initial Catalog=; User=; Password=";
    }

    public SqlConnection CrearConexion()
    {
        var connection = new SqlConnection(DBtest);
        try
        {
            connection.Open();

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        return connection;
    }

    public void CerrarConexion(SqlConnection connection)
    {
        connection.Close();
    }
}
