using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EscenariosQnta.Data;
using System.Configuration;

namespace EscenariosQnta
{
    public partial class wfrmDatosEmpleado : System.Web.UI.Page
    {
        #region Variables

        string Id = string.Empty;
        string Id_Escenario = string.Empty;
        string Id_Cliente = string.Empty;
        string Nombre = string.Empty;
        string Paterno = string.Empty;
        string Materno = string.Empty;
        string Puesto = string.Empty;
        string DescriPto = string.Empty;
        string Id_PrimaRgo = string.Empty;
        string FechaIngreso = string.Empty;
        string FechaNac = string.Empty;
        string SueldoBruto = string.Empty;
        string SueldoNeto = string.Empty;
        string SueldoIntegrado = string.Empty;
        string Id_Prestac = string.Empty;
        string UbicaLabora = string.Empty;
        string Id_Infonavit = string.Empty;
        string ImpFonacot = string.Empty;
        string Id_Pension = string.Empty;
        string ImportePension = string.Empty;
        string Id_EsquemaActual = string.Empty;
        string Id_ClasifEmp = string.Empty;
        string Nacionalidad = string.Empty;

        string strQuery = string.Empty;
        string RetunValue;
        clsDatos clsQuery = new clsDatos();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenClientes();
                ObtenPrimaRiesgo();
                ObtenInfonavit();
                ObtenPrestacion();
                ObtenPension();
                ObtenEsquema();
                ObtenClasificacionEmpleado();
                ObtenEmpleado();
            }
        }

        protected void ObtenClientes()
        {
            try
            {
                DataTable dtCliente = new DataTable();

                dtCliente = clsQuery.execQueryDataTable("SP_ObtenClientes");

                if (dtCliente.Rows.Count > 0)
                {
                    ddlCliente.DataSource = dtCliente;
                    ddlCliente.DataTextField = "Nombre_RazonSocial";
                    ddlCliente.DataValueField = "Id_Cliente";
                    ddlCliente.DataBind();

                    ddlCliente.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "0"));
                }

            }
            catch (Exception ex)
            {

            }
        }

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
                ddlPrimaRiesgo.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "0"));

            }
            catch (Exception ex)
            {

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
                    ddlInfonavit.DataTextField = "Descripcion";
                    ddlInfonavit.DataValueField = "Id_TpoInfo";
                    ddlInfonavit.DataBind();
                }

                ddlInfonavit.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "0"));

            }
            catch (Exception ex)
            {

            }
        }

        protected void ObtenPrestacion()
        {
            try
            {
                DataTable dtPrestacion = new DataTable();

                dtPrestacion = clsQuery.execQueryDataTable("SP_ObtenPrestaciones");

                if (dtPrestacion.Rows.Count > 0)
                {
                    ddlPrestacion.DataSource = dtPrestacion;
                    ddlPrestacion.DataTextField = "Nombre";
                    ddlPrestacion.DataValueField = "Id_Prest";
                    ddlPrestacion.DataBind();
                }

                ddlPrestacion.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "0"));

            }
            catch (Exception ex)
            {

            }
        }

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

                ddlPension.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "0"));

            }
            catch (Exception ex)
            {

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

                ddlEsquemaActual.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "0"));

            }
            catch (Exception ex)
            {

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

                ddlClasificacionEmpleado.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "0"));

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {


                Id_Escenario = txtEscenario.Text.ToString();
                Id_Cliente = ddlCliente.SelectedItem.Value;
                Nombre = txtNombre.Text.ToString();
                Paterno = txtPaterno.Text.ToString();
                Materno = txtMaterno.Text.ToString();
                Puesto = txtPuesto.Text.ToString();
                DescriPto = txtDescripcion.Text.ToString();
                Id_PrimaRgo = ddlPrimaRiesgo.SelectedItem.Value;
                FechaIngreso = txtFechaIngreso.Text.ToString();
                FechaNac = txtFechaNacimiento.Text.ToString();
                SueldoBruto = txtSueldoBruto.Text.ToString();
                SueldoNeto = txtSueldoNeto.Text.ToString();
                SueldoIntegrado = txtSueldoIntegrado.Text.ToString();
                Id_Prestac = ddlPrestacion.SelectedItem.Value;
                UbicaLabora = txtUbicacionLaboral.Text.ToString();
                Id_Infonavit = ddlInfonavit.SelectedItem.Value;
                ImpFonacot = txtImporteFonacot.Text.ToString();
                Id_Pension = ddlPension.SelectedItem.Value;
                ImportePension = txtImportePension.Text.ToString();
                Id_EsquemaActual = ddlEsquemaActual.SelectedItem.Value;
                Id_ClasifEmp = ddlClasificacionEmpleado.SelectedItem.Value;
                Nacionalidad = txtNacionalidad.Text.ToString();

                strQuery = string.Format("dbo.SP_InsertaEmpleado {0}, {1}, '{2}', '{3}', '{4}', '{5}', '{6}', {7}, '{8}', '{9}', '{10}', '{11}', '{12}', {13},'{14}',{15},'{16}',{17},'{18}',{19},{20},'{21}'",
                    Id_Escenario, Id_Cliente, Nombre, Paterno, Materno, Puesto, DescriPto, Id_PrimaRgo, FechaIngreso, FechaNac, SueldoBruto, SueldoNeto,
    SueldoIntegrado, Id_Prestac, UbicaLabora, Id_Infonavit, ImpFonacot, Id_Pension, ImportePension, Id_EsquemaActual, Id_ClasifEmp, Nacionalidad);

                RetunValue = clsQuery.execQueryString(strQuery);

                if (RetunValue == "1")
                {

                    LimpiarControles();
                    Response.Write("<script>alert('Guardado');</script>");
                    ObtenEmpleado();
                }

            }
            catch (Exception ex)
            {

                ex.ToString();

            }
        }

        private void LimpiarControles()
        {
            txtEscenario.Text = string.Empty;
            //ddlCliente.SelectedItem.Value;
            txtNombre.Text = string.Empty;
            txtPaterno.Text = string.Empty;
            txtMaterno.Text = string.Empty;
            txtPuesto.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            //ddlPrimaRiesgo.SelectedItem.Value;
            txtFechaIngreso.Text = string.Empty;
            txtFechaNacimiento.Text = string.Empty;
            txtSueldoBruto.Text = string.Empty;
            txtSueldoNeto.Text = string.Empty;
            txtSueldoIntegrado.Text = string.Empty;
            //ddlPrestacion.SelectedItem.Value;
            txtUbicacionLaboral.Text = string.Empty;
            //ddlInfonavit.SelectedItem.Value;
            txtImporteFonacot.Text = string.Empty;
            // ddlPension.SelectedItem.Value;
            txtImportePension.Text = string.Empty;
            //ddlEsquemaActual.SelectedItem.Value;
            //ddlClasificacionEmpleado.SelectedItem.Value;
            txtNacionalidad.Text = string.Empty;

            //ObtenEntidad();
            //ObtenEjecutivos();
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

        protected void gvEmpleado_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEmpleado.EditIndex = -1;
            ObtenEmpleado();
        }

        protected void gvEmpleado_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEmpleado.EditIndex = e.NewEditIndex;
            ObtenEmpleado();
        }

        protected void gvEmpleado_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {              
                Id = ((Label)gvEmpleado.Rows[e.RowIndex].FindControl("lbId_Empleado")).Text;
                Id_Escenario = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtExcenario")).Text;
                Id_Cliente = ((DropDownList)gvEmpleado.Rows[e.RowIndex].FindControl("ddlClientegv")).Text;
                Nombre = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtNombre")).Text;
                Paterno = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtPaterno")).Text;
                Materno = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtMaterno")).Text;
                Puesto = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtPuesto")).Text;
                DescriPto = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtDescriPto")).Text;
                Id_PrimaRgo = ((DropDownList)gvEmpleado.Rows[e.RowIndex].FindControl("ddlPrimaRiesgogv")).Text;
                FechaIngreso = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtFechaIngreso")).Text;
                FechaNac = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtFechaNac")).Text;
                SueldoBruto = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtSueldoBruto")).Text.Replace(",", ".");
                SueldoNeto = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtSueldoNeto")).Text.Replace(",", ".");
                SueldoIntegrado = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtSueldoIntegrado")).Text.Replace(",", ".");
                Id_Prestac = ((DropDownList)gvEmpleado.Rows[e.RowIndex].FindControl("ddlPrimaRiesgogv")).Text;
                UbicaLabora = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtUbicaLabora")).Text;
                Id_Infonavit = ((DropDownList)gvEmpleado.Rows[e.RowIndex].FindControl("ddlInfonavitgv")).Text;
                ImpFonacot = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtImpFonacot")).Text.Replace(",", ".");
                Id_Pension = ((DropDownList)gvEmpleado.Rows[e.RowIndex].FindControl("ddlPensiongv")).Text;
                ImportePension = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtImportePension")).Text.Replace(",", ".");
                Id_EsquemaActual = ((DropDownList)gvEmpleado.Rows[e.RowIndex].FindControl("ddlEsquemagv")).Text;
                Id_ClasifEmp = ((DropDownList)gvEmpleado.Rows[e.RowIndex].FindControl("ddlClasificaciongv")).Text;
                Nacionalidad = ((TextBox)gvEmpleado.Rows[e.RowIndex].FindControl("txtNacionalidad")).Text;

                strQuery = string.Format("SP_ActualizaEmpleado {0}, '{1}', {2}, '{3}', '{4}', '{5}', '{6}', '{7}', {8}, '{9}', '{10}', '{11}', '{12}', '{13}',{14},'{15}',{16},'{17}',{18},'{19}',{20},{21},'{22}'",
                   Id, Id_Escenario, Id_Cliente, Nombre, Paterno, Materno, Puesto, DescriPto, Id_PrimaRgo, FechaIngreso, FechaNac, SueldoBruto, SueldoNeto,
                    SueldoIntegrado, Id_Prestac, UbicaLabora, Id_Infonavit, ImpFonacot, Id_Pension, ImportePension, Id_EsquemaActual, Id_ClasifEmp, Nacionalidad);

                RetunValue = clsQuery.execQueryString(strQuery);

                if (RetunValue == "1")
                {

                    LimpiarControles();
                    Response.Write("<script>alert('Guardado');</script>");
                    ObtenEmpleado();
                    gvEmpleado.EditIndex = -1;
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void gvEmpleado_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            DataTable dtClientes = new DataTable();
            DataTable dtPrimaRiesgo = new DataTable();
            DataTable dtInfonavit = new DataTable();
            DataTable dtPrestacion = new DataTable();
            DataTable dtPension = new DataTable();
            DataTable dtEsquema = new DataTable();
            DataTable dtClasificacion = new DataTable();

            dtClientes = clsQuery.execQueryDataTable("SP_ObtenClientes");
            dtPrimaRiesgo = clsQuery.execQueryDataTable("SP_ObtenPrimaRiesgo");
            dtInfonavit = clsQuery.execQueryDataTable("SP_ObtenInfonavit");
            dtPrestacion = clsQuery.execQueryDataTable("SP_ObtenPrestaciones");
            dtPension = clsQuery.execQueryDataTable("SP_ObtenPensionAlimenticia");
            dtEsquema = clsQuery.execQueryDataTable("SP_ObtenEsquemaPago");
            dtClasificacion = clsQuery.execQueryDataTable("SP_ObtenClasificacionEmpleado");

            if (e.Row.RowType == DataControlRowType.DataRow && gvEmpleado.EditIndex == e.Row.RowIndex)
            {
                DropDownList ddlClientegv = (DropDownList)e.Row.FindControl("ddlClientegv");
                DropDownList ddlPrimaRiesgogv = (DropDownList)e.Row.FindControl("ddlPrimaRiesgogv");
                DropDownList ddlInfonavitgv = (DropDownList)e.Row.FindControl("ddlInfonavitgv");
                DropDownList ddlPrestaciongv = (DropDownList)e.Row.FindControl("ddlPrestaciongv");
                DropDownList ddlPensiongv = (DropDownList)e.Row.FindControl("ddlPensiongv");
                DropDownList ddlEsquemagv = (DropDownList)e.Row.FindControl("ddlEsquemagv");
                DropDownList ddlClasificaciongv = (DropDownList)e.Row.FindControl("ddlClasificaciongv");

                string cliente = ((DataRowView)e.Row.DataItem)["Nombre_RazonSocial"].ToString();
                string primariesgo = ((DataRowView)e.Row.DataItem)["PrimaRiesgo"].ToString();
                string infonavit = ((DataRowView)e.Row.DataItem)["Infonavit"].ToString();
                string prestacion = ((DataRowView)e.Row.DataItem)["Prestacion"].ToString();
                string pension = ((DataRowView)e.Row.DataItem)["Pension"].ToString();
                string esquema = ((DataRowView)e.Row.DataItem)["Esquema"].ToString();
                string clasificacion = ((DataRowView)e.Row.DataItem)["Clasificacion"].ToString();

                ddlClientegv.DataSource = dtClientes;
                ddlClientegv.DataTextField = "Nombre_RazonSocial";
                ddlClientegv.DataValueField = "Id_Cliente";
                ddlClientegv.DataBind();
                ddlClientegv.Items.FindByText(cliente).Selected = true;

                ddlPrimaRiesgogv.DataSource = dtPrimaRiesgo;
                ddlPrimaRiesgogv.DataTextField = "Clase";
                ddlPrimaRiesgogv.DataValueField = "Id_Clase";
                ddlPrimaRiesgogv.DataBind();
                ddlPrimaRiesgogv.Items.FindByText(primariesgo).Selected = true;

                ddlInfonavitgv.DataSource = dtInfonavit;
                ddlInfonavitgv.DataTextField = "Descripcion";
                ddlInfonavitgv.DataValueField = "Id_TpoInfo";
                ddlInfonavitgv.DataBind();
                ddlInfonavitgv.Items.FindByText(infonavit).Selected = true;

                ddlPrestaciongv.DataSource = dtPrestacion;
                ddlPrestaciongv.DataTextField = "Nombre";
                ddlPrestaciongv.DataValueField = "Id_Prest";
                ddlPrestaciongv.DataBind();
                ddlPrestaciongv.Items.FindByText(prestacion).Selected = true;


                ddlPensiongv.DataSource = dtPension;
                ddlPensiongv.DataTextField = "Descripcion";
                ddlPensiongv.DataValueField = "Id_TpoPensA";
                ddlPensiongv.DataBind();
                ddlPensiongv.Items.FindByText(pension).Selected = true;

                ddlEsquemagv.DataSource = dtEsquema;
                ddlEsquemagv.DataTextField = "Descripcion";
                ddlEsquemagv.DataValueField = "Id_TpoEsq";
                ddlEsquemagv.DataBind();
                ddlEsquemagv.Items.FindByText(esquema).Selected = true;

                ddlClasificaciongv.DataSource = dtClasificacion;
                ddlClasificaciongv.DataTextField = "Descripcion";
                ddlClasificaciongv.DataValueField = "Id_TpoClasEmp";
                ddlClasificaciongv.DataBind();
                ddlClasificaciongv.Items.FindByText(clasificacion).Selected = true;

            }




        }

    }
}