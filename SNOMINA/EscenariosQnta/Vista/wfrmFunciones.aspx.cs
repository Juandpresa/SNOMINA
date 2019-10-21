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
    public partial class wfrmFunciones : System.Web.UI.Page
    {
        #region Variables
        string Id = string.Empty;
        string NomCorto = string.Empty;
        string Descripcion = string.Empty;
        string Operacion = string.Empty;
        string strQuery = string.Empty;
        string strValue = string.Empty;
        clsDatos clsQuery = new clsDatos();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenFunciones();
            }
        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {               
                    NomCorto = txtNomCorto.Text.ToString();
                    Descripcion = txtDescripcion.Text.ToString();
                    Operacion = txtOperacion.Text.ToString();

                    strQuery = string.Format("SP_InsertaFucnciones  '{0}', '{1}', '{2}'", NomCorto, Descripcion, Operacion);
                    strValue = clsQuery.execQueryString(strQuery);

                    if (strValue == "1")
                    {
                        ObtenFunciones();
                        LimpiarControles();
                        Mensaje("GUARDADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
                    }
                
            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);

            }
        }
        protected void ObtenFunciones()
        {

            DataTable dtFunciones = new DataTable();

            dtFunciones = clsQuery.execQueryDataTable("SP_ObtenFunciones");

            if (dtFunciones.Rows.Count > 0)
            {
                gvFunciones.DataSource = dtFunciones;
                gvFunciones.DataBind();
            }
        }

        protected void gvFunciones_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvFunciones.EditIndex = -1;
            ObtenFunciones();            
        }

        protected void gvFunciones_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvFunciones.EditIndex = e.NewEditIndex;
            ObtenFunciones();

        }

        protected void gvFunciones_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Id = ((Label)gvFunciones.Rows[e.RowIndex]
                                .FindControl("lblId_Func")).Text;
                NomCorto = ((TextBox)gvFunciones.Rows[e.RowIndex]
                                   .FindControl("txtNomCorto")).Text;
                Descripcion = ((TextBox)gvFunciones.Rows[e.RowIndex]
                                   .FindControl("txtDescripcion")).Text;
                Operacion = ((TextBox)gvFunciones.Rows[e.RowIndex]
                                   .FindControl("txtOperacion")).Text;

                strQuery = string.Format("SP_ActualizaFunciones {0}, '{1}', '{2}', '{3}'", Id, NomCorto, Descripcion, Operacion);

                strValue = clsQuery.execQueryString(strQuery);

                if (strValue == "1")
                {
                    gvFunciones.EditIndex = -1;
                    ObtenFunciones();
                    Mensaje("ACTUALIZADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
                }
            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);

            }
        }


        protected void LimpiarControles()
        {
            txtNomCorto.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtOperacion.Text = string.Empty;
        }

        public void Mensaje(string textomensaje, CuadroMensaje.CuadroMensajeIcono icono)
        {
            CuadroMensaje Mensaje = new CuadroMensaje(textomensaje, "MENSAJE", icono, CuadroMensaje.CuadroMensajeBoton.Ok, CuadroMensaje.CuadroMensajeEstilo.EstiloB);
            Literal ltrMensaje = (Literal)this.Master.FindControl("ltrMensaje");
            ltrMensaje.Text = Mensaje.Mostrar(this);
        }
    }
}