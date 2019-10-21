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
    public partial class wfrmPensionAlimenticia : System.Web.UI.Page
    {
        #region Variables

        string Id = string.Empty;
        string Descripcion = string.Empty;
        string strQuery = string.Empty;
        string strReturnValue = string.Empty;

        clsDatos clsQuery = new clsDatos();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenPensionAlimenticia();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {


                Descripcion = txtDescripcion.Text.ToString();

                strQuery = string.Format("SP_InsertaPensionAlimenticia '{0}'", Descripcion);

                strReturnValue = clsQuery.execQueryString(strQuery);

                if (strReturnValue == "1")
                {
                    ObtenPensionAlimenticia();
                    LimpiarControles();
                    Mensaje("GUARDADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
                }
            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
               
            }
        }

        protected void gvPensionAlimenticia_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPensionAlimenticia.EditIndex = -1;
            ObtenPensionAlimenticia();
        }

        protected void gvPensionAlimenticia_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvPensionAlimenticia.EditIndex = e.NewEditIndex;
            ObtenPensionAlimenticia();
        }

        protected void gvPensionAlimenticia_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Id = ((Label)gvPensionAlimenticia.Rows[e.RowIndex].FindControl("lblId_TpoPensA")).Text;
                Descripcion = ((TextBox)gvPensionAlimenticia.Rows[e.RowIndex].FindControl("txtDescripcion")).Text;

                strQuery = string.Format("SP_ActualizaPensionAlimenticia {0}, '{1}'", Id, Descripcion);

                strReturnValue = clsQuery.execQueryString(strQuery);

                if (strReturnValue == "1")
                {
                    gvPensionAlimenticia.EditIndex = -1;
                    ObtenPensionAlimenticia();
                    Mensaje("ACTUALIZADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
                }
            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
                
            }
        }

        protected void ObtenPensionAlimenticia()
        {
            DataTable dtPensionAlimenticia = new DataTable();

            dtPensionAlimenticia = clsQuery.execQueryDataTable("SP_ObtenPensionAlimenticia");

            if (dtPensionAlimenticia.Rows.Count > 0)
            {
                gvPensionAlimenticia.DataSource = dtPensionAlimenticia;
                gvPensionAlimenticia.DataBind();
            }
        }
     
        protected void LimpiarControles()
        {
            txtDescripcion.Text = string.Empty;
        }

        public void Mensaje(string textomensaje, CuadroMensaje.CuadroMensajeIcono icono)
        {
            CuadroMensaje Mensaje = new CuadroMensaje(textomensaje, "MENSAJE", icono, CuadroMensaje.CuadroMensajeBoton.Ok, CuadroMensaje.CuadroMensajeEstilo.EstiloB);
            Literal ltrMensaje = (Literal)this.Master.FindControl("ltrMensaje");
            ltrMensaje.Text = Mensaje.Mostrar(this);
        }

    }
}