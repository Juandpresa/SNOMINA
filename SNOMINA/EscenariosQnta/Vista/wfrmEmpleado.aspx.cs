using EscenariosQnta.Clases;
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
    string Id_Escenario = "1";
    string Id_Empleadora = string.Empty;
    string Id_Cliente = string.Empty;
    string Nombre = string.Empty;
    string Paterno = string.Empty;
    string Materno = string.Empty;
    string Puesto = string.Empty;
    string DescriPto = string.Empty;
    string Id_PrimaRgo = string.Empty;
    string FechaIngreso = string.Empty;
    string FechaNac = string.Empty;
    string Nomina = string.Empty;
    string Asimilados = string.Empty;
    string Honorarios = string.Empty;
    string TN = string.Empty;
    string EZWallet = string.Empty;
    string Sueldo = string.Empty;
    string SueldoBruto = string.Empty;
    string SueldoNeto = string.Empty;
    string SueldoHonorarios = string.Empty;
    string SueldoTN = string.Empty;
    string SueldoEZWallet = string.Empty;
    string Id_Prestac = string.Empty;
    string UbicaLabora = string.Empty;
    string Id_Infonavit = string.Empty;
    string ImporteInfonavit = string.Empty;
    string ImpFonacot = string.Empty;
    string Bono = string.Empty;
    string ComisionEmpleado = string.Empty;
    string OtrosIngresos = string.Empty;
    string Id_Pension = string.Empty;
    string ImportePension = string.Empty;
    string Id_EsquemaActual = string.Empty;
    string Id_ClasifEmp = string.Empty;
    string Nacionalidad = string.Empty;
    string SueldoMensual = string.Empty;
    string SueldoDiario = string.Empty;
    int TipoEsquema = 0;
    string Cve = string.Empty;
    string RSPagadora = string.Empty;
    string Sexo = string.Empty;
    string TipoPago = string.Empty;
    string curp = string.Empty;
    string rfc = string.Empty;
    string correo = string.Empty;
    string telLocal = string.Empty;
    string telMovil = string.Empty;
    string FechaUltimoPago = string.Empty;
    string PeriodoPago = string.Empty;
    string IdEmpleado = string.Empty;
    string Identificador = string.Empty;
    string IdNEstudios = string.Empty;
    string IdInstituto = string.Empty;
    string IdCarrera = string.Empty;
    string IdBanco = string.Empty;
    string cuenta = string.Empty;
    string clabe = string.Empty;
    string tarjeta = string.Empty;
    string Antiguedad = string.Empty;

    int emp = 0;

    DateTime FechUltimoPago;
    DateTime FechIngreso;
    DateTime? FechNacimiento;

    //bool chkTipoNomina = false;
    string strQuery = string.Empty;
    string strQueryE = string.Empty;
    string strQueryIB = string.Empty;
    string strQueryCV = string.Empty;
    string strQueryEmp = string.Empty;
    string RetunValue;
    clsDatos clsQuery = new clsDatos();
    string ValidacionControles = string.Empty;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        ObtenPrimaRiesgo();
        ObtenInfonavit();
        //ObtenPrestacion();
        ObtenFactor();
        ObtenPension();
        ObtenEsquema();
        ObtenClasificacionEmpleado();
        ObtenTipoEsquema();
        //ObtenEmpleado();
        ObtenPagadoras();
        ObtenSexo();
        ObtenTipoPago();
        ObtenPeriodoPago();
        //ddlEscenario.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
        ObtenCarrera();
        ObtenerNivelE();
        ObtenerInstitucion();
        ObtenBanco();
        ObtenerEmpleadora();
        ObtenEmpleadoras();
        ObtenEntidad();
        ObtenTipoContrato();
      }
    }

    protected void ObtenTipoContrato()
    {
      try
      {
        DataTable dtTipoContrato = new DataTable();

        dtTipoContrato = clsQuery.execQueryDataTable("SP_ObtenTipoContrato");

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
    protected void ObtenerEmpleadora()
    {
      DataTable dtEmpleadora = new DataTable();

      dtEmpleadora = clsQuery.execQueryDataTable("SP_ObtenerEmpleadoras");

      if (dtEmpleadora.Rows.Count > 0)
      {
        ddlEmpleadora.DataSource = dtEmpleadora;
        ddlEmpleadora.DataTextField = "Alias";
        ddlEmpleadora.DataValueField = "idRazonSocial";
        ddlEmpleadora.DataBind();
      }
      ddlEmpleadora.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
    }
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

    protected void ObtenClientes(int idEmpleadora)
    {
      try
      {
        DataTable dtCliente = new DataTable();

        dtCliente = BLLEmpleado.ObtenClientes(idEmpleadora);

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

    protected void ObtenInfonavit()
    {
      try
      {
        DataTable dtInfonavit = new DataTable();

        dtInfonavit = clsQuery.execQueryDataTable("SP_ObtenInfonavit");

        if (dtInfonavit.Rows.Count > 0)
        {
          ddlInfonavit.DataSource = dtInfonavit;
          ddlInfonavit.DataTextField = "TipoInfonavit";
          ddlInfonavit.DataValueField = "Id_TpoInfo";
          ddlInfonavit.DataBind();
        }

        ddlInfonavit.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

      }
      catch (Exception ex)
      {
        Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
      }
    }

    //protected void ObtenPrestacion()
    //{
    //    try
    //    {
    //        DataTable dtPrestacion = new DataTable();

    //        dtPrestacion = clsQuery.execQueryDataTable("SP_ObtenPrestaciones");

    //        if (dtPrestacion.Rows.Count > 0)
    //        {
    //            ddlPrestacion.DataSource = dtPrestacion;
    //            ddlPrestacion.DataTextField = "Nombre";
    //            ddlPrestacion.DataValueField = "Id_Prest";
    //            ddlPrestacion.DataBind();
    //        }

    //        ddlPrestacion.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

    //    }
    //    catch (Exception ex)
    //    {
    //        Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);            
    //    }
    //}

    protected void ObtenPension()
    {
      try
      {
        DataTable dtPension = new DataTable();

        dtPension = clsQuery.execQueryDataTable("SP_ObtenPensionAlimenticia");

        if (dtPension.Rows.Count > 0)
        {
          ddlPension.DataSource = dtPension;
          ddlPension.DataTextField = "Descripcion";
          ddlPension.DataValueField = "Id_TpoPensA";
          ddlPension.DataBind();
        }

        ddlPension.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

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
        DataTable dtEsquemaActual = new DataTable();

        dtEsquemaActual = clsQuery.execQueryDataTable("SP_ObtenEsquemaPago");

        if (dtEsquemaActual.Rows.Count > 0)
        {
          ddlEsquemaActual.DataSource = dtEsquemaActual;
          ddlEsquemaActual.DataTextField = "Descripcion";
          ddlEsquemaActual.DataValueField = "Id_TpoEsq";
          ddlEsquemaActual.DataBind();
        }

        ddlEsquemaActual.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

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

    protected void ObtenFactor()
    {
      DataTable dtFactor = new DataTable();

      dtFactor = clsQuery.execQueryDataTable("SP_ObtenFactor");

      if (dtFactor.Rows.Count > 0)
      {
        //ddlPrestacion.DataSource = dtFactor;
        //ddlPrestacion.DataTextField = "Nombre";
        //ddlPrestacion.DataValueField = "Id_Factor";
        //ddlPrestacion.DataBind();

        //gvFactor.DataSource = dtFactor;
        //gvFactor.DataBind();
      }

      //ddlPrestacion.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

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
      txtNomina.Text = "0"; //string.Empty;
      txtAsimilados.Text = "0"; //string.Empty;
      txtHonorarios.Text = "0"; //string.Empty;            
      txtTN.Text = "0"; //string.Empty;
      txtEZWallet.Text = "0"; //string.Empty;
      txtSueldo.Text = "0";
      txtSueldoBruto.Text = "0"; //string.Empty;
      txtSueldoNeto.Text = "0"; //string.Empty;
      txtSueldoHonorarios.Text = "0";
      txtSueldoTN.Text = "0";
      txtImporteInfonavit.Text = "0"; //string.Empty;
      txtBono.Text = "0"; //string.Empty;
      txtComisionEmp.Text = "0"; //string.Empty;
      txtOtrosIngresos.Text = "0"; //string.Empty;
      //txtUbicacionLaboral.Text = string.Empty;
      txtImporteFonacot.Text = "0"; //string.Empty;
      txtImportePension.Text = "0"; //string.Empty;
                                    //txtNacionalidad.Text = string.Empty;

      rbtTipoEsquema.ClearSelection();

      //txtSueldoBruto.Enabled = false;
      //txtSueldoNeto.Enabled = false;

      
      ObtenPrimaRiesgo();
      ObtenInfonavit();
      //ObtenPrestacion();
      ObtenFactor();
      ObtenPension();
      ObtenEsquema();
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

            for (int i = 0; i <= rbtTipoEsquema.Items.Count - 1; i++)
            {
              if (rbtTipoEsquema.Items[i].Selected == true)
              {
                //if (rbtTipoEsquema.SelectedItem.Text.ToString() == "Alta Simple")
                //{
                  TipoEsquema = 1;
                //}
                //else if (rbtTipoEsquema.SelectedItem.Text.ToString() == "Alta Completa")
                //{
                  //TipoEsquema = 2;
                //}
              }
              else
              {
                TipoEsquema = 2;
              }
            }

            //Id_Escenario = ddlEscenario.SelectedValue;
            Id_Cliente = ddlCliente.SelectedItem.Value;
            Nombre = txtNombre.Text.ToString();
            Paterno = txtPaterno.Text.ToString();
            Materno = txtMaterno.Text.ToString();
            Puesto = ddlClasificacionEmpleado.SelectedItem.Text;
            DescriPto = txtDescripcion.Text.ToString();
            Id_PrimaRgo = ddlPrimaRiesgo.SelectedItem.Value;
            FechaIngreso = txtFechaIngreso.Text.ToString();
            FechaNac = txtFechaNacimiento.Text.ToString();
            //FechaNac = string.IsNullOrEmpty(txtFechaNacimiento.Text.ToString()) ? null : txtFechaNacimiento.Text.ToString();
            Nomina = txtNomina.Text.ToString();
            Asimilados = txtAsimilados.Text.ToString();
            Honorarios = txtHonorarios.Text.ToString();
            TN = txtTN.Text.ToString();
            EZWallet = txtEZWallet.Text.ToString();
            //OtrosProductos = "0";// txtOtrosProductos.Text.ToString();                        
            //SueldoIntegrado = "0";//txtSueldoIntegrado.Text.ToString();
            Sueldo = txtSueldo.Text.ToString();
            SueldoBruto = txtSueldoBruto.Text.ToString();
            SueldoNeto = txtSueldoNeto.Text.ToString();
            SueldoHonorarios = txtSueldoHonorarios.Text.ToString();
            SueldoTN = txtSueldoTN.Text.ToString();
            SueldoEZWallet = txtSueldoEZWallet.Text.ToString();
            //Id_Prestac = ddlPrestacion.SelectedItem.Value;
            //UbicaLabora = txtUbicacionLaboral.Text.ToString();
            Id_Infonavit = ddlInfonavit.SelectedItem.Value;
            ImporteInfonavit = txtImporteInfonavit.Text.ToString();
            Bono = txtBono.Text.ToString();
            ComisionEmpleado = txtComisionEmp.Text.ToString();
            OtrosIngresos = txtOtrosIngresos.Text.ToString();
            ImpFonacot = txtImporteFonacot.Text.ToString();
            Id_Pension = ddlPension.SelectedItem.Value;
            ImportePension = txtImportePension.Text.ToString();
            Id_EsquemaActual = ddlEsquemaActual.SelectedItem.Value;
            Id_ClasifEmp = ddlClasificacionEmpleado.SelectedItem.Value;
            //Nacionalidad = txtNacionalidad.Text.ToString();

            Cve = txtIdentificador.Text.ToString() + txtClave.Text.ToString();
            RSPagadora = ddlPagadora.SelectedItem.Value;
            Sexo = ddlSexo.SelectedItem.Value;
            TipoPago = ddlTipoPago.SelectedItem.Value;
            curp = txtCurp.Text.ToString();
            rfc = txtRfc.Text.ToString();
            correo = txtCorreo.Text.ToString();
            telLocal = txtTelefonoLocal.Text.ToString();
            telMovil = txtTelefonoLocal.Text.ToString();
            FechaUltimoPago = txtUltimoPago.Text.ToString();
            PeriodoPago = ddlPeriodoPago.SelectedItem.Value;
            IdNEstudios = ddlNivelE.SelectedItem.Value;
            IdInstituto = ddlInstitucion.SelectedItem.Value;
            IdCarrera = ddlCarrera.SelectedItem.Value;
            Antiguedad = txtAntiguedad.Text.ToString();

            Id_Empleadora = ddlEmpleadora.SelectedItem.Value;

            strQueryCV = BLLEmpleado.ObtenerClavesExistentes(Cve);
            if (strQueryCV != "1")
            {
              if (FechaNac == null)
              {
                strQuery = string.Format("dbo.SP_InsertaEmpleado {0}, {1}, {2}, '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}',{26},'{27}',{28},{29},'{30}',{31},{32},{33},'{34}',{35},{36},{37},'{38}','{39}','{40}','{41}','{42}','{43}',{44},{45},{46}",
                    Id_Escenario, Id_Cliente, Id_PrimaRgo, Nombre, Paterno, Materno, Puesto, DescriPto, UbicaLabora, FechaIngreso, FechaNac, Nomina, Asimilados, Honorarios, TN, EZWallet,
                    Sueldo, SueldoBruto, SueldoNeto, SueldoHonorarios, SueldoTN, SueldoEZWallet,
                    Bono, ComisionEmpleado, OtrosIngresos, ImpFonacot, Id_Infonavit, ImporteInfonavit, Id_Prestac, Id_Pension, ImportePension, Id_EsquemaActual, Id_ClasifEmp, TipoEsquema, Cve, RSPagadora, Sexo, TipoPago, curp, rfc, correo, telLocal, telMovil, FechaUltimoPago, PeriodoPago, int.Parse(Antiguedad), int.Parse(Id_Empleadora));
              }
              else
              {
                strQueryEmp = BLLEmpleado.InsEmpleado(int.Parse(Id_Escenario), int.Parse(Id_Cliente), int.Parse(Id_PrimaRgo), Nombre, Paterno, Materno, Puesto, DescriPto, UbicaLabora, FechaIngreso, FechaNac, Nomina, Asimilados, Honorarios, TN, EZWallet,
                    Sueldo, SueldoBruto, SueldoNeto, SueldoHonorarios, SueldoTN, SueldoEZWallet,
                    Bono, ComisionEmpleado, OtrosIngresos, ImpFonacot, int.Parse(Id_Infonavit), ImporteInfonavit, int.Parse(Id_Prestac), int.Parse(Id_Pension), ImportePension, int.Parse(Id_EsquemaActual), int.Parse(Id_ClasifEmp), TipoEsquema, Cve, int.Parse(RSPagadora), int.Parse(Sexo), int.Parse(TipoPago), curp, rfc, correo, telLocal, telMovil, FechaUltimoPago, int.Parse(PeriodoPago), int.Parse(Antiguedad), int.Parse(Id_Empleadora));
                
              }

              RetunValue = strQueryEmp;
              IdEmpleado = BLLEmpleado.ObtenerUltimoEmpleado();
              emp = int.Parse(IdEmpleado);
              int ne = int.Parse(IdNEstudios);
              int ins = int.Parse(IdInstituto);
              int carr = int.Parse(IdCarrera);
              strQueryE = BLLGradoAcademico.InsGradoAcademico(emp, ne, ins, carr);

              for (int i = 0; i <= tblBancos.Rows.Count; i++)
              {
                // Iterate through the cells of a row.
                for (int j = 0; j <= tblBancos.Rows[i].Cells.Count - 1; j++)
                {
                  if (j == 1)
                  {
                    cuenta = tblBancos.Rows[i + 1].Cells[j].InnerText;
                  }
                  if (j == 2)
                  {
                    clabe = tblBancos.Rows[i + 1].Cells[j].InnerText;
                  }
                  if (j == 3)
                  {
                    tarjeta = tblBancos.Rows[i + 1].Cells[j].InnerText;
                  }
                  if (j == 4)
                  {
                    IdBanco = tblBancos.Rows[i + 1].Cells[j].InnerText;
                  }
                }
                int idBan = int.Parse(IdBanco);
                strQueryIB = BLLInfoBancaria.InsInfoBancaria(idBan, emp, cuenta, clabe, tarjeta);
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
    }
  }
}