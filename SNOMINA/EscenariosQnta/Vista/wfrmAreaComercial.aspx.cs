using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EscenariosQnta.Data;
using EscenariosQnta.Clases;
using System.Web.Services;
using System.ComponentModel;

namespace EscenariosQnta
{
    public partial class wrfmDatosAreaComercial : System.Web.UI.Page
    {
        #region Variables

        string ID = string.Empty;
        string Nombre = string.Empty;
        string ApellidoPaterno = string.Empty;
        string ApellidoMaterno = string.Empty;
        string Telefono = string.Empty;
        string Correo = string.Empty;
        string Notas = string.Empty;
        bool Activo = false;
        string TipoAreaComercial = string.Empty;

        string strQuery = string.Empty;
        string strValue = string.Empty;

        clsDatos clsQuery = new clsDatos();
        //MaskedTextProvider MaskPhone = new MaskedTextProvider("(000) 000-0000)");

        #endregion
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenAreaComercial();
                ObtenTipoArea();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {

                if (ddlTipoArea.SelectedItem.Value != "-1")
                {
                   
                    Nombre = txtNombre.Text.ToString();
                    ApellidoPaterno = txtApellidoPaterno.Text.ToString();
                    ApellidoMaterno = txtApellidoMaterno.Text.ToString();
                    Telefono = txtTelefono.Text.ToString();
                    Correo = txtCorreo.Text.ToString();
                    Notas = txtNotas.Text.ToString();
                    TipoAreaComercial = ddlTipoArea.SelectedItem.Value.ToString();

                    strQuery = string.Format("SP_InsertaAreaComercial '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', {6}", Nombre, ApellidoPaterno, ApellidoMaterno, Telefono, Correo, Notas, TipoAreaComercial);
                    strValue = clsQuery.execQueryString(strQuery);

                    if (strValue == "1")
                    {
                        ObtenAreaComercial();
                        LimpiarControles();
                        Mensaje("GUARDADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
                    }
                }
                else
                {
                    Mensaje("Seleccione un Tipo de Area" , CuadroMensaje.CuadroMensajeIcono.Advertencia);
                }
            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);

            }
        }

        protected void ObtenAreaComercial()
        {
            DataTable dtEjecutivo = new DataTable();

            dtEjecutivo = clsQuery.execQueryDataTable("SP_ObtenAreaComercial");

            if (dtEjecutivo.Rows.Count > 0)
            {
                gvDatosAreaComercial.DataSource = dtEjecutivo;
                gvDatosAreaComercial.DataBind();               
            }

        }

        protected void LimpiarControles()
        {
            txtNombre.Text = string.Empty;
            txtApellidoPaterno.Text = string.Empty;
            txtApellidoMaterno.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            txtNotas.Text = string.Empty;
        }

        private void ObtenTipoArea()
        {

            DataTable dtTipoArea = new DataTable();

            dtTipoArea = clsQuery.execQueryDataTable("SP_ObtenTipoAreaComercial");

            if (dtTipoArea.Rows.Count > 0)
            {
                ddlTipoArea.DataSource = dtTipoArea;
                ddlTipoArea.DataTextField = "Descripcion";
                ddlTipoArea.DataValueField = "Id";
                ddlTipoArea.DataBind();
            }

            ddlTipoArea.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
        }        

        protected void gvDatosAreaComercial_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ObtenAreaComercial();
            gvDatosAreaComercial.PageIndex = e.NewPageIndex;
            gvDatosAreaComercial.DataBind();
        }
       
        protected void gvDatosAreaComercial_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDatosAreaComercial.EditIndex = -1;
            ObtenAreaComercial();
        }

        protected void gvDatosAreaComercial_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDatosAreaComercial.EditIndex = e.NewEditIndex;
            ObtenAreaComercial();
        }

        protected void gvDatosAreaComercial_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                 ID = ((Label)gvDatosAreaComercial.Rows[e.RowIndex].FindControl("lblId_EjecComer")).Text;
                 Nombre = ((TextBox)gvDatosAreaComercial.Rows[e.RowIndex].FindControl("txtNombre")).Text;
                 ApellidoPaterno = ((TextBox)gvDatosAreaComercial.Rows[e.RowIndex].FindControl("txtApellidoPaterno")).Text;
                 ApellidoMaterno = ((TextBox)gvDatosAreaComercial.Rows[e.RowIndex].FindControl("txtApellidoMaterno")).Text;
                 Telefono = ((TextBox)gvDatosAreaComercial.Rows[e.RowIndex].FindControl("txtTelefono")).Text;
                 Correo = ((TextBox)gvDatosAreaComercial.Rows[e.RowIndex].FindControl("txtCorreo")).Text;
                 Notas = ((TextBox)gvDatosAreaComercial.Rows[e.RowIndex].FindControl("txtNotas")).Text;
                 Activo = ((CheckBox)gvDatosAreaComercial.Rows[e.RowIndex].FindControl("chkActivo")).Checked;
                 TipoAreaComercial = ((DropDownList)gvDatosAreaComercial.Rows[e.RowIndex].FindControl("ddlTipoAreagv")).Text;

                strQuery = string.Format("SP_ActualizaAreaComercial {0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}, {8}", ID, Nombre, ApellidoPaterno, ApellidoMaterno, Telefono, Correo, Notas, Activo, TipoAreaComercial);
                strValue = clsQuery.execQueryString(strQuery);

                if (strValue == "1")
                {
                    gvDatosAreaComercial.EditIndex = -1;
                    ObtenAreaComercial();
                    Mensaje("ACTUALIZADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
                }
            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);

            }
        }

        protected void gvDatosAreaComercial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable dtTipoArea = new DataTable();

            dtTipoArea = clsQuery.execQueryDataTable("SP_ObtenTipoAreaComercial");

            if (e.Row.RowType == DataControlRowType.DataRow && gvDatosAreaComercial.EditIndex == e.Row.RowIndex)
            {
                DropDownList ddlAreaComercial = (DropDownList)e.Row.FindControl("ddlTipoAreagv");

                string TipoArea = ((DataRowView)e.Row.DataItem)["TipoArea"].ToString();

                ddlAreaComercial.DataSource = dtTipoArea;
                ddlAreaComercial.DataTextField = "Descripcion";
                ddlAreaComercial.DataValueField = "Id";
                ddlAreaComercial.DataBind();
                ddlAreaComercial.Items.FindByText(TipoArea).Selected = true;
            }
        }

        [WebMethod]
        public void Mensaje(string textomensaje, CuadroMensaje.CuadroMensajeIcono icono)
        {
            CuadroMensaje Mensaje = new CuadroMensaje(textomensaje, "MENSAJE", icono, CuadroMensaje.CuadroMensajeBoton.Ok, CuadroMensaje.CuadroMensajeEstilo.EstiloB);
            Literal ltrMensaje = (Literal)this.Master.FindControl("ltrMensaje");
            ltrMensaje.Text = Mensaje.Mostrar(this);
        }
    }
}