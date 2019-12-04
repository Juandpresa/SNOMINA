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

    public static string[] ObtenerSNETO_SD_SDI(decimal sueldoB, DateTime antiguedad, int periodoPago, int factor)
    {
      try
      {
        string [] res = new string [3];
        conn.Open();
        string Query = "SP_Piramidacion";
        SqlCommand cmd = new SqlCommand(Query, conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@SueldoNomina", sueldoB);
        cmd.Parameters.AddWithValue("@Antigueda", antiguedad);
        cmd.Parameters.AddWithValue("@PeriodoPagoID", periodoPago);
        cmd.Parameters.AddWithValue("@IdFactor", factor);
        // El resultado lo guardaremos en una tabla
        DataTable tabla = new DataTable();
        // Usamos un DataAdapter para leer los datos
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        // Llenamos la tabla con los datos leídos
        da.Fill(tabla);
        //DataColumn Clave = new DataColumn();
        for (int i = 0; i < tabla.Rows.Count; i++)
        {
          for (int j = 0; j < 3; j++)
          {
            res[j] = tabla.Rows[i][j].ToString();
          }
        }
        return res;
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

    public static string[] ObtenerSBRUTO_SD_SDI(decimal sueldoN, DateTime antiguedad, int periodoPago, int factor)
    {
      try
      {
        string[] res = new string[3];
        conn.Open();
        string Query = "SP_PiramidacionNetoABruto";
        SqlCommand cmd = new SqlCommand(Query, conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@SueldoNomina", sueldoN);
        cmd.Parameters.AddWithValue("@Antigueda", antiguedad);
        cmd.Parameters.AddWithValue("@PeriodoPagoID", periodoPago);
        cmd.Parameters.AddWithValue("@IdFactor", factor);
        // El resultado lo guardaremos en una tabla
        DataTable tabla = new DataTable();
        // Usamos un DataAdapter para leer los datos
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        // Llenamos la tabla con los datos leídos
        da.Fill(tabla);
        //DataColumn Clave = new DataColumn();
        for (int i = 0; i < tabla.Rows.Count; i++)
        {
          for (int j = 0; j < 3; j++)
          {
            res[j] = tabla.Rows[i][j].ToString();
          }
        }
        return res;
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