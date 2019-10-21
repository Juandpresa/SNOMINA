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
    public partial class wfrmUMA : System.Web.UI.Page
    {
        #region Variables
        string Id = string.Empty;
        string Fecha = string.Empty;
        string UMA = string.Empty;
        string SMG = string.Empty;

        string strQuery = string.Empty;
        string returnValue = string.Empty;

        clsDatos clsQuery = new clsDatos();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenUMA();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Fecha = txtFecha.Text.ToString().Replace("-", "");
                UMA = txtUMA.Text.ToString();
                SMG = txtSMG.Text.ToString();

                strQuery = string.Format("SP_InsertaUMA '{0}', '{1}','{2}'", Fecha, UMA, SMG);

                returnValue = clsQuery.execQueryString(strQuery);

                if (returnValue == "1")
                {
                    ObtenUMA();
                    LimpiarControles();
                    Response.Write("<script>alert('Guardado');</script>");
                }
            }
            catch (Exception ex)
            {
                MensageError(ex.ToString());
            }
        }

        protected void ObtenUMA()
        {
            DataTable dtUMA = new DataTable();

            dtUMA = clsQuery.execQueryDataTable("SP_ObtenUMA");

            if (dtUMA.Rows.Count > 0)
            {
                gvUMA.DataSource = dtUMA;
                gvUMA.DataBind();
            }
        }

        public void MensageError(string Mensaje)
        {
            try
            {
                Response.Write("<script>alert('Hubo un Error en el siatemapor favor contacte a Soporte: " + Mensaje.ToString() + "');</script>");
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected void LimpiarControles()
        {
            txtFecha.Text = string.Empty;
            txtUMA.Text = string.Empty;
            txtSMG.Text = string.Empty;

        }

        protected void gvUMA_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvUMA.EditIndex = -1;
            ObtenUMA();
        }

        protected void gvUMA_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvUMA.EditIndex = e.NewEditIndex;
            ObtenUMA();
        }

        protected void gvUMA_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Id = ((Label)gvUMA.Rows[e.RowIndex].FindControl("lblId")).Text;
                Fecha = ((TextBox)gvUMA.Rows[e.RowIndex].FindControl("txtLimInf")).Text.Replace(',', '.');
                UMA = ((TextBox)gvUMA.Rows[e.RowIndex].FindControl("txtLimSup")).Text.Replace(',', '.');
                SMG = ((TextBox)gvUMA.Rows[e.RowIndex].FindControl("txtCuotaFija")).Text.Replace(',', '.');



                strQuery = string.Format("SP_ActualizaUMA {0}, '{1}', '{2}', '{3}'", Id, Fecha, UMA, SMG);

                returnValue = clsQuery.execQueryString(strQuery);

                if (returnValue == "1")
                {
                    gvUMA.EditIndex = -1;
                    ObtenUMA();
                    Response.Write("<script>alert('Actualizado');</script>");
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }


    }
}