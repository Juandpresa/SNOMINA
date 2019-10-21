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
    public partial class wfrmPrimaRiesgo : System.Web.UI.Page
    {

        #region Variables

        string ID = string.Empty;
        string Clase = string.Empty;
        string PrimaRiesgo = string.Empty;        

        string strValue = string.Empty;
        string strQuery = string.Empty;

        clsDatos clsQuery = new clsDatos();

        #endregion

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
                Clase = txtClase.Text.ToString();
                PrimaRiesgo = txtPrimaRiesgo.Text.ToString();

                strQuery = string.Format("SP_InsertaPrimaRiesgo '{0}', '{1}'", Clase, PrimaRiesgo);
                strValue = clsQuery.execQueryString(strQuery);

                if (strValue == "1")
                {
                    ObtenPrimaRiesgo();
                    LimpiarControles();
                    Mensaje("GUARDADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
                }
            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
                
            }
        }

        protected void gvPrimaRiesgo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPrimaRiesgo.EditIndex = -1;
            ObtenPrimaRiesgo();
        }

        protected void gvPrimaRiesgo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                ID = ((Label)gvPrimaRiesgo.Rows[e.RowIndex].FindControl("lblId_Clase")).Text;                
                Clase = ((TextBox)gvPrimaRiesgo.Rows[e.RowIndex].FindControl("txtClase")).Text;
                PrimaRiesgo = ((TextBox)gvPrimaRiesgo.Rows[e.RowIndex].FindControl("txtPrimaRiesgo")).Text;

                strQuery = string.Format("SP_ActualizaPrimaRiesgo {0}, '{1}', '{2}'", ID, Clase, PrimaRiesgo);
                strValue = clsQuery.execQueryString(strQuery);

                if (strValue == "1")
                {
                    gvPrimaRiesgo.EditIndex = -1;
                    ObtenPrimaRiesgo();
                    Mensaje("ACTUALIZADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
                }
            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);                
            }
        }

        protected void gvPrimaRiesgo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvPrimaRiesgo.EditIndex = e.NewEditIndex;
            ObtenPrimaRiesgo();
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
 
        protected void LimpiarControles()
        {
            txtPrimaRiesgo.Text = string.Empty;
            txtClase.Text = string.Empty;
        }

        public void Mensaje(string textomensaje, CuadroMensaje.CuadroMensajeIcono icono)
        {
            CuadroMensaje Mensaje = new CuadroMensaje(textomensaje, "MENSAJE", icono, CuadroMensaje.CuadroMensajeBoton.Ok, CuadroMensaje.CuadroMensajeEstilo.EstiloB);
            Literal ltrMensaje = (Literal)this.Master.FindControl("ltrMensaje");
            ltrMensaje.Text = Mensaje.Mostrar(this);
        }


     
    }
}