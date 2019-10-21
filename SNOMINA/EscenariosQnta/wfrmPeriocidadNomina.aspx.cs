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
    public partial class wfrmPeriocidadNomina : System.Web.UI.Page
    {

        clsDatos clsQuery = new clsDatos();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenPeriocidadNomina();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidaClave() == 0)
                {
                    string Clave = string.Empty;
                    string Nombre = string.Empty;
                    string strValue = string.Empty;

                    Clave = txtClave.Text.ToString();
                    Nombre = txtNombre.Text.ToString();

                    strValue = clsQuery.execQueryString("SP_InsertaPeriocidadNomina '" + Clave + "','" + Nombre + "'");

                    if (strValue == "1")
                    {
                        ObtenPeriocidadNomina();
                        LimpiarControles();
                        Response.Write("<script>alert('Guardado');</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('La Clave debe ser diferente');</script>");
                }
            }
            catch (Exception ex)
            {
                MensageError(ex.ToString());
            }
        }

        protected void gvPeriocidadNomina_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPeriocidadNomina.EditIndex = -1;
        }

        protected void gvPeriocidadNomina_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvPeriocidadNomina.EditIndex = e.NewEditIndex;
        }

        protected void gvPeriocidadNomina_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                string ID = ((Label)gvPeriocidadNomina.Rows[e.RowIndex]
                                 .FindControl("lblId_Periodo")).Text;
                string Clave = ((TextBox)gvPeriocidadNomina.Rows[e.RowIndex]
                                    .FindControl("txtClave")).Text;
                string Nombre = ((TextBox)gvPeriocidadNomina.Rows[e.RowIndex]
                                    .FindControl("txtNombre")).Text;


                string strValue = string.Empty;

                strValue = clsQuery.execQueryString("SP_ActualizaPeriocidadNomina " + ID + ",'" + Clave + "','" + Nombre + "'");

                if (strValue == "1")
                {
                    gvPeriocidadNomina.EditIndex = -1;
                    ObtenPeriocidadNomina();
                    Response.Write("<script>alert('Actualizado');</script>");
                }
            }
            catch (Exception ex)
            {
                MensageError(ex.ToString());
            }
        }

        protected void ObtenPeriocidadNomina()
        {
            DataTable dtPrimaRiesgo = new DataTable();

            dtPrimaRiesgo = clsQuery.execQueryDataTable("SP_ObtenPeriocidadNomina");

            if (dtPrimaRiesgo.Rows.Count > 0)
            {
                gvPeriocidadNomina.DataSource = dtPrimaRiesgo;
                gvPeriocidadNomina.DataBind();
            }
        }

        public void MensageError(string Mensaje)
        {
            Response.Write("<script>alert('Hubo un Error en el siatemapor favor contacte a Soporte: " + Mensaje + "');</script>");
        }

        protected void LimpiarControles()
        {
            txtNombre.Text = "";
            txtClave.Text = "";
        }


        private int ValidaClave()
        {
            int valor = 0;
            string clave = string.Empty;

            for (int i = 0; gvPeriocidadNomina.Rows.Count > i; i++)
            {

                clave = ((Label)gvPeriocidadNomina.Rows[i].FindControl("lblClave")).Text;

                if (clave == txtClave.Text)
                {
                    valor = valor + 1;
                }

            }

            return valor;

        }

    }
}