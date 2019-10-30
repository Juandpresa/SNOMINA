using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace EscenariosQnta.Data
{
  public class DALGradoAcademico
  {
    static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnxSQL"].ConnectionString);

    public static void InsGradoAcademico(int idEmpleado, int idNEstudios, int idInstitucion, int idCarrera)
    {
      try
      {
        conn.Open();
        string Query = "SP_InsertaIEscolar";
        SqlCommand cmd = new SqlCommand(Query, conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
        cmd.Parameters.AddWithValue("@NivelEstudioID", idNEstudios);
        cmd.Parameters.AddWithValue("@InstitutoID", idInstitucion);
        cmd.Parameters.AddWithValue("@CarreraID", idCarrera);
        
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