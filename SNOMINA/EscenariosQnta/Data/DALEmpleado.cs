using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using EscenariosQnta.VO;


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

    public static List<EmpleadoVO> ObtenerClavesExistentes(string cv)
    {
      try
      {
        conn.Open();
        string Query = "SP_ObtenerClavesExistentes";
        SqlCommand cmd = new SqlCommand(Query, conn);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        cmd.CommandType = CommandType.StoredProcedure;
        DataSet cves = new DataSet();
        adapter.Fill(cves);

        List<EmpleadoVO> LstClaves = new List<EmpleadoVO>();

        foreach (DataRow dr in cves.Tables[0].Rows)
        {
          LstClaves.Add(new EmpleadoVO(dr));
        }

        return LstClaves;

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