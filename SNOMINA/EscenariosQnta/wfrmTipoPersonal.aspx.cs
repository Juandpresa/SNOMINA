using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EscenariosQnta.Data;

namespace EscenariosQnta.Data
{
    public partial class wfrmTipoPersonal : System.Web.UI.Page
    {
        clsDatos clsQuery = new clsDatos();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenTipoPersonal();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string Nombre = string.Empty;
                string strValue = string.Empty;

                Nombre = txtNombre.Text.ToString();


                strValue = clsQuery.execQueryString("SP_InsertaTipoPersonal '" + Nombre + "'");

                if (strValue == "1")
                {
                    ObtenTipoPersonal();
                    LimpiarControles();
                    Response.Write("<script>alert('Guardado');</script>");
                }
            }
            catch (Exception ex)
            {
                MensageError(ex.ToString());
            }
        }      

        protected void gvTipoPersonal_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvTipoPersonal.EditIndex = -1;
        }

        protected void gvTipoPersonal_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvTipoPersonal.EditIndex = e.NewEditIndex;
        }

        protected void gvTipoPersonal_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                string ID = ((Label)gvTipoPersonal.Rows[e.RowIndex]
                                 .FindControl("lblId_TpoPersonal")).Text;
                string Nombre = ((TextBox)gvTipoPersonal.Rows[e.RowIndex]
                                    .FindControl("txtNombre")).Text;
                

                string strValue = string.Empty;

                strValue = clsQuery.execQueryString("SP_ActualizaTipoPersonal " + ID + ",'" + Nombre + "'");

                if (strValue == "1")
                {
                    gvTipoPersonal.EditIndex = -1;
                    ObtenTipoPersonal();
                    Response.Write("<script>alert('Actualizado');</script>");
                }
            }
            catch (Exception ex)
            {
                MensageError(ex.ToString());
            }
        }

        protected void ObtenTipoPersonal()
        {
            DataTable dtTipoPersonal = new DataTable();

            dtTipoPersonal = clsQuery.execQueryDataTable("SP_ObtenTipoPersonal");

            if (dtTipoPersonal.Rows.Count > 0)
            {
                gvTipoPersonal.DataSource = dtTipoPersonal;
                gvTipoPersonal.DataBind();
            }
        }

        public void MensageError(string Mensaje)
        {
            Response.Write("<script>alert('Hubo un Error en el siatemapor favor contacte a Soporte: " + Mensaje + "');</script>");
        }

        protected void LimpiarControles()
        {
            txtNombre.Text = "";
        }
    }
}