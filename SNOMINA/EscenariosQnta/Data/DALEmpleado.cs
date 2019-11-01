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

    public static string ObtenerIdentificador(int idRSocial)
    {
      try
      {
        conn.Open();
        string Query = "SP_ObtenIdentificadorRSocial";
        SqlCommand cmd = new SqlCommand(Query, conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@idRazonSocial", idRSocial);
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

    //public static List<T> ObtenerClavesExistentes()
    //{
    //  try
    //  {
    //    conn.Open();
    //    string Query = "SP_ObtenerClavesExistentes";
    //    SqlCommand cmd = new SqlCommand(Query, conn);
    //    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    DataSet cves = new DataSet();
    //    adapter.Fill(cves);

    //    if (cves.Tables[0].Rows.Count > 0)
    //    {
    //      //Encontro un registro
    //      DataRow dr = cves.Tables[0].Rows[0];
    //      CamionVO Camion = new CamionVO(dr);
    //      return Camion;
    //    }
    //    else
    //    {
    //      //La tabla esta vacia
    //      CamionVO Camion = new CamionVO();
    //      return Camion;
    //    }

    //  }
    //  catch (Exception)
    //  {

    //    throw;
    //  }
    //  finally
    //  {
    //    conn.Close();
    //  }
    //}
  }
}