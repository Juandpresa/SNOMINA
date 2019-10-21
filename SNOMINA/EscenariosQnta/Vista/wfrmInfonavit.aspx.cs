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
    public partial class wfrmInfonavit : System.Web.UI.Page
    {

        #region Variables

        string Id = string.Empty;
        string TipoInfonavit = string.Empty;
        string strQuery = string.Empty;
        string strReturnValue = string.Empty;       

        clsDatos clsQuery = new clsDatos();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenInfonavit();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {


                TipoInfonavit = txtTipoInfonavit.Text.ToString();

                strQuery = string.Format("SP_InsertaInfonavit '{0}'", TipoInfonavit);

                strReturnValue = clsQuery.execQueryString(strQuery);

                if (strReturnValue == "1")
                {
                    ObtenInfonavit();
                    LimpiarControles();
                    Mensaje("GUARDADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
                }
            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
                
            }
        }

        protected void gvInfonavit_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvInfonavit.EditIndex = -1;
        }

        protected void gvInfonavit_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvInfonavit.EditIndex = e.NewEditIndex;
        }

        protected void gvInfonavit_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Id = ((Label)gvInfonavit.Rows[e.RowIndex].FindControl("lblId_TpoInfo")).Text;
                TipoInfonavit = ((TextBox)gvInfonavit.Rows[e.RowIndex].FindControl("txtTipoInfonavit")).Text;

                strQuery = string.Format("SP_ActualizaInfonavit {0}, '{1}'", Id, TipoInfonavit);

                strReturnValue = clsQuery.execQueryString(strQuery);

                if (strReturnValue == "1")
                {
                    gvInfonavit.EditIndex = -1;
                    ObtenInfonavit();
                    Mensaje("ACTUALIZADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
                }
            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
                
            }
        }

        protected void ObtenInfonavit()
        {
            DataTable dtInfonavit = new DataTable();

            dtInfonavit = clsQuery.execQueryDataTable("SP_ObtenInfonavit");

            if (dtInfonavit.Rows.Count > 0)
            {
                gvInfonavit.DataSource = dtInfonavit;
                gvInfonavit.DataBind();
            }
        }    

        protected void LimpiarControles()
        {
            txtTipoInfonavit.Text = "";
        }

        public void Mensaje(string textomensaje, CuadroMensaje.CuadroMensajeIcono icono)
        {
            CuadroMensaje Mensaje = new CuadroMensaje(textomensaje, "MENSAJE", icono, CuadroMensaje.CuadroMensajeBoton.Ok, CuadroMensaje.CuadroMensajeEstilo.EstiloB);
            Literal ltrMensaje = (Literal)this.Master.FindControl("ltrMensaje");
            ltrMensaje.Text = Mensaje.Mostrar(this);
        }

    }
}