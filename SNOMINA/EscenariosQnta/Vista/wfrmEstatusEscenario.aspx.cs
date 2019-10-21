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
    public partial class wfrmEstatusEscenario : System.Web.UI.Page
    {
        #region Variables}

        string Id = string.Empty;
        string Descripcion = string.Empty;
        string strQuery = string.Empty;
        string returnValor = string.Empty;

        clsDatos clsQuery = new clsDatos();
        
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenEstatus();
            }
        }

        protected void ObtenEstatus()
        {
            DataTable dtEstatus = new DataTable();

            dtEstatus = clsQuery.execQueryDataTable("SP_ObtenEstatusEscenario");

            if (dtEstatus.Rows.Count > 0)
            {
                gvEstatus.DataSource = dtEstatus;
                gvEstatus.DataBind();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string Nombre = string.Empty;
                string strValue = string.Empty;

                Nombre = txtEstatus.Text.ToString();


                strValue = clsQuery.execQueryString("SP_InsertaEstatusEscenario '" + Nombre + "'");

                if (strValue == "1")
                {
                    ObtenEstatus();
                    LimpiarControles();
                    Mensaje("GUARDADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
                }
            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
                
            }
        }

        protected void LimpiarControles()
        {
            txtEstatus.Text = string.Empty;
        }

        protected void gvEstatus_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEstatus.EditIndex = -1;
            ObtenEstatus();
        }

        protected void gvEstatus_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEstatus.EditIndex = e.NewEditIndex;
            ObtenEstatus();
        }

        protected void gvEstatus_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Id = ((Label)gvEstatus.Rows[e.RowIndex]
                                 .FindControl("lblId_StsEsc")).Text;
                Descripcion = ((TextBox)gvEstatus.Rows[e.RowIndex]
                                    .FindControl("txtDescripcion")).Text;


                strQuery = string.Format("SP_ActualizaEstatusEscenario {0}, '{1}'", Id, Descripcion);

                returnValor = clsQuery.execQueryString(strQuery);

                if (returnValor == "1")
                {
                    gvEstatus.EditIndex = -1;
                    ObtenEstatus();
                    Mensaje("ACTUALIZADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
                }
            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
               
            }
        }

        public void Mensaje(string textomensaje, CuadroMensaje.CuadroMensajeIcono icono)
        {
            CuadroMensaje Mensaje = new CuadroMensaje(textomensaje, "MENSAJE", icono, CuadroMensaje.CuadroMensajeBoton.Ok, CuadroMensaje.CuadroMensajeEstilo.EstiloB);
            Literal ltrMensaje = (Literal)this.Master.FindControl("ltrMensaje");
            ltrMensaje.Text = Mensaje.Mostrar(this);
        }

    }
}