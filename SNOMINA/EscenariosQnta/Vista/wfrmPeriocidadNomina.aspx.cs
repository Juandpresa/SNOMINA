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

namespace EscenariosQnta
{
    public partial class wfrmPeriocidadNomina : System.Web.UI.Page
    {
        #region Variables
        string Id = string.Empty;
        string Clave = string.Empty;
        string Nombre = string.Empty;
        string Dias = string.Empty;
        string strQuery = string.Empty;
        string strValue = string.Empty;
        clsDatos clsQuery = new clsDatos();
        #endregion

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


                    Clave = txtClave.Text.ToString();
                    Nombre = txtNombre.Text.ToString();
                    Dias = txtDias.Text.ToString();

                    strQuery = string.Format("SP_InsertaPeriocidadNomina  '{0}', '{1}', {2}", Clave, Nombre, Dias);
                    strValue = clsQuery.execQueryString(strQuery);

                    if (strValue == "1")
                    {
                        ObtenPeriocidadNomina();
                        LimpiarControles();
                        Mensaje("GUARDADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);

            }
        }

        protected void gvPeriocidadNomina_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPeriocidadNomina.EditIndex = -1;
            ObtenPeriocidadNomina();
            Response.Redirect("/wfrmPeriocidadNomina.aspx");
        }

        protected void gvPeriocidadNomina_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvPeriocidadNomina.EditIndex = e.NewEditIndex;
            ObtenPeriocidadNomina();
            
        }

        protected void gvPeriocidadNomina_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Id = ((Label)gvPeriocidadNomina.Rows[e.RowIndex]
                                .FindControl("lblId_Periodo")).Text;
                Clave = ((TextBox)gvPeriocidadNomina.Rows[e.RowIndex]
                                   .FindControl("txtClave")).Text;
                Nombre = ((TextBox)gvPeriocidadNomina.Rows[e.RowIndex]
                                   .FindControl("txtNombre")).Text;
                Dias = ((TextBox)gvPeriocidadNomina.Rows[e.RowIndex]
                                   .FindControl("txtDias")).Text;

                strQuery = string.Format("SP_ActualizaPeriocidadNomina {0}, '{1}', '{2}', {3}", Id, Clave, Nombre, Dias);

                strValue = clsQuery.execQueryString(strQuery);

                if (strValue == "1")
                {
                    gvPeriocidadNomina.EditIndex = -1;
                    ObtenPeriocidadNomina();
                    Mensaje("ACTUALIZADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);                    
                }
            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);

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

        protected void LimpiarControles()
        {
            txtNombre.Text = string.Empty;
            txtClave.Text = string.Empty;
            txtDias.Text = string.Empty;
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

        public void Mensaje(string textomensaje, CuadroMensaje.CuadroMensajeIcono icono)
        {
            CuadroMensaje Mensaje = new CuadroMensaje(textomensaje, "MENSAJE", icono, CuadroMensaje.CuadroMensajeBoton.Ok, CuadroMensaje.CuadroMensajeEstilo.EstiloB);            
            Literal ltrMensaje = (Literal)this.Master.FindControl("ltrMensaje");
            ltrMensaje.Text = Mensaje.Mostrar(this);
        }

    }
}