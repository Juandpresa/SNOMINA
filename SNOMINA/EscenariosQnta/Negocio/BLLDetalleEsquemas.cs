﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EscenariosQnta.Data;
using System.Data;

namespace EscenariosQnta.Negocio
{
  public class BLLDetalleEsquemas
  {
    public static string InsDetalleEsquemas(int idEmp, int idEsq, decimal porc, decimal sueldoB, decimal sueldoN, decimal sueldoD, decimal sdi)
    {
      try
      {
        DALDetalleEsquemas.InsDetalleEsquemas(idEmp,idEsq,porc,sueldoB,sueldoN,sueldoD,sdi);
        return "1";
      }
      catch (Exception)
      {
        throw;
      }
    }
  }
}