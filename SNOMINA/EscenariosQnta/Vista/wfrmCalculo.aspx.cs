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


namespace EscenariosQnta
{
    public partial class wfrmCalculo : System.Web.UI.Page
    {
        #region Variables

        string Id_Cliente = string.Empty;
        string Id_Escenario = string.Empty;
        string Id_StsEsc = string.Empty;
        string Notas = string.Empty;

        string strReturnValue = string.Empty;
        string strQuery = string.Empty;

        clsDatos clsQuery = new clsDatos();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenClientes();
                ddlEscenario.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
                //ddlPropuesta.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
                ddlEscenario.Attributes.Add("onchange", "openpopup()");                
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

                ddlCliente.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

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

                if (dtEscenario.Rows.Count > 0)
                {
                    ddlEscenario.DataSource = dtEscenario;
                    ddlEscenario.DataTextField = "Id_Escenario";
                    ddlEscenario.DataValueField = "Id_Escenario";
                    ddlEscenario.DataBind();
                    ddlEscenario.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
                }
                else
                {
                    ddlEscenario.DataSource = dtEscenario;
                    ddlEscenario.DataBind();
                    ddlEscenario.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
                }


            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);

            }

        }

        protected void ObtenPropuesta(int Id_Escenario)
        {

            //try
            //{
            //    DataTable dtPropuesta = new DataTable();

            //    strQuery = string.Format("SP_ObtenPropuestaByEscenario {0}", Id_Escenario);

            //    dtPropuesta = clsQuery.execQueryDataTable(strQuery);

            //    if (dtPropuesta.Rows.Count > 0)
            //    {
            //        ddlPropuesta.DataSource = dtPropuesta;
            //        ddlPropuesta.DataTextField = "Id_Propuesta";
            //        ddlPropuesta.DataValueField = "Id_Propuesta";
            //        ddlPropuesta.DataBind();
            //    }

            //    ddlPropuesta.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

            //}
            //catch (Exception ex)
            //{
            //    Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);

            //}
        }

        protected void ObtenCalculo()
        {
            DataTable dtCalculo = new DataTable();

            int Id_Cliente = int.Parse(ddlCliente.SelectedItem.Value);
            int Id_Escenario = int.Parse(ddlEscenario.SelectedItem.Value);
            //int Id_Propuesta = int.Parse(ddlPropuesta.SelectedItem.Value);

            strQuery = string.Format("SP_ObtenCalculo {0}, {1}", Id_Cliente, Id_Escenario);

            dtCalculo = clsQuery.execQueryDataTable(strQuery);

            if (dtCalculo.Rows.Count > 0)
            {
                gvCalculo.DataSource = dtCalculo;
                gvCalculo.DataBind();
                gvCalculo.Visible = true;
                btnExportar.Visible = true;
            }
            else
            {
                gvCalculo.Visible = false;
                btnExportar.Visible = true;
                Mensaje("El Escenario No tiene Empleados o Hubo un Error en el Calculo", CuadroMensaje.CuadroMensajeIcono.Advertencia);
            }
        }

        protected void ObtenEstatus()
        {
            try
            {
                DataTable dtEstatus = new DataTable();
                DataTable dtEstatusEscenario = new DataTable();

                Id_Cliente = ddlCliente.SelectedItem.Value;
                Id_Escenario = ddlEscenario.SelectedItem.Value;

                strQuery = string.Format("SP_ObtenEstatusEscenario");

                dtEstatus = clsQuery.execQueryDataTable(strQuery);

                if (dtEstatus.Rows.Count > 0)
                {
                    ddlEstatus.DataSource = dtEstatus;
                    ddlEstatus.DataTextField = "Descripcion";
                    ddlEstatus.DataValueField = "Id_StsEsc";
                    ddlEstatus.DataBind();
                    ddlEstatus.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
                }

                strQuery = string.Format("SP_ObtenEstatusPorEscenario {0}, {1}", Id_Cliente, Id_Escenario);

                dtEstatusEscenario = clsQuery.execQueryDataTable(strQuery);

                if (dtEstatusEscenario.Rows.Count > 0)
                {
                    ddlEstatus.SelectedValue = dtEstatusEscenario.Rows[0]["Id_StsEsc"].ToString();
                    txtNotas.Text = dtEstatusEscenario.Rows[0]["Notas"].ToString();
                }
                else 
                {
                    ddlEstatus.SelectedValue = "-1";
                    txtNotas.Text = "";                
                }


            }
            catch (Exception ex)
            { }
        }

        protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Id_Cliente = int.Parse(ddlCliente.SelectedItem.Value);
            ObtenEscenario(Id_Cliente);
            tbEstaus.Visible = false;
            gvCalculo.Visible = false;
            btnExportar.Visible = false;
        }

        protected void ddlEscenario_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int Id_Escenario = int.Parse(ddlEscenario.SelectedItem.Value);
            //ObtenPropuesta(Id_Escenario);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "openpopup();", true);            
            ObtenCalculo();
            tbEstaus.Visible = true;
            ObtenEstatus();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CerrarPopUp", "closepopup();", true);
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "closepopup()", "", true);
            //ClientScript.RegisterStartupScript(GetType(), "CerrarPopUp", "closepopup()");
            btnCalcular.Visible = true;

        }

        protected void ddlPropuesta_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ObtenCalculo();
        }

        protected void gvCalculo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Id_Cliente = ddlCliente.SelectedItem.Value;
            Id_Escenario = ddlEscenario.SelectedItem.Value;
            Id_StsEsc = ddlEstatus.SelectedItem.Value;
            Notas = txtNotas.Text.ToString();

            strQuery = string.Format("SP_InsertaHistorialStsEsc {0}, {1}, {2}, '{3}'", Id_Cliente, Id_Escenario, Id_StsEsc, Notas);

            strReturnValue = clsQuery.execQueryString(strQuery);

            if (strReturnValue == "1")
            {

                Mensaje("Guardado", CuadroMensaje.CuadroMensajeIcono.Exitoso);
            }
        }

        public void Mensaje(string textomensaje, CuadroMensaje.CuadroMensajeIcono icono)
        {
            CuadroMensaje Mensaje = new CuadroMensaje(textomensaje, "MENSAJE", icono, CuadroMensaje.CuadroMensajeBoton.Ok, CuadroMensaje.CuadroMensajeEstilo.EstiloB);
            Literal ltrMensaje = (Literal)this.Master.FindControl("ltrMensaje");
            ltrMensaje.Text = Mensaje.Mostrar(this);
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            ExportarArchivoExcel();
        }

        private void ExportarArchivoExcel()
        {
            try
            {
                //Response.ClearContent();
                //Response.Buffer = true;
                //Response.ClearContent();
                //Response.ClearHeaders();
                //Response.Charset = "";
                //string FileName = "Empleados_NO_Insertados" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                //string attachment = "attachment; filename = " + FileName;
                //Response.ClearContent();
                //Response.AddHeader("content-disposition", attachment);
                //Response.ContentType = "application/ms-excel";  //Excel 2003  
                //StringWriter strWrite = new StringWriter();
                //HtmlTextWriter htmWrite = new HtmlTextWriter(strWrite);
                ////HtmlForm htmfrm = new HtmlForm();
                ////htmfrm.Attributes["runat"] = "server";
                ////gvEmpleados.Parent.Controls.Add(htmWrite);
                //gvEmpleados.RenderControl(htmWrite);            
                ////htmfrm.Controls.Add(gvEmpleados);
                ////htmfrm.RenderControl(htmWrite);
                //Response.Write(strWrite.ToString());
                //Response.Flush();
                //Response.End();

                string nombrearchivo = ddlCliente.SelectedItem.Text.ToString() + "_Escenario_" +  ddlEscenario.SelectedItem.Text.ToString();
              
                Response.Clear();
                Response.Buffer = true;
                //Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
                string FileName = nombrearchivo + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                string attachment = "attachment; filename = " + FileName;
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //To Export all pages
                    //GridView1.AllowPaging = false;
                    //this.BindGrid();

                    gvCalculo.HeaderRow.BackColor = Color.DarkRed;
                    gvCalculo.HeaderRow.ForeColor = Color.White;

                    foreach (TableCell cell in gvCalculo.HeaderRow.Cells)
                    {
                        cell.BackColor = gvCalculo.HeaderStyle.BackColor;
                    }
                    foreach (GridViewRow row in gvCalculo.Rows)
                    {
                        row.BackColor = Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = gvCalculo.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = gvCalculo.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    gvCalculo.RenderControl(hw);

                    //style to format numbers to string
                     //string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                    //Response.Write(sw.ToString());          
                    string style = @"<style> .textmode { } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();


                   

                }

            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
            }

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void btnCalcular_Click(object sender, EventArgs e)
        {
            ObtenCalculo();
            tbEstaus.Visible = true;
            ObtenEstatus();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CerrarPopUp", "closepopup();", true);
        }
       
    }
}