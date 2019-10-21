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

namespace EscenariosQnta.js
{
    public partial class PopUpFunciones : System.Web.UI.Page
    {
        #region Variables
        string CalculoPopUp = string.Empty;

        clsDatos clsQuery = new clsDatos();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenFunciones();
            }
            ltrMensaje.Text = string.Empty;
        }

        protected void ObtenFunciones()
        {
            DataTable dtFunciones = new DataTable();

            dtFunciones = clsQuery.execQueryDataTable("SP_ObtenFuncionesTodas");

            if (dtFunciones.Rows.Count > 0)
            {
                gvFunciones.DataSource = dtFunciones;
                gvFunciones.DataBind();
            }
        }

        protected void gvFunciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvFunciones.SelectedRow;

            txtCalculcoPopUp.Text = string.Concat(txtCalculcoPopUp.Text, (row.FindControl("lblNomCorto") as Label).Text);

            Session["Calculo"] = txtCalculcoPopUp.Text.ToString();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "window.close();", true);
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {

            //if (string.IsNullOrEmpty(txtCalculcoPopUp.Text))
            //{
            //    Mensaje("Campo Vacio", CuadroMensaje.CuadroMensajeIcono.Error);
            //}
            //else 
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "myScript", "<script>javascript:validatextbox();</script>");
            //    //Session["Calculo"] = txtCalculcoPopUp.Text.ToString();
            //}


        }

        [System.Web.Services.WebMethod]
        public static void Mensaje(string textomensaje, CuadroMensaje.CuadroMensajeIcono icono)
        {
            CuadroMensaje Mensaje = new CuadroMensaje(textomensaje, "MENSAJE", icono, CuadroMensaje.CuadroMensajeBoton.Ok, CuadroMensaje.CuadroMensajeEstilo.EstiloB);
            //Literal ltrMensaje = (Literal)form1.FindControl("ltrMensaje");
            //ltrMensaje.Text = Mensaje.Mostrar(Mensaje);            
        }
    }
}