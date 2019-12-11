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
        ObtenEntidad();
        ObtenClasificacionEmpleado();
        ObtenTipoContrato();
        ObtenTurno();
        ObtenJornada();
        ObtenHorarios();
        ObtenPeriodoPago();
        ObtenTipoPago();
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

    protected void ObtenEntidad()
    {
      try
      {
        DataTable dtEntidad = new DataTable();

        dtEntidad = clsQuery.execQueryDataTable("SP_ObtenerEntidad");

        if (dtEntidad.Rows.Count > 0)
        {
          ddlEntidad.DataSource = dtEntidad;
          ddlEntidad.DataTextField = "Nombre";
          ddlEntidad.DataValueField = "Id_EntFed";
          ddlEntidad.DataBind();
        }

        ddlEntidad.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

      }
      catch (Exception ex)
      {
        Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
      }
    }

    protected void ObtenClasificacionEmpleado()
    {
      try
      {
        DataTable dtClasificacionEmpleado = new DataTable();

        dtClasificacionEmpleado = clsQuery.execQueryDataTable("SP_ObtenClasificacionEmpleado");

        if (dtClasificacionEmpleado.Rows.Count > 0)
        {
          ddlClasificacionEmpleado.DataSource = dtClasificacionEmpleado;
          ddlClasificacionEmpleado.DataTextField = "Descripcion";
          ddlClasificacionEmpleado.DataValueField = "Id_TpoClasEmp";
          ddlClasificacionEmpleado.DataBind();
        }

        ddlClasificacionEmpleado.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

      }
      catch (Exception ex)
      {
        Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
      }
    }

    protected void ObtenTipoContrato()
    {
      try
      {
        DataTable dtTipoContrato = new DataTable();

        dtTipoContrato = clsQuery.execQueryDataTable("SP_ObtenTipoContrato");
        ddlPagadora.Items.Clear();

        if (dtTipoContrato.Rows.Count > 0)
        {
          ddlContrato.DataSource = dtTipoContrato;
          ddlContrato.DataTextField = "Contrato";
          ddlContrato.DataValueField = "TipoContratoID";
          ddlContrato.DataBind();
        }

        ddlContrato.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

      }
      catch (Exception ex)
      {
        Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
      }
    }

    protected void ObtenTurno()
    {
      try
      {
        DataTable dtTurno = new DataTable();

        dtTurno = clsQuery.execQueryDataTable("SP_ObtenTurno");
        ddlTurno.Items.Clear();

        if (dtTurno.Rows.Count > 0)
        {
          ddlTurno.DataSource = dtTurno;
          ddlTurno.DataTextField = "Turno";
          ddlTurno.DataValueField = "IdTurno";
          ddlTurno.DataBind();
        }

        ddlTurno.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

      }
      catch (Exception ex)
      {
        Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
      }
    }

    protected void ObtenJornada()
    {
      try
      {
        DataTable dtJornada = new DataTable();

        dtJornada = clsQuery.execQueryDataTable("SP_ObtenJornada");
        ddlJornada.Items.Clear();

        if (dtJornada.Rows.Count > 0)
        {
          ddlJornada.DataSource = dtJornada;
          ddlJornada.DataTextField = "Jornada";
          ddlJornada.DataValueField = "IdJornada";
          ddlJornada.DataBind();
        }

        ddlJornada.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

      }
      catch (Exception ex)
      {
        Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
      }
    }

    protected void ObtenHorarios()
    {
      try
      {
        DataTable dtHorarios = new DataTable();

        dtHorarios = clsQuery.execQueryDataTable("SP_ObtenHorarios");
        ddlHorario.Items.Clear();

        if (dtHorarios.Rows.Count > 0)
        {
          ddlHorario.DataSource = dtHorarios;
          ddlHorario.DataTextField = "NomHorario";
          ddlHorario.DataValueField = "IdHorario";
          ddlHorario.DataBind();
        }

        ddlHorario.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

      }
      catch (Exception ex)
      {
        Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
      }
    }

    protected void ObtenPeriodoPago()
    {
      try
      {
        DataTable dtPeriodoPago = new DataTable();

        dtPeriodoPago = clsQuery.execQueryDataTable("SP_ObtenPeriodoPago");

        if (dtPeriodoPago.Rows.Count > 0)
        {
          ddlPeriodoPago.DataSource = dtPeriodoPago;
          ddlPeriodoPago.DataTextField = "Nombre";
          ddlPeriodoPago.DataValueField = "Id_Periodo";
          ddlPeriodoPago.DataBind();
        }

        ddlPeriodoPago.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

      }
      catch (Exception ex)
      {
        Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
      }
    }

    protected void ObtenPagadoras(int idEmpleadora)
    {
      try
      {
        DataTable dtPagadora = new DataTable();

        dtPagadora = BLLEmpleado.ObtenPagadoras(idEmpleadora);
        ddlPagadora.Items.Clear();

        if (dtPagadora.Rows.Count > 0)
        {
          ddlPagadora.DataSource = dtPagadora;
          ddlPagadora.DataTextField = "Alias";
          ddlPagadora.DataValueField = "PagadoraID";
          ddlPagadora.DataBind();
        }

        ddlPagadora.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

      }
      catch (Exception ex)
      {
        Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
      }
    }

    protected void ObtenTipoPago()
    {
      try
      {
        DataTable dtTipoPago = new DataTable();

        dtTipoPago = clsQuery.execQueryDataTable("SP_ObtenTipoPago");

        if (dtTipoPago.Rows.Count > 0)
        {
          ddlTipoPago.DataSource = dtTipoPago;
          ddlTipoPago.DataTextField = "Nombre";
          ddlTipoPago.DataValueField = "TipoPagoID";
          ddlTipoPago.DataBind();
        }

        ddlTipoPago.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

      }
      catch (Exception ex)
      {
        Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
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
      txtCalle.Text = es[15].ToString();
      txtNumero.Text = es[16].ToString();
      txtColonia.Text = es[17].ToString();
      ddlEntidad.SelectedValue = es[18].ToString();
      txtCiudadDel.Text = es[19].ToString();
      txtCP.Text = es[20].ToString();
      ddlClasificacionEmpleado.SelectedItem.Text = es[21].ToString();
      txtDescripcion.Text = es[22].ToString();
      txtDepto.Text = es[23].ToString();
      txtFechaIngreso.Text = es[24].ToString();
      ddlContrato.SelectedValue = es[25].ToString();
      txtDiasC.Text = es[26].ToString();
      ddlTurno.SelectedValue = es[27].ToString();
      ddlJornada.SelectedValue = es[28].ToString();
      ddlHorario.SelectedValue = es[29].ToString();
      ddlPeriodoPago.SelectedValue = es[30].ToString();
      ObtenPagadoras(int.Parse(es[1].ToString()));
      ddlPagadora.SelectedValue = es[31].ToString();
      ddlTipoPago.SelectedValue = es[32].ToString();
    }

    public void Mensaje(string textomensaje, CuadroMensaje.CuadroMensajeIcono icono)
    {
      CuadroMensaje Mensaje = new CuadroMensaje(textomensaje, "MENSAJE", icono, CuadroMensaje.CuadroMensajeBoton.Ok, CuadroMensaje.CuadroMensajeEstilo.EstiloB);
      Literal ltrMensaje = (Literal)this.Master.FindControl("ltrMensaje");
      ltrMensaje.Text = Mensaje.Mostrar(this);
    }
  }
}