using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using EscenariosQnta.Data;
using EscenariosQnta.Clases;


namespace EscenariosQnta
{
    public partial class wfrmConceptos : System.Web.UI.Page
    {
        #region Variables

        string Id_Conceptos = string.Empty;
        string NomCorto = string.Empty;
        string Descripcion = string.Empty;
        string TipoConcepto = string.Empty;
        string AppTNom = string.Empty;
        string Prioridad = string.Empty;
        string Calculo = string.Empty;
        string IntegraSS = string.Empty;
        string DatoSup = string.Empty;
        string DatoInf = string.Empty;
        string ImpSup = string.Empty;
        string ImpInf = string.Empty;
        string PorcSup = string.Empty;
        string PorInf = string.Empty;
        bool esPrevisionSocial = false;
        string Tipoexc = string.Empty;
        string TopeExc = string.Empty;
        bool AfectaNeto = false;

        string strQuery = string.Empty;
        string RetunValue;
        clsDatos clsQuery = new clsDatos();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenConceptos();
                ObtenTipoConceptos();
                ObtenIntegraSS();
                ObtenTipoExcension();
            }
            else
            {
                txtNomCorto.Attributes["value"] = txtNomCorto.Text;
            }

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                NomCorto = txtNomCorto.Text.ToString();
                Descripcion = txtDescripcion.Text.ToString();
                TipoConcepto = ddlTipoConcepto.SelectedItem.Value.ToString(); //txtTipoConcepto.Text.ToString();
                AppTNom = txtAppTNom.Text.ToString();
                Prioridad = txtPrioridad.Text.ToString();
                Calculo = txtCalculo.Text.ToString();
                IntegraSS = ddlIntegra.SelectedItem.Value.ToString();
                DatoSup = txtDatoSup.Text.ToString();
                DatoInf = txtDatoInfo.Text.ToString();
                ImpSup = txtImpSup.Text.ToString();
                ImpInf = txtImpInf.Text.ToString();
                PorcSup = txtPorcSup.Text.ToString();
                PorInf = txtPorcInf.Text.ToString();

                Tipoexc = ddlTipoExc.SelectedItem.Value.ToString();
                TopeExc = txtTopeExc.Text.ToString();

                esPrevisionSocial = chkPrevision.Checked;

                AfectaNeto = chkAfectaNeto.Checked;

                //esPrevisionSocial = txtesPrevisionSocial.Text.ToString();
                //AfectaNeto = txtAfectaNeto.Text.ToString();

                strQuery = string.Format("dbo.SP_InsertaConceptos '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', {6}, {7}, {8}, {9}, {10}, '{11}', '{12}', {13}, {14}, {15},{16}",
                        NomCorto, Descripcion, TipoConcepto, AppTNom, Prioridad, Calculo, IntegraSS, DatoSup, DatoInf, ImpSup, ImpInf, PorcSup, PorInf, esPrevisionSocial, Tipoexc, TopeExc, AfectaNeto);

                RetunValue = clsQuery.execQueryString(strQuery);

                if (RetunValue == "1")
                {
                    LimpiarControles();
                    ObtenConceptos();
                    Mensaje("GUARDADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
                }
            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
            }
        }

        protected void gvConceptos_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }

            DataTable dtTipoConceptos = new DataTable();
            DataTable dtIntegraSS = new DataTable();
            DataTable dtTipoExc = new DataTable();

            dtTipoConceptos = clsQuery.execQueryDataTable("SP_ObtenTipoConceptos");
            dtIntegraSS = clsQuery.execQueryDataTable("SP_ObtenIntegraSS");
            dtTipoExc = clsQuery.execQueryDataTable("SP_ObtenPeriocidadNomina");

            if (e.Row.RowType == DataControlRowType.DataRow && gvConceptos.EditIndex == e.Row.RowIndex)
            {
                DropDownList ddlTipoConceptogv = (DropDownList)e.Row.FindControl("ddlTipoConceptogv");
                DropDownList ddlIntegraSSgv = (DropDownList)e.Row.FindControl("ddlIntegraSSgv");
                DropDownList ddlTipoExcgv = (DropDownList)e.Row.FindControl("ddlTipoExcgv");

                string TipoConcepto = ((DataRowView)e.Row.DataItem)["TipoConcepto"].ToString();
                string IntegraSS = ((DataRowView)e.Row.DataItem)["IntegraSS"].ToString();
                string TipoExc = ((DataRowView)e.Row.DataItem)["Tipoexc"].ToString();

                if (dtTipoConceptos.Rows.Count > 0)
                {
                    ddlTipoConceptogv.DataSource = dtTipoConceptos;
                    ddlTipoConceptogv.DataTextField = "TipoConcepto";
                    ddlTipoConceptogv.DataValueField = "Id_TipoConcepto";
                    ddlTipoConceptogv.DataBind();

                    if (!string.IsNullOrEmpty(TipoConcepto))
                    {
                        ddlTipoConceptogv.Items.FindByText(TipoConcepto).Selected = true;
                    }
                }
                ddlTipoConceptogv.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

                if (dtIntegraSS.Rows.Count > 0)
                {
                    ddlIntegraSSgv.DataSource = dtIntegraSS;
                    ddlIntegraSSgv.DataTextField = "IntegraSS";
                    ddlIntegraSSgv.DataValueField = "Id_IntegraSS";
                    ddlIntegraSSgv.DataBind();

                    if (!string.IsNullOrEmpty(IntegraSS))
                    {
                        ddlIntegraSSgv.Items.FindByText(IntegraSS).Selected = true;
                    }
                }
                ddlIntegraSSgv.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
                
                if (dtTipoExc.Rows.Count > 0)
                {
                    ddlTipoExcgv.DataSource = dtTipoExc;
                    ddlTipoExcgv.DataTextField = "Nombre";
                    ddlTipoExcgv.DataValueField = "Id_Periodo";
                    ddlTipoExcgv.DataBind();

                    if (!string.IsNullOrEmpty(TipoExc))
                    {
                        ddlTipoExcgv.Items.FindByText(TipoExc).Selected = true;
                    }
                }
                ddlTipoExcgv.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

            }

        }

        protected void gvConceptos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvConceptos.EditIndex = e.NewEditIndex;
            ObtenConceptos();

        }

        protected void gvConceptos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Id_Conceptos = ((Label)gvConceptos.Rows[e.RowIndex].FindControl("lbId_Conceptos")).Text;
                NomCorto = ((TextBox)gvConceptos.Rows[e.RowIndex].FindControl("txtNomCorto")).Text;
                Descripcion = ((TextBox)gvConceptos.Rows[e.RowIndex].FindControl("txtDescripcion")).Text;
                TipoConcepto = ((DropDownList)gvConceptos.Rows[e.RowIndex].FindControl("ddlTipoConceptogv")).Text;
                AppTNom = ((TextBox)gvConceptos.Rows[e.RowIndex].FindControl("txtAppTNom")).Text;
                Prioridad = ((TextBox)gvConceptos.Rows[e.RowIndex].FindControl("txtPrioridad")).Text;
                Calculo = ((TextBox)gvConceptos.Rows[e.RowIndex].FindControl("txtCalculo")).Text;
                IntegraSS = ((DropDownList)gvConceptos.Rows[e.RowIndex].FindControl("ddlIntegraSSgv")).Text;
                DatoSup = ((TextBox)gvConceptos.Rows[e.RowIndex].FindControl("txtDatoSup")).Text;
                DatoInf = ((TextBox)gvConceptos.Rows[e.RowIndex].FindControl("txtDatoInf")).Text;
                ImpSup = ((TextBox)gvConceptos.Rows[e.RowIndex].FindControl("txtImpSup")).Text;
                ImpInf = ((TextBox)gvConceptos.Rows[e.RowIndex].FindControl("txtImpInf")).Text.Replace(",", ".");
                PorcSup = ((TextBox)gvConceptos.Rows[e.RowIndex].FindControl("txtPorcSup")).Text.Replace(",", ".");
                PorInf = ((TextBox)gvConceptos.Rows[e.RowIndex].FindControl("txtPorInf")).Text.Replace(",", ".");
                esPrevisionSocial = ((CheckBox)gvConceptos.Rows[e.RowIndex].FindControl("chkesPrevisionSocial")).Checked;
                Tipoexc = ((DropDownList)gvConceptos.Rows[e.RowIndex].FindControl("ddlTipoExcgv")).Text;
                TopeExc = ((TextBox)gvConceptos.Rows[e.RowIndex].FindControl("txtTopeExc")).Text;
                AfectaNeto = ((CheckBox)gvConceptos.Rows[e.RowIndex].FindControl("chkAfectaNeto")).Checked;

                strQuery = string.Format("dbo.SP_ActualizaConceptos {0}, '{1}', '{2}', '{3}', '{4}', {5}, '{6}', {7}, {8}, {9}, {10}, '{11}', '{12}', '{13}', {14}, {15}, {16}, {17}",
                        Id_Conceptos, NomCorto, Descripcion, TipoConcepto, AppTNom, Prioridad, Calculo, IntegraSS, DatoSup, DatoInf, ImpSup, ImpInf, PorcSup, PorInf, esPrevisionSocial, Tipoexc, TopeExc, AfectaNeto);

                RetunValue = clsQuery.execQueryString(strQuery);

                if (RetunValue == "1")
                {
                    gvConceptos.EditIndex = -1;
                    ObtenConceptos();
                    Mensaje("ACTUALIZADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
                }
            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
            }
        }

        protected void gvConceptos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvConceptos.EditIndex = -1;
            ObtenConceptos();
        }

        protected void ObtenConceptos()
        {

            DataTable dtConceptos = new DataTable();

            dtConceptos = clsQuery.execQueryDataTable("SP_ObtenConceptos");

            if (dtConceptos.Rows.Count > 0)
            {
                gvConceptos.DataSource = dtConceptos;
                gvConceptos.DataBind();
            }
        }
         
        protected void ObtenTipoConceptos()
        {

            DataTable dtTipoConcepto = new DataTable();

            dtTipoConcepto = clsQuery.execQueryDataTable("SP_ObtenTipoConceptos");

            if (dtTipoConcepto.Rows.Count > 0)
            {
                ddlTipoConcepto.DataSource = dtTipoConcepto;
                ddlTipoConcepto.DataTextField = "TipoConcepto";
                ddlTipoConcepto.DataValueField = "Id_TipoConcepto";
                ddlTipoConcepto.DataBind();
            }

            ddlTipoConcepto.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
        }

        protected void ObtenIntegraSS()
        {

            DataTable dtIntegraSS = new DataTable();

            dtIntegraSS = clsQuery.execQueryDataTable("SP_ObtenIntegraSS");

            if (dtIntegraSS.Rows.Count > 0)
            {
                ddlIntegra.DataSource = dtIntegraSS;
                ddlIntegra.DataTextField = "IntegraSS";
                ddlIntegra.DataValueField = "Id_IntegraSS";
                ddlIntegra.DataBind();
            }

            ddlIntegra.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
        }

        protected void ObtenTipoExcension()
        {

            DataTable dtTipoExc = new DataTable();

            dtTipoExc = clsQuery.execQueryDataTable("SP_ObtenPeriocidadNomina");

            if (dtTipoExc.Rows.Count > 0)
            {
                ddlTipoExc.DataSource = dtTipoExc;
                ddlTipoExc.DataTextField = "Nombre";
                ddlTipoExc.DataValueField = "Id_Periodo";
                ddlTipoExc.DataBind();
            }

            ddlTipoExc.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
        }

        public void Mensaje(string textomensaje, CuadroMensaje.CuadroMensajeIcono icono)
        {
            CuadroMensaje Mensaje = new CuadroMensaje(textomensaje, "MENSAJE", icono, CuadroMensaje.CuadroMensajeBoton.Ok, CuadroMensaje.CuadroMensajeEstilo.EstiloB);
            Literal ltrMensaje = (Literal)this.Master.FindControl("ltrMensaje");
            ltrMensaje.Text = Mensaje.Mostrar(this);
        }

        protected void btnAddFuncion_Click(object sender, EventArgs e)
        {

            if (IsPostBack)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('PopUpFunciones.aspx','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=500,height=500');", true);
            }
        }

        protected void txtTopeExc_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTopeExc.Text))
            { ddlTipoExc.Enabled = true; }
        }

        protected void LimpiarControles()
        {
            txtNomCorto.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtAppTNom.Text = string.Empty;
            txtPrioridad.Text = string.Empty;
            txtCalculo.Text = string.Empty;
            txtDatoSup.Text = "0";
            txtDatoInfo.Text = "0";
            txtImpSup.Text = "0";
            txtImpInf.Text = "0";
            txtPorcSup.Text = "0";
            txtPorcInf.Text = "0";
            txtTopeExc.Text = "0";

            ObtenTipoConceptos();
            ObtenIntegraSS();
            ObtenTipoExcension();

            chkPrevision.Checked = false;
            chkAfectaNeto.Checked = false;

        }

    }
}