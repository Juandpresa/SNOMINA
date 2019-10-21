using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EscenariosQnta.Clases;
using System.Data;
using EscenariosQnta.Data;

namespace EscenariosQnta.Vista
{
    public partial class wfrmConceptosEscenario : System.Web.UI.Page
    {
        #region Variables

        string Cliente = string.Empty;
        string Escenario = string.Empty;

        string Id_Concepto = string.Empty;
        string NomCorto = string.Empty;
        string Calculo = string.Empty;

        string strQuery = string.Empty;

        string ReturnValue = string.Empty;

        clsDatos clsQuery = new clsDatos();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenClientes();
                ddlEscenario.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
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

        protected void ObtenEscenario(string Id_Cliente)
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
                }
                else
                {
                    ddlEscenario.Items.Clear();
                }

                ddlEscenario.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);

            }

        }      

        protected void ObtenConceptosPorEscenario()
        {
            DataTable dtConceptos = new DataTable();
            Cliente = ddlCliente.SelectedItem.Value;
            Escenario = ddlEscenario.SelectedItem.Value;

            strQuery = string.Format("SP_ObtenConceptosEscenario {0}, {1}", Cliente, Escenario);

            dtConceptos = clsQuery.execQueryDataTable(strQuery);

            if (dtConceptos.Rows.Count > 0)
            {
                gvConceptoEscenario.DataSource = dtConceptos;
                gvConceptoEscenario.DataBind();
                gvConceptoEscenario.Visible = true;
            }
            else
            {
                gvConceptoEscenario.Visible = false;
            }

        }

        protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
           Cliente = ddlCliente.SelectedItem.Value;
            ObtenEscenario(Cliente);
        }

        protected void ddlEscenario_SelectedIndexChanged(object sender, EventArgs e)
        {            
            ObtenConceptosPorEscenario();
        }

        public void Mensaje(string textomensaje, CuadroMensaje.CuadroMensajeIcono icono)
        {
            CuadroMensaje Mensaje = new CuadroMensaje(textomensaje, "MENSAJE", icono, CuadroMensaje.CuadroMensajeBoton.Ok, CuadroMensaje.CuadroMensajeEstilo.EstiloB);
            Literal ltrMensaje = (Literal)this.Master.FindControl("ltrMensaje");
            ltrMensaje.Text = Mensaje.Mostrar(this);
        }

        protected void gvConceptoEscenario_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvConceptoEscenario.EditIndex = e.NewEditIndex;
            ObtenConceptosPorEscenario();
        }

        protected void gvConceptoEscenario_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvConceptoEscenario.EditIndex = -1;
            ObtenConceptosPorEscenario();
        }

        protected void gvConceptoEscenario_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Cliente = ddlCliente.SelectedItem.Value;
            Escenario = ddlEscenario.SelectedItem.Value;
            Id_Concepto = ((Label)gvConceptoEscenario.Rows[e.RowIndex].FindControl("lbId_Conceptos")).Text; ;
            Calculo = ((TextBox)gvConceptoEscenario.Rows[e.RowIndex].FindControl("txtCalculo")).Text; ;

            strQuery = string.Format("dbo.SP_ActualizaConceptoEscenario {0}, '{1}', '{2}', '{3}'",
                      Cliente, Escenario, Id_Concepto, Calculo);

            ReturnValue = clsQuery.execQueryString(strQuery);

            if (ReturnValue == "1")
            {
                gvConceptoEscenario.EditIndex = -1;
                ObtenConceptosPorEscenario();
                Mensaje("ACTUALIZADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
            }

        }

        protected void gvConceptoEscenario_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }
        }

    }
}