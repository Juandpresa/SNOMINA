using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using EscenariosQnta.Data;
using EscenariosQnta.Clases;
using System.Globalization;
using System.Web.Services;

namespace EscenariosQnta
{
    public partial class wfrmEmpleadoConsulta : System.Web.UI.Page
    {

        #region Variables

        string Id = string.Empty;
        string Id_Escenario = string.Empty;
        string Id_Cliente = string.Empty;
        String Nombre = string.Empty;
        String Paterno = string.Empty;
        String Materno = string.Empty;
        string Puesto = string.Empty;
        string DescriPto = string.Empty;
        string Id_PrimaRgo = string.Empty;
        string FechaIngreso = string.Empty;
        string FechaNac = null;
        string Nomina = string.Empty;
        string Asimilados = string.Empty;
        string Honorarios = string.Empty;
        string TN = string.Empty;
        string EZWallet = string.Empty;
        //string OtrosProductos = string.Empty;
        string Sueldo = string.Empty;
        string SueldoBruto = string.Empty;
        string SueldoNeto = string.Empty;
        string SueldoHonorarios = string.Empty;
        string SueldoTN = string.Empty;
        string SueldoEZWallet = string.Empty;
        //string SueldoIntegrado = string.Empty;        
        string Id_Prestac = string.Empty;
        string UbicaLabora = string.Empty;
        string Id_Infonavit = string.Empty;
        string ImporteInfonavit = string.Empty;
        string Bono = string.Empty;
        string ComisionEmpleado = string.Empty;
        string OtrosIngresos = string.Empty;
        string ImpFonacot = string.Empty;
        string Id_Pension = string.Empty;
        string ImportePension = string.Empty;
        string Id_EsquemaActual = string.Empty;
        string Id_ClasifEmp = string.Empty;
        string Nacionalidad = string.Empty;
        DateTime FechIngreso;
        DateTime? FechNacimiento;
        string TipoEsquema = string.Empty;

        String strQuery = string.Empty;
        string RetunValue;
        clsDatos clsQuery = new clsDatos();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ObtenEmpleado();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            ObtenEmpleadoPorNombre();
        }

        protected void ObtenEmpleado()
        {

            DataTable dtEmpleado = new DataTable();

            dtEmpleado = clsQuery.execQueryDataTable("SP_ObtenEmpleado");

            if (dtEmpleado.Rows.Count > 0)
            {
                gvEmpleado.DataSource = dtEmpleado;
                gvEmpleado.DataBind();
            }
        }

        protected void ObtenEmpleadoPorNombre()
        {

            DataTable dtEmpleado = new DataTable();

            Nombre = string.IsNullOrEmpty(txtNombre.Text.ToString()) ? null : txtNombre.Text.ToString();
            Paterno = string.IsNullOrEmpty(txtPaterno.Text.ToString()) ? null : txtPaterno.Text.ToString();
            Materno = string.IsNullOrEmpty(txtMaterno.Text.ToString()) ? null : txtMaterno.Text.ToString();

            strQuery = string.Format("SP_ObtenEmpleadoPorNombre '{0}', '{1}', '{2}'", Nombre, Paterno, Materno);

            dtEmpleado = clsQuery.execQueryDataTable(strQuery);

            if (dtEmpleado.Rows.Count > 0)
            {
                gvEmpleado.DataSource = dtEmpleado;
                gvEmpleado.DataBind();
            }
        }

        protected void gvEmpleado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ObtenEmpleado();
            gvEmpleado.PageIndex = e.NewPageIndex;
            gvEmpleado.DataBind();
        }

        protected void gvEmpleado_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEmpleado.EditIndex = -1;
            ObtenEmpleado();
        }

        protected void gvEmpleado_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEmpleado.EditIndex = e.NewEditIndex;
            if (txtNombre.Text == string.Empty && txtPaterno.Text == string.Empty && txtMaterno.Text == string.Empty)
            {

                ObtenEmpleado();
            }
            else
            {
                DataTable dtEmpleado = new DataTable();

                Nombre = string.IsNullOrEmpty(txtNombre.Text.ToString()) ? null : txtNombre.Text.ToString();
                Paterno = string.IsNullOrEmpty(txtPaterno.Text.ToString()) ? null : txtPaterno.Text.ToString();
                Materno = string.IsNullOrEmpty(txtMaterno.Text.ToString()) ? null : txtMaterno.Text.ToString();

                strQuery = string.Format("SP_ObtenEmpleadoPorNombre '{0}', '{1}', '{2}'", Nombre, Paterno, Materno);

                dtEmpleado = clsQuery.execQueryDataTable(strQuery);

                if (dtEmpleado.Rows.Count > 0)
                {
                    gvEmpleado.DataSource = dtEmpleado;
                    gvEmpleado.DataBind();
                }
            }
        }

        protected void gvEmpleado_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Id = ((Label)gvEmpleado.Rows[e.RowIndex].FindControl("lbId_Empleado")).Text;
                Id_Escenario = ((DropDownList)gvEmpleado.Rows[e.RowIndex].FindControl("ddlEscenariogv")).Text;
                Id_Cliente = ((DropDownList)gvEmpleado.Rows[e.RowIndex].FindControl("ddlClientegv")).Text;
                Nombre = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtNombre")).Text;
                Paterno = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtPaterno")).Text;
                Materno = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtMaterno")).Text;
                Puesto = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtPuesto")).Text;
                DescriPto = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtDescriPto")).Text;
                Id_PrimaRgo = ((DropDownList)gvEmpleado.Rows[e.RowIndex].FindControl("ddlPrimaRiesgogv")).Text;
                FechaIngreso = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtFechaIngreso")).Text;
                FechaNac = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtFechaNac")).Text;
                Nomina = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtPorcNomina")).Text;
                Asimilados = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtPorcAsimilados")).Text;
                Honorarios = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtPorcHonorarios")).Text;
                TN = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtPorcTN")).Text;
                EZWallet = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtPorcEZWallet")).Text;
                Sueldo = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtSueldo")).Text;
                //OtrosProductos = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtPorOtrosProductos")).Text;
                SueldoBruto = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtSueldoBruto")).Text.Replace(",", ".");
                SueldoNeto = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtSueldoNeto")).Text.Replace(",", ".");
                SueldoHonorarios = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtSueldoHonorarios")).Text.Replace(",", ".");
                SueldoTN = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtSueldoTN")).Text.Replace(",", ".");
                SueldoEZWallet = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtSueldoEZWallet")).Text.Replace(",", ".");
                //SueldoIntegrado = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtSueldoIntegrado")).Text.Replace(",", ".");
                Id_Prestac = ((DropDownList)gvEmpleado.Rows[e.RowIndex].FindControl("ddlPrimaRiesgogv")).Text;
                UbicaLabora = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtUbicaLabora")).Text;
                Id_Infonavit = ((DropDownList)gvEmpleado.Rows[e.RowIndex].FindControl("ddlInfonavitgv")).Text;
                ImporteInfonavit = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtImporteInfonavit")).Text.Replace(",", ".");
                Bono = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtBono")).Text.Replace(",", ".");
                ComisionEmpleado = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtComisionEmpleado")).Text.Replace(",", ".");
                OtrosIngresos = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtOtrosIngresos")).Text.Replace(",", ".");
                ImpFonacot = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtImpFonacot")).Text.Replace(",", ".");
                Id_Pension = ((DropDownList)gvEmpleado.Rows[e.RowIndex].FindControl("ddlPensiongv")).Text;
                ImportePension = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtImportePension")).Text.Replace(",", ".");
                Id_EsquemaActual = ((DropDownList)gvEmpleado.Rows[e.RowIndex].FindControl("ddlEsquemagv")).Text;
                Id_ClasifEmp = ((DropDownList)gvEmpleado.Rows[e.RowIndex].FindControl("ddlClasificaciongv")).Text;
                Nacionalidad = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtNacionalidad")).Text;
                
                TipoEsquema = ((RadioButtonList)gvEmpleado.Rows[e.RowIndex].FindControl("rbtTipoEsquema")).SelectedValue;

                CultureInfo provider = CultureInfo.InvariantCulture;

                FechIngreso = DateTime.Parse(FechaIngreso);
                FechNacimiento = string.IsNullOrEmpty(FechaNac) ? (DateTime?)null : DateTime.Parse(FechaNac);

                if (FechNacimiento == null)
                {
                    //strQuery = string.Format("dbo.SP_ActualizaEmpleado {0}, {1}, {2}, '{3}', '{4}', '{5}', '{6}', '{7}', {8}, '{9}', '{10}', '{11}', '{12}', '{13}','{14}','{15}','{16}',{17},{18},'{19}',{20},'{21}','{22}','{23}',{24},'{25}',{26},{27},'{28}','{29}','{30}'",
                    //Id, Id_Escenario, Id_Cliente, Nombre, Paterno, Materno, Puesto, DescriPto, Id_PrimaRgo, FechIngreso.ToString("yyyyMMdd HH:mm:ss"), FechNacimiento, Nomina, Asimilados, Honorarios, OtrosProductos, SueldoBruto, SueldoNeto,
                    //  SueldoIntegrado, Id_Prestac, UbicaLabora, Id_Infonavit, ImporteInfonvit, Bono, ComisionesEmpleado, OtrosIngresos, ImpFonacot, Id_Pension, ImportePension, Id_EsquemaActual, Id_ClasifEmp, Nacionalidad);
                    strQuery = string.Format("dbo.SP_ActualizaEmpleado {0}, {1}, {2}, {3}, '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}',{26},'{27}',{28},{29},'{30}',{31},{32},{33},'{34}', {35}",
                              Id, Id_Escenario, Id_Cliente, Id_PrimaRgo, Nombre, Paterno, Materno, Puesto, DescriPto, UbicaLabora, FechIngreso.ToString("yyyyMMdd HH:mm:ss"), FechNacimiento, Nomina, Asimilados, Honorarios, TN, EZWallet,
                              Sueldo, SueldoBruto, SueldoNeto, SueldoHonorarios, SueldoTN, SueldoEZWallet,
                              Bono, ComisionEmpleado, OtrosIngresos, ImpFonacot, Id_Infonavit, ImporteInfonavit, Id_Prestac, Id_Pension, ImportePension, Id_EsquemaActual, Id_ClasifEmp, Nacionalidad, TipoEsquema);
                }
                else
                {
                    //strQuery = string.Format("dbo.SP_ActualizaEmpleado {0}, {1}, {2}, '{3}', '{4}', '{5}', '{6}', '{7}', {8}, '{9}', '{10}', '{11}', '{12}', '{13}','{14}','{15}','{16}',{17},{18},'{19}',{20},'{21}','{22}','{23}',{24},'{25}',{26},{27},'{28}','{29}','{30}'",
                    //Id, Id_Escenario, Id_Cliente, Nombre, Paterno, Materno, Puesto, DescriPto, Id_PrimaRgo, FechIngreso.ToString("yyyyMMdd HH:mm:ss"), FechNacimiento.Value.ToString("yyyyMMdd"), Nomina, Asimilados, Honorarios, OtrosProductos, SueldoBruto, SueldoNeto,
                    //SueldoIntegrado, Id_Prestac, UbicaLabora, Id_Infonavit, ImporteInfonvit, Bono, ComisionesEmpleado, OtrosIngresos, ImpFonacot, Id_Pension, ImportePension, Id_EsquemaActual, Id_ClasifEmp, Nacionalidad);

                    strQuery = string.Format("dbo.SP_ActualizaEmpleado {0}, {1}, {2}, {3}, '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}',{26},'{27}',{28},{29},'{30}',{31},{32},{33},'{34}', {35}",
                            Id, Id_Escenario, Id_Cliente, Id_PrimaRgo, Nombre, Paterno, Materno, Puesto, DescriPto, UbicaLabora, FechIngreso.ToString("yyyyMMdd HH:mm:ss"), FechNacimiento.Value.ToString("yyyyMMdd"), Nomina, Asimilados, Honorarios, TN, EZWallet,
                            Sueldo, SueldoBruto, SueldoNeto, SueldoHonorarios, SueldoTN, SueldoEZWallet,
                            Bono, ComisionEmpleado, OtrosIngresos, ImpFonacot, Id_Infonavit, ImporteInfonavit, Id_Prestac, Id_Pension, ImportePension, Id_EsquemaActual, Id_ClasifEmp, Nacionalidad, TipoEsquema);
                }

                RetunValue = clsQuery.execQueryString(strQuery);

                if (RetunValue == "1")
                {
                    gvEmpleado.EditIndex = -1;
                    ObtenEmpleado();
                    Mensaje("ACTUALIZADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
                }
            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
            }
        }

        protected void gvEmpleado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }


            if ((e.Row.RowType == DataControlRowType.Header) && gvEmpleado.EditIndex == e.Row.RowIndex)
            {
                //adding an attribut for onclick event on the check box in the hearder and passing the ClientID of the Select All checkbox 
                ((CheckBox)e.Row.FindControl("checkAll")).Attributes.Add("onclick", "javascript:SelectAll('" + ((CheckBox)e.Row.FindControl("checkAll")).ClientID + "')");
            }

            DataTable dtClientes = new DataTable();
            DataTable dtEscenario = new DataTable();
            DataTable dtPrimaRiesgo = new DataTable();
            DataTable dtInfonavit = new DataTable();
            DataTable dtPrestacion = new DataTable();
            DataTable dtPension = new DataTable();
            DataTable dtEsquema = new DataTable();
            DataTable dtClasificacion = new DataTable();
            DataTable dtTipoEsquema = new DataTable();

            dtClientes = clsQuery.execQueryDataTable("SP_ObtenClientes");

            dtPrimaRiesgo = clsQuery.execQueryDataTable("SP_ObtenPrimaRiesgo");
            dtInfonavit = clsQuery.execQueryDataTable("SP_ObtenInfonavit");
            dtPrestacion = clsQuery.execQueryDataTable("SP_ObtenPrestaciones");
            dtPension = clsQuery.execQueryDataTable("SP_ObtenPensionAlimenticia");
            dtEsquema = clsQuery.execQueryDataTable("SP_ObtenEsquemaPago");
            dtClasificacion = clsQuery.execQueryDataTable("SP_ObtenClasificacionEmpleado");
            dtTipoEsquema = clsQuery.execQueryDataTable("Sp_ObtenTipoEsquema");

            if (e.Row.RowType == DataControlRowType.DataRow && gvEmpleado.EditIndex == e.Row.RowIndex)
            {
                DropDownList ddlClientegv = (DropDownList)e.Row.FindControl("ddlClientegv");
                DropDownList ddlEscenariogv = (DropDownList)e.Row.FindControl("ddlEscenariogv");
                DropDownList ddlPrimaRiesgogv = (DropDownList)e.Row.FindControl("ddlPrimaRiesgogv");
                DropDownList ddlInfonavitgv = (DropDownList)e.Row.FindControl("ddlInfonavitgv");
                DropDownList ddlPrestaciongv = (DropDownList)e.Row.FindControl("ddlPrestaciongv");
                DropDownList ddlPensiongv = (DropDownList)e.Row.FindControl("ddlPensiongv");
                DropDownList ddlEsquemagv = (DropDownList)e.Row.FindControl("ddlEsquemagv");
                DropDownList ddlClasificaciongv = (DropDownList)e.Row.FindControl("ddlClasificaciongv");
                RadioButtonList rbtTipoEsquema = (RadioButtonList)e.Row.FindControl("rbtTipoEsquema");

                string cliente = ((DataRowView)e.Row.DataItem)["Nombre_RazonSocial"].ToString();
                string Escenario = ((DataRowView)e.Row.DataItem)["Id_Escenario"].ToString();
                string primariesgo = ((DataRowView)e.Row.DataItem)["PrimaRiesgo"].ToString();
                string infonavit = ((DataRowView)e.Row.DataItem)["Infonavit"].ToString();
                string prestacion = ((DataRowView)e.Row.DataItem)["Prestacion"].ToString();
                string pension = ((DataRowView)e.Row.DataItem)["Pension"].ToString();
                string esquema = ((DataRowView)e.Row.DataItem)["Esquema"].ToString();
                string clasificacion = ((DataRowView)e.Row.DataItem)["Clasificacion"].ToString();
                string TipoEsquema = ((DataRowView)e.Row.DataItem)["TipoEsquema"].ToString();

                if (dtClientes.Rows.Count > 0)
                {
                    ddlClientegv.DataSource = dtClientes;
                    ddlClientegv.DataTextField = "Nombre_RazonSocial";
                    ddlClientegv.DataValueField = "Id_Cliente";
                    ddlClientegv.DataBind();
                    ddlClientegv.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));


                    if (!string.IsNullOrEmpty(cliente))
                    {
                        ddlClientegv.Items.FindByText(cliente).Selected = true;
                    }

                }

                dtEscenario = clsQuery.execQueryDataTable("SP_ObtenEscenarioPorCliente " + ddlClientegv.SelectedValue);

                if (dtEscenario.Rows.Count > 0)
                {
                    ddlEscenariogv.DataSource = dtEscenario;
                    ddlEscenariogv.DataTextField = "Id_Escenario";
                    ddlEscenariogv.DataValueField = "Id_Escenario";
                    ddlEscenariogv.DataBind();

                    if (!string.IsNullOrEmpty(Escenario))
                    {
                        ddlEscenariogv.Items.FindByText(Escenario).Selected = true;
                    }
                }
                ddlEscenariogv.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

                if (dtPrimaRiesgo.Rows.Count > 0)
                {
                    ddlPrimaRiesgogv.DataSource = dtPrimaRiesgo;
                    ddlPrimaRiesgogv.DataTextField = "Clase";
                    ddlPrimaRiesgogv.DataValueField = "Id_Clase";
                    ddlPrimaRiesgogv.DataBind();

                    if (!string.IsNullOrEmpty(primariesgo))
                    {
                        ddlPrimaRiesgogv.Items.FindByText(primariesgo).Selected = true;
                    }

                }
                ddlPrimaRiesgogv.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));


                if (dtInfonavit.Rows.Count > 0)
                {
                    ddlInfonavitgv.DataSource = dtInfonavit;
                    ddlInfonavitgv.DataTextField = "TipoInfonavit";
                    ddlInfonavitgv.DataValueField = "Id_TpoInfo";
                    ddlInfonavitgv.DataBind();

                    if (!string.IsNullOrEmpty(infonavit))
                    {
                        ddlInfonavitgv.Items.FindByText(infonavit).Selected = true;
                    }
                }
                ddlInfonavitgv.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

                if (dtPrestacion.Rows.Count > 0)
                {
                    ddlPrestaciongv.DataSource = dtPrestacion;
                    ddlPrestaciongv.DataTextField = "Nombre";
                    ddlPrestaciongv.DataValueField = "Id_Prest";
                    ddlPrestaciongv.DataBind();

                    if (!string.IsNullOrEmpty(prestacion))
                    {
                        ddlPrestaciongv.Items.FindByText(prestacion).Selected = true;
                    }
                }
                ddlPrestaciongv.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

                if (dtPension.Rows.Count > 0)
                {
                    ddlPensiongv.DataSource = dtPension;
                    ddlPensiongv.DataTextField = "Descripcion";
                    ddlPensiongv.DataValueField = "Id_TpoPensA";
                    ddlPensiongv.DataBind();

                    if (!string.IsNullOrEmpty(pension))
                    {
                        ddlPensiongv.Items.FindByText(pension).Selected = true;
                    }
                }
                ddlPensiongv.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

                if (dtEsquema.Rows.Count > 0)
                {
                    ddlEsquemagv.DataSource = dtEsquema;
                    ddlEsquemagv.DataTextField = "Descripcion";
                    ddlEsquemagv.DataValueField = "Id_TpoEsq";
                    ddlEsquemagv.DataBind();

                    if (!string.IsNullOrEmpty(esquema))
                    {
                        ddlEsquemagv.Items.FindByText(esquema).Selected = true;
                    }
                }
                ddlEsquemagv.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

                if (dtClasificacion.Rows.Count > 0)
                {
                    ddlClasificaciongv.DataSource = dtClasificacion;
                    ddlClasificaciongv.DataTextField = "Descripcion";
                    ddlClasificaciongv.DataValueField = "Id_TpoClasEmp";
                    ddlClasificaciongv.DataBind();

                    if (!string.IsNullOrEmpty(clasificacion))
                    {
                        ddlClasificaciongv.Items.FindByText(clasificacion).Selected = true;

                    }
                }
                ddlClasificaciongv.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
                

                if (dtTipoEsquema.Rows.Count > 0)
                {
                    rbtTipoEsquema.DataSource = dtTipoEsquema;
                    rbtTipoEsquema.DataTextField = "TipoEsquema";
                    rbtTipoEsquema.DataValueField = "Id_TipoEsquema";
                    rbtTipoEsquema.DataBind();
                }

                rbtTipoEsquema.Items.FindByText(TipoEsquema).Selected = true;

                if (TipoEsquema == "Porcentaje")
                {
                    ((TextBox)e.Row.FindControl("txtPorcNomina")).Enabled = true;
                    ((TextBox)e.Row.FindControl("txtPorcAsimilados")).Enabled = true;
                    ((TextBox)e.Row.FindControl("txtPorcHonorarios")).Enabled = true;
                    ((TextBox)e.Row.FindControl("txtPorcNomina")).Enabled = true;
                    ((TextBox)e.Row.FindControl("txtPorcTN")).Enabled = true;
                    ((TextBox)e.Row.FindControl("txtPorcEZWallet")).Enabled = true;
                    ((TextBox)e.Row.FindControl("txtSueldo")).Enabled = true;

                    ((TextBox)e.Row.FindControl("txtSueldoBruto")).Enabled = false;
                    ((TextBox)e.Row.FindControl("txtSueldoNeto")).Enabled = false;
                    ((TextBox)e.Row.FindControl("txtSueldoHonorarios")).Enabled = false;
                    ((TextBox)e.Row.FindControl("txtSueldoTN")).Enabled = false;
                    ((TextBox)e.Row.FindControl("txtSueldoEZWallet")).Enabled = false;
                    
                    
                }
                else
                {
                    ((TextBox)e.Row.FindControl("txtPorcNomina")).Enabled = false;
                    ((TextBox)e.Row.FindControl("txtPorcAsimilados")).Enabled = false;
                    ((TextBox)e.Row.FindControl("txtPorcHonorarios")).Enabled = false;
                    ((TextBox)e.Row.FindControl("txtPorcNomina")).Enabled = false;
                    ((TextBox)e.Row.FindControl("txtPorcTN")).Enabled = false;
                    ((TextBox)e.Row.FindControl("txtPorcEZWallet")).Enabled = false;
                    ((TextBox)e.Row.FindControl("txtSueldo")).Enabled = false;

                    ((TextBox)e.Row.FindControl("txtSueldoBruto")).Enabled = true;
                    ((TextBox)e.Row.FindControl("txtSueldoNeto")).Enabled = true;
                    ((TextBox)e.Row.FindControl("txtSueldoHonorarios")).Enabled = true;
                    ((TextBox)e.Row.FindControl("txtSueldoTN")).Enabled = true;
                    ((TextBox)e.Row.FindControl("txtSueldoEZWallet")).Enabled = true;
                }

            }
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

        protected void btnBorrar_Click(object sender, EventArgs e)
        {
            //CuadroMensaje Mensaje = new CuadroMensaje("","", CuadroMensaje.CuadroMensajeIcono.Advertencia, CuadroMensaje.CuadroMensajeBoton.OKCancel, CuadroMensaje.CuadroMensajeEstilo.EstiloB);
            //Literal ltrMensaje = (Literal)this.Master.FindControl("ltrMensaje");
            //Mensaje.SuccessEvent.Add("OkClick");
            //Mensaje.SuccessEvent.Add("CancelClick");
            //ltrMensaje.Text = Mensaje.Mostrar(this);
            BorrarEmpleado();
        }

        //[WebMethod]
        //public static string OkClick(object sender, EventArgs e)
        //{
        //    //BorrarEmpleado();
        //} 

        protected void BorrarEmpleado()
        {
            foreach (GridViewRow row in gvEmpleado.Rows)
            {
                CheckBox cellSelecion = (CheckBox)row.FindControl("chkSeleccion");

                if (Convert.ToBoolean(cellSelecion.Checked))
                {
                    Id = ((Label)row.Cells[1].FindControl("lbId_Empleado")).Text;
                    strQuery = string.Format("SP_BorrarEmpleado {0}", Id);
                    clsQuery.execQueryString(strQuery);
                }
            }            

            ObtenEmpleado();

            Mensaje("Borrardo", CuadroMensaje.CuadroMensajeIcono.Exitoso);

        }

        protected void rbtTipoEsquema_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioButtonList rbtTipoEsquema = (RadioButtonList)gvEmpleado.FindControl("rbtTipoEsquema");

            TipoEsquema = ((RadioButtonList)gvEmpleado.Rows[gvEmpleado.EditIndex].FindControl("rbtTipoEsquema")).SelectedValue;

            if (TipoEsquema == "1")
            {
                ((TextBox)gvEmpleado.Rows[gvEmpleado.EditIndex].FindControl("txtPorcNomina")).Enabled = true;
                ((TextBox)gvEmpleado.Rows[gvEmpleado.EditIndex].FindControl("txtPorcAsimilados")).Enabled = true;
                ((TextBox)gvEmpleado.Rows[gvEmpleado.EditIndex].FindControl("txtPorcHonorarios")).Enabled = true;
                ((TextBox)gvEmpleado.Rows[gvEmpleado.EditIndex].FindControl("txtPorcTN")).Enabled = true;
                ((TextBox)gvEmpleado.Rows[gvEmpleado.EditIndex].FindControl("txtPorcEZWallet")).Enabled = true;
                ((TextBox)gvEmpleado.Rows[gvEmpleado.EditIndex].FindControl("txtSueldo")).Enabled = true;

                ((TextBox)gvEmpleado.Rows[gvEmpleado.EditIndex].FindControl("txtSueldoBruto")).Enabled = false;
                ((TextBox)gvEmpleado.Rows[gvEmpleado.EditIndex].FindControl("txtSueldoNeto")).Enabled = false;
                ((TextBox)gvEmpleado.Rows[gvEmpleado.EditIndex].FindControl("txtSueldoHonorarios")).Enabled = false;
                ((TextBox)gvEmpleado.Rows[gvEmpleado.EditIndex].FindControl("txtSueldoTN")).Enabled = false;
                ((TextBox)gvEmpleado.Rows[gvEmpleado.EditIndex].FindControl("txtSueldoEZWallet")).Enabled = false;

            }
            else
            {
                ((TextBox)gvEmpleado.Rows[gvEmpleado.EditIndex].FindControl("txtPorcNomina")).Enabled = false;
                ((TextBox)gvEmpleado.Rows[gvEmpleado.EditIndex].FindControl("txtPorcAsimilados")).Enabled = false;
                ((TextBox)gvEmpleado.Rows[gvEmpleado.EditIndex].FindControl("txtPorcHonorarios")).Enabled = false;
                ((TextBox)gvEmpleado.Rows[gvEmpleado.EditIndex].FindControl("txtPorcTN")).Enabled = false;
                ((TextBox)gvEmpleado.Rows[gvEmpleado.EditIndex].FindControl("txtPorcEZWallet")).Enabled = false;
                ((TextBox)gvEmpleado.Rows[gvEmpleado.EditIndex].FindControl("txtSueldo")).Enabled = false;

                ((TextBox)gvEmpleado.Rows[gvEmpleado.EditIndex].FindControl("txtSueldoBruto")).Enabled = true;
                ((TextBox)gvEmpleado.Rows[gvEmpleado.EditIndex].FindControl("txtSueldoNeto")).Enabled = true;
                ((TextBox)gvEmpleado.Rows[gvEmpleado.EditIndex].FindControl("txtSueldoHonorarios")).Enabled = true;
                ((TextBox)gvEmpleado.Rows[gvEmpleado.EditIndex].FindControl("txtSueldoTN")).Enabled = true;
                ((TextBox)gvEmpleado.Rows[gvEmpleado.EditIndex].FindControl("txtSueldoEZWallet")).Enabled = true;
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