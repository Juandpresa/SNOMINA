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

    public static string ObtenerClavesExistentes(string cv)
    {
      try
      {
        string cveXfila = "";
        string cve = "";
        conn.Open();
        string Query = "SP_ObtenerClavesExistentes";
        SqlCommand cmd = new SqlCommand(Query, conn);
        cmd.CommandType = CommandType.StoredProcedure;
        //SqlDataReader reader = cmd.ExecuteReader();
        DataTable tabla = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(tabla);
        DataColumn Clave = new DataColumn();
        for (int i = 0; i < tabla.Rows.Count; i++)
        {
          cveXfila = tabla.Rows[i]["Clave"].ToString();
          if (cv == cveXfila)
          {
            cve = cveXfila;
          }
          
        }
        return cve;
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