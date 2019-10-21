using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EscenariosQnta.Data;
using System.Data;

namespace EscenariosQnta
{
    public partial class wrfmDatosAreaComercial : System.Web.UI.Page
    {

        clsDatos clsQuery = new clsDatos();

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
                string Nombre = string.Empty;
                string ApellidoPaterno = string.Empty;
                string ApellidoMaterno = string.Empty;
                string Telefono = string.Empty;
                string Correo = string.Empty;
                string Notas = string.Empty;
                string strValue = string.Empty;
                int TipoAreaComercial = 0;
 
                Nombre = txtNombre.Text.ToString();
                ApellidoPaterno = txtApellidoPaterno.Text.ToString();
                ApellidoMaterno = txtApellidoMaterno.Text.ToString();
                Telefono = txtTelefono.Text.ToString();
                Correo = txtCorreo.Text.ToString();
                Notas = txtNotas.Text.ToString();
                TipoAreaComercial = int.Parse(ddlTipoArea.SelectedItem.Value.ToString());


                strValue = clsQuery.execQueryString("SP_InsertaAreaComercial '" + Nombre + "','" + ApellidoPaterno + "','" + ApellidoMaterno + "','" + Telefono + "','" + Correo + "','" + Notas + "'," + TipoAreaComercial);

               if (strValue == "1")
               {
                   ObtenAreaComercial();
                   LimpiarControles();
                   Response.Write("<script>alert('Guardado');</script>");
               }
            }
            catch (Exception ex)
            {
                MensageError(ex.ToString());
            }
        }

        protected void ObtenAreaComercial()
        {            
            DataTable dtEjecutivo = new DataTable();

            dtEjecutivo = clsQuery.execQueryDataTable("SP_ObtenAreaComercial");

            if (dtEjecutivo.Rows.Count > 0)
            {
                gvEjecutivos.DataSource = dtEjecutivo;
                gvEjecutivos.DataBind();
            }
            
        }

        protected void gvEjecutivos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEjecutivos.EditIndex = -1;
            ObtenAreaComercial();
        }

        protected void gvEjecutivos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEjecutivos.EditIndex = e.NewEditIndex;
            ObtenAreaComercial();
        }

        protected void gvEjecutivos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                string ID = ((Label)gvEjecutivos.Rows[e.RowIndex]
                                 .FindControl("lblId_EjecComer")).Text;
                string Nombre = ((TextBox)gvEjecutivos.Rows[e.RowIndex]
                                    .FindControl("txtNombre")).Text;
                string ApelldioPaterno = ((TextBox)gvEjecutivos.Rows[e.RowIndex]
                                   .FindControl("txtApellidoPaterno")).Text;
                string ApellidoMaterno = ((TextBox)gvEjecutivos.Rows[e.RowIndex]
                                    .FindControl("txtApellidoMaterno")).Text;
                string Telefono = ((TextBox)gvEjecutivos.Rows[e.RowIndex]
                                    .FindControl("txtTelefono")).Text;
                string Correo = ((TextBox)gvEjecutivos.Rows[e.RowIndex]
                                   .FindControl("txtCorreo")).Text;
                string Notas = ((TextBox)gvEjecutivos.Rows[e.RowIndex]
                                    .FindControl("txtNotas")).Text;
                bool Activo = ((CheckBox)gvEjecutivos.Rows[e.RowIndex]
                                    .FindControl("chkActivo")).Checked;
                string TipoAreaComercial = ((DropDownList)gvEjecutivos.Rows[e.RowIndex]
                                    .FindControl("ddlTipoAreagv")).Text;

                string strValue = string.Empty;

                strValue = clsQuery.execQueryString("SP_ActualizaAreaComercial " + ID + ",'" + Nombre + "','" + ApelldioPaterno + "','" + ApellidoMaterno + "','" + Telefono + "','" + Correo + "','" + Notas + "'," + Activo + "," + TipoAreaComercial);

                if (strValue == "1")
                {
                    gvEjecutivos.EditIndex = -1;
                    ObtenAreaComercial();
                    Response.Write("<script>alert('Actualizado');</script>");
                }
            }
            catch (Exception ex)
            {
                MensageError(ex.ToString());
            }
        }

        protected void LimpiarControles()
        {
            txtNombre.Text = "";
            txtApellidoPaterno.Text = "";
            txtApellidoMaterno.Text = "";
            txtTelefono.Text = "";
            txtCorreo.Text = "";
            txtNotas.Text = "";
        }

        public void MensageError(string Mensaje)
        {
            Response.Write("<script>alert('Hubo un Error en el siatemapor favor contacte a Soporte: " + Mensaje + "');</script>");
        }

        protected void gvEjecutivos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ObtenAreaComercial();
            gvEjecutivos.PageIndex = e.NewPageIndex;
            gvEjecutivos.DataBind();
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

            ddlTipoArea.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "0"));
        }

        protected void gvEjecutivos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable dtTipoArea = new DataTable();

            dtTipoArea = clsQuery.execQueryDataTable("SP_ObtenTipoAreaComercial");

            if (e.Row.RowType == DataControlRowType.DataRow && gvEjecutivos.EditIndex == e.Row.RowIndex)
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
    }
}