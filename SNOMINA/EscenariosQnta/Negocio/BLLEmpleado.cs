using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EscenariosQnta.VO;

using EscenariosQnta.Data;

namespace EscenariosQnta.Negocio
{
  public class BLLEmpleado
  {
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

  }
}