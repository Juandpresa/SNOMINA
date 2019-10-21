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
    public partial class wfrmPropuesta : System.Web.UI.Page
    {
        #region Variables

        int Id_Escenario = 0;
        string Id_Propuesta = string.Empty;
        string Folio = string.Empty;
        string Cliente = string.Empty;
        string Prestaciones = string.Empty;
        string Nomina = string.Empty;
        string Asimilados = string.Empty;
        string Honorarios = string.Empty;
        string OtrosProductos = string.Empty;
        string Clasificacion = string.Empty;
        string SueldoIni = string.Empty;
        string SueldoFin = string.Empty;
        string Nota = string.Empty;

        string strQuery = string.Empty;
        string strReturnVlaue = string.Empty;

        clsDatos clsQuery = new clsDatos();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenClientes();
                ObtenTipoArea();
                ObtenPrestacion();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                //Folio = txtFolio.Text.ToString();
                Cliente = ddlCliente.SelectedItem.Value;
                Prestaciones = ddlPrestacion.SelectedItem.Value;
                Nomina = txtNomina.Text.ToString();
                Asimilados = txtAsimilados.Text.ToString();
                Honorarios = txtHonorarios.Text.ToString();
                OtrosProductos = txtOtrosProductos.Text.ToString();
                Clasificacion = ddlClasificacion.SelectedItem.Value;
                SueldoIni = txtRangoSueldoIni.Text.ToString();
                SueldoFin = txtRangoSueldoFin.Text.ToString();
                Nota = txtNotas.Text.ToString();

                strQuery = string.Format("SP_InsertaEscenario {0}, {1}, {2}, '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}'", Cliente, Prestaciones, Clasificacion, Nomina, Asimilados, Honorarios, OtrosProductos, SueldoIni, SueldoFin, Nota);

                Session["Id_Escenario"] = clsQuery.execQueryInt(strQuery);
                Id_Escenario = int.Parse(Session["Id_Escenario"].ToString());


                if (Id_Escenario != 0)
                {
                    ObtenEscenarioByID(Id_Escenario);
                    txtFolio.Text = Session["Id_Escenario"].ToString();
                    BloquearControles();
                    btnNueva.Visible = true;
                    btnGuardar.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
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

        private void ObtenTipoArea()
        {
            try
            {
                DataTable dtTipoArea = new DataTable();

                dtTipoArea = clsQuery.execQueryDataTable("SP_ObtenTipoAreaComercial");

                if (dtTipoArea.Rows.Count > 0)
                {
                    ddlClasificacion.DataSource = dtTipoArea;
                    ddlClasificacion.DataTextField = "Descripcion";
                    ddlClasificacion.DataValueField = "Id";
                    ddlClasificacion.DataBind();
                }

                ddlClasificacion.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
            }
        }

        protected void ObtenPrestacion()
        {
            try
            {
                DataTable dtPrestacion = new DataTable();

                dtPrestacion = clsQuery.execQueryDataTable("SP_ObtenPrestaciones");

                if (dtPrestacion.Rows.Count > 0)
                {
                    ddlPrestacion.DataSource = dtPrestacion;
                    ddlPrestacion.DataTextField = "Nombre";
                    ddlPrestacion.DataValueField = "Id_Prest";
                    ddlPrestacion.DataBind();
                }

                ddlPrestacion.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);

            }
        }

        protected void BloquearControles()
        {
            txtFolio.Enabled = false;
            ddlCliente.Enabled = false;
            ddlPrestacion.Enabled = false;
            txtNomina.Enabled = false;
            txtAsimilados.Enabled = false;
            txtHonorarios.Enabled = false;
            txtOtrosProductos.Enabled = false;
            ddlClasificacion.Enabled = false;
            txtRangoSueldoIni.Enabled = false;
            txtRangoSueldoFin.Enabled = false;
            txtNotas.Enabled = false;

            //txtFolio.Text = "0";
            //ddlCliente.SelectedItem.Value = "";
            //ddlPrestacion.SelectedItem.Value = "";
            //txtNomina.Text = "";
            //txtAsimilados.Text = "";
            //txtHonorarios.Text = "";
            //txtOtrosProductos.Text = "";
            //ddlClasificacion.SelectedItem.Value = "";
            //txtRangoSueldoIni.Text = "";
            //txtRangoSueldoFin.Text = "";
            //txtNotas.Text = "";

        }

        protected void DesbloquearControles()
        {

            ddlCliente.Enabled = true;
            ddlPrestacion.Enabled = true;
            txtNomina.Enabled = true;
            txtAsimilados.Enabled = true;
            txtHonorarios.Enabled = true;
            txtOtrosProductos.Enabled = true;
            ddlClasificacion.Enabled = true;
            txtRangoSueldoIni.Enabled = true;
            txtRangoSueldoFin.Enabled = true;
            txtNotas.Enabled = true;

            btnGuardar.Visible = true;
            btnNueva.Visible = false;

        }

        protected void LimpiarControles()
        {
            txtFolio.Text = "0";
            ddlCliente.SelectedValue = "0";
            ddlPrestacion.SelectedValue = "0";
            txtNomina.Text = string.Empty;
            txtAsimilados.Text = string.Empty;
            txtHonorarios.Text = string.Empty;
            txtOtrosProductos.Text = string.Empty;
            ddlClasificacion.SelectedValue = "0";
            txtRangoSueldoIni.Text = string.Empty;
            txtRangoSueldoFin.Text = string.Empty;
            txtNotas.Text = string.Empty;
        }

        protected void ObtenEscenarios()
        {
            try
            {
                DataTable dtEscenarios = new DataTable();

                dtEscenarios = clsQuery.execQueryDataTable("");

            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
            }

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Id_Escenario = int.Parse(Session["Id_Escenario"].ToString());
            Cliente = ((DropDownList)gvEscenario.FooterRow.FindControl("ddlClientegv")).Text;
            Prestaciones = ((DropDownList)gvEscenario.FooterRow.FindControl("ddlPrestaciongv")).Text;
            Nomina = ((TextBox)gvEscenario.FooterRow.FindControl("txtPorcNomina")).Text;
            Asimilados = ((TextBox)gvEscenario.FooterRow.FindControl("txtPorcAsimilados")).Text;
            Honorarios = ((TextBox)gvEscenario.FooterRow.FindControl("txtPorcHonorarios")).Text;
            OtrosProductos = ((TextBox)gvEscenario.FooterRow.FindControl("txtPorOtrosProductoss")).Text;
            Clasificacion = ((DropDownList)gvEscenario.FooterRow.FindControl("ddlClasificaciongv")).Text;
            SueldoIni = ((TextBox)gvEscenario.FooterRow.FindControl("txtRangoSueldoIni")).Text;
            SueldoFin = ((TextBox)gvEscenario.FooterRow.FindControl("txtRangoSueldoFin")).Text;
            Nota = ((TextBox)gvEscenario.FooterRow.FindControl("txtNota")).Text;

            strQuery = string.Format("SP_InsertaEscenarioPropuesta {0}, {1}, {2}, '{3}', '{4}', '{5}', '{6}', {7}, '{8}', '{9}', '{10}'",
                Id_Escenario, Cliente, Prestaciones, Nomina, Asimilados, Honorarios, OtrosProductos, Clasificacion, SueldoIni, SueldoFin, Nota);

            strReturnVlaue = clsQuery.execQueryString(strQuery);

            if (strReturnVlaue == "1")
            {
                gvEscenario.EditIndex = -1;
                ObtenEscenarioByID(int.Parse(Session["Id_Escenario"].ToString()));
                Mensaje("AGREGADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
            }
        }

        protected void btnNueva_Click(object sender, EventArgs e)
        {
            DesbloquearControles();
            LimpiarControles();
        }

        protected void ObtenEscenarioByID(int Id_Escenario)
        {
            try
            {
                DataTable dtEscenarioById = new DataTable();

                strQuery = string.Format("SP_ObtenEscenarioPorID {0}", Id_Escenario);

                dtEscenarioById = clsQuery.execQueryDataTable(strQuery);

                if (dtEscenarioById.Rows.Count > 0)
                {
                    gvEscenario.DataSource = dtEscenarioById;
                    gvEscenario.DataBind();
                }


            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
            }

        }

        protected void gvEscenario_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEscenario.EditIndex = -1;
        }

        protected void gvEscenario_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvEscenario_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEscenario.EditIndex = e.NewEditIndex;
            ObtenEscenarioByID(int.Parse(Session["Id_Escenario"].ToString()));
        }

        protected void gvEscenario_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Id_Escenario = int.Parse(((Label)gvEscenario.FooterRow.FindControl("lbId_Escenario")).Text);
                Id_Propuesta = ((Label)gvEscenario.FooterRow.FindControl("lblId_Propuesta")).Text;
                Cliente = ((DropDownList)gvEscenario.FooterRow.FindControl("ddlClientegv")).Text;
                Prestaciones = ((DropDownList)gvEscenario.FooterRow.FindControl("ddlPrestaciongv")).Text;
                Nomina = ((TextBox)gvEscenario.FooterRow.FindControl("txtPorcNomina")).Text;
                Asimilados = ((TextBox)gvEscenario.FooterRow.FindControl("txtPorcAsimilados")).Text;
                Honorarios = ((TextBox)gvEscenario.FooterRow.FindControl("txtPorcHonorarios")).Text;
                OtrosProductos = ((TextBox)gvEscenario.FooterRow.FindControl("txtPorOtrosProductoss")).Text;
                Clasificacion = ((DropDownList)gvEscenario.FooterRow.FindControl("ddlClasificaciongv")).Text;
                SueldoIni = ((TextBox)gvEscenario.FooterRow.FindControl("txtRangoSueldoIni")).Text;
                SueldoFin = ((TextBox)gvEscenario.FooterRow.FindControl("txtRangoSueldoFin")).Text;
                Nota = ((TextBox)gvEscenario.FooterRow.FindControl("txtNota")).Text;

                strQuery = string.Format("SP_ActulizaEscenarioPropuesta {0}, {1}, {2}, {3}, '{4}', '{5}', '{6}', {7}, '{8}', '{9}', '{10}', '{11}', {12}",
             Id_Escenario, Id_Propuesta, Cliente, Prestaciones, Nomina, Asimilados, Honorarios, OtrosProductos, Clasificacion, SueldoIni, SueldoFin, Nota, 0);

                if (strReturnVlaue == "1")
                {
                    gvEscenario.EditIndex = -1;
                    ObtenEscenarioByID(int.Parse(Session["Id_Escenario"].ToString()));                    
                    Mensaje("AGREGADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
                } 

            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
            }
        }

        protected void gvEscenario_DataBound(object sender, EventArgs e)
        {
            DropDownList ddlClientegv = gvEscenario.FooterRow.FindControl("ddlClientegv") as DropDownList;
            DropDownList ddlPrestaciongv = gvEscenario.FooterRow.FindControl("ddlPrestaciongv") as DropDownList;
            DropDownList ddlClasificaciongv = gvEscenario.FooterRow.FindControl("ddlClasificaciongv") as DropDownList;

            DataTable dtCliente = new DataTable();
            DataTable dtPrestacion = new DataTable();
            DataTable dtTipoArea = new DataTable();

            dtCliente = clsQuery.execQueryDataTable("SP_ObtenClientes");

            if (dtCliente.Rows.Count > 0)
            {
                ddlClientegv.DataSource = dtCliente;
                ddlClientegv.DataTextField = "Nombre_RazonSocial";
                ddlClientegv.DataValueField = "Id_Cliente";
                ddlClientegv.DataBind();
            }

            dtPrestacion = clsQuery.execQueryDataTable("SP_ObtenPrestaciones");

            if (dtPrestacion.Rows.Count > 0)
            {
                ddlPrestaciongv.DataSource = dtPrestacion;
                ddlPrestaciongv.DataTextField = "Nombre";
                ddlPrestaciongv.DataValueField = "Id_Prest";
                ddlPrestaciongv.DataBind();
            }

            dtTipoArea = clsQuery.execQueryDataTable("SP_ObtenTipoAreaComercial");

            if (dtTipoArea.Rows.Count > 0)
            {
                ddlClasificaciongv.DataSource = dtTipoArea;
                ddlClasificaciongv.DataTextField = "Descripcion";
                ddlClasificaciongv.DataValueField = "Id";
                ddlClasificaciongv.DataBind();
            }
        }

        public void Mensaje(string textomensaje, CuadroMensaje.CuadroMensajeIcono icono)
        {
            CuadroMensaje Mensaje = new CuadroMensaje(textomensaje, "MENSAJE", icono, CuadroMensaje.CuadroMensajeBoton.Ok, CuadroMensaje.CuadroMensajeEstilo.EstiloB);
            Literal ltrMensaje = (Literal)this.Master.FindControl("ltrMensaje");
            ltrMensaje.Text = Mensaje.Mostrar(this);
        }

    }
}