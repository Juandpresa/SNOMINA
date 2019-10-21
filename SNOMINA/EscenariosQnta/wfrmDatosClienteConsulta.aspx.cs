using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EscenariosQnta.Data;

namespace EscenariosQnta
{
    public partial class wfrmDatosClienteConsulta : System.Web.UI.Page
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
                Response.Write("<script>alert('No Se Enccontraron Resultados');</script>");
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
                Response.Write("<script>alert('No Se Enccontraron Resultados');</script>");
            }
        }

        protected void grClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ObtenClientes();
            grClientes.PageIndex = e.NewPageIndex;
            grClientes.DataBind();
        }
    }
}