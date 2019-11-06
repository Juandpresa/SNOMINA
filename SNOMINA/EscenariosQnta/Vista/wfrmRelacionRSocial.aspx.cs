using EscenariosQnta.Clases;
using EscenariosQnta.Data;
using EscenariosQnta.Negocio;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace EscenariosQnta.Vista
{
  public partial class wfrmRelacionRSocial : System.Web.UI.Page
  {
    string strQueryR = string.Empty;
    int Contratista = 0;
    int Empleadora = 0;
    int Pagadora = 0;
    int Cliente = 0;
    int Facturista = 0;
    clsDatos clsQuery = new clsDatos();
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        ObtenContratistas();
        ObtenEmpleadoras();
        ObtenPagadoras();
        ObtenClientes();
        ObtenFacturistas();
      }
      
    }

    protected void ObtenContratistas()
    {
      try
      {
        DataTable dtContratista = new DataTable();

        dtContratista = clsQuery.execQueryDataTable("SP_ObtenContratistas");

        if (dtContratista.Rows.Count > 0)
        {
          ddlContratista.DataSource = dtContratista;
          ddlContratista.DataTextField = "Alias";
          ddlContratista.DataValueField = "idRazonSocial";
          ddlContratista.DataBind();
        }

        ddlContratista.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

      }
      catch (Exception ex)
      {
        Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
      }
    }

    protected void ObtenEmpleadoras()
    {
      try
      {
        DataTable dtEmpleadora = new DataTable();

        dtEmpleadora = clsQuery.execQueryDataTable("SP_ObtenEmpleadoras");

        if (dtEmpleadora.Rows.Count > 0)
        {
          ddlEmpleadora.DataSource = dtEmpleadora;
          ddlEmpleadora.DataTextField = "Alias";
          ddlEmpleadora.DataValueField = "idRazonSocial";
          ddlEmpleadora.DataBind();
        }

        ddlEmpleadora.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

      }
      catch (Exception ex)
      {
        Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
      }
    }

    protected void ObtenPagadoras()
    {
      try
      {
        DataTable dtPagadora = new DataTable();

        dtPagadora = clsQuery.execQueryDataTable("SP_ObtenPagadoras");

        if (dtPagadora.Rows.Count > 0)
        {
          ddlPagadora.DataSource = dtPagadora;
          ddlPagadora.DataTextField = "Alias";
          ddlPagadora.DataValueField = "idRazonSocial";
          ddlPagadora.DataBind();
        }

        ddlPagadora.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

      }
      catch (Exception ex)
      {
        Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
      }
    }

    protected void ObtenClientes()
    {
      try
      {
        DataTable dtCliente = new DataTable();

        dtCliente = clsQuery.execQueryDataTable("SP_ObtenClientes2");

        if (dtCliente.Rows.Count > 0)
        {
          ddlCliente.DataSource = dtCliente;
          ddlCliente.DataTextField = "Alias";
          ddlCliente.DataValueField = "idRazonSocial";
          ddlCliente.DataBind();
        }

        ddlCliente.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

      }
      catch (Exception ex)
      {
        Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
      }
    }

    protected void ObtenFacturistas()
    {
      try
      {
        DataTable dtFacturista = new DataTable();

        dtFacturista = clsQuery.execQueryDataTable("SP_ObtenFacturistas");

        if (dtFacturista.Rows.Count > 0)
        {
          ddlFacturista.DataSource = dtFacturista;
          ddlFacturista.DataTextField = "Alias";
          ddlFacturista.DataValueField = "idRazonSocial";
          ddlFacturista.DataBind();
        }

        ddlFacturista.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

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

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
      try
      {
        Contratista = int.Parse(ddlContratista.SelectedItem.Value);
        Empleadora = int.Parse(ddlEmpleadora.SelectedItem.Value);
        Pagadora = int.Parse(ddlPagadora.SelectedItem.Value);
        Cliente = int.Parse(ddlCliente.SelectedItem.Value);
        Facturista = int.Parse(ddlFacturista.SelectedItem.Value);

        strQueryR = BLLRelacionRSocial.InsRelacionRSocial(Contratista, Empleadora, Pagadora, Cliente, Facturista);

        if (strQueryR == "1")
        {
          LimpiarControles();
          Mensaje("GUARDADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
        }

        
      }
      catch (Exception)
      {

        throw;
      }
     
    }

    private void LimpiarControles()
    {
      ObtenContratistas();
      ObtenEmpleadoras();
      ObtenPagadoras();
      ObtenClientes();
      ObtenFacturistas();
    }
  }
}