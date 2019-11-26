using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace EscenariosQnta.Data
{
  public class DALDetalleEsquemas
  {
    static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnxSQL"].ConnectionString);

    public static void InsDetalleEsquemas(int idEmp, int idEsq, decimal porc, decimal sueldoB, decimal sueldoN, decimal sueldoD, decimal sdi)
    {
      try
      {
        conn.Open();
        string Query = "SP_InsertaDetalleEsquemas";
        SqlCommand cmd = new SqlCommand(Query, conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Id_Empleado", idEmp);
        cmd.Parameters.AddWithValue("@IdCatEsquemas", idEsq);
        cmd.Parameters.AddWithValue("@Porcentaje", porc);
        cmd.Parameters.AddWithValue("@SueldoB", sueldoB);
        cmd.Parameters.AddWithValue("@SueldoN", sueldoN);
        cmd.Parameters.AddWithValue("@SueldoD", sueldoD);
        cmd.Parameters.AddWithValue("@SDI", sdi);

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