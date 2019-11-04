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
      try
      {
        List<EmpleadoVO> LstCves = DALEmpleado.ObtenerClavesExistentes(cv);
        bool Existe = false;

        foreach (EmpleadoVO item in LstCves)
        {
          if (item.Clave == cv)
          {
            Existe = true;
          }
        }

        if (Existe)
        {
          return "Esta clave fue utilizada con anteriodidad";
        }

      }
      catch (Exception)
      {
        throw;
      }
    }

  }
}