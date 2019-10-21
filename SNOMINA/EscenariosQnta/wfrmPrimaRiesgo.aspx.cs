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
    public partial class wfrmPrimaRiesgo : System.Web.UI.Page
    {
        clsDatos clsQuery = new clsDatos();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenPrimaRiesgo();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string Clase = string.Empty;
                string PrimaRiesgo = string.Empty;                
                string strValue = string.Empty;

                Clase = txtClase.Text.ToString();
                PrimaRiesgo = txtPrimaRiesgo.Text.ToString();
                
                strValue = clsQuery.execQueryString("SP_InsertaPrimaRiesgo '" + Clase + "','" + PrimaRiesgo + "'");

                if (strValue == "1")
                {
                    ObtenPrimaRiesgo();
                    LimpiarControles();
                    Response.Write("<script>alert('Guardado');</script>");
                }
            }
            catch (Exception ex)
            {
                MensageError(ex.ToString());
            }
        }

        protected void gvPrimaRiesgo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPrimaRiesgo.EditIndex = -1;
        }

        protected void gvPrimaRiesgo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                string ID = ((Label)gvPrimaRiesgo.Rows[e.RowIndex]
                                 .FindControl("lblId_Clase")).Text;                
                string Clase = ((TextBox)gvPrimaRiesgo.Rows[e.RowIndex]
                                    .FindControl("txtClase")).Text;
                string PrimaRiesgo = ((TextBox)gvPrimaRiesgo.Rows[e.RowIndex]
                                    .FindControl("txtPrimaRiesgo")).Text;


                string strValue = string.Empty;

                strValue = clsQuery.execQueryString("SP_ActualizaPrimaRiesgo " + ID + ",'" + Clase + "','" + PrimaRiesgo + "'");

                if (strValue == "1")
                {
                    gvPrimaRiesgo.EditIndex = -1;
                    ObtenPrimaRiesgo();
                    Response.Write("<script>alert('Actualizado');</script>");
                }
            }
            catch (Exception ex)
            {
                MensageError(ex.ToString());
            }
        }

        protected void gvPrimaRiesgo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvPrimaRiesgo.EditIndex = e.NewEditIndex;
        }

        protected void ObtenPrimaRiesgo()
        {
            DataTable dtPrimaRiesgo = new DataTable();

            dtPrimaRiesgo = clsQuery.execQueryDataTable("SP_ObtenPrimaRiesgo");

            if (dtPrimaRiesgo.Rows.Count > 0)
            {
                gvPrimaRiesgo.DataSource = dtPrimaRiesgo;
                gvPrimaRiesgo.DataBind();
            }
        }

        public void MensageError(string Mensaje)
        {
            Response.Write("<script>alert('Hubo un Error en el siatemapor favor contacte a Soporte: " + Mensaje + "');</script>");
        }

        protected void LimpiarControles()
        {
            txtPrimaRiesgo.Text = "";
            txtClase.Text = "";
        }

  

     
    }
}