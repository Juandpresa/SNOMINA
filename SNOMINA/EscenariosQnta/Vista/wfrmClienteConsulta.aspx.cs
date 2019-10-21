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
    public partial class wfrmClienteConsulta : System.Web.UI.Page
    {
        clsDatos clsQuery = new clsDatos();

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
                ObtenClientes();

            //}
        }

        protected void ObtenClientes()
        {
            DataTable dtClientes = new DataTable();

            dtClientes = clsQuery.execQueryDataTable("SP_ObtenClientes");

            if (dtClientes.Rows.Count > 0)
            {

                grClientes.DataSource = dtClientes;
                grClientes.DataBind();
            }
            else 
            {
                Mensaje("ADVERTENCIA", CuadroMensaje.CuadroMensajeIcono.Advertencia);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {

                ObtenClientesNombre();

            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
            
            }
        }

        protected void ObtenClientesNombre()
        {

            DataTable dtClientes = new DataTable();
            string Nombre = string.Empty;
            string strQuery = string.Empty;

            Nombre = txtNombre.Text.ToString();

            strQuery = string.Format("SP_ObtenClientesPorNombre '{0}'", Nombre);

            dtClientes = clsQuery.execQueryDataTable(strQuery);

            if (dtClientes.Rows.Count > 0)
            {

                grClientes.DataSource = dtClientes;
                grClientes.DataBind();
            }
            else
            {
                Mensaje("ADVERTENCIA", CuadroMensaje.CuadroMensajeIcono.Advertencia);
            }
        }

        protected void grClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ObtenClientes();
            grClientes.PageIndex = e.NewPageIndex;
            grClientes.DataBind();
        }

        public void Mensaje(string textomensaje, CuadroMensaje.CuadroMensajeIcono icono)
        {
            CuadroMensaje Mensaje = new CuadroMensaje(textomensaje, "MENSAJE", icono, CuadroMensaje.CuadroMensajeBoton.Ok, CuadroMensaje.CuadroMensajeEstilo.EstiloB);
            Literal ltrMensaje = (Literal)this.Master.FindControl("ltrMensaje");
            ltrMensaje.Text = Mensaje.Mostrar(this);
        }

        protected void grClientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }
        }
    }
}