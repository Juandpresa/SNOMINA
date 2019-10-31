using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace EscenariosQnta.Data
{
  public class DALInfoBancaria
  {
    static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnxSQL"].ConnectionString);

    public static void InsInfoBancaria(int idBan, int idEmp, string cuenta, string clabe, string sucursal)
    {
      try
      {
        conn.Open();
        string Query = "SP_InsertaIBancaria";
        SqlCommand cmd = new SqlCommand(Query, conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@BancoID", idBan);
        cmd.Parameters.AddWithValue("@Empleado", idEmp);
        cmd.Parameters.AddWithValue("@Cuenta", cuenta);
        cmd.Parameters.AddWithValue("@Clabe", clabe);
        cmd.Parameters.AddWithValue("@Sucursal", sucursal);

        cmd.ExecuteNonQuery();
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