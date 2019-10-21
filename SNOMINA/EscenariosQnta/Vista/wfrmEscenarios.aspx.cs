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
    public partial class wfrmEscenarios : System.Web.UI.Page
    {
        #region Variables

        int Id_Escenario = 0;
        string Id_EscDetalle = string.Empty;
        string Folio = string.Empty;
        string Cliente = string.Empty;
        string Prestaciones = string.Empty;
        string Nomina = string.Empty;
        string Asimilados = string.Empty;
        string Honorarios = string.Empty;
        string TN = string.Empty;
        string EZWallet = string.Empty;
        //string OtrosProductos = string.Empty;
        string Clasificacion = string.Empty;
        //string SueldoIni = string.Empty;
        //string SueldoFin = string.Empty;
        string Id_Comision = string.Empty;
        string ImporteComision = string.Empty;
        string Nota = string.Empty;
        int EscenarioActual = 0;
        string ValidacionCampos = string.Empty;
        //bool checkeTipoNomina = false;
        
        string strQuery = string.Empty;
        string strReturnVlaue = string.Empty;

        clsDatos clsQuery = new clsDatos();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenClientes();
                ObtenClasificacionEmpleado();
                ObtenPrestacion();
                ObtenComision();
                //ObtenEscenarios();
                //gvEscenario.Visible = false;
                //ObtenTipoArea();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                ValidacionCampos = ValidaCampos();
              
                if (string.IsNullOrEmpty(ValidacionCampos))
                {
                    //Folio = txtFolio.Text.ToString();
                    Cliente = ddlCliente.SelectedItem.Value;
                    Prestaciones = ddlPrestacion.SelectedItem.Value;
                    Nomina = txtNomina.Text.ToString();
                    Asimilados = txtAsimilados.Text.ToString();
                    Honorarios = txtHonorarios.Text.ToString();
                    TN = txtTN.Text.ToString();
                    EZWallet = txtEZWallet.Text.ToString();
                    //OtrosProductos = txtOtrosProductos.Text.ToString();
                    Clasificacion = ddlClasificacion.SelectedItem.Value;
                    Id_Comision = ddlTipoComision.SelectedItem.Value;
                    ImporteComision = txtImporteComision.Text.ToString();
                    //SueldoIni = txtRangoSueldoIni.Text.ToString();
                    //SueldoFin = txtRangoSueldoFin.Text.ToString();
                    Nota = txtNotas.Text.ToString();

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

                    if (chkEsnecarioActual.Checked == true)
                    {
                        EscenarioActual = 1;
                    }
                    else
                    {
                        EscenarioActual = 0;
                    }

                    strQuery = string.Format("SP_InsertaEscenario {0}, {1}, '{2}', '{3}', '{4}', '{5}', '{6}', {7}, {8}, '{9}', '{10}', {11}",
                        Cliente, Prestaciones,  Nomina, Asimilados, Honorarios, TN, EZWallet, //OtrosProductos, 
                        Clasificacion, Id_Comision, ImporteComision, Nota, EscenarioActual);

                    Session["Id_Escenario"] = clsQuery.execQueryInt(strQuery);
                    Id_Escenario = int.Parse(Session["Id_Escenario"].ToString());
                    

                    if (Id_Escenario != -1)
                    {
                        Mensaje("GUARDADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
                        ObtenEscenarioByID(Id_Escenario);
                        txtFolio.Text = Session["Id_Escenario"].ToString();
                        BloquearControles();
                        btnNueva.Visible = true;
                        btnGuardar.Visible = false;
                        gvEscenario.Visible = true;
                    }
                }
                else
                {
                    Mensaje(ValidacionCampos, CuadroMensaje.CuadroMensajeIcono.Advertencia);
                }
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

                dtCliente = clsQuery.execQueryDataTable("SP_ObtenClientes");

                if (dtCliente.Rows.Count > 0)
                {
                    ddlCliente.DataSource = dtCliente;
                    ddlCliente.DataTextField = "Nombre_RazonSocial";
                    ddlCliente.DataValueField = "Id_Cliente";
                    ddlCliente.DataBind();
                }

                ddlCliente.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);

            }
        }

        //private void ObtenTipoArea()
        //{
        //    try
        //    {
        //        DataTable dtTipoArea = new DataTable();

        //        dtTipoArea = clsQuery.execQueryDataTable("SP_ObtenTipoAreaComercial");

        //        if (dtTipoArea.Rows.Count > 0)
        //        {
        //            ddlClasificacion.DataSource = dtTipoArea;
        //            ddlClasificacion.DataTextField = "Descripcion";
        //            ddlClasificacion.DataValueField = "Id";
        //            ddlClasificacion.DataBind();
        //        }

        //        ddlClasificacion.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
        //    }
        //    catch (Exception ex)
        //    {
        //        Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
        //    }
        //}

        protected void ObtenClasificacionEmpleado()
        {
            try
            {
                DataTable dtClasificacionEmpleado = new DataTable();

                dtClasificacionEmpleado = clsQuery.execQueryDataTable("SP_ObtenClasificacionEmpleado");

                if (dtClasificacionEmpleado.Rows.Count > 0)
                {
                    ddlClasificacion.DataSource = dtClasificacionEmpleado;
                    ddlClasificacion.DataTextField = "Descripcion";
                    ddlClasificacion.DataValueField = "Id_TpoClasEmp";
                    ddlClasificacion.DataBind();
                }

                ddlClasificacion.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
            }
        }

        protected void ObtenPrestacion()
        {
            try
            {
                DataTable dtPrestacion = new DataTable();

                dtPrestacion = clsQuery.execQueryDataTable("SP_ObtenFactor");

                if (dtPrestacion.Rows.Count > 0)
                {
                    ddlPrestacion.DataSource = dtPrestacion;
                    ddlPrestacion.DataTextField = "Nombre";
                    ddlPrestacion.DataValueField = "Id_Factor";
                    ddlPrestacion.DataBind();
                }

                ddlPrestacion.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
            }
        }

        protected void ObtenComision()
        {
            try
            {
                DataTable dtComision = new DataTable();

                dtComision = clsQuery.execQueryDataTable("SP_ObtenComision");

                if (dtComision.Rows.Count > 0)
                {
                    ddlTipoComision.DataSource = dtComision;
                    ddlTipoComision.DataTextField = "TipoComision";
                    ddlTipoComision.DataValueField = "Id_Comision";
                    ddlTipoComision.DataBind();
                }

                ddlTipoComision.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);

            }        
        }

        protected void BloquearControles()
        {
            txtFolio.Enabled = false;
            ddlCliente.Enabled = false;
            ddlPrestacion.Enabled = false;
            //rdlTipoNomina.Enabled = false;
            txtNomina.Enabled = false;
            txtAsimilados.Enabled = false;
            txtHonorarios.Enabled = false;
            txtTN.Enabled = false;
            txtEZWallet.Enabled = false;
            //txtOtrosProductos.Enabled = false;
            ddlClasificacion.Enabled = false;
            //txtRangoSueldoIni.Enabled = false;
            //txtRangoSueldoFin.Enabled = false;
            txtNotas.Enabled = false;
            txtImporteComision.Enabled = false;
            ddlTipoComision.Enabled = false;
            chkEsnecarioActual.Enabled = false;

            //txtFolio.Text = "0";
            //ddlCliente.SelectedItem.Value = "";
            //ddlPrestacion.SelectedItem.Value = "";
            //txtNomina.Text = "";
            //txtAsimilados.Text = "";
            //txtHonorarios.Text = "";
            //txtOtrosProductos.Text = "";
            //ddlClasificacion.SelectedItem.Value = "";
            //txtRangoSueldoIni.Text = "";
            //txtRangoSueldoFin.Text = "";
            //txtNotas.Text = "";

        }

        protected void DesbloquearControles()
        {

            ddlCliente.Enabled = true;
            ddlPrestacion.Enabled = true;
            //rdlTipoNomina.Enabled = true;
            txtNomina.Enabled = true;
            txtAsimilados.Enabled = true;
            txtHonorarios.Enabled = true;
            txtTN.Enabled = true;
            txtEZWallet.Enabled = true;
            //txtOtrosProductos.Enabled = true;
            ddlClasificacion.Enabled = true;
            //txtRangoSueldoIni.Enabled = true;
            //txtRangoSueldoFin.Enabled = true;
            txtNotas.Enabled = true;
            chkEsnecarioActual.Checked = false;
            txtImporteComision.Enabled = true;
            ddlTipoComision.Enabled = true;
            chkEsnecarioActual.Enabled = true;

            btnGuardar.Visible = true;
            btnNueva.Visible = false;

        }

        protected void LimpiarControles()
        {
            txtFolio.Text = "0";
            ddlCliente.SelectedValue = "-1";
            ddlPrestacion.SelectedValue = "-1";
            //rdlTipoNomina.ClearSelection();
            txtNomina.Text = "0";// string.Empty;
            txtAsimilados.Text = "0";// string.Empty;
            txtHonorarios.Text = "0";// string.Empty;
            txtTN.Text = "0";
            txtEZWallet.Text = "0";
            //txtOtrosProductos.Text = string.Empty;
            ddlClasificacion.SelectedValue = "-1";
            //txtRangoSueldoIni.Text = string.Empty;
            //txtRangoSueldoFin.Text = string.Empty;
            txtNotas.Text = string.Empty;
            txtImporteComision.Text = string.Empty;
            ddlTipoComision.SelectedValue = "-1";
        }

        protected void ObtenEscenarios()
        {
            try
            {
                DataTable dtEscenarios = new DataTable();

                dtEscenarios = clsQuery.execQueryDataTable("SP_ObtenEscenarios");

                gvEscenario.DataSource = dtEscenarios;
                gvEscenario.DataBind();

            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
            }

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Id_Escenario = int.Parse(Session["Id_Escenario"].ToString());
            Cliente = ((DropDownList)gvEscenario.FooterRow.FindControl("ddlClientegv")).Text;
            Prestaciones = ((DropDownList)gvEscenario.FooterRow.FindControl("ddlPrestaciongv")).Text;
            Nomina = ((TextBox)gvEscenario.FooterRow.FindControl("txtPorcNomina")).Text;
            Asimilados = ((TextBox)gvEscenario.FooterRow.FindControl("txtPorcAsimilados")).Text;
            Honorarios = ((TextBox)gvEscenario.FooterRow.FindControl("txtPorcHonorarios")).Text;
            //OtrosProductos = ((TextBox)gvEscenario.FooterRow.FindControl("txtPorOtrosProductos")).Text;
            Clasificacion = ((DropDownList)gvEscenario.FooterRow.FindControl("ddlClasificaciongv")).Text;
            //SueldoIni = ((TextBox)gvEscenario.FooterRow.FindControl("txtRangoSueldoIni")).Text;
            //SueldoFin = ((TextBox)gvEscenario.FooterRow.FindControl("txtRangoSueldoFin")).Text;
            Nota = ((TextBox)gvEscenario.FooterRow.FindControl("txtNota")).Text;

            strQuery = string.Format("SP_InsertaEscenarioPropuesta {0}, {1}, {2}, '{3}', '{4}', '{5}', '{6}', {7}, '{8}'",
                Id_Escenario, Cliente, Prestaciones, Nomina, Asimilados, Honorarios, //OtrosProductos, 
                Clasificacion, Nota);

            strReturnVlaue = clsQuery.execQueryString(strQuery);

            if (strReturnVlaue == "1")
            {
                gvEscenario.EditIndex = -1;
                ObtenEscenarioByID(int.Parse(Session["Id_Escenario"].ToString()));
                Mensaje("AGREGADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
            }
        }

        protected void btnNueva_Click(object sender, EventArgs e)
        {
            DesbloquearControles();
            LimpiarControles();
            gvEscenario.Visible = false;
        }

        protected void ObtenEscenarioByID(int Id_Escenario)
        {
            try
            {
                DataTable dtEscenarioById = new DataTable();

                strQuery = string.Format("SP_ObtenEscenarioPorID {0}", Id_Escenario);

                dtEscenarioById = clsQuery.execQueryDataTable(strQuery);

                if (dtEscenarioById.Rows.Count > 0)
                {
                    gvEscenario.DataSource = dtEscenarioById;
                    gvEscenario.DataBind();

                    txtFolio.Text = dtEscenarioById.Rows[0]["Id_Escenario"].ToString();
                }


            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
            }

        }

        protected void gvEscenario_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEscenario.EditIndex = -1;
            ObtenEscenarioByID(int.Parse(Session["Id_Escenario"].ToString()));
        }

        protected void gvEscenario_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            DataTable dtCliente = new DataTable();
            DataTable dtPrestacion = new DataTable();
            DataTable dtClasificacion = new DataTable();

            dtCliente = clsQuery.execQueryDataTable("SP_ObtenClientes");
            dtPrestacion = clsQuery.execQueryDataTable("SP_ObtenPrestaciones");
            dtClasificacion = clsQuery.execQueryDataTable("SP_ObtenClasificacionEmpleado");

            if (e.Row.RowType == DataControlRowType.DataRow && gvEscenario.EditIndex == e.Row.RowIndex)
            {
                DropDownList ddlClientegv = (DropDownList)e.Row.FindControl("ddlClientegv") as DropDownList;
                DropDownList ddlPrestaciongv = (DropDownList)e.Row.FindControl("ddlPrestaciongv") as DropDownList;
                DropDownList ddlClasificaciongv = (DropDownList)e.Row.FindControl("ddlClasificaciongv") as DropDownList;

                string Clientegv = ((DataRowView)e.Row.DataItem)["Cliente"].ToString();
                string Prestaciongv = ((DataRowView)e.Row.DataItem)["Prestacion"].ToString();
                string Clasificaciongv = ((DataRowView)e.Row.DataItem)["Clasificacion"].ToString();

                if (dtCliente.Rows.Count > 0)
                {
                    ddlClientegv.DataSource = dtCliente;
                    ddlClientegv.DataTextField = "Nombre_RazonSocial";
                    ddlClientegv.DataValueField = "Id_Cliente";
                    ddlClientegv.DataBind();
                    ddlClientegv.Items.FindByText(Clientegv).Selected = true;
                }



                if (dtPrestacion.Rows.Count > 0)
                {
                    ddlPrestaciongv.DataSource = dtPrestacion;
                    ddlPrestaciongv.DataTextField = "Nombre";
                    ddlPrestaciongv.DataValueField = "Id_Prest";
                    ddlPrestaciongv.DataBind();
                    ddlPrestaciongv.Items.FindByText(Prestaciongv).Selected = true;
                }



                if (dtClasificacion.Rows.Count > 0)
                {
                    ddlClasificaciongv.DataSource = dtClasificacion;
                    ddlClasificaciongv.DataTextField = "Descripcion";
                    ddlClasificaciongv.DataValueField = "Id_TpoClasEmp";
                    ddlClasificaciongv.DataBind();
                    ddlClasificaciongv.Items.FindByText(Clasificaciongv).Selected = true;
                }
            }
        }

        protected void gvEscenario_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEscenario.EditIndex = e.NewEditIndex;
            ObtenEscenarioByID(int.Parse(Session["Id_Escenario"].ToString()));
        }

        protected void gvEscenario_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Id_Escenario = int.Parse(((Label)gvEscenario.Rows[e.RowIndex].FindControl("lbId_Escenario")).Text);
                Id_EscDetalle = ((Label)gvEscenario.Rows[e.RowIndex].FindControl("lbId_EscDetalle")).Text;
                Cliente = ((DropDownList)gvEscenario.Rows[e.RowIndex].FindControl("ddlClientegv")).Text;
                Prestaciones = ((DropDownList)gvEscenario.Rows[e.RowIndex].FindControl("ddlPrestaciongv")).Text;
                Nomina = ((TextBox)gvEscenario.Rows[e.RowIndex].FindControl("txtPorcNomina")).Text;
                Asimilados = ((TextBox)gvEscenario.Rows[e.RowIndex].FindControl("txtPorcAsimilados")).Text;
                Honorarios = ((TextBox)gvEscenario.Rows[e.RowIndex].FindControl("txtPorcHonorarios")).Text;
                //OtrosProductos = ((TextBox)gvEscenario.Rows[e.RowIndex].FindControl("txPorOtrosProductos")).Text;
                Clasificacion = ((DropDownList)gvEscenario.Rows[e.RowIndex].FindControl("ddlClasificaciongv")).Text;
                //SueldoIni = ((TextBox)gvEscenario.FooterRow.FindControl("txtRangoSueldoIni")).Text;
                //SueldoFin = ((TextBox)gvEscenario.FooterRow.FindControl("txtRangoSueldoFin")).Text;
                Nota = ((TextBox)gvEscenario.Rows[e.RowIndex].FindControl("txtNota")).Text;

                strQuery = string.Format("SP_ActualizaEscenarioPropuesta {0}, {1}, {2}, {3}, '{4}', '{5}', '{6}', {7}, '{8}', '{9}'",
             Id_Escenario, Id_EscDetalle, Cliente, Prestaciones, Nomina, Asimilados, Honorarios, //OtrosProductos, 
             Clasificacion, Nota);

                strReturnVlaue = clsQuery.execQueryString(strQuery);

                if (strReturnVlaue == "1")
                {
                    gvEscenario.EditIndex = -1;
                    ObtenEscenarioByID(int.Parse(Session["Id_Escenario"].ToString()));
                    Mensaje("ACTUALIZADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
                }

            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
            }
        }

        protected void gvEscenario_DataBound(object sender, EventArgs e)
        {
            DropDownList ddlClientegv = gvEscenario.FooterRow.FindControl("ddlClientegv") as DropDownList;
            DropDownList ddlPrestaciongv = gvEscenario.FooterRow.FindControl("ddlPrestaciongv") as DropDownList;
            DropDownList ddlClasificaciongv = gvEscenario.FooterRow.FindControl("ddlClasificaciongv") as DropDownList;

            DataTable dtCliente = new DataTable();
            DataTable dtPrestacion = new DataTable();
            DataTable dtClasificacion = new DataTable();

            dtCliente = clsQuery.execQueryDataTable("SP_ObtenClientes");

            if (dtCliente.Rows.Count > 0)
            {
                ddlClientegv.DataSource = dtCliente;
                ddlClientegv.DataTextField = "Nombre_RazonSocial";
                ddlClientegv.DataValueField = "Id_Cliente";
                ddlClientegv.DataBind();
            }

            dtPrestacion = clsQuery.execQueryDataTable("SP_ObtenPrestaciones");

            if (dtPrestacion.Rows.Count > 0)
            {
                ddlPrestaciongv.DataSource = dtPrestacion;
                ddlPrestaciongv.DataTextField = "Nombre";
                ddlPrestaciongv.DataValueField = "Id_Prest";
                ddlPrestaciongv.DataBind();
            }

            dtClasificacion = clsQuery.execQueryDataTable("SP_ObtenClasificacionEmpleado");

            if (dtClasificacion.Rows.Count > 0)
            {
                ddlClasificaciongv.DataSource = dtClasificacion;
                ddlClasificaciongv.DataTextField = "Descripcion";
                ddlClasificaciongv.DataValueField = "Id_TpoClasEmp";
                ddlClasificaciongv.DataBind();
            }
        }

        protected string ValidaCampos()
        {
            string returnValidacion = string.Empty;

            //for (int i = 0; i <= rdlTipoNomina.Items.Count - 1; i++)
            //{
            //    if (rdlTipoNomina.Items[i].Selected == true)
            //    {
            //        if (rdlTipoNomina.SelectedValue.ToString() == "Brutos")
            //        {
            //            checkeTipoNomina = true;
            //        }
            //        else if (rdlTipoNomina.SelectedValue.ToString() == "Netos")
            //        {
            //            checkeTipoNomina = true;
            //        }
            //    }
            //}

            //if (checkeTipoNomina == false)
            //    returnValidacion = "Seleccione un Tipo de Nomina Por Favor";

            //if (string.IsNullOrEmpty(txtNomina.Text) && string.IsNullOrEmpty(txtAsimilados.Text) && string.IsNullOrEmpty(txtHonorarios.Text) && string.IsNullOrEmpty(txtOtrosProductos.Text))
            //    returnValidacion = "Ingrese un monto Por Favor";

            if(ddlPrestacion.SelectedItem.Value == "-1")
                returnValidacion = "Seleccione una Prestacion Por Favor";

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
        
    }
}