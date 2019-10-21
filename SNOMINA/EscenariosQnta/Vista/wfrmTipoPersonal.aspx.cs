using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EscenariosQnta.Data;
using EscenariosQnta.Clases;

namespace EscenariosQnta.Data
{
    public partial class wfrmTipoPersonal : System.Web.UI.Page
    {
        clsDatos clsQuery = new clsDatos();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenTipoPersonal();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string Nombre = string.Empty;
                string strValue = string.Empty;

                Nombre = txtNombre.Text.ToString();


                strValue = clsQuery.execQueryString("SP_InsertaTipoPersonal '" + Nombre + "'");

                if (strValue == "1")
                {
                    ObtenTipoPersonal();
                    LimpiarControles();
                    Mensaje("GUARDADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
                }
            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
                
            }
        }      

        protected void gvTipoPersonal_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvTipoPersonal.EditIndex = -1;
            ObtenTipoPersonal();
        }

        protected void gvTipoPersonal_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvTipoPersonal.EditIndex = e.NewEditIndex;
            ObtenTipoPersonal();
        }

        protected void gvTipoPersonal_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                string ID = ((Label)gvTipoPersonal.Rows[e.RowIndex]
                                 .FindControl("lblId_TpoPersonal")).Text;
                string Nombre = ((TextBox)gvTipoPersonal.Rows[e.RowIndex]
                                    .FindControl("txtNombre")).Text;
                

                string strValue = string.Empty;

                strValue = clsQuery.execQueryString("SP_ActualizaTipoPersonal " + ID + ",'" + Nombre + "'");

                if (strValue == "1")
                {
                    gvTipoPersonal.EditIndex = -1;
                    ObtenTipoPersonal();
                    Mensaje("ACTUALIZADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
                }
                else
                {
                    Mensaje("ERROR: Hubo un error por favor contactar al departamento de sistemas ", CuadroMensaje.CuadroMensajeIcono.Error);
                }
            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
                
            }
        }

        protected void ObtenTipoPersonal()
        {
            DataTable dtTipoPersonal = new DataTable();

            dtTipoPersonal = clsQuery.execQueryDataTable("SP_ObtenTipoPersonal");

            if (dtTipoPersonal.Rows.Count > 0)
            {
                gvTipoPersonal.DataSource = dtTipoPersonal;
                gvTipoPersonal.DataBind();
            }
        }

        protected void LimpiarControles()
        {
            txtNombre.Text = "";
        }

        public void Mensaje(string textomensaje, CuadroMensaje.CuadroMensajeIcono icono)
        {
            CuadroMensaje Mensaje = new CuadroMensaje(textomensaje, "MENSAJE", icono, CuadroMensaje.CuadroMensajeBoton.Ok, CuadroMensaje.CuadroMensajeEstilo.EstiloB);
            Literal ltrMensaje = (Literal)this.Master.FindControl("ltrMensaje");
            ltrMensaje.Text = Mensaje.Mostrar(this);
        }

    }
}