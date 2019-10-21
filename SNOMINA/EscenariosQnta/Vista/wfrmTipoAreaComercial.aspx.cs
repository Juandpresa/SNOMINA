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
    public partial class wfrmTipoAreaComercial : System.Web.UI.Page
    {
        #region Variables
        string Id = string.Empty;
        string Descripcion = string.Empty;

        string strQuery = string.Empty;
        string strValue = string.Empty;
        #endregion

        clsDatos clsQuery = new clsDatos();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenTipoAreaComercial();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {

                string strValue = string.Empty;

                Descripcion = txtDescripcion.Text.ToString();

                strQuery = string.Format("SP_InsertaTipoAreaComercial '{0}'", Descripcion);
                strValue = clsQuery.execQueryString(strQuery);

                if (strValue == "1")
                {
                    ObtenTipoAreaComercial();
                    LimpiarControles();
                    Mensaje("GUARDADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
                }
            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);

            }
        }

        protected void gvTipoAreaComercial_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvTipoAreaComercial.EditIndex = -1;
            ObtenTipoAreaComercial();
        }

        protected void gvTipoAreaComercial_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvTipoAreaComercial.EditIndex = e.NewEditIndex;
            ObtenTipoAreaComercial();
        }

        protected void gvTipoAreaComercial_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Id = ((Label)gvTipoAreaComercial.Rows[e.RowIndex].FindControl("lblId")).Text;
                Descripcion = ((TextBox)gvTipoAreaComercial.Rows[e.RowIndex].FindControl("txtDescripcion")).Text;

                strQuery =  string.Format("SP_ActualizaTipoAreaComercial {0}, '{1}'",  Id, Descripcion);
                strValue = clsQuery.execQueryString(strQuery);

                if (strValue == "1")
                {
                    gvTipoAreaComercial.EditIndex = -1;
                    ObtenTipoAreaComercial();
                    Mensaje("ACTUALIZADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);                    
                }
            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);

            }
        }

        protected void ObtenTipoAreaComercial()
        {
            DataTable dtTipoPersonal = new DataTable();

            dtTipoPersonal = clsQuery.execQueryDataTable("SP_ObtenTipoAreaComercial");

            if (dtTipoPersonal.Rows.Count > 0)
            {
                gvTipoAreaComercial.DataSource = dtTipoPersonal;
                gvTipoAreaComercial.DataBind();
            }
        }

        protected void LimpiarControles()
        {
            txtDescripcion.Text = string.Empty;
        }

        protected void Mensaje(string textomensaje, CuadroMensaje.CuadroMensajeIcono icono)
        {
            CuadroMensaje Mensaje = new CuadroMensaje(textomensaje, "MENSAJE", icono, CuadroMensaje.CuadroMensajeBoton.Ok, CuadroMensaje.CuadroMensajeEstilo.EstiloB);
            Literal ltrMensaje = (Literal)this.Master.FindControl("ltrMensaje");
            ltrMensaje.Text = Mensaje.Mostrar(this);            
        }

    }
}