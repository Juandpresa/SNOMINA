using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EscenariosQnta.Data;
using EscenariosQnta.Clases;
using System.IO;
using System.Drawing;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Text;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace EscenariosQnta.Vista
{
    public partial class wfrmReportes : System.Web.UI.Page
    {
        #region Variables
        string Id_Cliente = string.Empty;
        string Cliente = string.Empty;
        string PropuestaCosto = string.Empty;
        string PropuestaCostoSuma = string.Empty;
        string NombreColumna = string.Empty;
        string strIngresos = string.Empty;
        string strCostos = string.Empty;
        string strIngresosTotales = string.Empty;
        string strCostosTotales = string.Empty;

        string FormatoReporte = string.Empty;
        string Reporte = string.Empty;

        DataTable dtResumen = new DataTable();
        DataTable dtSimplificado = new DataTable();

        DataTable dtResumenTotales = new DataTable();
        DataTable dtSimplificadoTotales = new DataTable();

        string strQuery = string.Empty;

        clsDatos clsQuery = new clsDatos();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenClientes();
            }
        }

        protected void ObtenClientes()
        {
            try
            {
                DataTable dtCliente = new DataTable();

                dtCliente = clsQuery.execQueryDataTable("SP_ObtenClientes");

                if (dtCliente.Rows.Count > 0)
                {
                    ddlCliente.DataSource = dtCliente;
                    ddlCliente.DataTextField = "Nombre_RazonSocial";
                    ddlCliente.DataValueField = "Id_Cliente";
                    ddlCliente.DataBind();
                }

                ddlCliente.Items.Insert(0, new System.Web.UI.WebControls.ListItem(">> Seleccione una Opcion <<", "-1"));

            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);

            }
        }

        protected void ObtenEscenario(int Id_Cliente)
        {
            try
            {
                DataTable dtEscenario = new DataTable();
                strQuery = string.Format("SP_ObtenEscenarioPorCliente {0}", Id_Cliente);
                dtEscenario = clsQuery.execQueryDataTable(strQuery);

                chkEscenarios.Items.Clear();

                if (dtEscenario.Rows.Count > 0)
                {
                    for (int i = 0; i < dtEscenario.Rows.Count; i++)
                    {
                        chkEscenarios.Items.Add(new System.Web.UI.WebControls.ListItem(dtEscenario.Rows[i]["Id_Escenario"].ToString(), dtEscenario.Rows[i]["Id_Escenario"].ToString()));

                    }
                    chkEscenarios.RepeatColumns = dtEscenario.Rows.Count;
                }

            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);

            }

        }

        protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Id_Cliente = int.Parse(ddlCliente.SelectedItem.Value);
            ObtenEscenario(Id_Cliente);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Cliente = ddlCliente.SelectedItem.Text.ToString();

            var ltrIngresos = new List<string>();
            var ltrCosto = new List<string>();

            var ltrIngresosTotales = new List<string>();
            var ltrCostoTotales = new List<string>();

            Id_Cliente = ddlCliente.SelectedItem.Value;

            for (int i = 0; i <= chkEscenarios.Items.Count - 1; i++)
            {
                if (chkEscenarios.Items[i].Selected == true)
                {
                    strIngresos = "[" + chkEscenarios.Items[i].Value + "]";
                    strIngresosTotales = "SUM([" + chkEscenarios.Items[i].Value + "]) '" + chkEscenarios.Items[i].Value + "'";

                    ltrIngresos.Add(strIngresos);
                    ltrIngresosTotales.Add(strIngresosTotales);

                    switch (strIngresos)
                    {
                        case "[0]":
                            PropuestaCosto = "[A]";
                            PropuestaCostoSuma = "SUM([A]) 'A'";
                            break;
                        case "[1]":
                            PropuestaCosto = "[B]";
                            PropuestaCostoSuma = "SUM([B]) 'B'";
                            break;
                        case "[2]":
                            PropuestaCosto = "[C]";
                            PropuestaCostoSuma = "SUM([C]) 'C'";
                            break;
                        case "[3]":
                            PropuestaCosto = "[D]";
                            PropuestaCostoSuma = "SUM([D]) 'D'";
                            break;
                        case "[4]":
                            PropuestaCosto = "[E]";
                            PropuestaCostoSuma = "SUM([E]) 'E'";
                            break;
                        case "[5]":
                            PropuestaCosto = "[F]";
                            PropuestaCostoSuma = "SUM([F]) 'F'";
                            break;
                        case "[6]":
                            PropuestaCosto = "[G]";
                            PropuestaCostoSuma = "SUM([G]) 'G'";
                            break;
                        case "[7]":
                            PropuestaCosto = "[H]";
                            PropuestaCostoSuma = "SUM([H]) 'H'";
                            break;
                        case "[8]":
                            PropuestaCosto = "[I]";
                            PropuestaCostoSuma = "SUM([I]) 'I'";
                            break;
                        case "[9]":
                            PropuestaCosto = "[J]";
                            PropuestaCostoSuma = "SUM([J]) 'J'";
                            break;
                        case "[10]":
                            PropuestaCosto = "[K]";
                            PropuestaCostoSuma = "SUM([K]) 'K'";
                            break;
                    }

                    ltrCosto.Add(PropuestaCosto);
                    ltrCostoTotales.Add(PropuestaCostoSuma);


                }
            }

            strIngresos = string.Join(",", ltrIngresos);
            strCostos = string.Join(",", ltrCosto);

            strIngresosTotales = string.Join(",", ltrIngresosTotales);
            strCostosTotales = string.Join(",", ltrCostoTotales);

            for (int i = 0; i <= chkReporte.Items.Count - 1; i++)
            {
                if (chkReporte.Items[i].Selected == true)
                {
                    Reporte = chkReporte.SelectedValue.ToString();
                }
            }

            for (int i = 0; i <= chkFormatoReporte.Items.Count - 1; i++)
            {
                if (chkFormatoReporte.Items[i].Selected == true)
                {
                    FormatoReporte = chkFormatoReporte.SelectedValue.ToString();
                }
            }


            /******************/
            /**** RESUMEN ****/
            /****************/
            if (!string.IsNullOrEmpty(strIngresos))
            {
                if (Reporte == "Resumen")
                {
                    strQuery = "SELECT [Nombre Empleado], " + strIngresos + ", " + strCostos + " FROM [dbo].[vwReporte_Resumen] WHERE ID_CLIENTE = " + Id_Cliente;
                    dtResumen = clsQuery.execQueryDataTable(strQuery);

                    strQuery = "SELECT MAX(LEN([Nombre Empleado])) Tamaño," + strIngresosTotales + ", " + strCostosTotales + " FROM [dbo].[vwReporte_Resumen] WHERE ID_CLIENTE = " + Id_Cliente;
                    dtResumenTotales = clsQuery.execQueryDataTable(strQuery);

                    if (FormatoReporte == "PDF")
                    {
                        ExportarPdf(dtResumen);
                    }
                    else if (FormatoReporte == "Excel")
                    {
                        ExportarArchivoExcel(dtResumen);
                    }
                    else
                    {
                        Mensaje("Error: Debe seleccionar un formato de reporte", CuadroMensaje.CuadroMensajeIcono.Error);
                    }
                }

                /***********************/
                /**** SIMPLIFICADO ****/
                /*********************/
                else if (Reporte == "Simplificado")
                {
                    strQuery = "SELECT NumeroEmpleados, " + strIngresos + ", " + strCostos + " FROM [dbo].[vwReporte_Simplificado] WHERE ID_CLIENTE = " + Id_Cliente;
                    dtSimplificado = clsQuery.execQueryDataTable(strQuery);

                    if (FormatoReporte == "PDF")
                    {
                        ExportarPdf(dtSimplificado);
                    }
                    else if (FormatoReporte == "Excel")
                    {
                        ExportarArchivoExcel(dtSimplificado);
                    }
                    else
                    {
                        Mensaje("Error: Debe seleccionar un formato de reporte", CuadroMensaje.CuadroMensajeIcono.Error);
                    }

                }
                else
                {
                    Mensaje("Error: Debe seleccionar un reporte", CuadroMensaje.CuadroMensajeIcono.Error);
                }
            }

            else
            {
                Mensaje("Error: Seleccione uno o varios escenarios", CuadroMensaje.CuadroMensajeIcono.Error);
            }
        }

        private string ReporteHtml(DataTable dt)
        {
            var strHtml = string.Empty;
            strHtml = "<html>";
            //strHtml += "<head><style>   table.center {width:50%; margin-left:0%; margin-right:15%; } </style></head>";
            strHtml += "<body>";

            /******************/
            /**** RESUMEN ****/
            /****************/

            if (Reporte == "Resumen")
            {

                strHtml += "<table>";
                strHtml += "    <tr><td bgcolor='#9f1f24 ' style='text-align: center; color: white; '><b> Ingresos y Costos Individuales Netos</b></td></tr>";
                strHtml += "</table>";
                strHtml += "<table>";
                strHtml += "    <tr>";
                strHtml += "        <td bgcolor='#9f1f24' style='text-align:center; color: white;'> <b> Ingresos Netos del personal </b></td>";
                strHtml += "        <td bgcolor='#FFFFFF' style='text-align:center;'><b> Costo </b></td>";
                strHtml += "    </tr>";
                strHtml += "</table>";
                strHtml += "<table >";
                strHtml += "    <tr><td bgcolor='#FFFFFF' style='text-align: left;'><b> Empleados </b></td>";

                foreach (DataColumn column in dt.Columns)
                {
                    NombreColumna = column.ColumnName;

                    if (NombreColumna == "0")
                    {
                        strHtml += "<td bgcolor='#FFFFFF' style='text-align: right;'><b> Actual </b></td>";
                    }
                    else if (NombreColumna != "Nombre Empleado" && NombreColumna != "A" && NombreColumna != "B" && NombreColumna != "C" && NombreColumna != "D" && NombreColumna != "E" && NombreColumna != "F" && NombreColumna != "G" && NombreColumna != "H" && NombreColumna != "I" && NombreColumna != "J" && NombreColumna != "K")
                    {
                        strHtml += "<td bgcolor='#FFFFFF' style='text-align: right;'><b> Propuesta " + NombreColumna + " </b></td>";
                    }
                }

                foreach (DataColumn column in dt.Columns)
                {
                    NombreColumna = column.ColumnName;
                    if (NombreColumna == "0")
                    {
                        strHtml += "<td bgcolor='#FFFFFF' style='text-align: right;'><b> Actual </b></td>";
                    }
                    else if (NombreColumna != "Nombre Empleado" && NombreColumna != "A" && NombreColumna != "B" && NombreColumna != "C" && NombreColumna != "D" && NombreColumna != "E" && NombreColumna != "F" && NombreColumna != "G" && NombreColumna != "H" && NombreColumna != "I" && NombreColumna != "J" && NombreColumna != "K")
                    {
                        strHtml += "<td bgcolor='#FFFFFF' style='text-align: right;'><b> Propuesta " + NombreColumna + " </b></td>";
                    }
                }

                strHtml += "</tr>";

                for (int i = 0; dt.Rows.Count > i; i++)
                {
                    strHtml += "<tr>";
                    strHtml += "<td bgcolor='#FFFFFF' style='text-align: left; font-size: 6px;' width='" + dtResumenTotales.Rows[0]["Tamaño"] + "px'><b> " + dt.Rows[i]["Nombre Empleado"] + " </b></td>";
                    foreach (DataColumn column in dt.Columns)
                    {
                        NombreColumna = column.ColumnName;
                        if (NombreColumna != "Nombre Empleado" && NombreColumna != "A" && NombreColumna != "B" && NombreColumna != "C" && NombreColumna != "D" && NombreColumna != "E" && NombreColumna != "F" && NombreColumna != "G" && NombreColumna != "H" && NombreColumna != "I" && NombreColumna != "J" && NombreColumna != "K")
                        {
                            strHtml += "<td bgcolor='#FFFFFF' style='text-align: right ; font-size: 6px;'><b> $" + dt.Rows[i][NombreColumna] + " </b></td>";
                        }

                        if (NombreColumna != "Nombre Empleado" && NombreColumna != "0" && NombreColumna != "1" && NombreColumna != "2" && NombreColumna != "3" && NombreColumna != "4" && NombreColumna != "5" && NombreColumna != "6" && NombreColumna != "7" && NombreColumna != "8" && NombreColumna != "9" && NombreColumna != "10")
                        {
                            strHtml += "<td bgcolor='#FFFFFF' style='text-align: right ; font-size: 6px;'><b> $" + dt.Rows[i][NombreColumna] + " </b></td>";
                        }                                    
                    }

                    strHtml += "</tr>";
                }

                strHtml += "</table>";
                strHtml += "<table>";
                strHtml += "    <tr><td></td>";

                for (int i = 0; dtResumenTotales.Rows.Count > i; i++)
                {
                    foreach (DataColumn column in dtResumenTotales.Columns)
                    {
                        strHtml += "  <td bgcolor='#FFFFFF' style='text-align: right; font-size: 6px;'> <b> $" + dtResumenTotales.Rows[i][column.ColumnName] + " </b></td>";
                    }
                }

                strHtml += "  </tr>";
                strHtml += "</table>";

                strHtml += "<table>";
                strHtml += "    <tr><td></td>";

                for (int i = 0; dtResumenTotales.Rows.Count > i; i++)
                {
                    foreach (DataColumn column in dtResumenTotales.Columns)
                    {
                        decimal ahorro = 0;
                        NombreColumna = column.ColumnName;
                        if (NombreColumna != "Nombre Empleado" && NombreColumna != "0" && NombreColumna != "A" && NombreColumna != "B" && NombreColumna != "C" && NombreColumna != "D" && NombreColumna != "E" && NombreColumna != "F" && NombreColumna != "G" && NombreColumna != "H" && NombreColumna != "I" && NombreColumna != "J" && NombreColumna != "K")
                        {
                            if (dtResumenTotales.Columns.Contains("0"))
                            {
                                ahorro = Convert.ToDecimal(dtResumenTotales.Rows[i]["0"].ToString()) - Convert.ToDecimal(dtResumenTotales.Rows[i][column.ColumnName].ToString());
                                strHtml += "  <td bgcolor='#FFFFFF' style='text-align: right; font-size: 6px;'> <b> $" + ahorro + " </b></td>";
                            }
                        }

                        else if (NombreColumna != "Nombre Empleado" && NombreColumna != "A" && NombreColumna != "0" && NombreColumna != "1" && NombreColumna != "2" && NombreColumna != "3" && NombreColumna != "4" && NombreColumna != "5" && NombreColumna != "6" && NombreColumna != "7" && NombreColumna != "8" && NombreColumna != "9" && NombreColumna != "10")
                        {
                            if (dtResumenTotales.Columns.Contains("A"))
                            {
                                ahorro = Convert.ToDecimal(dtResumenTotales.Rows[i]["A"].ToString()) - Convert.ToDecimal(dtResumenTotales.Rows[i][column.ColumnName].ToString());
                                strHtml += "  <td bgcolor='#FFFFFF' style='text-align: right; font-size: 6px;'> <b> $" + ahorro + " </b></td>";
                            }
                        }
                        else
                        {
                            strHtml += "  <td bgcolor='#FFFFFF' style='text-align: right; font-size: 6px;'> <b>  </b></td>";
                        }
                    }
                }

                strHtml += "  </tr>";
                strHtml += "</table>";
            }

             /***********************/
            /**** SIMPLIFICADO ****/
            /*********************/
            else if (Reporte == "Simplificado")
            {

                decimal a = 0;
                decimal b = 0;
                decimal c = 0;

                strHtml += "<table>";
                strHtml += "<tr> <td bgcolor='#990033 ' style='text-align: center; color: white;' ><b> Ingresos y Costos de Nómina Mensual</b></td></tr>";
                strHtml += "<tr> <td bgcolor='#ffffff ' style='text-align: center; color: white;' ><b></b></td></tr>";
                strHtml += "<tr><td>";
                strHtml += "</td></tr>";
                strHtml += "</table>";
                strHtml += "<table>";
                strHtml += "<tr>";

                strHtml += "<td bgcolor='#990033' style='text-align: center; color: white;' ><b> No. Empleados</b></td>";
                strHtml += "<td bgcolor='#ffffff' style='text-align: center; color: white;' ><b></b></td> ";
                strHtml += "<td bgcolor='#ffffff' style='text-align: left;' ><b>" + dt.Rows[0]["NumeroEmpleados"] + "</b></td>";

                strHtml += "</tr> ";
                strHtml += "</table>";
                strHtml += "<table>";
                strHtml += "<tr>";
                strHtml += "<td bgcolor='#ffffff'></td>";

                foreach (DataColumn column in dt.Columns)
                {
                    NombreColumna = column.ColumnName;

                    if (NombreColumna == "0")
                    {
                        strHtml += "<td bgcolor='#990033' style='text-align: left; color: white;'><b> Actual </b></td>";
                    }
                    else if (NombreColumna != "NumeroEmpleados" && NombreColumna != "A" && NombreColumna != "B" && NombreColumna != "C" && NombreColumna != "D" && NombreColumna != "E" && NombreColumna != "F" && NombreColumna != "G" && NombreColumna != "H" && NombreColumna != "I" && NombreColumna != "J" && NombreColumna != "K")
                    {
                        strHtml += "<td bgcolor='#990033' style='text-align: left; color: white;'><b> Propuesta " + NombreColumna + " </b></td>";
                    }
                }

                strHtml += "</tr> ";
                strHtml += "<tr>";
                strHtml += "<td bgcolor='#d9d9d9 ' style='text-align: left; '><b> Ingreso Neto Total </b></td>";

                for (int i = 0; dt.Rows.Count > i; i++)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        NombreColumna = column.ColumnName;

                        if (NombreColumna == "0")
                        {
                            strHtml += "<td bgcolor='#d9d9d9 ' style='text-align: left; '><b> $" + dt.Rows[i][NombreColumna] + "</b></td>";
                        }
                        else if (NombreColumna != "NumeroEmpleados" && NombreColumna != "A" && NombreColumna != "B" && NombreColumna != "C" && NombreColumna != "D" && NombreColumna != "E" && NombreColumna != "F" && NombreColumna != "G" && NombreColumna != "H" && NombreColumna != "I" && NombreColumna != "J" && NombreColumna != "K")
                        {
                            strHtml += "<td bgcolor='#d9d9d9 ' style='text-align: left; '><b> $" + dt.Rows[i][NombreColumna] + "</td>";
                        }
                    }
                }

                strHtml += "</tr> ";

                strHtml += "<tr>";

                strHtml += "<td bgcolor='#d9d9d9'></td>";

                foreach (DataColumn column in dt.Columns)
                {
                    NombreColumna = column.ColumnName;

                    if (NombreColumna == "0")
                    {
                        strHtml += "<td bgcolor='#990033' style='text-align: left; color: white;'><b> Actual </b></td>";
                    }
                    else if (NombreColumna != "NumeroEmpleados" && NombreColumna != "A" && NombreColumna != "B" && NombreColumna != "C" && NombreColumna != "D" && NombreColumna != "E" && NombreColumna != "F" && NombreColumna != "G" && NombreColumna != "H" && NombreColumna != "I" && NombreColumna != "J" && NombreColumna != "K")
                    {
                        strHtml += "<td bgcolor='#990033' style='text-align: left; color: white;'><b> Propuesta " + NombreColumna + " </b></td>";
                    }
                }

                strHtml += "</tr>";

                strHtml += "<tr>";
                strHtml += "<td bgcolor='#d9d9d9 ' style='text-align: left; '><b> Costo Nómina </b></td>";

                for (int i = 0; dt.Rows.Count > i; i++)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        NombreColumna = column.ColumnName;

                        if (NombreColumna == "A")
                        {
                            strHtml += "<td bgcolor='#d9d9d9 ' style='text-align: left; '><b> $" + dt.Rows[i][NombreColumna] + "</b></td>";
                        }
                        else if (NombreColumna != "NumeroEmpleados" && NombreColumna != "0" && NombreColumna != "1" && NombreColumna != "2" && NombreColumna != "3" && NombreColumna != "4" && NombreColumna != "5" && NombreColumna != "6" && NombreColumna != "7" && NombreColumna != "8" && NombreColumna != "9" && NombreColumna != "10")
                        {
                            strHtml += "<td bgcolor='#d9d9d9 ' style='text-align: left; '><b> $" + dt.Rows[i][NombreColumna] + "</td>";
                        }
                    }
                }

                strHtml += "<td bgcolor='#d9d9d9' style='text-align: left;' ><b> Comisión</b></td>";
                strHtml += "<td bgcolor='#d9d9d9' style='text-align: left;' ><b>0</b></td></tr>";

                strHtml += "</tr> ";
                strHtml += "</table>";

                strHtml += "<table>";
                strHtml += "<tr><td bgcolor='#d9d9d9' style='text-align: left;' ><b> Costo Total</b></td>";

                for (int i = 0; dt.Rows.Count > i; i++)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        if (NombreColumna == "0" || NombreColumna == "A")
                        {
                            NombreColumna = column.ColumnName;

                            a = decimal.Parse(dt.Rows[0]["0"].ToString());

                            if (NombreColumna != "NumeroEmpleados" && NombreColumna != "0" && NombreColumna != "A")
                            {
                                b = decimal.Parse(dt.Rows[i][NombreColumna].ToString());
                                strHtml += "<td bgcolor='#d9d9d9 ' style='text-align: left; '><b> $" + (a - b) + "</td>";
                            }
                            else
                            {
                                strHtml += "<td bgcolor='#d9d9d9 ' style='text-align: left; '></td>";
                            }
                        }
                    }
                }

                strHtml += "</tr>";
                strHtml += "<tr><td bgcolor='#d9d9d9' style='text-align: left;' ><b> Costo Total</b></td>";

                for (int i = 0; dt.Rows.Count > i; i++)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        NombreColumna = column.ColumnName;

                        a = decimal.Parse(dt.Rows[0]["0"].ToString());

                        if (NombreColumna != "NumeroEmpleados" && NombreColumna != "0" && NombreColumna != "A")
                        {
                            b = decimal.Parse(dt.Rows[i][NombreColumna].ToString());
                            c = (a - b);
                            strHtml += "<td bgcolor='#d9d9d9 ' style='text-align: left; '><b> %" + c / (a - b) + "</td>";
                        }
                    }
                }
                strHtml += "</tr>";

                strHtml += "</table>";


            }

            strHtml += "</body>";
            strHtml += "</html>	";

            return strHtml;
        }

        private void ExportarArchivoExcel(DataTable dt)
        {
            try
            {
                string Ruta = string.Empty;
                string FileName = Cliente + " " + Reporte + ".xlsx";
                string attachment = "attachment; filename = " + FileName;
                int x = 0;
                int z = 0;

                int cellIngreso = 3;
                int cellCosto = 0;

                Ruta = Server.MapPath("\\UploadFiles\\");

                System.Drawing.Color coltitle = System.Drawing.ColorTranslator.FromHtml("#990033");
                System.Drawing.Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#d9d9d9");

                using (var excel = new ExcelPackage(new FileInfo(Ruta + FileName)))
                {

                    //Nombre Hoja
                    ExcelWorksheet worksheet = excel.Workbook.Worksheets.Add(Reporte);

                    //****************************//
                    //***** REPORTE RESUMEN *****//
                    //**************************//
                    if (Reporte == "Resumen")
                    {
                        strQuery = "SELECT " + strIngresosTotales + ", " + strCostosTotales + " FROM [dbo].[vwReporte_Resumen] WHERE ID_CLIENTE = " + Id_Cliente;
                        dtResumenTotales = clsQuery.execQueryDataTable(strQuery);

                        //Nombre Empleado
                        worksheet.Cells[3, 2].Value = "Empleado";
                        worksheet.Cells[3, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[3, 2].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);

                        //Escenarios
                        foreach (DataColumn column in dt.Columns)
                        {
                            NombreColumna = column.ColumnName;

                            if (NombreColumna == "0")
                            {
                                worksheet.Cells[3, cellIngreso].Value = "Actual";
                                worksheet.Cells[3, cellIngreso].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[3, cellIngreso].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                                cellIngreso++;
                            }
                            else if (NombreColumna != "Nombre Empleado" && NombreColumna != "A" && NombreColumna != "B" && NombreColumna != "C" && NombreColumna != "D" && NombreColumna != "E" && NombreColumna != "F" && NombreColumna != "G" && NombreColumna != "H" && NombreColumna != "I" && NombreColumna != "J" && NombreColumna != "K")
                            {
                                worksheet.Cells[3, cellIngreso].Value = "Propuesta " + NombreColumna;
                                worksheet.Cells[3, cellIngreso].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[3, cellIngreso].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                                cellIngreso++;
                            }
                        }

                        var cell = worksheet.Cells[2, 2];
                        var row = worksheet.Cells[2, cellIngreso - 1];
                        string rango = cell + ":" + row;
                        worksheet.Cells[rango].Merge = true;
                        worksheet.Cells[2, 2].Value = "INGRESOS NETOS DEL PERSONAL";

                        using (var rangeingresos = worksheet.Cells[2, 2, 2, cellIngreso - 1])
                        {
                            rangeingresos.Style.Font.Bold = true;
                            rangeingresos.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            rangeingresos.Style.Fill.BackgroundColor.SetColor(coltitle);
                            rangeingresos.Style.Font.Color.SetColor(System.Drawing.Color.White);
                            rangeingresos.Style.ShrinkToFit = false;
                        }

                        cellCosto = cellIngreso;

                        cell = worksheet.Cells[2, cellCosto];
                        worksheet.Cells[2, cellCosto].Value = "COSTO";

                        foreach (DataColumn column in dt.Columns)
                        {
                            NombreColumna = column.ColumnName;
                            if (NombreColumna == "0")
                            {
                                worksheet.Cells[3, cellCosto].Value = "Actual";
                                worksheet.Cells[3, cellCosto].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[3, cellCosto].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                                cellCosto++;
                            }
                            else if (NombreColumna != "Nombre Empleado" && NombreColumna != "A" && NombreColumna != "B" && NombreColumna != "C" && NombreColumna != "D" && NombreColumna != "E" && NombreColumna != "F" && NombreColumna != "G" && NombreColumna != "H" && NombreColumna != "I" && NombreColumna != "J" && NombreColumna != "K")
                            {
                                worksheet.Cells[3, cellCosto].Value = "Propuesta " + NombreColumna;
                                worksheet.Cells[3, cellCosto].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[3, cellCosto].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                                cellCosto++;
                            }

                        }

                        row = worksheet.Cells[2, cellCosto - 1];
                        string rangocosto = cell + ":" + row;
                        worksheet.Cells[rangocosto].Merge = true;

                        using (var rangecosto = worksheet.Cells[2, cellIngreso, 2, cellCosto - 1])
                        {
                            rangecosto.Style.Font.Bold = true;
                            rangecosto.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            rangecosto.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                            rangecosto.Style.ShrinkToFit = false;
                        }

                        x = 3;
                        z = 4;
                        int i = 0;

                        for (i = 0; dt.Rows.Count > i; i++)
                        {
                            worksheet.Cells[4 + i, 2].Value = dt.Rows[i]["Nombre Empleado"].ToString();
                            worksheet.Cells[4 + i, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[4 + i, 2].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        }

                        foreach (DataColumn column in dt.Columns)
                        {
                            NombreColumna = column.ColumnName;

                            for (i = 0; dt.Rows.Count > i; i++)
                            {
                                if (NombreColumna != "Nombre Empleado")
                                {
                                    worksheet.Cells[z + i, x].Value = dt.Rows[i][NombreColumna].ToString();
                                    worksheet.Cells[z + i, x].Merge = true;
                                    worksheet.Cells[z + i, x].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    if (NombreColumna == "0" || NombreColumna == "A")
                                    {
                                        worksheet.Cells[z + i, x].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                                    }
                                    else
                                    {
                                        worksheet.Cells[z + i, x].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                                    }
                                }
                            }

                            if (NombreColumna != "Nombre Empleado")
                            {
                                x++;
                            }
                        }


                        x = 3;

                        foreach (DataColumn column in dtResumenTotales.Columns)
                        {
                            NombreColumna = column.ColumnName;

                            for (int a = 0; dtResumenTotales.Rows.Count > a; a++)
                            {
                                if (NombreColumna != "Nombre Empleado")
                                {
                                    worksheet.Cells[z + i, x].Value = dtResumenTotales.Rows[a][NombreColumna].ToString();
                                    worksheet.Cells[z + i, x].Merge = true;
                                    worksheet.Cells[z + i, x].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    if (NombreColumna == "0" || NombreColumna == "A")
                                    {
                                        worksheet.Cells[z + i, x].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                                    }
                                    else
                                    {
                                        worksheet.Cells[z + i, x].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                                    }
                                }
                            }

                            if (NombreColumna != "Nombre Empleado")
                            {
                                x++;
                            }
                        }


                        x = 3;
                       //i = 0;

                        foreach (DataColumn column in dtResumenTotales.Columns)
                        {
                            NombreColumna = column.ColumnName;

                            for (int a = 0; dtResumenTotales.Rows.Count > a; a++)
                            {
                                if (NombreColumna != "Nombre Empleado")
                                {
                                    if (dt.Columns.Contains("0"))
                                    {
                                        if (NombreColumna == "0" || NombreColumna == "A")
                                        {
                                            worksheet.Cells[z + i, x].Value = Convert.ToDecimal(dtResumenTotales.Rows[0][NombreColumna]) - Convert.ToDecimal(dtResumenTotales.Rows[a][NombreColumna]);
                                        }
                                    }

                                    worksheet.Cells[z + i, x].Merge = true;
                                    worksheet.Cells[z + i, x].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    if (NombreColumna == "0" || NombreColumna == "A")
                                    {
                                        worksheet.Cells[z + i, x].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                                    }
                                    else
                                    {
                                        worksheet.Cells[z + i, x].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                                    }
                                }
                            }

                            if (NombreColumna != "Nombre Empleado")
                            {
                                x++;
                            }
                        }

                        worksheet.Cells[1, 2].Value = "Ingresos y Costos Individuales Netos";
                        cell = worksheet.Cells[1, 2];
                        row = worksheet.Cells[1, cellCosto - 1];
                        rango = cell + ":" + row;
                        worksheet.Cells[rango].Merge = true;

                        using (var range = worksheet.Cells[1, 2, 1, cellCosto - 1])
                        {
                            range.Style.Font.Bold = true;
                            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            range.Style.Fill.BackgroundColor.SetColor(coltitle);
                            range.Style.Font.Color.SetColor(System.Drawing.Color.White);
                            range.Style.ShrinkToFit = false;
                        }


                    }
                    //*********************************//
                    //***** REPORTE SIMPLIFICADO *****//
                    //*******************************//
                    else if (Reporte == "Simplificado")
                    {

                        int y = 5;
                        foreach (DataColumn column in dt.Columns)
                        {
                            NombreColumna = column.ColumnName;
                            if (NombreColumna == "0")
                            {
                                y++;
                            }
                            else if (NombreColumna != "NumeroEmpleados" && NombreColumna != "A" && NombreColumna != "B" && NombreColumna != "C" && NombreColumna != "D" && NombreColumna != "E" && NombreColumna != "F" && NombreColumna != "G" && NombreColumna != "H" && NombreColumna != "I" && NombreColumna != "J" && NombreColumna != "K")
                            {
                                y++;
                            }

                        }

                        using (var range = worksheet.Cells[1, 2, 20, y])
                        {
                            range.Style.Font.Bold = true;
                            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                            range.Style.ShrinkToFit = false;
                        }

                        //Nombre Empleado
                        worksheet.Cells[4, 3].Value = "No. Empleados";
                        worksheet.Cells[4, 3].Style.Font.Bold = true;
                        worksheet.Cells[4, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[4, 3].Style.Fill.BackgroundColor.SetColor(coltitle);
                        worksheet.Cells[4, 3].Style.Font.Color.SetColor(System.Drawing.Color.White);

                        worksheet.Cells[4, 4].Style.Font.Bold = true;
                        worksheet.Cells[4, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[4, 4].Style.Fill.BackgroundColor.SetColor(coltitle);

                        worksheet.Cells[4, 5].Value = dtSimplificado.Rows[0]["NumeroEmpleados"].ToString();
                        worksheet.Cells[4, 5].Style.Font.Bold = true;
                        worksheet.Cells[4, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[4, 5].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);

                        var cell = worksheet.Cells[2, 3];
                        var row = worksheet.Cells[2, 6];
                        string rango = cell + ":" + row;
                        worksheet.Cells[rango].Merge = true;
                        worksheet.Cells[2, 3].Value = "Ingresos y Costos de Nómina";

                        using (var range = worksheet.Cells[2, 3, 2, 6])
                        {
                            range.Style.Font.Bold = true;
                            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            range.Style.Fill.BackgroundColor.SetColor(coltitle);
                            range.Style.Font.Color.SetColor(System.Drawing.Color.White);
                            range.Style.ShrinkToFit = false;
                            range.Style.Font.Size = 18;
                        }

                        int i = 5;

                        foreach (DataColumn column in dt.Columns)
                        {
                            NombreColumna = column.ColumnName;
                            if (NombreColumna == "0")
                            {
                                worksheet.Cells[6, i].Value = "Actual";
                                worksheet.Cells[6, i].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[6, i].Style.Fill.BackgroundColor.SetColor(coltitle);
                                worksheet.Cells[6, i].Style.Font.Color.SetColor(System.Drawing.Color.White);

                                i++;
                            }
                            else if (NombreColumna != "NumeroEmpleados" && NombreColumna != "A" && NombreColumna != "B" && NombreColumna != "C" && NombreColumna != "D" && NombreColumna != "E" && NombreColumna != "F" && NombreColumna != "G" && NombreColumna != "H" && NombreColumna != "I" && NombreColumna != "J" && NombreColumna != "K")
                            {
                                worksheet.Cells[6, i].Value = "Propuesta " + NombreColumna;
                                worksheet.Cells[6, i].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[6, i].Style.Fill.BackgroundColor.SetColor(coltitle);
                                worksheet.Cells[6, i].Style.Font.Color.SetColor(System.Drawing.Color.White);
                                i++;
                            }

                        }

                        worksheet.Cells[7, 3].Value = "Ingreso Neto Total";
                        worksheet.Cells[7, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[7, 3].Style.Fill.BackgroundColor.SetColor(colFromHex);
                        worksheet.Cells[7, 4].Style.Fill.BackgroundColor.SetColor(colFromHex);

                        i = 5;

                        foreach (DataColumn column in dt.Columns)
                        {
                            NombreColumna = column.ColumnName;

                            for (int a = 0; dt.Rows.Count > a; a++)
                            {
                                if (NombreColumna != "NumeroEmpleados" && NombreColumna != "A" && NombreColumna != "B" && NombreColumna != "C" && NombreColumna != "D" && NombreColumna != "E" && NombreColumna != "F" && NombreColumna != "G" && NombreColumna != "H" && NombreColumna != "I" && NombreColumna != "J" && NombreColumna != "K")
                                {
                                    worksheet.Cells[7, i].Value = Convert.ToDecimal(dt.Rows[a][NombreColumna].ToString());
                                    worksheet.Cells[7, i].Merge = true;
                                    worksheet.Cells[7, i].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[7, i].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                                    worksheet.Cells[7, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    worksheet.Cells[7, i].Style.Border.Bottom.Color.SetColor(System.Drawing.Color.White);
                                    worksheet.Cells[7, i].Style.Numberformat.Format = "$#,##0.00";
                                    i++;
                                }
                            }
                        }

                        i = 5;

                        foreach (DataColumn column in dt.Columns)
                        {
                            NombreColumna = column.ColumnName;
                            if (NombreColumna == "0")
                            {
                                worksheet.Cells[9, i].Value = "Actual";
                                worksheet.Cells[9, i].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[9, i].Style.Fill.BackgroundColor.SetColor(coltitle);
                                worksheet.Cells[9, i].Style.Font.Color.SetColor(System.Drawing.Color.White);
                                i++;
                            }
                            else if (NombreColumna != "NumeroEmpleados" && NombreColumna != "A" && NombreColumna != "B" && NombreColumna != "C" && NombreColumna != "D" && NombreColumna != "E" && NombreColumna != "F" && NombreColumna != "G" && NombreColumna != "H" && NombreColumna != "I" && NombreColumna != "J" && NombreColumna != "K")
                            {
                                worksheet.Cells[9, i].Value = "Propuesta " + NombreColumna;
                                worksheet.Cells[9, i].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[9, i].Style.Fill.BackgroundColor.SetColor(coltitle);
                                worksheet.Cells[9, i].Style.Font.Color.SetColor(System.Drawing.Color.White);
                                i++;
                            }

                        }

                        worksheet.Cells[10, 3].Value = "Costo Nómina";
                        worksheet.Cells[10, 3].Style.Font.Bold = true;
                        worksheet.Cells[10, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[10, 3].Style.Fill.BackgroundColor.SetColor(colFromHex);
                        worksheet.Cells[10, 4].Style.Fill.BackgroundColor.SetColor(colFromHex);
                        worksheet.Cells["C10:D10"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["C10:D10"].Style.Border.Bottom.Color.SetColor(System.Drawing.Color.White);


                        i = 5;

                        foreach (DataColumn column in dt.Columns)
                        {
                            NombreColumna = column.ColumnName;

                            for (int a = 0; dt.Rows.Count > a; a++)
                            {
                                if (NombreColumna != "NumeroEmpleados" && NombreColumna != "0" && NombreColumna != "1" && NombreColumna != "2" && NombreColumna != "3" && NombreColumna != "4" && NombreColumna != "5" && NombreColumna != "6" && NombreColumna != "7" && NombreColumna != "8" && NombreColumna != "9" && NombreColumna != "10")
                                {
                                    worksheet.Cells[10, i].Value = Convert.ToDecimal(dt.Rows[a][NombreColumna].ToString());
                                    worksheet.Cells[10, i].Merge = true;
                                    worksheet.Cells[10, i].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[10, i].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                                    worksheet.Cells[10, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    worksheet.Cells[10, i].Style.Border.Bottom.Color.SetColor(System.Drawing.Color.White);
                                    worksheet.Cells[10, i].Style.Numberformat.Format = "$#,##0.00";
                                    i++;
                                }
                            }
                        }

                        worksheet.Cells[11, 3].Value = "Comisión";
                        worksheet.Cells[11, 3].Style.Font.Bold = true;
                        worksheet.Cells[11, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[11, 3].Style.Fill.BackgroundColor.SetColor(colFromHex);
                        worksheet.Cells[11, 4].Style.Fill.BackgroundColor.SetColor(colFromHex);
                        worksheet.Cells["C11:D11"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["C11:D11"].Style.Border.Bottom.Color.SetColor(System.Drawing.Color.White);

                        i = 5;

                        foreach (DataColumn column in dt.Columns)
                        {
                            NombreColumna = column.ColumnName;

                            for (int a = 0; dt.Rows.Count > a; a++)
                            {
                                if (NombreColumna != "NumeroEmpleados" && NombreColumna != "0" && NombreColumna != "1" && NombreColumna != "2" && NombreColumna != "3" && NombreColumna != "4" && NombreColumna != "5" && NombreColumna != "6" && NombreColumna != "7" && NombreColumna != "8" && NombreColumna != "9" && NombreColumna != "10")
                                {
                                    worksheet.Cells[11, i].Merge = true;
                                    worksheet.Cells[11, i].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[11, i].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                                    worksheet.Cells[11, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    worksheet.Cells[11, i].Style.Border.Bottom.Color.SetColor(System.Drawing.Color.White);
                                    i++;
                                }
                            }
                        }

                        worksheet.Cells[12, 3].Value = "Costo Total";
                        worksheet.Cells[12, 3].Style.Font.Bold = true;
                        worksheet.Cells[12, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[12, 3].Style.Fill.BackgroundColor.SetColor(colFromHex);
                        worksheet.Cells[12, 4].Style.Fill.BackgroundColor.SetColor(colFromHex);
                        worksheet.Cells["C12:D12"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["C12:D12"].Style.Border.Bottom.Color.SetColor(System.Drawing.Color.White);

                        i = 5;

                        foreach (DataColumn column in dt.Columns)
                        {
                            NombreColumna = column.ColumnName;

                            for (int a = 0; dt.Rows.Count > a; a++)
                            {
                                if (NombreColumna != "NumeroEmpleados" && NombreColumna != "0" && NombreColumna != "1" && NombreColumna != "2" && NombreColumna != "3" && NombreColumna != "4" && NombreColumna != "5" && NombreColumna != "6" && NombreColumna != "7" && NombreColumna != "8" && NombreColumna != "9" && NombreColumna != "10")
                                {
                                    worksheet.Cells[12, i].Value = Convert.ToDecimal(dt.Rows[a][NombreColumna].ToString());
                                    worksheet.Cells[12, i].Merge = true;
                                    worksheet.Cells[12, i].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[12, i].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                                    worksheet.Cells[12, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    worksheet.Cells[12, i].Style.Border.Bottom.Color.SetColor(System.Drawing.Color.White);
                                    worksheet.Cells[12, i].Style.Numberformat.Format = "$#,##0.00";
                                    i++;
                                }
                            }
                        }

                        worksheet.Cells[13, 3].Value = "Ahorro $";
                        worksheet.Cells[13, 3].Style.Font.Bold = true;
                        worksheet.Cells[13, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[13, 3].Style.Fill.BackgroundColor.SetColor(colFromHex);
                        worksheet.Cells["C13:E13"].Style.Fill.BackgroundColor.SetColor(colFromHex);

                        i = 5;

                        foreach (DataColumn column in dt.Columns)
                        {
                            NombreColumna = column.ColumnName;

                            for (int a = 0; dt.Rows.Count > a; a++)
                            {
                                if (NombreColumna != "NumeroEmpleados" && NombreColumna != "0" && NombreColumna != "1" && NombreColumna != "2" && NombreColumna != "3" && NombreColumna != "4" && NombreColumna != "5" && NombreColumna != "6" && NombreColumna != "7" && NombreColumna != "8" && NombreColumna != "9" && NombreColumna != "10")
                                {
                                    if (NombreColumna != "A")
                                    {

                                        if (dt.Columns.Contains("A"))
                                        {
                                            worksheet.Cells[13, i].Value = Convert.ToDecimal(Decimal.Parse(dt.Rows[0]["A"].ToString()) - decimal.Parse(dt.Rows[a][NombreColumna].ToString()));
                                        }
                                        worksheet.Cells[13, i].Merge = true;
                                        worksheet.Cells[13, i].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells[13, i].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                                        worksheet.Cells[13, i].Style.Numberformat.Format = "$#,##0.00";
                                        worksheet.Cells[13, i].AutoFitColumns();
                                        i++;
                                    }
                                }
                            }
                        }

                        worksheet.Cells[14, 3].Value = "Ahorro %";
                        worksheet.Cells[14, 3].Style.Font.Bold = true;
                        worksheet.Cells[14, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[14, 3].Style.Fill.BackgroundColor.SetColor(colFromHex);
                        worksheet.Cells["C14:E14"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                        worksheet.Cells["C14:D14"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["C14:D14"].Style.Border.Bottom.Color.SetColor(System.Drawing.Color.White);

                        i = 5;

                        foreach (DataColumn column in dt.Columns)
                        {
                            NombreColumna = column.ColumnName;

                            for (int a = 0; dt.Rows.Count > a; a++)
                            {
                                if (NombreColumna != "NumeroEmpleados" && NombreColumna != "0" && NombreColumna != "1" && NombreColumna != "2" && NombreColumna != "3" && NombreColumna != "4" && NombreColumna != "5" && NombreColumna != "6" && NombreColumna != "7" && NombreColumna != "8" && NombreColumna != "9" && NombreColumna != "10")
                                {
                                    if (NombreColumna != "A")
                                    {
                                        if (dt.Columns.Contains("A"))
                                        {
                                            worksheet.Cells[14, i].Value = ((Decimal.Parse(dt.Rows[0]["A"].ToString()) - Decimal.Parse(dt.Rows[a][NombreColumna].ToString())) / Decimal.Parse(dt.Rows[0]["A"].ToString()) * 100).ToString("##.00");
                                        }
                                        worksheet.Cells[14, i].Merge = true;
                                        worksheet.Cells[14, i].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells[14, i].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                                        worksheet.Cells[14, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells[14, i].Style.Border.Bottom.Color.SetColor(System.Drawing.Color.White);
                                        i++;
                                    }
                                }
                            }
                        }

                    }

                    worksheet.Column(5).Width = 20;
                    worksheet.Column(6).Width = 20;
                    worksheet.Column(7).Width = 20;
                    worksheet.Column(8).Width = 20;
                    worksheet.Column(9).Width = 20;
                    worksheet.Column(10).Width = 20;

                    worksheet.BackgroundImage.SetFromFile(new FileInfo(Server.MapPath("\\image\\fondo_Reportes.JPG")));

                    //excel.Save();
                    using (var memoryStream = new MemoryStream())
                    {
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
                        excel.SaveAs(memoryStream);
                        memoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                        memoryStream.Dispose();
                    }
                }

            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
            }

        }

        private void ExportarPdf(DataTable dt)
        {
            var document = new Document(PageSize.A4, 0, 0, 0, 0);

            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, output);

            document.Open();

            var parsedHtmlElements = HTMLWorker.ParseToList(new StringReader(ReporteHtml(dt)), null);
            foreach (var htmlElement in parsedHtmlElements)
                document.Add(htmlElement as IElement);

            document.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}.pdf", Cliente + "_" + Reporte));
            Response.BinaryWrite(output.ToArray());

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        public void Mensaje(string textomensaje, CuadroMensaje.CuadroMensajeIcono icono)
        {
            CuadroMensaje Mensaje = new CuadroMensaje(textomensaje, "MENSAJE", icono, CuadroMensaje.CuadroMensajeBoton.Ok, CuadroMensaje.CuadroMensajeEstilo.EstiloB);
            Literal ltrMensaje = (Literal)this.Master.FindControl("ltrMensaje");
            ltrMensaje.Text = Mensaje.Mostrar(this);
        }

    }
}