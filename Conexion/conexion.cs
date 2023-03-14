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
         DBtest = "Data Source=gpstestsql.database.windows.net; Initial Catalog=YoVendoSaldotest; User=gats-admin; Password=gpspro@2015*";
         DBProd = "Data Source=gpsprodsql.database.windows.net; Initial Catalog=PagaTodo; User=gats-admin; Password=globalpay*12";
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
