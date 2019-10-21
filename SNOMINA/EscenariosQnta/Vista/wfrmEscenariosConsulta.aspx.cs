using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EscenariosQnta.Data;
using EscenariosQnta.Clases;

namespace EscenariosQnta
{
    public partial class wfrmEscenarioConsulta : System.Web.UI.Page
    {
        #region Variables

        int Id_Escenario = 0;
        string Id_EscDetalle = string.Empty;
        string Folio = string.Empty;
        string Cliente = string.Empty;
        string Prestaciones = string.Empty;
        string Nomina = string.Empty;
        string Asimilados = string.Empty;
        string Honorarios = string.Empty;
        //string OtrosProductos = string.Empty;
        string TN = string.Empty;
        string EZWallet = string.Empty;
        string Clasificacion = string.Empty;
        string SueldoIni = string.Empty;
        string SueldoFin = string.Empty;
        string Id_Comision = string.Empty;
        string ImporteComsion = string.Empty;
        string Nota = string.Empty;

        string strQuery = string.Empty;
        string strReturnVlaue = string.Empty;

        clsDatos clsQuery = new clsDatos();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ObtenEscenarios();
        }

        protected void ObtenEscenarios()
        {
            DataTable dtEscenarios = new DataTable();

            dtEscenarios = clsQuery.execQueryDataTable("SP_ObtenEscenarios");

            if (dtEscenarios.Rows.Count > 0)
            {

                gvEscenario.DataSource = dtEscenarios;
                gvEscenario.DataBind();
            }
            else
            {
                Mensaje("ADVERTENCIA: No se encontraron resultados", CuadroMensaje.CuadroMensajeIcono.Advertencia);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {

        }

        protected void gvEscenario_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEscenario.EditIndex = -1;
            ObtenEscenarios();
        }

        protected void gvEscenario_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEscenario.EditIndex = e.NewEditIndex;
            ObtenEscenarios();
        }

        protected void gvEscenario_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Id_Escenario = int.Parse(((Label)gvEscenario.Rows[e.RowIndex].FindControl("lbId_Escenario")).Text);
                Id_EscDetalle = ((Label)gvEscenario.Rows[e.RowIndex].FindControl("lbId_EscDetalle")).Text;
                Cliente = ((DropDownList)gvEscenario.Rows[e.RowIndex].FindControl("ddlClientegv")).Text;
                Prestaciones = ((DropDownList)gvEscenario.Rows[e.RowIndex].FindControl("ddlPrestaciongv")).Text;
                Nomina = ((TextBox)gvEscenario.Rows[e.RowIndex].FindControl("txtPorcNomina")).Text;
                Asimilados = ((TextBox)gvEscenario.Rows[e.RowIndex].FindControl("txtPorcAsimilados")).Text;
                Honorarios = ((TextBox)gvEscenario.Rows[e.RowIndex].FindControl("txtPorcHonorarios")).Text;
                //OtrosProductos = ((TextBox)gvEscenario.Rows[e.RowIndex].FindControl("txtPorOtrosProductos")).Text;
                TN = ((TextBox)gvEscenario.Rows[e.RowIndex].FindControl("txtPorcTN")).Text;
                EZWallet = ((TextBox)gvEscenario.Rows[e.RowIndex].FindControl("txtPorcEZWallet")).Text;
                Clasificacion = ((DropDownList)gvEscenario.Rows[e.RowIndex].FindControl("ddlClasificaciongv")).Text;
                //SueldoIni = ((TextBox)gvEscenario.FooterRow.FindControl("txtRangoSueldoIni")).Text;
                //SueldoFin = ((TextBox)gvEscenario.FooterRow.FindControl("txtRangoSueldoFin")).Text;
                Id_Comision = ((DropDownList)gvEscenario.Rows[e.RowIndex].FindControl("ddlTipoComisiongv")).Text;
                ImporteComsion = ((TextBox)gvEscenario.Rows[e.RowIndex].FindControl("txtImporteComision")).Text;
                Nota = ((TextBox)gvEscenario.Rows[e.RowIndex].FindControl("txtNota")).Text;

                strQuery = string.Format("SP_ActualizaEscenarioPropuesta {0}, {1}, {2}, {3}, '{4}', '{5}', '{6}', '{7}', '{8}', {9}, {10}, '{11}', '{12}'",
                Id_Escenario, Id_EscDetalle, Cliente, Prestaciones, Nomina, Asimilados, Honorarios, TN, EZWallet, Clasificacion, Id_Comision, ImporteComsion, Nota);

                strReturnVlaue = clsQuery.execQueryString(strQuery);

                if (strReturnVlaue == "1")
                {
                    gvEscenario.EditIndex = -1;
                    ObtenEscenarios();
                    Mensaje("ACTUALIZADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
                }

            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
            }
        }

        protected void gvEscenario_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            DataTable dtCliente = new DataTable();
            DataTable dtPrestacion = new DataTable();
            DataTable dtClasificacion = new DataTable();
            DataTable dtComision = new DataTable();

            dtCliente = clsQuery.execQueryDataTable("SP_ObtenClientes");
            //dtPrestacion = clsQuery.execQueryDataTable("SP_ObtenPrestaciones");
            dtPrestacion = clsQuery.execQueryDataTable("SP_ObtenFactor");
            dtClasificacion = clsQuery.execQueryDataTable("SP_ObtenClasificacionEmpleado");
            dtComision = clsQuery.execQueryDataTable("SP_ObtenComision");

            if (e.Row.RowType == DataControlRowType.DataRow && gvEscenario.EditIndex == e.Row.RowIndex)
            {
                //DropDownList ddlClientegv = gvEscenario.FooterRow.FindControl("ddlClientegv") as DropDownList;
                //DropDownList ddlPrestaciongv = gvEscenario.FooterRow.FindControl("ddlPrestaciongv") as DropDownList;
                //DropDownList ddlClasificaciongv = gvEscenario.FooterRow.FindControl("ddlClasificaciongv") as DropDownList;

                DropDownList ddlClientegv = (DropDownList)e.Row.FindControl("ddlClientegv");
                DropDownList ddlPrestaciongv = (DropDownList)e.Row.FindControl("ddlPrestaciongv");
                DropDownList ddlClasificaciongv = (DropDownList)e.Row.FindControl("ddlClasificaciongv");
                DropDownList ddlTipoComisiongv = (DropDownList)e.Row.FindControl("ddlTipoComisiongv");

                string Cliente = ((DataRowView)e.Row.DataItem)["Cliente"].ToString();
                string Prestaciones = ((DataRowView)e.Row.DataItem)["Prestacion"].ToString();
                string Clasificacion = ((DataRowView)e.Row.DataItem)["Clasificacion"].ToString();
                string Comision = ((DataRowView)e.Row.DataItem)["TipoComision"].ToString();

                if (dtCliente.Rows.Count > 0)
                {
                    ddlClientegv.DataSource = dtCliente;
                    ddlClientegv.DataTextField = "Nombre_RazonSocial";
                    ddlClientegv.DataValueField = "Id_Cliente";
                    ddlClientegv.DataBind();
                    if (!string.IsNullOrEmpty(Cliente))
                    {
                        ddlClientegv.Items.FindByText(Cliente).Selected = true;               
                    }
                }
                ddlPrestaciongv.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));


                if (dtPrestacion.Rows.Count > 0)
                {
                    ddlPrestaciongv.DataSource = dtPrestacion;
                    ddlPrestaciongv.DataTextField = "Nombre";
                    ddlPrestaciongv.DataValueField = "Id_Factor";
                    ddlPrestaciongv.DataBind();
                    if (!string.IsNullOrEmpty(Prestaciones))
                    {
                        ddlPrestaciongv.Items.FindByText(Prestaciones).Selected = true;                                            
                    }
                }
                ddlPrestaciongv.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));


                if (dtClasificacion.Rows.Count > 0)
                {
                    ddlClasificaciongv.DataSource = dtClasificacion;
                    ddlClasificaciongv.DataTextField = "Descripcion";
                    ddlClasificaciongv.DataValueField = "Id_TpoClasEmp";
                    ddlClasificaciongv.DataBind();
                    
                    if (!string.IsNullOrEmpty(Clasificacion))
                    {
                        ddlClasificaciongv.Items.FindByText(Clasificacion).Selected = true;                      
                    }
                }
                ddlClasificaciongv.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

                if (dtComision.Rows.Count > 0)
                {
                    ddlTipoComisiongv.DataSource = dtComision;
                    ddlTipoComisiongv.DataTextField = "TipoComision";
                    ddlTipoComisiongv.DataValueField = "Id_Comision";
                    ddlTipoComisiongv.DataBind();

                    if (!string.IsNullOrEmpty(Comision))
                    {
                        ddlTipoComisiongv.Items.FindByText(Comision).Selected = true;
                    }
                }
                ddlTipoComisiongv.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

            }
        }

        public void Mensaje(string textomensaje, CuadroMensaje.CuadroMensajeIcono icono)
        {
            CuadroMensaje Mensaje = new CuadroMensaje(textomensaje, "MENSAJE", icono, CuadroMensaje.CuadroMensajeBoton.Ok, CuadroMensaje.CuadroMensajeEstilo.EstiloB);
            Literal ltrMensaje = (Literal)this.Master.FindControl("ltrMensaje");
            ltrMensaje.Text = Mensaje.Mostrar(this);
        }

        protected void ObtenEscenarioPorId()
        {
            DataTable dtEscenarios = new DataTable();

            dtEscenarios = clsQuery.execQueryDataTable("SP_ObtenEscenarios");

            if (dtEscenarios.Rows.Count > 0)
            {
                gvEscenario.DataSource = dtEscenarios;
                gvEscenario.DataBind();
            }
            else
            {
                Mensaje("ADVERTENCIA: No se encontraron resultados", CuadroMensaje.CuadroMensajeIcono.Advertencia);
            }
        }

        protected void ObtenEscenarioPorCliente()
        {
            DataTable dtEscenarios = new DataTable();
            Cliente = txtNombre.Text.ToString();

            dtEscenarios = clsQuery.execQueryDataTable("SP_ObtenEscenarioPorCliente");

            if (dtEscenarios.Rows.Count > 0)
            {
                gvEscenario.DataSource = dtEscenarios;
                gvEscenario.DataBind();
            }
            else
            {
                Mensaje("ADVERTENCIA: No se encontraron resultados", CuadroMensaje.CuadroMensajeIcono.Advertencia);
            }
        }

    }
}