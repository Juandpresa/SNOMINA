﻿using EscenariosQnta.Clases;
using EscenariosQnta.Data;
using EscenariosQnta.Negocio;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EscenariosQnta
{
  public partial class wfrmEmpleado : System.Web.UI.Page
  {
    #region Variables

    string Id = string.Empty;
    int Id_Escenario = 1;
    int Id_Empleadora = 0;
    int Id_Cliente = 0;
    string Nombre;
    string Paterno;
    string Materno;
    string Puesto;
    string DescriPto;
    int Id_PrimaRgo = 0;
    DateTime FechaIngreso;
    DateTime FechaNac;
    float Nomina;
    float Asimilados;
    float Honorarios;
    float TN;
    float EZWallet;
    float Sueldo;
    float SueldoBruto;
    float SueldoNeto;
    float SueldoHonorarios;
    float SueldoTN;
    float SueldoEZWallet;
    int Id_Prestac;
    string UbicaLabora = "Hola";
    int Id_Infonavit;
    decimal ImporteInfonavit;
    decimal ImpFonacot;
    decimal Bono;
    decimal ComisionEmpleado;
    decimal OtrosIngresos;
    int Id_Pension;
    decimal ImportePension;
    int Id_EsquemaActual;
    int Id_ClasifEmp;
    string Nacionalidad;
    int TipoEsquema;
    string Cve;
    int RSPagadora;
    int Sexo;
    int TipoPago;
    string curp;
    string rfc;
    string correo;
    string telLocal;
    string telMovil;
    DateTime FechaUltimoPago;
    int PeriodoPago;
    string IdEmpleado;
    string Identificador = string.Empty;
    int IdNEstudios;
    int IdInstituto;
    int IdCarrera;
    int IdBanco;
    string cuenta;
    string clabe;
    string tarjeta;
    DateTime Antiguedad;
    string foto;
    string calle;
    string numero;
    string colonia;
    int estado;
    string ciudad;
    string cp;
    string departamento;
    int turno;
    int horario;
    int jornada;
    int diasContrato;
    int estadoCivil;
    int tipoContrato;
    int idEsquema;
    decimal porcentaje;
    decimal sueldoB;
    decimal sueldoN;
    decimal sueldoD;
    decimal sdi;
    string sb;
    int porc = 0;
    int emp = 0;
    string sp_imss_BaN = "SP_Piramidacion";
    string sp_ASAM = "SP_PiramidacionASAM";
    int contimss = 0;
    string ruta;
    DateTime FechUltimoPago;
    DateTime FechIngreso;
    DateTime? FechNacimiento;

    //bool chkTipoNomina = false;
    string strQuery = string.Empty;
    string strQueryE = string.Empty;
    string strQueryIB = string.Empty;
    string strQueryCV = string.Empty;
    string strQueryEmp = string.Empty;
    string strQueryDE = string.Empty;
    string strQuerySP_Esq = string.Empty;
    string strQuerySP_EMix = string.Empty;
    string RetunValue;
    clsDatos clsQuery = new clsDatos();
    string ValidacionControles = string.Empty;
    DataTable dtTarjeta = new DataTable();
    DataTable dtEsquema = new DataTable();
    DataTable dtHorarios = new DataTable();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        ObtenPrimaRiesgo();
        //ObtenInfonavit();
        //ObtenPrestacion();
        ObtenFactor();
        //ObtenPension();
        //ObtenEsquema();
        ObtenClasificacionEmpleado();
        ObtenTipoEsquema();
        //ObtenEmpleado();
        ObtenSexo();
        ObtenTipoPago();
        ObtenPeriodoPago();
        //ddlEscenario.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
        ObtenCarrera();
        ObtenerNivelE();
        ObtenerInstitucion();
        ObtenBanco();
        //ObtenerEmpleadora();
        ObtenEmpleadoras();
        ObtenEntidad();
        ObtenTipoContrato();
        ObtenEsquema();
        ObtenEsquemaNoMixto();
        ObtenEstadoCivil();
        ObtenHorarios();
        ObtenJornada();
        ObtenTurno();

        grd.DataSource = GetTableWithInitialData(); //get first initial data
        grd.DataBind();
        grd.DataSource = GetGridEsquemas(); //get first initial data
        grd.DataBind();
        grdHorario2.DataSource = GetGridHorarios(); //get first initial data
        grdHorario2.DataBind();
        Session["contador"] = 1;
        //chkObra.Checked = true;
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

    protected void ObtenEstadoCivil()
    {
      try
      {
        DataTable dtEstadoCivil = new DataTable();

        dtEstadoCivil = clsQuery.execQueryDataTable("SP_ObtenEstadoCivil");
        ddlECivil.Items.Clear();

        if (dtEstadoCivil.Rows.Count > 0)
        {
          ddlECivil.DataSource = dtEstadoCivil;
          ddlECivil.DataTextField = "EstadoCivil";
          ddlECivil.DataValueField = "IdEstadoCivil";
          ddlECivil.DataBind();
        }

        ddlECivil.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

      }
      catch (Exception ex)
      {
        Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
      }
    }

    protected void ObtenEsquemaNoMixto()
    {
      try
      {
        DataTable dtEsquemaNM = new DataTable();

        dtEsquemaNM = clsQuery.execQueryDataTable("SP_ObtenEsquemasNoMixto");
        ddlEMixto.Items.Clear();

        if (dtEsquemaNM.Rows.Count > 0)
        {
          ddlEMixto.DataSource = dtEsquemaNM;
          ddlEMixto.DataTextField = "NombreEsquema";
          ddlEMixto.DataValueField = "IdCatEsquemas";
          ddlEMixto.DataBind();
        }

        ddlEMixto.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

      }
      catch (Exception ex)
      {
        Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
      }
    }

    protected void ObtenEsquema()
    {
      try
      {
        DataTable dtEsquema = new DataTable();

        dtEsquema = clsQuery.execQueryDataTable("SP_ObtenerEsquemas");
        ddlEsquemas.Items.Clear();

        if (dtEsquema.Rows.Count > 0)
        {
          ddlEsquemas.DataSource = dtEsquema;
          ddlEsquemas.DataTextField = "NombreEsquema";
          ddlEsquemas.DataValueField = "IdCatEsquemas";
          ddlEsquemas.DataBind();
        }

        ddlEsquemas.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

      }
      catch (Exception ex)
      {
        Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
      }
    }
    public DataTable GetTableWithInitialData() //this might be your sp for select
    {
      DataTable table = new DataTable();
      table.Columns.Add("Banco", typeof(string));
      table.Columns.Add("Cuenta", typeof(string));
      table.Columns.Add("Clabe", typeof(string));
      table.Columns.Add("Tarjeta", typeof(string));
      table.Columns.Add("Prioridad", typeof(string));
      table.Columns.Add("IdBanco", typeof(string));
      return table;
    }
    public DataTable GetGridEsquemas() //this might be your sp for select
    {
      DataTable table = new DataTable();
      table.Columns.Add("Esquema", typeof(string));
      table.Columns.Add("Porcentaje", typeof(string));
      table.Columns.Add("SueldoBruto", typeof(string));
      table.Columns.Add("SueldoNeto", typeof(string));
      table.Columns.Add("SueldoDiario", typeof(string));
      table.Columns.Add("SueldoDiarioI", typeof(string));
      table.Columns.Add("IdEsquema", typeof(string));
      return table;
    }
    public DataTable GetGridHorarios() //this might be your sp for select
    {
      DataTable table = new DataTable();
      table.Columns.Add("Dia", typeof(string));
      table.Columns.Add("Entrada", typeof(string));
      table.Columns.Add("Salida", typeof(string));
      return table;
    }

    public DataTable GetTableWithNoData() //returns only structure if the select columns
    {
      DataTable table = new DataTable();
      table.Columns.Add("Banco", typeof(string));
      table.Columns.Add("Cuenta", typeof(string));
      table.Columns.Add("Clabe", typeof(string));
      table.Columns.Add("Tarjeta", typeof(string));
      table.Columns.Add("Prioridad", typeof(string));
      table.Columns.Add("IdBanco", typeof(string));
      return table;
    }
    public DataTable GetGridEsquemasConDatos() //returns only structure if the select columns
    {
      DataTable table = new DataTable();
      table.Columns.Add("Esquema", typeof(string));
      table.Columns.Add("Porcentaje", typeof(string));
      table.Columns.Add("SueldoBruto", typeof(string));
      table.Columns.Add("SueldoNeto", typeof(string));
      table.Columns.Add("SueldoDiario", typeof(string));
      table.Columns.Add("SueldoDiarioI", typeof(string));
      table.Columns.Add("IdEsquema", typeof(string));
      return table;
    }
    public DataTable GetGridHorariosConDatos() //returns only structure if the select columns
    {
      DataTable table = new DataTable();
      table.Columns.Add("Dia", typeof(string));
      table.Columns.Add("Entrada", typeof(string));
      table.Columns.Add("Salida", typeof(string));
      return table;
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
    //protected void ObtenerEmpleadora()
    //{
    //  DataTable dtEmpleadora = new DataTable();

    //  dtEmpleadora = clsQuery.execQueryDataTable("SP_ObtenerEmpleadoras");

    //  if (dtEmpleadora.Rows.Count > 0)
    //  {
    //    ddlEmpleadora.DataSource = dtEmpleadora;
    //    ddlEmpleadora.DataTextField = "Alias";
    //    ddlEmpleadora.DataValueField = "idRazonSocial";
    //    ddlEmpleadora.DataBind();
    //  }
    //  ddlEmpleadora.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
    //}
    protected void ObtenClave(int idCte)
    {
      IdEmpleado = BLLEmpleado.ObtenerUltimoEmpleado();
      emp = (int.Parse(IdEmpleado)) + 1;
      txtClave.Text = emp.ToString();
      Identificador = BLLEmpleado.ObtenerIdentificador(idCte);
      txtIdentificador.Text = Identificador.ToString();
    }

    protected void ObtenBanco()
    {
      DataTable dtBanco = new DataTable();

      dtBanco = clsQuery.execQueryDataTable("SP_ObtenerBanco");

      if (dtBanco.Rows.Count > 0)
      {
        ddlBanco.DataSource = dtBanco;
        ddlBanco.DataTextField = "NombreBanco";
        ddlBanco.DataValueField = "BancoId";
        ddlBanco.DataBind();
      }
      ddlBanco.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
    }

    protected void ObtenCarrera()
    {
      DataTable dtCarrera = new DataTable();

      dtCarrera = clsQuery.execQueryDataTable("SP_ObtenerCarrera");

      if (dtCarrera.Rows.Count > 0)
      {
        ddlCarrera.DataSource = dtCarrera;
        ddlCarrera.DataTextField = "NombreCarrera";
        ddlCarrera.DataValueField = "CarreraID";
        ddlCarrera.DataBind();
      }
      ddlCarrera.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
    }
    protected void ObtenerNivelE()
    {
      DataTable dtNivelE = new DataTable();

      dtNivelE = clsQuery.execQueryDataTable("SP_ObtenerNivelE");

      if (dtNivelE.Rows.Count > 0)
      {
        ddlNivelE.DataSource = dtNivelE;
        ddlNivelE.DataTextField = "NombreNivelE";
        ddlNivelE.DataValueField = "NivelEstudioId";
        ddlNivelE.DataBind();
      }
      ddlNivelE.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
    }
    protected void ObtenerInstitucion()
    {
      DataTable dtInstitucion = new DataTable();

      dtInstitucion = clsQuery.execQueryDataTable("SP_ObtenerInstituto");

      if (dtInstitucion.Rows.Count > 0)
      {
        ddlInstitucion.DataSource = dtInstitucion;
        ddlInstitucion.DataTextField = "NombreInstituto";
        ddlInstitucion.DataValueField = "InstitutoId";
        ddlInstitucion.DataBind();
      }
      ddlInstitucion.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
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

    //protected void ObtenEscenarioPorCliente(int Id_Cliente)
    //{
    //  try
    //  {
    //    DataTable dtEscenarioByCliente = new DataTable();

    //    strQuery = string.Format("SP_ObtenEscenarioPorCliente {0}", Id_Cliente);

    //    dtEscenarioByCliente = clsQuery.execQueryDataTable(strQuery);

    //    if (dtEscenarioByCliente.Rows.Count > 0)
    //    {
    //      ddlEscenario.DataSource = dtEscenarioByCliente;
    //      ddlEscenario.DataTextField = "Id_Escenario";
    //      ddlEscenario.DataValueField = "Id_Escenario";
    //      ddlEscenario.DataBind();
    //    }

    //    ddlEscenario.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
    //  }
    //  catch (Exception ex)
    //  {
    //    Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
    //  }

    //}

    //SE COMENTA PRIMA DE RIESGO YA QUE SE QUITA DE ALTA DE EMPLEADO 06/11/2019 XD :V
    protected void ObtenPrimaRiesgo()
    {
      try
      {
        DataTable dtPrimaRiesgo = new DataTable();

        dtPrimaRiesgo = clsQuery.execQueryDataTable("SP_ObtenPrimaRiesgo");

        if (dtPrimaRiesgo.Rows.Count > 0)
        {
          ddlPrimaRiesgo.DataSource = dtPrimaRiesgo;
          ddlPrimaRiesgo.DataTextField = "Clase";
          ddlPrimaRiesgo.DataValueField = "Id_Clase";
          ddlPrimaRiesgo.DataBind();
        }
        ddlPrimaRiesgo.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

      }
      catch (Exception ex)
      {
        Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
      }
    }

    //protected void ObtenInfonavit()
    //{
    //  try
    //  {
    //    DataTable dtInfonavit = new DataTable();

    //    dtInfonavit = clsQuery.execQueryDataTable("SP_ObtenInfonavit");

    //    if (dtInfonavit.Rows.Count > 0)
    //    {
    //      ddlInfonavit.DataSource = dtInfonavit;
    //      ddlInfonavit.DataTextField = "TipoInfonavit";
    //      ddlInfonavit.DataValueField = "Id_TpoInfo";
    //      ddlInfonavit.DataBind();
    //    }

    //    ddlInfonavit.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

    //  }
    //  catch (Exception ex)
    //  {
    //    Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
    //  }
    //}


    //protected void ObtenPension()
    //{
    //  try
    //  {
    //    DataTable dtPension = new DataTable();

    //    dtPension = clsQuery.execQueryDataTable("SP_ObtenPensionAlimenticia");

    //    if (dtPension.Rows.Count > 0)
    //    {
    //      ddlPension.DataSource = dtPension;
    //      ddlPension.DataTextField = "Descripcion";
    //      ddlPension.DataValueField = "Id_TpoPensA";
    //      ddlPension.DataBind();
    //    }

    //    ddlPension.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

    //  }
    //  catch (Exception ex)
    //  {
    //    Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
    //  }
    //}

    //protected void ObtenEsquema()
    //{
    //  try
    //  {
    //    DataTable dtEsquemaActual = new DataTable();

    //    dtEsquemaActual = clsQuery.execQueryDataTable("SP_ObtenEsquemaPago");

    //    if (dtEsquemaActual.Rows.Count > 0)
    //    {
    //      ddlEsquemaActual.DataSource = dtEsquemaActual;
    //      ddlEsquemaActual.DataTextField = "Descripcion";
    //      ddlEsquemaActual.DataValueField = "Id_TpoEsq";
    //      ddlEsquemaActual.DataBind();
    //    }

    //    ddlEsquemaActual.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

    //  }
    //  catch (Exception ex)
    //  {
    //    Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
    //  }
    //}

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

    protected void ObtenFactor()
    {
      DataTable dtFactor = new DataTable();

      dtFactor = clsQuery.execQueryDataTable("SP_ObtenFactor");

      if (dtFactor.Rows.Count > 0)
      {
        ddlPrestacion.DataSource = dtFactor;
        ddlPrestacion.DataTextField = "Nombre";
        ddlPrestacion.DataValueField = "Id_Factor";
        ddlPrestacion.DataBind();

        //gvFactor.DataSource = dtFactor;
        //gvFactor.DataBind();
      }

      ddlPrestacion.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

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

    private void LimpiarControles()
    {

      txtNombre.Text = string.Empty;
      txtPaterno.Text = string.Empty;
      txtMaterno.Text = string.Empty;
      //txtPuesto.Text = string.Empty;
      txtDescripcion.Text = string.Empty;
      txtFechaIngreso.Text = string.Empty;
      txtFechaNacimiento.Text = string.Empty;
      //1txtNomina.Text = "0"; //string.Empty;
      //txtAsimilados.Text = "0"; //string.Empty;
      //txtHonorarios.Text = "0"; //string.Empty;            
      //txtTN.Text = "0"; //string.Empty;
      //txtEZWallet.Text = "0"; //string.Empty;
      //2txtSueldo.Text = "0";
      txtSueldoBruto.Text = "0"; //string.Empty;
      txtSueldoNeto.Text = "0"; //string.Empty;
      //txtSueldoHonorarios.Text = "0";
      //txtSueldoTN.Text = "0";
      //txtImporteInfonavit.Text = "0"; //string.Empty;
      //txtBono.Text = "0"; //string.Empty;
      //txtComisionEmp.Text = "0"; //string.Empty;
      //txtOtrosIngresos.Text = "0"; //string.Empty;
      //txtUbicacionLaboral.Text = string.Empty;
      //txtImporteFonacot.Text = "0"; //string.Empty;
      //txtImportePension.Text = "0"; //string.Empty;
                                    //txtNacionalidad.Text = string.Empty;

      rbtTipoEsquema.ClearSelection();

      //txtSueldoBruto.Enabled = false;
      //txtSueldoNeto.Enabled = false;

      
      ObtenPrimaRiesgo();
      //ObtenInfonavit();
      //ObtenPrestacion();
      ObtenFactor();
      //ObtenPension();
      //ObtenEsquema();
      ObtenClasificacionEmpleado();
      ObtenerNivelE();
      ObtenerInstitucion();
      ObtenBanco();
      //ddlEscenario.SelectedIndex = 0;
    }

    //protected void ObtenEmpleado()
    //{

    //    DataTable dtEmpleado = new DataTable();

    //    dtEmpleado = clsQuery.execQueryDataTable("SP_ObtenEmpleado");

    //    if (dtEmpleado.Rows.Count > 0)
    //    {
    //        gvEmpleado.DataSource = dtEmpleado;
    //        gvEmpleado.DataBind();
    //    }
    //}

    //protected void gvEmpleado_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    //{
    //    gvEmpleado.EditIndex = -1;
    //    ObtenEmpleado();
    //}

    //protected void gvEmpleado_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    gvEmpleado.EditIndex = e.NewEditIndex;
    //    ObtenEmpleado();
    //}

    //protected void gvEmpleado_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    try
    //    {
    //        Id = ((Label)gvEmpleado.Rows[e.RowIndex].FindControl("lbId_Empleado")).Text;
    //        Id_Escenario = ((DropDownList)gvEmpleado.Rows[e.RowIndex].FindControl("ddlEscenariogv")).Text;
    //        Id_Cliente = ((DropDownList)gvEmpleado.Rows[e.RowIndex].FindControl("ddlClientegv")).Text;
    //        Nombre = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtNombre")).Text;
    //        Paterno = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtPaterno")).Text;
    //        Materno = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtMaterno")).Text;
    //        Puesto = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtPuesto")).Text;
    //        DescriPto = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtDescriPto")).Text;
    //        Id_PrimaRgo = ((DropDownList)gvEmpleado.Rows[e.RowIndex].FindControl("ddlPrimaRiesgogv")).Text;
    //        FechaIngreso = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtFechaIngreso")).Text;
    //        FechaNac = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtFechaNac")).Text;
    //        Nomina = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtPorcNomina")).Text;
    //        Asimilados = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtPorcAsimilados")).Text;
    //        Honorarios = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtPorcHonorarios")).Text;
    //        OtrosProductos = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtPorcOtrosProductos")).Text;
    //        SueldoBruto = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtSueldoBruto")).Text.Replace(",", ".");
    //        SueldoNeto = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtSueldoNeto")).Text.Replace(",", ".");
    //        SueldoIntegrado = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtSueldoIntegrado")).Text.Replace(",", ".");
    //        Id_Prestac = ((DropDownList)gvEmpleado.Rows[e.RowIndex].FindControl("ddlPrimaRiesgogv")).Text;
    //        UbicaLabora = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtUbicaLabora")).Text;
    //        Id_Infonavit = ((DropDownList)gvEmpleado.Rows[e.RowIndex].FindControl("ddlInfonavitgv")).Text;
    //        ImporteInfonavit = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtImporteInfonvit")).Text.Replace(",", ".");
    //        ImpFonacot = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtImpFonacot")).Text.Replace(",", ".");
    //        Id_Pension = ((DropDownList)gvEmpleado.Rows[e.RowIndex].FindControl("ddlPensiongv")).Text;
    //        ImportePension = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtImportePension")).Text.Replace(",", ".");
    //        Id_EsquemaActual = ((DropDownList)gvEmpleado.Rows[e.RowIndex].FindControl("ddlEsquemagv")).Text;
    //        Id_ClasifEmp = ((DropDownList)gvEmpleado.Rows[e.RowIndex].FindControl("ddlClasificaciongv")).Text;
    //        Nacionalidad = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtNacionalidad")).Text;

    //        FechIngreso = DateTime.Parse(FechaIngreso);
    //        FechNacimiento = DateTime.Parse(FechaNac);

    //        strQuery = string.Format("dbo.SP_ActualizaEmpleado {0}, {1}, '{2}', '{3}', '{4}', '{5}', '{6}', {7}, '{8}', '{9}', '{10}', '{11}', '{12}', '{13}','{14}','{15}','{16}',{17},{18},'{19}',{20},'{21}','{22}',{23},'{24}',{25},{26},'{27}'",
    //        Id, Id_Escenario, Id_Cliente, Nombre, Paterno, Materno, Puesto, DescriPto, Id_PrimaRgo, FechaIngreso, FechaNac, Nomina, Asimilados, Honorarios, OtrosProductos, SueldoBruto, SueldoNeto,
    //          SueldoIntegrado, Id_Prestac, UbicaLabora, Id_Infonavit, ImporteInfonavit, ImpFonacot, Id_Pension, ImportePension, Id_EsquemaActual, Id_ClasifEmp, Nacionalidad);

    //        RetunValue = clsQuery.execQueryString(strQuery);

    //        if (RetunValue == "1")
    //        {
    //            gvEmpleado.EditIndex = -1;
    //            ObtenEmpleado();
    //            Mensaje("ACTUALIZADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
    //    }
    //}

    //protected void gvEmpleado_RowDataBound(object sender, GridViewRowEventArgs e)
    //{

    //    for (int i = 0; i < e.Row.Cells.Count; i++)
    //    {
    //        e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
    //    }

    //    DataTable dtClientes = new DataTable();
    //    DataTable dtEscenario = new DataTable();
    //    DataTable dtPrimaRiesgo = new DataTable();
    //    DataTable dtInfonavit = new DataTable();
    //    DataTable dtPrestacion = new DataTable();
    //    DataTable dtPension = new DataTable();
    //    DataTable dtEsquema = new DataTable();
    //    DataTable dtClasificacion = new DataTable();

    //    dtClientes = clsQuery.execQueryDataTable("SP_ObtenClientes");

    //    dtPrimaRiesgo = clsQuery.execQueryDataTable("SP_ObtenPrimaRiesgo");
    //    dtInfonavit = clsQuery.execQueryDataTable("SP_ObtenInfonavit");
    //    dtPrestacion = clsQuery.execQueryDataTable("SP_ObtenPrestaciones");
    //    dtPension = clsQuery.execQueryDataTable("SP_ObtenPensionAlimenticia");
    //    dtEsquema = clsQuery.execQueryDataTable("SP_ObtenEsquemaPago");
    //    dtClasificacion = clsQuery.execQueryDataTable("SP_ObtenClasificacionEmpleado");

    //    if (e.Row.RowType == DataControlRowType.DataRow && gvEmpleado.EditIndex == e.Row.RowIndex)
    //    {
    //        DropDownList ddlClientegv = (DropDownList)e.Row.FindControl("ddlClientegv");
    //        DropDownList ddlEscenariogv = (DropDownList)e.Row.FindControl("ddlEscenariogv");
    //        DropDownList ddlPrimaRiesgogv = (DropDownList)e.Row.FindControl("ddlPrimaRiesgogv");
    //        DropDownList ddlInfonavitgv = (DropDownList)e.Row.FindControl("ddlInfonavitgv");
    //        DropDownList ddlPrestaciongv = (DropDownList)e.Row.FindControl("ddlPrestaciongv");
    //        DropDownList ddlPensiongv = (DropDownList)e.Row.FindControl("ddlPensiongv");
    //        DropDownList ddlEsquemagv = (DropDownList)e.Row.FindControl("ddlEsquemagv");
    //        DropDownList ddlClasificaciongv = (DropDownList)e.Row.FindControl("ddlClasificaciongv");

    //        string cliente = ((DataRowView)e.Row.DataItem)["Nombre_RazonSocial"].ToString();
    //        string Escenario = ((DataRowView)e.Row.DataItem)["Id_Escenario"].ToString();
    //        string primariesgo = ((DataRowView)e.Row.DataItem)["PrimaRiesgo"].ToString();
    //        string infonavit = ((DataRowView)e.Row.DataItem)["Infonavit"].ToString();
    //        string prestacion = ((DataRowView)e.Row.DataItem)["Prestacion"].ToString();
    //        string pension = ((DataRowView)e.Row.DataItem)["Pension"].ToString();
    //        string esquema = ((DataRowView)e.Row.DataItem)["Esquema"].ToString();
    //        string clasificacion = ((DataRowView)e.Row.DataItem)["Clasificacion"].ToString();

    //        ddlClientegv.DataSource = dtClientes;
    //        ddlClientegv.DataTextField = "Nombre_RazonSocial";
    //        ddlClientegv.DataValueField = "Id_Cliente";
    //        ddlClientegv.DataBind();
    //        ddlClientegv.Items.FindByText(cliente).Selected = true;

    //        dtEscenario = clsQuery.execQueryDataTable("SP_ObtenEscenarioPorCliente " + ddlClientegv.SelectedValue);

    //        ddlEscenariogv.DataSource = dtEscenario;
    //        ddlEscenariogv.DataTextField = "Id_Escenario";
    //        ddlEscenariogv.DataValueField = "Id_Escenario";
    //        ddlEscenariogv.DataBind();

    //        if (dtEscenario.Rows.Count > 0)
    //        {
    //            if (!string.IsNullOrEmpty(Escenario))
    //            {
    //                ddlEscenariogv.Items.FindByText(Escenario).Selected = true;
    //                ddlEscenariogv.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
    //            }
    //        }
    //        else
    //        {
    //            ddlEscenariogv.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
    //        }

    //        ddlPrimaRiesgogv.DataSource = dtPrimaRiesgo;
    //        ddlPrimaRiesgogv.DataTextField = "Clase";
    //        ddlPrimaRiesgogv.DataValueField = "Id_Clase";
    //        ddlPrimaRiesgogv.DataBind();

    //        if (dtPrimaRiesgo.Rows.Count > 0)
    //        {
    //            if (!string.IsNullOrEmpty(primariesgo))
    //            {
    //                ddlPrimaRiesgogv.Items.FindByText(primariesgo).Selected = true;
    //                ddlPrimaRiesgogv.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
    //            }
    //        }
    //        else
    //        {
    //            ddlPrimaRiesgogv.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
    //        }

    //        ddlInfonavitgv.DataSource = dtInfonavit;
    //        ddlInfonavitgv.DataTextField = "Descripcion";
    //        ddlInfonavitgv.DataValueField = "Id_TpoInfo";
    //        ddlInfonavitgv.DataBind();

    //        if (dtInfonavit.Rows.Count > 0)
    //        {
    //            if (!string.IsNullOrEmpty(infonavit))
    //            {
    //                ddlInfonavitgv.Items.FindByText(infonavit).Selected = true;
    //                ddlInfonavitgv.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
    //            }
    //        }
    //        else
    //        {
    //            ddlInfonavitgv.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
    //        }

    //        ddlPrestaciongv.DataSource = dtPrestacion;
    //        ddlPrestaciongv.DataTextField = "Nombre";
    //        ddlPrestaciongv.DataValueField = "Id_Prest";
    //        ddlPrestaciongv.DataBind();

    //        if (dtPrestacion.Rows.Count > 0)
    //        {
    //            if (!string.IsNullOrEmpty(prestacion))
    //            {
    //                ddlPrestaciongv.Items.FindByText(prestacion).Selected = true;
    //                ddlPrestaciongv.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
    //            }
    //        }
    //        else
    //        {
    //            ddlPrestaciongv.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
    //        }

    //        ddlPensiongv.DataSource = dtPension;
    //        ddlPensiongv.DataTextField = "Descripcion";
    //        ddlPensiongv.DataValueField = "Id_TpoPensA";
    //        ddlPensiongv.DataBind();

    //        if (dtPension.Rows.Count > 0)
    //        {
    //            if (!string.IsNullOrEmpty(pension))
    //            {
    //                ddlPensiongv.Items.FindByText(pension).Selected = true;
    //                ddlPensiongv.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
    //            }
    //        }
    //        else
    //        {
    //            ddlPensiongv.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
    //        }

    //        ddlEsquemagv.DataSource = dtEsquema;
    //        ddlEsquemagv.DataTextField = "Descripcion";
    //        ddlEsquemagv.DataValueField = "Id_TpoEsq";
    //        ddlEsquemagv.DataBind();

    //        if (dtEsquema.Rows.Count > 0)
    //        {
    //            if (!string.IsNullOrEmpty(esquema))
    //            {
    //                ddlEsquemagv.Items.FindByText(esquema).Selected = true;
    //                ddlEsquemagv.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
    //            }
    //        }
    //        else
    //        {
    //            ddlEsquemagv.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
    //        }

    //        ddlClasificaciongv.DataSource = dtClasificacion;
    //        ddlClasificaciongv.DataTextField = "Descripcion";
    //        ddlClasificaciongv.DataValueField = "Id_TpoClasEmp";
    //        ddlClasificaciongv.DataBind();

    //        if (dtClasificacion.Rows.Count > 0)
    //        {
    //            if (!string.IsNullOrEmpty(clasificacion))
    //            {
    //                ddlClasificaciongv.Items.FindByText(clasificacion).Selected = true;
    //                ddlClasificaciongv.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
    //            }
    //        }
    //        else
    //        {
    //            ddlClasificaciongv.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
    //        }
    //    }


    //}

    protected void btnGuardar_Click(object sender, EventArgs e)
   {
      
      try
      {
        ValidacionControles = ValidadorControles();

        //if (ValidaPorcentajes())
        //{
        if (string.IsNullOrEmpty(ValidacionControles))
        {
          if (string.IsNullOrEmpty(ValidaSalarios()))
          {

            
              if (fileFoto.HasFile)
              {
                ruta = "~/fotos/" + fileFoto.FileName;
              fileFoto.SaveAs(Server.MapPath(ruta));
              }


              //for (int i = 0; i <= rdlTipoNomina.Items.Count - 1; i++)
              //{
              //    if (rdlTipoNomina.Items[i].Selected == true)
              //    {
              //        if (rdlTipoNomina.SelectedValue.ToString() == "Brutos")
              //        {
              //            Nomina = "100";
              //            Asimilados = "0";
              //        }
              //        else if (rdlTipoNomina.SelectedValue.ToString() == "Netos")
              //        {
              //            Nomina = "0";
              //            Asimilados = "100";
              //        }
              //    }
              //}

              if (rbtTipoEsquema.Items[0].Selected == true)
              {
                  TipoEsquema = 1;
              }
              else
              {
                TipoEsquema = 2;
              }
            

            //Id_Escenario = ddlEscenario.SelectedValue;
            Id_Cliente = int.Parse(ddlCliente.SelectedItem.Value);
            Nombre = txtNombre.Text.ToString();
            Paterno = txtPaterno.Text.ToString();
            Materno = txtMaterno.Text.ToString();
            Puesto = ddlClasificacionEmpleado.SelectedItem.Text;
            DescriPto = txtDescripcion.Text.ToString();
            Id_PrimaRgo = int.Parse(ddlPrimaRiesgo.SelectedItem.Value);
            FechaIngreso = DateTime.Parse(txtFechaIngreso.Text.ToString());
            FechaNac = DateTime.Parse(txtFechaNacimiento.Text.ToString());
            //FechaNac = string.IsNullOrEmpty(txtFechaNacimiento.Text.ToString()) ? null : txtFechaNacimiento.Text.ToString();
            //3Nomina = float.Parse(txtNomina.Text.ToString());
            //Asimilados = float.Parse(txtAsimilados.Text.ToString());
            //Honorarios = float.Parse(txtHonorarios.Text.ToString());
            //TN = float.Parse(txtTN.Text.ToString());
            //EZWallet = float.Parse(txtEZWallet.Text.ToString());
            //OtrosProductos = "0";// txtOtrosProductos.Text.ToString();                        
            //SueldoIntegrado = "0";//txtSueldoIntegrado.Text.ToString();
            //4Sueldo = float.Parse(txtSueldo.Text.ToString());
            SueldoBruto = float.Parse(txtSueldoBruto.Text.ToString());
            SueldoNeto = float.Parse(txtSueldoNeto.Text.ToString());
            //SueldoHonorarios = float.Parse(txtSueldoHonorarios.Text.ToString());
            //SueldoTN = float.Parse(txtSueldoTN.Text.ToString());
            //5SueldoEZWallet = float.Parse(txtSueldoEZWallet.Text.ToString());
            Id_Prestac = int.Parse(ddlPrestacion.SelectedItem.Value);
            //UbicaLabora = txtUbicacionLaboral.Text.ToString();
            //Id_Infonavit = int.Parse(ddlInfonavit.SelectedItem.Value);
            //ImporteInfonavit = decimal.Parse(txtImporteInfonavit.Text.ToString());
            //Bono = decimal.Parse(txtBono.Text.ToString());
            //ComisionEmpleado = decimal.Parse(txtComisionEmp.Text.ToString());
            //OtrosIngresos = decimal.Parse(txtOtrosIngresos.Text.ToString());
            //ImpFonacot = decimal.Parse(txtImporteFonacot.Text.ToString());
            //Id_Pension = int.Parse(ddlPension.SelectedItem.Value);
            //ImportePension = decimal.Parse(txtImportePension.Text.ToString());
            //Id_EsquemaActual = int.Parse(ddlEsquemaActual.SelectedItem.Value);
            Id_ClasifEmp = int.Parse(ddlClasificacionEmpleado.SelectedItem.Value);
            Nacionalidad = txtNacionalidad.Text.ToString();

            Cve = txtIdentificador.Text.ToString() + txtClave.Text.ToString();
            RSPagadora = int.Parse(ddlPagadora.SelectedItem.Value);
            Sexo = int.Parse(ddlSexo.SelectedItem.Value);
            TipoPago = int.Parse(ddlTipoPago.SelectedItem.Value);
            curp = txtCurp.Text.ToString();
            rfc = txtRfc.Text.ToString();
            correo = txtCorreo.Text.ToString();
            telLocal = txtTelefonoLocal.Text.ToString();
            telMovil = txtTelefonoLocal.Text.ToString();
            PeriodoPago = int.Parse(ddlPeriodoPago.SelectedItem.Value);
            IdNEstudios = int.Parse(ddlNivelE.SelectedItem.Value);
            IdInstituto = int.Parse(ddlInstitucion.SelectedItem.Value);
            IdCarrera = int.Parse(ddlCarrera.SelectedItem.Value);
            if (txtAntiguedad.Text == "" || txtAntiguedad.Text == null)
            {
              Antiguedad = DateTime.Now;
            }
            else
            {
              Antiguedad = DateTime.Parse(txtAntiguedad.Text.ToString());
            }
            Id_Empleadora = int.Parse(ddlEmpleadora.SelectedItem.Value);
            foto = ruta;
            calle = txtCalle.Text.ToString();
            numero = txtNumero.Text.ToString();
            colonia = txtColonia.Text.ToString();
            estado = int.Parse(ddlEntidad.SelectedItem.Value);
            ciudad = txtCiudadDel.Text.ToString();
            cp = txtCP.Text.ToString();
            departamento = txtDepto.Text.ToString();
            turno = int.Parse(ddlTurno.SelectedItem.Value);
            horario = int.Parse(ddlHorario.SelectedItem.Value);
            jornada = int.Parse(ddlJornada.SelectedItem.Value);
            diasContrato = int.Parse(txtDiasC.Text.ToString());
            estadoCivil = int.Parse(ddlECivil.SelectedItem.Value);
            tipoContrato = int.Parse(ddlContrato.SelectedItem.Value);

            strQueryCV = BLLEmpleado.ObtenerClavesExistentes(Cve);
            if (strQueryCV != "1")
            {
              if (FechaNac == null)
              {
                //strQuery = string.Format("dbo.SP_InsertaEmpleado {0}, {1}, {2}, '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}',{26},'{27}',{28},{29},'{30}',{31},{32},{33},'{34}',{35},{36},{37},'{38}','{39}','{40}','{41}','{42}','{43}',{44},{45},{46}",
                //    Id_Escenario, Id_Cliente, Id_PrimaRgo, Nombre, Paterno, Materno, Puesto, DescriPto, UbicaLabora, FechaIngreso, FechaNac, Nomina, Asimilados, Honorarios, TN, EZWallet,
                //    Sueldo, SueldoBruto, SueldoNeto, SueldoHonorarios, SueldoTN, SueldoEZWallet,
                //    Bono, ComisionEmpleado, OtrosIngresos, ImpFonacot, Id_Infonavit, ImporteInfonavit, Id_Prestac, Id_Pension, ImportePension, Id_EsquemaActual, Id_ClasifEmp, TipoEsquema, Cve, RSPagadora, Sexo, TipoPago, curp, rfc, correo, telLocal, telMovil, FechaUltimoPago, PeriodoPago, int.Parse(Antiguedad), int.Parse(Id_Empleadora));
              }
              else
              {
                strQueryEmp = BLLEmpleado.InsEmpleado(Id_Escenario, Id_Cliente, Id_PrimaRgo, Nombre, Paterno, Materno, Puesto, DescriPto, FechaIngreso, FechaNac, SueldoBruto, SueldoNeto, Id_Prestac, Id_ClasifEmp, Nacionalidad, TipoEsquema, Cve, RSPagadora, Sexo, TipoPago, curp, rfc, correo, telLocal, telMovil, PeriodoPago, Antiguedad, Id_Empleadora, foto, calle, numero, colonia, estado, ciudad, cp, departamento, turno, horario, jornada, diasContrato, estadoCivil, tipoContrato);
                
              }

              RetunValue = strQueryEmp;
              IdEmpleado = BLLEmpleado.ObtenerUltimoEmpleado();
              emp = int.Parse(IdEmpleado);
       
              strQueryE = BLLGradoAcademico.InsGradoAcademico(emp, IdNEstudios, IdInstituto, IdCarrera);

              foreach (GridViewRow gvr in grd.Rows)
              {
                
                Label txtGCuenta = gvr.FindControl("txtGCuenta") as Label;
                Label txtGClabe = gvr.FindControl("txtGClabe") as Label;
                Label txtGTarjeta = gvr.FindControl("txtGTarjeta") as Label;
                CheckBox chkGPrioridad = gvr.FindControl("chkGPrioridad") as CheckBox;
                Label txtGIdBanco = gvr.FindControl("txtGIdBanco") as Label;
                cuenta = txtGCuenta.Text;
                clabe = txtGClabe.Text;
                tarjeta = txtGTarjeta.Text;
                IdBanco = int.Parse(txtGIdBanco.Text);
                strQueryIB = BLLInfoBancaria.InsInfoBancaria(IdBanco, emp, cuenta, clabe, tarjeta);
              }

              foreach (GridViewRow gvrE in grdEsquemas.Rows)
              {
                Label txtGPorcentaje = gvrE.FindControl("txtPorcentaje") as Label;
                Label txtGSueldoB = gvrE.FindControl("txtSueldoB") as Label;
                Label txtGSueldoN = gvrE.FindControl("txtSueldoN") as Label;
                Label txtGSueldoD = gvrE.FindControl("txtSueldoD") as Label;
                Label txtGSueldoDI = gvrE.FindControl("txtSueldoDI") as Label;
                Label txtGIEsquema = gvrE.FindControl("txtGIEsquema") as Label;
                porcentaje = decimal.Parse(txtGPorcentaje.Text);
                sueldoB = decimal.Parse(txtGSueldoB.Text);
                sueldoN = decimal.Parse(txtGSueldoN.Text);
                sueldoD = decimal.Parse(txtGSueldoD.Text);
                sdi = decimal.Parse(txtGSueldoDI.Text);
                idEsquema = int.Parse(txtGIEsquema.Text);
                strQueryDE = BLLDetalleEsquemas.InsDetalleEsquemas(emp, idEsquema, porcentaje, sueldoB, sueldoN, sueldoD, sdi);
              }


              if (RetunValue == "1")
              {
                LimpiarControles();
                //ObtenEmpleado();
                Mensaje("GUARDADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
              }
            }
            else
            {
              Mensaje("Error: Clave de Empleado Repetida, Intente con otra ", CuadroMensaje.CuadroMensajeIcono.Advertencia);
            }

           
          }
          else
          {
            Mensaje("Error: Debe Ingresar el " + ValidaSalarios(), CuadroMensaje.CuadroMensajeIcono.Error);
          }
          //}
          //else
          //{
          //    Mensaje("Error: El Escenario no tiene Porcentaje. \n Debe ingresarle un porcentaje al empleado", CuadroMensaje.CuadroMensajeIcono.Error);
          //}
        }
        else
        {
          Mensaje(ValidacionControles, CuadroMensaje.CuadroMensajeIcono.Error);
        }

      }
      catch (Exception ex)
      {
        Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
      }
    }

    protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
    {
      //ObtenEscenarioPorCliente(int.Parse(ddlCliente.SelectedValue));
      ObtenClave(int.Parse(ddlCliente.SelectedValue));
    }

    protected void ddlClientegv_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        DropDownList ddlClientegv = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ddlClientegv.Parent.Parent;
        int idx = row.RowIndex;

        DataTable dtEscenarioByCliente = new DataTable();

        strQuery = string.Format("SP_ObtenEscenarioPorCliente {0}", idx);

        dtEscenarioByCliente = clsQuery.execQueryDataTable(strQuery);

        //if (dtEscenarioByCliente.Rows.Count > 0)
        //{
        //    ddlEscenariogv.DataSource = dtEscenarioByCliente;
        //    ddlEscenariogv.DataTextField = "Id_Escenario";
        //    ddlEscenariogv.DataValueField = "Id_Escenario";
        //    ddlEscenariogv.DataBind();
        //}

        //ddlEscenariogv.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
      }
      catch (Exception ex)
      {
        Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
      }
    }

    //protected void ddlEscenario_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //  ValidaPorcentajes();
    //}

    //public void ValidaPorcentajes()
    //{
    //  int returnvalor = 0;

    //  DataTable dtPorcentajes = new DataTable();
    //  string Cliente = ddlCliente.SelectedItem.Value;
    //  string Escenario = ddlEscenario.SelectedItem.Value;

    //  strQuery = string.Format("SP_ObtenPorcentajes {0}, {1}", Cliente, Escenario);
    //  dtPorcentajes = clsQuery.execQueryDataTable(strQuery);

    //  if (dtPorcentajes.Rows.Count > 0)
    //  {
    //    Nomina = dtPorcentajes.Rows[0]["PorcNomina"].ToString();
    //    Asimilados = dtPorcentajes.Rows[0]["PorcAsimilados"].ToString();
    //    Honorarios = dtPorcentajes.Rows[0]["PorcHonorarios"].ToString();
    //    //OtrosProductos = dtPorcentajes.Rows[0]["PorOtrosProductos"].ToString();

    //    //if (Nomina == "0" & Asimilados == "0" & Honorarios == "0" & OtrosProductos == "0")
    //    //{

    //    if (!string.IsNullOrEmpty(txtNomina.Text) && txtNomina.Text != "0")
    //      returnvalor = 1;

    //    if (!string.IsNullOrEmpty(txtAsimilados.Text) && txtAsimilados.Text != "0")
    //      returnvalor = 1;

    //    if (!string.IsNullOrEmpty(txtHonorarios.Text) && txtHonorarios.Text != "0")
    //      returnvalor = 1;

    //    //if (!string.IsNullOrEmpty(txtOtrosProductos.Text) && txtOtrosProductos.Text != "0")
    //    //    returnvalor = 1;
    //    //}
    //    //else
    //    //{
    //    //    returnvalor = 1;
    //    //}

    //    //if (Nomina == "100")
    //    //{
    //    //    //rdlTipoNomina.Items[0].Selected = true;
    //    //    //rdlTipoNomina.Items[1].Selected = false;
    //    //    txtSueldoBruto.Enabled = true;
    //    //    txtSueldoNeto.Enabled = false;
    //    //}
    //    //else if (Asimilados == "100")
    //    //{
    //    //    //rdlTipoNomina.Items[0].Selected = false;
    //    //    //rdlTipoNomina.Items[1].Selected = true;
    //    //    txtSueldoBruto.Enabled = false;
    //    //    txtSueldoNeto.Enabled = true;
    //    //}


    //  }
    //  //return returnvalor;
    //}

    public string ValidaSalarios()
    {
      string returnvalue = string.Empty; ;

      //if (!string.IsNullOrEmpty(txtNomina.Text) & string.IsNullOrEmpty(txtSueldoBruto.Text) && txtSueldoBruto.Text != "0")          
      //returnvalue = "Sueldo Bruto";

      //if (!string.IsNullOrEmpty(txtAsimilados.Text) & string.IsNullOrEmpty(txtSueldoNeto.Text) && txtSueldoNeto.Text != "0")
      //returnvalue = "Sueldo Neto";

      //if (!string.IsNullOrEmpty(txtNomina.Text) & string.IsNullOrEmpty(txtSueldoNeto.Text))
      //    returnvalue = 1;

      //if (!string.IsNullOrEmpty(txtNomina.Text) & string.IsNullOrEmpty(txtSueldoNeto.Text))
      //    returnvalue = 1;

      //if (rdlTipoNomina.Items[0].Selected == true && txtSueldoBruto.Text == "0" || string.IsNullOrEmpty(txtSueldoBruto.Text))
      //    returnvalue = "Sueldo Bruto";

      //if (rdlTipoNomina.Items[1].Selected == true && txtSueldoNeto.Text == "0" || string.IsNullOrEmpty(txtSueldoNeto.Text))
      //    returnvalue = "Sueldo Neto";

      return returnvalue;
    }

    protected string ValidadorControles()
    {
      string returnValidacion = string.Empty;

      //if (ddlEscenario.SelectedItem.Value == "-1")
      //  returnValidacion = "Seleccione un Escenario Por Favor";

      if (ddlCliente.SelectedItem.Value == "-1")
        returnValidacion = "Seleccione un Cliente Por Favor";

      return returnValidacion;
    }

    public void Mensaje(string textomensaje, CuadroMensaje.CuadroMensajeIcono icono)
    {
      CuadroMensaje Mensaje = new CuadroMensaje(textomensaje, "MENSAJE", icono, CuadroMensaje.CuadroMensajeBoton.Ok, CuadroMensaje.CuadroMensajeEstilo.EstiloB);
      Literal ltrMensaje = (Literal)this.Master.FindControl("ltrMensaje");
      ltrMensaje.Text = Mensaje.Mostrar(this);
    }

    protected void chkAntiguedad_CheckedChanged(object sender, EventArgs e)
    {
      if (chkAntiguedad.Checked == true)
      {
        txtAntiguedad.Visible = true;
        lblAntiguedad.Visible = true;
      }
      else
      {
        txtAntiguedad.Visible = false;
        lblAntiguedad.Visible = false;
      }
    }

    protected void ddlEmpleadora_SelectedIndexChanged(object sender, EventArgs e)
    {
      ObtenClientes(int.Parse(ddlEmpleadora.SelectedValue));
      ObtenPagadoras(int.Parse(ddlEmpleadora.SelectedValue));
    }

    protected void btbAddTarjeta_Click(object sender, EventArgs e)
    {
      dtTarjeta = GetTableWithNoData(); //get select column header only records not required
      DataRow dr;
      foreach (GridViewRow gvr in grd.Rows)
      {
        dr = dtTarjeta.NewRow();
        Label txtGBanco = gvr.FindControl("txtGBanco") as Label;
        Label txtGCuenta = gvr.FindControl("txtGCuenta") as Label;
        Label txtGClabe = gvr.FindControl("txtGClabe") as Label;
        Label txtGTarjeta = gvr.FindControl("txtGTarjeta") as Label;
        CheckBox chkGPrioridad = gvr.FindControl("chkGPrioridad") as CheckBox;
        Label txtGIdBanco = gvr.FindControl("txtGIdBanco") as Label;
        dr[0] = txtGBanco.Text;
        dr[1] = txtGCuenta.Text;
        dr[2] = txtGClabe.Text;
        dr[3] = txtGTarjeta.Text;
        dr[4] = chkGPrioridad.Checked == false;
        dr[5] = txtGIdBanco.Text;

        dtTarjeta.Rows.Add(dr); //add grid values in to row and add row to the blank table
      }
      dr = dtTarjeta.NewRow(); //add last empty row
      dr[0] = ddlBanco.SelectedItem.Text;
      dr[1] = txtCuenta.Text;
      dr[2] = txtClabe.Text;
      dr[3] = txtTarjeta.Text;
      dr[4] = true;
      dr[5] = ddlBanco.SelectedItem.Value;
      dtTarjeta.Rows.Add(dr);
      int fil = dtTarjeta.Rows.Count;
      grd.DataSource = dtTarjeta; //bind new datatable to grid
      grd.DataBind();
      
    }

    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
      dtTarjeta = GetTableWithNoData();
      DataRow dr;
      foreach (GridViewRow gvr in grd.Rows)
      {
        dr = dtTarjeta.NewRow();
        Label txtGBanco = gvr.FindControl("txtGBanco") as Label;
        Label txtGCuenta = gvr.FindControl("txtGCuenta") as Label;
        Label txtGClabe = gvr.FindControl("txtGClabe") as Label;
        Label txtGTarjeta = gvr.FindControl("txtGTarjeta") as Label;
        CheckBox chkGPrioridad = gvr.FindControl("chkGPrioridad") as CheckBox;
        Label txtGIdBanco = gvr.FindControl("txtGIdBanco") as Label;
        dr[0] = txtGBanco.Text;
        dr[1] = txtGCuenta.Text;
        dr[2] = txtGClabe.Text;
        dr[3] = txtGTarjeta.Text;
        dr[4] = chkGPrioridad.Checked == false;
        dr[5] = txtGIdBanco.Text;
        dtTarjeta.Rows.Add(dr); //add grid values in to row and add row to the blank table
      }
      dtTarjeta.Rows.RemoveAt(e.RowIndex);
      grd.DataSource = dtTarjeta;
      grd.DataBind();

    }

    protected void btnEsquema_Click(object sender, EventArgs e)
    {
      if (txtAntiguedad.Text == "" || txtAntiguedad.Text == null)
      {
        Antiguedad = DateTime.Now;
      }
      else
      {
        Antiguedad = DateTime.Parse(txtAntiguedad.Text.ToString());
      }
      
      PeriodoPago = int.Parse(ddlPeriodoPago.SelectedItem.Value);
      Id_Prestac = int.Parse(ddlPrestacion.SelectedItem.Value);
      string[] re = new string[3];
      decimal sbruto = decimal.Parse(txtSueldoBruto.Text.ToString());
      decimal sneto = decimal.Parse(txtSueldo.Text.ToString());
      if (int.Parse(ddlEMixto.SelectedItem.Value) > 0)
      {
        strQuerySP_EMix = BLLDetalleEsquemas.ObtenerNomSPEsquema(int.Parse(ddlEMixto.SelectedItem.Value));
      }
      strQuerySP_Esq = BLLDetalleEsquemas.ObtenerNomSPEsquema(int.Parse(ddlEsquemas.SelectedItem.Value));
      
      
        if (strQuerySP_Esq == sp_imss_BaN || strQuerySP_EMix == sp_imss_BaN)
        {
          if (txtSueldoBruto.Text != "0")
          {
            re = BLLDetalleEsquemas.ObtenerSNETO_SD_SDI(sbruto, Antiguedad, PeriodoPago, Id_Prestac);
          }
          else
          {
            re = BLLDetalleEsquemas.ObtenerSBRUTO_SD_SDI(sneto, Antiguedad, PeriodoPago, Id_Prestac);
          }
        }

      if (strQuerySP_Esq == sp_ASAM || strQuerySP_EMix == sp_ASAM)
      {
        re = BLLDetalleEsquemas.ObtenerSBRUTO_ASAM(sneto, Antiguedad, PeriodoPago, Id_Prestac);
      }      

        int TotalP = int.Parse(txtTotalE.Text);
        int res = TotalP + int.Parse(txtPorcentaje.Text);
      if (res <= 100)
      {
        for (int i = 0; i < re.Length; i++)
        {
          sb = re[0];
          sueldoD = decimal.Parse(re[1]);
          sdi = decimal.Parse(re[2]);
        }
      }
      if (res <= 100)
        {
          dtEsquema = GetGridEsquemasConDatos(); //get select column header only records not required
          DataRow dr;

          foreach (GridViewRow gvrE in grdEsquemas.Rows)
          {
            dr = dtEsquema.NewRow();
            Label txtGEsquema = gvrE.FindControl("txtEsquema") as Label;
            Label txtGPorcentaje = gvrE.FindControl("txtPorcentaje") as Label;
            Label txtGSueldoB = gvrE.FindControl("txtSueldoB") as Label;
            Label txtGSueldoN = gvrE.FindControl("txtSueldoN") as Label;
            Label txtGSueldoD = gvrE.FindControl("txtSueldoD") as Label;
            Label txtGSueldoDI = gvrE.FindControl("txtSueldoDI") as Label;
            Label txtGIEsquema = gvrE.FindControl("txtGIEsquema") as Label;
            dr[0] = txtGEsquema.Text;
            dr[1] = txtGPorcentaje.Text;
            dr[2] = txtGSueldoB.Text;
            dr[3] = txtGSueldoN.Text;
            dr[4] = txtGSueldoD.Text;
            dr[5] = txtGSueldoDI.Text;
            dr[6] = txtGIEsquema.Text;

            dtEsquema.Rows.Add(dr); //add grid values in to row and add row to the blank table
          }
          dr = dtEsquema.NewRow(); //add last empty row
          if (ddlEMixto.SelectedIndex > 0)
          {
            dr[0] = ddlEMixto.SelectedItem.Text;
          }
          else
          {
            dr[0] = ddlEsquemas.SelectedItem.Text;
          }
          dr[1] = txtPorcentaje.Text;
          if (txtSueldoBruto.Text != "0")
          {
            dr[2] = txtSueldoBruto.Text;
            dr[3] = sb;
          }
          else
          {
            dr[2] = sb;
            dr[3] = txtSueldo.Text;
          }

          dr[4] = sueldoD;
          dr[5] = sdi;
          dr[6] = ddlEsquemas.SelectedItem.Value;
          dtEsquema.Rows.Add(dr);
          porc = int.Parse(txtTotalE.Text);
          porc = porc + int.Parse(txtPorcentaje.Text);
          txtTotalE.Text = porc.ToString();
          int fil = dtEsquema.Rows.Count;
          grdEsquemas.DataSource = dtEsquema; //bind new datatable to grid
          grdEsquemas.DataBind();

        }
        else
        {
          Mensaje("GUARDADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
        }
        ObtenEsquemaNoMixto();
      ObtenEsquema();
      ObtenFactor();
      txtSueldo.Text = "0";
      txtSueldoBruto.Text = "0";
    }

    protected void ddlHorario_SelectedIndexChanged(object sender, EventArgs e)
    {
      grdHorario2.DataSourceID = null;
      grdHorario2.DataSource = null;
      grdHorario2.DataBind();
      
      DataTable dtHorario = new DataTable();
        DataTable dtDias = new DataTable();
        dtDias.Columns.Add("Dia");
        DataRow dia = dtDias.NewRow();
        dia["Dia"] = "Lunes";
        dtDias.Rows.Add(dia);

      dia = dtDias.NewRow();
      dia["Dia"] = "Martes";
      dtDias.Rows.Add(dia);

      dia = dtDias.NewRow();
      dia["Dia"] = "Miercoles";
      dtDias.Rows.Add(dia);

      dia = dtDias.NewRow();
      dia["Dia"] = "Jueves";
      dtDias.Rows.Add(dia);

      dia = dtDias.NewRow();
      dia["Dia"] = "Viernes";
      dtDias.Rows.Add(dia);

      dia = dtDias.NewRow();
      dia["Dia"] = "Sabado";
      dtDias.Rows.Add(dia);

      dia = dtDias.NewRow();
      dia["Dia"] = "Domingo";
      dtDias.Rows.Add(dia);

      dtHorario = BLLHorario.LlenaGridHorario(int.Parse(ddlHorario.SelectedValue));

      
      dtHorarios = GetGridHorariosConDatos(); //get select column header only records not required
      DataRow dr;

      foreach (GridViewRow gvr in grdHorario2.Rows)
      {
        dr = dtHorarios.NewRow();
        Label txtGDia = gvr.FindControl("txtGDia") as Label;
        Label txtGEntrada = gvr.FindControl("txtGEntrada") as Label;
        Label txtGSalida = gvr.FindControl("txtGSalida") as Label;
        dr[0] = txtGDia.Text;
        dr[1] = txtGEntrada.Text;
        dr[2] = txtGSalida.Text;

        dtHorarios.Rows.Add(dr); //add grid values in to row and add row to the blank table
      }
      for (int i = 0; i < dtDias.Rows.Count; i++)
      {
        dr = dtHorarios.NewRow(); //add last empty row
        dr[0] = dtDias.Rows[i]["Dia"].ToString();
        if (i==0)
        {
          dr[1] = dtHorario.Rows[0][2].ToString();
          dr[2] = dtHorario.Rows[0][9].ToString();
        }
        if (i == 1)
        {
          dr[1] = dtHorario.Rows[0][3].ToString();
          dr[2] = dtHorario.Rows[0][10].ToString();
        }
        if (i == 2)
        {
          dr[1] = dtHorario.Rows[0][4].ToString();
          dr[2] = dtHorario.Rows[0][11].ToString();
        }
        if (i == 3)
        {
          dr[1] = dtHorario.Rows[0][5].ToString();
          dr[2] = dtHorario.Rows[0][12].ToString();
        }
        if (i == 4)
        {
          dr[1] = dtHorario.Rows[0][6].ToString();
          dr[2] = dtHorario.Rows[0][13].ToString();
        }
        if (i == 5)
        {
          dr[1] = dtHorario.Rows[0][7].ToString();
          dr[2] = dtHorario.Rows[0][14].ToString();
        }
        if (i == 6)
        {
          dr[1] = dtHorario.Rows[0][8].ToString();
          dr[2] = dtHorario.Rows[0][15].ToString();
        }

        dtHorarios.Rows.Add(dr);
      }
      
      grdHorario2.DataSource = dtHorarios; //bind new datatable to grid
      grdHorario2.DataBind();
    }

    protected void grdEsquemas_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
      dtEsquema = GetGridEsquemasConDatos();
      DataRow dr;
      foreach (GridViewRow gvrE in grdEsquemas.Rows)
      {
        dr = dtEsquema.NewRow();
        Label txtGEsquema = gvrE.FindControl("txtEsquema") as Label;
        Label txtGPorcentaje = gvrE.FindControl("txtPorcentaje") as Label;
        Label txtGSueldoB = gvrE.FindControl("txtSueldoB") as Label;
        Label txtGSueldoN = gvrE.FindControl("txtSueldoN") as Label;
        Label txtGSueldoD = gvrE.FindControl("txtSueldoD") as Label;
        Label txtGSueldoDI = gvrE.FindControl("txtSueldoDI") as Label;
        Label txtGIEsquema = gvrE.FindControl("txtGIEsquema") as Label;
        dr[0] = txtGEsquema.Text;
        dr[1] = txtGPorcentaje.Text;
        dr[2] = txtGSueldoB.Text;
        dr[3] = txtGSueldoN.Text;
        dr[4] = txtGSueldoD.Text;
        dr[5] = txtGSueldoDI.Text;
        dr[6] = txtGIEsquema.Text;

        dtEsquema.Rows.Add(dr);
      }
      //dr[1] = dtHorario.Rows[0][7].ToString();
      int Porc = int.Parse(dtEsquema.Rows[e.RowIndex][1].ToString());
      txtTotalE.Text = (int.Parse(txtTotalE.Text) - Porc).ToString();
      dtEsquema.Rows.RemoveAt(e.RowIndex);
      grdEsquemas.DataSource = dtEsquema;
      grdEsquemas.DataBind();

    }

    protected void rbtTipoEsquema_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (rbtTipoEsquema.Items[0].Selected == true)
      {
        ddlNivelE.Enabled = false;
        ddlInstitucion.Enabled = false;
        ddlCarrera.Enabled = false;
        ddlPagadora.Enabled = false;
        ddlTipoPago.Enabled = false;
        ddlPrestacion.Enabled = false;
        chkAntiguedad.Enabled = false;
        //txtAntiguedad.Enabled = false;
        ddlEsquemas.Enabled = false;
        ddlEMixto.Enabled = false;
        txtPorcentaje.Enabled = false;
        txtSueldo.Enabled = false;
        txtSueldoBruto.Enabled = false;
        btnEsquema.Enabled = false;
        txtTotalE.Enabled = false;
        grdEsquemas.Enabled = false;
        ddlPrimaRiesgo.Enabled = false;
        txtNacionalidad.Enabled = false;
        ddlECivil.Enabled = false;
      }
      else
      {
        ddlNivelE.Enabled = true;
        ddlInstitucion.Enabled = true;
        ddlCarrera.Enabled = true;
        ddlPagadora.Enabled = true;
        ddlTipoPago.Enabled = true;
        ddlPrestacion.Enabled = true;
        chkAntiguedad.Enabled = true;
        //txtAntiguedad.Enabled = false;
        ddlEsquemas.Enabled = true;
        ddlEMixto.Enabled = true;
        txtPorcentaje.Enabled = true;
        txtSueldo.Enabled = true;
        txtSueldoBruto.Enabled = true;
        btnEsquema.Enabled = true;
        txtTotalE.Enabled = true;
        grdEsquemas.Enabled = true;
        ddlPrimaRiesgo.Enabled = true;
        txtNacionalidad.Enabled = true;
        ddlECivil.Enabled = true;
      }
    }

    protected void chkObra_CheckedChanged(object sender, EventArgs e)
    {
      //if (chkObra.Checked == true)
      //{
      //  txtCentroCostos.Enabled = true;
      //  txtFIObra.Enabled = true;
      //  txtFFObra.Enabled = true;
      //  txtDiasTobra.Enabled = true;
      //  txtTObra.Enabled = true;
      //  txtUbicacionO.Enabled = true;
      //  txtCentroCostos.Visible = false;
      //}
      //else
      //{
      //  txtCentroCostos.Enabled = false;
      //  txtFIObra.Enabled = false;
      //  txtFFObra.Enabled = false;
      //  txtDiasTobra.Enabled = false;
      //  txtTObra.Enabled = false;
      //  txtUbicacionO.Enabled = false;
      //}
    }
  }
}