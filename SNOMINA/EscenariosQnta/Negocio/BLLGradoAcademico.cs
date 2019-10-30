using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EscenariosQnta.Data;
namespace EscenariosQnta.Negocio
{
  public class BLLGradoAcademico
  {
    public static string InsGradoAcademico(int idEmpleado, int idNEstudios, int idInstitucion, int idCarrera)
    {
      try
      {
        DALGradoAcademico.InsGradoAcademico(idEmpleado, idNEstudios, idInstitucion, idCarrera);
        return "Grado Academico Agregado";
      }
      catch (Exception)
      {

        throw;
      }
      

    }
  }
}