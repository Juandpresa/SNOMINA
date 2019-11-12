using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EscenariosQnta.VO;

using EscenariosQnta.Data;
using System.Data;

namespace EscenariosQnta.Negocio
{
  public class BLLEmpleado
  {
    public static string InsEmpleado(int idEscenario, int idCte, int idPrimaRgo, string nombre, string paterno, string materno, string puesto, string descPuesto, string ubicacion, DateTime fechIngreso, DateTime fechNacimiento, float porcNomina, float porcAsimilados, float porcHonorarios, float porcTN, float porcEZWallet, float sueldo, float sueldoB, float sueldoN, float sueldoH, float sueldoT, float sueldoEZ, decimal bono, decimal comision, decimal otrosIngresos, decimal impFonacot, int idInfonavit, decimal impInfonavit, int idPrestaciones, int idPension, decimal impPension, int idEsquemaActual, int idClasifEmp, int idTipoEsquema, string cve, int pagadora, int sexo, int tipoPago, string curp, string rfc, string correo, string telefonoL, string telefonoM, DateTime fechUltimoPago, int periodoPago, int antiguedad, int idEmpleadora)
    {
      try
      {
        DALEmpleado.InsEmpleado(idEscenario, idCte, idPrimaRgo, nombre, paterno, materno, puesto, descPuesto, ubicacion, fechIngreso, fechNacimiento, porcNomina, porcAsimilados, porcHonorarios, porcTN, porcEZWallet, sueldo, sueldoB, sueldoN, sueldoH, sueldoT, sueldoEZ, bono, comision, otrosIngresos, impFonacot, idInfonavit, impInfonavit, idPrestaciones, idPension, impPension, idEsquemaActual, idClasifEmp, idTipoEsquema, cve, pagadora, sexo, tipoPago, curp, rfc, correo, telefonoL, telefonoM, fechUltimoPago, periodoPago, antiguedad, idEmpleadora);
        return "1";
      }
      catch (Exception)
      {
        throw;
      }
    }

    public static string ObtenerUltimoEmpleado()
    {
      try
      {
        return DALEmpleado.ObtenerUltimoEmpleado();
      }
      catch (Exception)
      {
        throw;
      }
    }

    public static string ObtenerIdentificador(int idIdentificador)
    {
      try
      {
        return DALEmpleado.ObtenerIdentificador(idIdentificador);
      }
      catch (Exception)
      {
        throw;
      }
    }

    public static string ObtenerClavesExistentes(string cv)
    {
      string claves = "";
      try
      {
        string  Cves = DALEmpleado.ObtenerClavesExistentes(cv);
        bool Existe = false;

        if (Cves == cv)
        {
          Existe = true;
        }

        if (Existe)
        {
          claves= "1";
        }
        return claves;

      }
      catch (Exception)
      {
        throw;
      }
    }

    public static DataTable ObtenClientes(int idEmpleadora)
    {
      DataTable tabla = new DataTable();
      try
      {
        tabla = DALEmpleado.ObtenClientes(idEmpleadora);
      }
      catch (Exception)
      {
        throw;
      }
      return tabla;
    }
  }
}