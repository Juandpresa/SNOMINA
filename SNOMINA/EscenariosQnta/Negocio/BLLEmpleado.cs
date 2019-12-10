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
    public static string InsEmpleado(int idEscenario, int idCte, int idPrimaRgo, string nombre, string paterno, string materno, string puesto, string descPuesto, DateTime fechIngreso, DateTime fechNacimiento, float sueldoB, float sueldoN, int idPrestaciones, int idClasifEmp, string nacionalidad, int idTipoEsquema, string cve, int pagadora, int sexo, int tipoPago, string curp, string rfc, string correo, string telefonoL, string telefonoM, int periodoPago, DateTime antiguedad, int idEmpleadora, string foto, string calle, string numero, string colonia, int estado, string ciudad, string cp, string departamento, int turno, int horario, int jornada, int diasContrato, int ecivil, int tipoContrato)
    {
      try
      {
        DALEmpleado.InsEmpleado(idEscenario, idCte, idPrimaRgo, nombre, paterno, materno, puesto, descPuesto, fechIngreso, fechNacimiento, sueldoB, sueldoN, idPrestaciones, idClasifEmp, nacionalidad, idTipoEsquema, cve, pagadora, sexo, tipoPago, curp, rfc, correo, telefonoL, telefonoM, periodoPago, antiguedad, idEmpleadora, foto, calle, numero, colonia, estado, ciudad, cp, departamento, turno, horario, jornada, diasContrato, ecivil, tipoContrato);
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

    public static DataTable ObtenPagadoras(int idEmpleadora)
    {
      DataTable tabla = new DataTable();
      try
      {
        tabla = DALEmpleado.ObtenPagadoras(idEmpleadora);
      }
      catch (Exception)
      {
        throw;
      }
      return tabla;
    }

    public static DataTable ObtenEmpleadoById(int idEmp)
    {
      DataTable tabla = new DataTable();
      try
      {
        tabla = DALEmpleado.ObtenEmpleadoById(idEmp);
      }
      catch (Exception)
      {
        throw;
      }
      return tabla;
    }
  }
}