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
    public partial class wfrmUMA : System.Web.UI.Page
    {
        #region Variables
        string Id = string.Empty;
        string Fecha = string.Empty;
        DateTime FormatoFecha;
        string UMA = string.Empty;
        string SMG = string.Empty;
        string FactorInfonavit = string.Empty;

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
                if (validaFechas() == false)
                {
                    Fecha = txtFecha.Text.ToString().Replace("-", "") + " " + DateTime.Now.ToString("HH:mm:ss");
                    UMA = txtUMA.Text.ToString();
                    //SMG = txtSMG.Text.ToString();
                    FactorInfonavit = txtFactorInfonavit.Text.ToString();

                    strQuery = string.Format("SP_InsertaUMA '{0}', '{1}','{2}'", Fecha, UMA, FactorInfonavit);

                    returnValue = clsQuery.execQueryString(strQuery);

                    if (returnValue == "1")
                    {
                        ObtenUMA();
                        LimpiarControles();
                        Mensaje("GUARDADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
                    }

                }

            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
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

        protected void LimpiarControles()
        {
            txtFecha.Text = string.Empty;
            txtUMA.Text = string.Empty;
            //txtSMG.Text = string.Empty;

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
                Fecha = ((TextBox)gvUMA.Rows[e.RowIndex].FindControl("txtFecha")).Text.Replace(',', '.');
                UMA = ((TextBox)gvUMA.Rows[e.RowIndex].FindControl("txtUMA")).Text.Replace(',', '.');
                //SMG = ((TextBox)gvUMA.Rows[e.RowIndex].FindControl("txtSMG")).Text.Replace(',', '.');
                FactorInfonavit = ((TextBox)gvUMA.Rows[e.RowIndex].FindControl("txtFcInfonavit")).Text.Replace(',', '.');
                FormatoFecha = DateTime.Parse(Fecha);

                strQuery = string.Format("SP_ActualizaUMA {0}, '{1}', '{2}', '{3}'", Id, FormatoFecha.ToString("yyyyMMdd HH:mm:ss"), UMA, FactorInfonavit);

                returnValue = clsQuery.execQueryString(strQuery);

                if (returnValue == "1")
                {
                    gvUMA.EditIndex = -1;
                    ObtenUMA();
                    Mensaje("ACTUALIZADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
                }
            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
            }
        }       

        private bool validaFechas()
        {
            Boolean returnvalor = false;

            DateTime fechahoy = DateTime.Now.Date; // new DateTime //DateTime.Now.ToShortDateString("mm/dd/yyyy");
            fechahoy = DateTime.Now.Date;

            if ( DateTime.Parse(txtFecha.Text) > fechahoy )
            {
                Mensaje("La fecha no puede ser mayor a la de hoy", CuadroMensaje.CuadroMensajeIcono.Error);
                returnvalor = true;
            }


            return returnvalor;
        }

        public void Mensaje(string textomensaje, CuadroMensaje.CuadroMensajeIcono icono)
        {
            CuadroMensaje Mensaje = new CuadroMensaje(textomensaje, "MENSAJE", icono, CuadroMensaje.CuadroMensajeBoton.Ok, CuadroMensaje.CuadroMensajeEstilo.EstiloB);
            Literal ltrMensaje = (Literal)this.Master.FindControl("ltrMensaje");
            ltrMensaje.Text = Mensaje.Mostrar(this);
        }

    }
}