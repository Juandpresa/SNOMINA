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
    public partial class wfrmSalarioMinimo : System.Web.UI.Page
    {

        #region Variables
        string Id = string.Empty;
        string Fecha = string.Empty;
        DateTime FormatoFecha;
        string ValZonaA = string.Empty;
        string ValZonaB = string.Empty;
        string ValZonaC = string.Empty;
        string strQuery = string.Empty;
        string strGuardado = string.Empty;

        clsDatos clsQuery = new clsDatos();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenSalarioMinimo();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Fecha = txtFecha.Text.ToString().Replace("-","") + " " + DateTime.Now.ToString("HH:mm:ss");
                ValZonaA = txtValZonaA.Text.ToString();
                //ValZonaB = txtValZonaB.Text.ToString();
                //ValZonaC = txtValZonaC.Text.ToString();

                strQuery = string.Format("SP_InsertaSalarioMinimo '{0}', '{1}', '{2}', '{3}'", Fecha, ValZonaA, ValZonaB, ValZonaC);

                strGuardado = clsQuery.execQueryString(strQuery);

                if (strGuardado == "1")
                {
                    LimpiaContorlos();
                    Mensaje("GUARDADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
                }
                else
                {

                    Mensaje("ERROR: Hubo un error por favor contacte al departemento de sistemas", CuadroMensaje.CuadroMensajeIcono.Error);
                }


                ObtenSalarioMinimo();
            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
            }

        }

        protected void ObtenSalarioMinimo()
        {
            DataTable dtSalarioMin = new DataTable();
            dtSalarioMin = clsQuery.execQueryDataTable("SP_ObtenSalarioMinimo");

            if (dtSalarioMin.Rows.Count > 0)
            {
                gvSalarioMinimo.DataSource = dtSalarioMin;
                gvSalarioMinimo.DataBind();
            }

        }

        protected void gvSalarioMinimo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ObtenSalarioMinimo();
            gvSalarioMinimo.PageIndex = e.NewPageIndex;
            gvSalarioMinimo.DataBind();
        }

        protected void gvSalarioMinimo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSalarioMinimo.EditIndex = -1;
            ObtenSalarioMinimo();
        }

        protected void gvSalarioMinimo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvSalarioMinimo.EditIndex = e.NewEditIndex;
            ObtenSalarioMinimo();
        }

        protected void gvSalarioMinimo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Id = ((Label)gvSalarioMinimo.Rows[e.RowIndex]
                                   .FindControl("lblId")).Text;
            Fecha = ((TextBox)gvSalarioMinimo.Rows[e.RowIndex]
                                    .FindControl("txtFecha")).Text;
            ValZonaA = ((TextBox)gvSalarioMinimo.Rows[e.RowIndex]
                                    .FindControl("txtValZonaA")).Text.Replace(",", ".");
            //ValZonaB = ((TextBox)gvSalarioMinimo.Rows[e.RowIndex]
            //                        .FindControl("txtValZonaB")).Text.Replace(",", ".");
            //ValZonaC = ((TextBox)gvSalarioMinimo.Rows[e.RowIndex]
            //                        .FindControl("txtValZonaC")).Text.Replace(",", ".");            
            FormatoFecha = DateTime.Parse(Fecha);
                        
            strQuery = string.Format("SP_ActualizaSalarioMinimo {0}, '{1}', '{2}', '{3}', '{4}'", Id, FormatoFecha.ToString("yyyyMMdd HH:mm:ss"), ValZonaA, ValZonaB, ValZonaC);

            strGuardado = clsQuery.execQueryString(strQuery);

            if (strGuardado == "1")
            {
                gvSalarioMinimo.EditIndex = -1;
                ObtenSalarioMinimo();
                Mensaje("ACTUALIZADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);

            }
        }


        protected void LimpiaContorlos()
        {
            txtFecha.Text = string.Empty;
            txtValZonaA.Text = string.Empty;
            //txtValZonaB.Text = string.Empty;
            //txtValZonaC.Text = string.Empty;
        
        }

        public void Mensaje(string textomensaje, CuadroMensaje.CuadroMensajeIcono icono)
        {
            CuadroMensaje Mensaje = new CuadroMensaje(textomensaje, "MENSAJE", icono, CuadroMensaje.CuadroMensajeBoton.Ok, CuadroMensaje.CuadroMensajeEstilo.EstiloB);
            Literal ltrMensaje = (Literal)this.Master.FindControl("ltrMensaje");
            ltrMensaje.Text = Mensaje.Mostrar(this);
        }

    }
}