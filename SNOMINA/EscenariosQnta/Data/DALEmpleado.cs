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

    public static void InsEmpleado(int idEscenario, int idCte, int idPrimaRgo, string nombre, string paterno, string materno, string puesto, string descPuesto, string ubicacion, DateTime fechIngreso, DateTime fechNacimiento, float porcNomina, float porcAsimilados, float porcHonorarios, float porcTN, float porcEZWallet, float sueldo, float sueldoB, float sueldoN, float sueldoH, float sueldoT, float sueldoEZ, decimal bono, decimal comision, decimal otrosIngresos, decimal impFonacot, int idInfonavit, decimal impInfonavit, int idPrestaciones, int idPension, decimal impPension, int idEsquemaActual, int idClasifEmp, int idTipoEsquema, string cve, int pagadora, int sexo, int tipoPago, string curp, string rfc, string correo, string telefonoL, string telefonoM, DateTime fechUltimoPago, int periodoPago, int antiguedad, int idEmpleadora)
    {
      try
      {
        conn.Open();
        string Query = "SP_InsertaEmpleado";
        SqlCommand cmd = new SqlCommand(Query, conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Id_Escenario ", idEscenario);
        cmd.Parameters.AddWithValue("@Id_Cliente", idCte);
        cmd.Parameters.AddWithValue("@Id_PrimaRgo", idPrimaRgo);
        cmd.Parameters.AddWithValue("@Nombre", nombre);
        cmd.Parameters.AddWithValue("@Paterno", paterno);
        cmd.Parameters.AddWithValue("@Materno", materno);
        cmd.Parameters.AddWithValue("@Puesto", puesto);
        cmd.Parameters.AddWithValue("@DescriPto", descPuesto);
        cmd.Parameters.AddWithValue("@UbicaLabora", ubicacion);
        cmd.Parameters.AddWithValue("@FechaIngreso", fechIngreso);
        cmd.Parameters.AddWithValue("@FechaNac", fechNacimiento);
        cmd.Parameters.AddWithValue("@PorcNomina", porcNomina);
        cmd.Parameters.AddWithValue("@PorcAsimilados", porcAsimilados);
        cmd.Parameters.AddWithValue("@PorcHonorarios", porcHonorarios);
        cmd.Parameters.AddWithValue("@PorcTN", porcTN);
        cmd.Parameters.AddWithValue("@PorcEZWallet", porcEZWallet);
        cmd.Parameters.AddWithValue("@Sueldo", sueldo);
        cmd.Parameters.AddWithValue("@SueldoBruto", sueldoB);
        cmd.Parameters.AddWithValue("@SueldoNeto", sueldoN);
        cmd.Parameters.AddWithValue("@SueldoHonorarios", sueldoH);
        cmd.Parameters.AddWithValue("@SueldoTN", sueldoT);
        cmd.Parameters.AddWithValue("@SueldoEZWallet", sueldoEZ);
        cmd.Parameters.AddWithValue("@Bono", bono);
        cmd.Parameters.AddWithValue("@ComisionEmpleado", comision);
        cmd.Parameters.AddWithValue("@OtrosIngresos", otrosIngresos);
        cmd.Parameters.AddWithValue("@ImpFonacot", impFonacot);
        cmd.Parameters.AddWithValue("@Id_Infonavit", idInfonavit);
        cmd.Parameters.AddWithValue("@ImpInfonavit", impInfonavit);
        cmd.Parameters.AddWithValue("@Id_Prestac", idPrestaciones);
        cmd.Parameters.AddWithValue("@Id_Pension", idPension);
        cmd.Parameters.AddWithValue("@ImportePension", impPension);
        cmd.Parameters.AddWithValue("@Id_EsquemaActual", idEsquemaActual);
        cmd.Parameters.AddWithValue("@Id_ClasifEmp", idClasifEmp);
        cmd.Parameters.AddWithValue("@Id_TipoEsquema", idTipoEsquema);
        cmd.Parameters.AddWithValue("@Clave", cve);
        cmd.Parameters.AddWithValue("@RSocialPagadoraID", pagadora);
        cmd.Parameters.AddWithValue("@SexoID", sexo);
        cmd.Parameters.AddWithValue("@TipoPagoID", tipoPago);
        cmd.Parameters.AddWithValue("@Curp", curp);
        cmd.Parameters.AddWithValue("@Rfc", rfc);
        cmd.Parameters.AddWithValue("@CorreoElectronico", correo);
        cmd.Parameters.AddWithValue("@TelefonoLocal", telefonoL);
        cmd.Parameters.AddWithValue("@TelefonoMovil", telefonoM);
        cmd.Parameters.AddWithValue("@FechaUltimoPago", fechUltimoPago);
        cmd.Parameters.AddWithValue("@PeriodoPagoID", periodoPago);
        cmd.Parameters.AddWithValue("@Antigueda", antiguedad);
        cmd.Parameters.AddWithValue("@Id_Empleadora", idEmpleadora);

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
        // El resultado lo guardaremos en una tabla
        DataTable tabla = new DataTable();
        // Usamos un DataAdapter para leer los datos
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        // Llenamos la tabla con los datos leídos
        da.Fill(tabla);
        //DataColumn Clave = new DataColumn();
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

    public static DataTable ObtenClientes(int idEmpleadora)
    {
      DataSet ds = new DataSet();
      DataTable tabla = new DataTable();
      try
      {
        conn.Open();
        string Query = "SP_ObtenClientesRelacionEmpleadora";
        SqlCommand cmd = new SqlCommand(Query, conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@EmpleadoraID", idEmpleadora);  
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

    public static DataTable ObtenPagadoras(int idEmpleadora)
    {
      DataSet ds = new DataSet();
      DataTable tabla = new DataTable();
      try
      {
        conn.Open();
        string Query = "SP_ObtenPagadorasRelacionEmpleadora";
        SqlCommand cmd = new SqlCommand(Query, conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@EmpleadoraID", idEmpleadora);
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