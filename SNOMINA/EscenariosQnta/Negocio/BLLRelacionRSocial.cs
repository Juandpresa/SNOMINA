using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EscenariosQnta.Data;

namespace EscenariosQnta.Negocio
{
  public class BLLRelacionRSocial
  {
    public static string InsRelacionRSocial(int idContra, int idEmp, int idPag, int idCte, int idFac)
    {
      try
      {
        DALRelacionRSocial.InsRelacionRSocial(idContra, idEmp, idPag, idCte, idFac);
        return "1";
      }
      catch (Exception)
      {

        throw;
      }


    }
  }
}