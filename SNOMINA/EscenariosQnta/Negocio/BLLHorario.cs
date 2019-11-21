using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EscenariosQnta.Data;
using System.Data;


namespace EscenariosQnta.Negocio
{
  public class BLLHorario
  {
    public static DataTable LlenaGridHorario(int idHorario)
    {
      DataTable tabla = new DataTable();
      try
      {
        tabla = DALHorario.LlenaGridHorario(idHorario);
      }
      catch (Exception)
      {
        throw;
      }
      return tabla;
    }
  }
}