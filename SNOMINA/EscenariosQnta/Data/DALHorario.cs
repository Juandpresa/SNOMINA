using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace EscenariosQnta.Data
{
  public class DALHorario
  {
    static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnxSQL"].ConnectionString);

    public static DataTable LlenaGridHorario(int idHorario)
    {
      DataSet ds = new DataSet();
      DataTable tabla = new DataTable();
      try
      {
        conn.Open();
        string Query = "SP_LlenarHorarios";
        SqlCommand cmd = new SqlCommand(Query, conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@IdHorario", idHorario);
        // Usamos un DataAdapter para leer los datos
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        // Llenamos la tabla con los datos leídos
        da.Fill(ds, "TB");

        if (ds.Tables.Count > 0)
          tabla = ds.Tables[0];
      }
      catch (Exception)
      {
        throw;
      }
      finally
      {
        conn.Close();
      }
      return tabla;
    }
  }
}