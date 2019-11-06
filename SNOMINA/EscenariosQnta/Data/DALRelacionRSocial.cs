using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace EscenariosQnta.Data
{
  public class DALRelacionRSocial
  {
    static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnxSQL"].ConnectionString);

    public static void InsRelacionRSocial(int idContra, int idEmp, int idPag, int idCte, int idFac)
    {
      try
      {
        conn.Open();
        string Query = "SP_InsertaRelacionRSocial";
        SqlCommand cmd = new SqlCommand(Query, conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ContratistaID", idContra);
        cmd.Parameters.AddWithValue("@EmpleadoraID", idEmp);
        cmd.Parameters.AddWithValue("@PagadoraID", idPag);
        cmd.Parameters.AddWithValue("@ClienteID", idCte);
        cmd.Parameters.AddWithValue("@FacturistaID", idFac);

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