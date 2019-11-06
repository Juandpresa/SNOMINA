using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EscenariosQnta.Data;

namespace EscenariosQnta.Negocio
{
  public class BLLInfoBancaria
  {
    public static string InsInfoBancaria(int idBan, int idEmp, string cuenta, string clabe, string tarjeta)
    {
      try
      {
        DALInfoBancaria.InsInfoBancaria(idBan, idEmp, cuenta, clabe, tarjeta);
        return "Informacion Bancaria Agregada";
      }
      catch (Exception)
      {

        throw;
      }


    }
  }
}