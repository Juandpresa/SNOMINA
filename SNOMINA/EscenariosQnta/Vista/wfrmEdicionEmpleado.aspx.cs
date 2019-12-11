using System;
using EscenariosQnta.Clases;
using EscenariosQnta.Data;
using EscenariosQnta.Negocio;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EscenariosQnta.Clases;
using EscenariosQnta.Data;
using EscenariosQnta.Negocio;

namespace EscenariosQnta.Vista
{
  public partial class wfrmEdicionEmpleado : System.Web.UI.Page
  {
    clsDatos clsQuery = new clsDatos();
    int idEmp;
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        idEmp = (int)Session["IdEmpleado"];
        ObtenEmpleadoras();
        ObtenTipoEsquema();
        ObtenSexo();
        SpInformacion();


        
      }
    }

    protected void ObtenEmpleadoras()
    {
      try
      {
        DataTable dtEmpleadora = new DataTable();

        dtEmpleadora = clsQuery.execQueryDataTable("SP_ObtenEmpleadoras");
        ddlEmpleadora.Items.Clear();

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

    protected void ObtenClientes(int idEmpleadora)
    {
      try
      {
        DataTable dtCliente = new DataTable();

        dtCliente = BLLEmpleado.ObtenClientes(idEmpleadora);
        ddlCliente.Items.Clear();

        if (dtCliente.Rows.Count > 0)
        {
          ddlCliente.DataSource = dtCliente;
          ddlCliente.DataTextField = "Alias";
          ddlCliente.DataValueField = "ClienteID";
          ddlCliente.DataBind();
        }

        ddlCliente.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

      }
      catch (Exception ex)
      {
        Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
      }
    }

    protected void ObtenSexo()
    {
      try
      {
        DataTable dtSexo = new DataTable();

        dtSexo = clsQuery.execQueryDataTable("SP_ObtenSexo");

        if (dtSexo.Rows.Count > 0)
        {
          ddlSexo.DataSource = dtSexo;
          ddlSexo.DataTextField = "Nombre";
          ddlSexo.DataValueField = "SexoID";
          ddlSexo.DataBind();
        }

        ddlSexo.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

      }
      catch (Exception ex)
      {
        Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
      }
    }

    protected void ObtenTipoEsquema()
    {
      DataTable dtTipoEsquema = new DataTable();

      dtTipoEsquema = clsQuery.execQueryDataTable("Sp_ObtenTipoEsquema");

      if (dtTipoEsquema.Rows.Count > 0)
      {
        rbtTipoEsquema.DataSource = dtTipoEsquema;
        rbtTipoEsquema.DataTextField = "TipoEsquema";
        rbtTipoEsquema.DataValueField = "Id_TipoEsquema";
        rbtTipoEsquema.DataBind();
      }
    }

    public void SpInformacion()
    {
      string[] es = new string[38];
      DataTable dtEMP = new DataTable();
      dtEMP = BLLEmpleado.ObtenEmpleadoById(idEmp);
      for (int i = 0; i <= dtEMP.Rows.Count; i++)
      {
        for (int j = 0; j < 37; j++)
        {
          es[j] = dtEMP.Rows[0][j].ToString();
        }
      }
      ddlEmpleadora.SelectedValue = es[1].ToString();
      ObtenClientes(int.Parse(es[1].ToString()));
      ddlCliente.SelectedValue = es[2].ToString();
      txtClave.Text = es[3].ToString();
      txtNombre.Text = es[4].ToString();
      txtPaterno.Text = es[5].ToString();
      txtMaterno.Text = es[6].ToString();
      ddlSexo.SelectedValue = es[7].ToString();
      txtFechaNacimiento.Text = es[8].ToString();
      txtCurp.Text = es[9].ToString();
      txtRfc.Text = es[10].ToString();
      txtCorreo.Text = es[11].ToString();
      txtTelefonoMovil.Text = es[12].ToString();
      txtTelefonoLocal.Text = es[13].ToString();
      if (es[14].ToString() == "1")
      {
        rbtTipoEsquema.Items[0].Selected = true;
      }
      else
      {
        rbtTipoEsquema.Items[1].Selected = true;
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