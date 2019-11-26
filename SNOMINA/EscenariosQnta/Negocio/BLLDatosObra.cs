using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EscenariosQnta.Data;
using System.Data;
namespace EscenariosQnta.Negocio
{
  public class BLLDatosObra
  {
    public static string ObtenerDiasObra(DateTime inicio, DateTime fin)
    {
      try
      {
        return DALDatosObra.ObtenerDiasObra(inicio,fin);
      }
      catch (Exception)
      {
        throw;
      }
    }
  }
}