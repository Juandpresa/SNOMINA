using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using EscenariosQnta.Data;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;

namespace EscenariosQnta.Vista
{
    public partial class wfrmVistaReportes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ReportDocument rprt = new ReportDocument();
            DataTable dtResumen = new DataTable();
            DataTable dtSimplificado = new DataTable();
            string Reporte = string.Empty;
            string propuesta = string.Empty;

            Reporte = Session["Reporte"].ToString();
            propuesta = Session["Propuesta"].ToString();

            if(Reporte == "Resumen")
            {
                dtResumen = (DataTable)Session["Resumen"];           
                rprt.Load(Server.MapPath("/Reportes/Reporte_Resumen.rpt"));            
                rprt.SetDataSource(dtResumen);                
            }
            else if (Reporte == "Simplificado")
            {
               dtSimplificado = (DataTable)Session["Simplificado"];           
               rprt.Load(Server.MapPath("/Reportes/Reporte_Simplificado.rpt"));            
               rprt.SetDataSource(dtSimplificado);               
            }

            rprt.SetParameterValue("Propuesta", propuesta);
            ViewReports.ReportSource = rprt;
        }
    }
}