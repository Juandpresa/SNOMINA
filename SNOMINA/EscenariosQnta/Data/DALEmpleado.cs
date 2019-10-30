using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace EscenariosQnta.Data
{
  public class DALEmpleado
  {
    static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnxSQL"].ConnectionString);

    public static string ObtenerUltimoEmpleado()
    {
      try
      {
        conn.Open();
        string Query = "SP_ObtenerUltimoID";
        SqlCommand cmd = new SqlCommand(Query, conn);
        cmd.CommandType = CommandType.StoredProcedure;
        return cmd.ExecuteScalar().ToString();
        
      }
      catch (Exception)
      {

        throw;
      }
      finally
      {
        conn.Close();
      }
    }
  }
}