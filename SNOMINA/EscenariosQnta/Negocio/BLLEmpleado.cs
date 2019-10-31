using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

  }
}