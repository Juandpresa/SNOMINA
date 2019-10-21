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
    public partial class wfrmEscenarioConcepto : System.Web.UI.Page
    {
        #region Variables

        string Cliente = string.Empty;
        string Escnario = string.Empty;

        string Id_Concepto = string.Empty;
        string NomCorto = string.Empty;
        string Descripcion = string.Empty;
        string Calculo = string.Empty;

        string strQuery = string.Empty;

        DataTable dtConceptoP = new DataTable();
        DataTable dtConceptoH = new DataTable();
        DataTable objdtListaP;
        DataTable objdtListaH;

        clsDatos clsQuery = new clsDatos();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenClientes();
                ddlEscenario.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
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

        protected void ObtenEscenario(int Id_Cliente)
        {
            try
            {
                DataTable dtEscenario = new DataTable();

                strQuery = string.Format("SP_ObtenEscenarioPorCliente {0}", Id_Cliente);

                dtEscenario = clsQuery.execQueryDataTable(strQuery);

                if (dtEscenario.Rows.Count > 0)
                {
                    ddlEscenario.DataSource = dtEscenario;
                    ddlEscenario.DataTextField = "Id_Escenario";
                    ddlEscenario.DataValueField = "Id_Escenario";
                    ddlEscenario.DataBind();
                    ddlEscenario.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
                }
                else
                {
                    ddlEscenario.DataSource = dtEscenario;
                    ddlEscenario.DataBind();
                    ddlEscenario.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
                }


            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);

            }

        }

        protected void ObtenConceptos()
        {

            dtConceptoP = clsQuery.execQueryDataTable("SP_ObtenConceptos");

            if (dtConceptoP.Rows.Count > 0)
            {
                ViewState["Conceptos"] = dtConceptoP;

                gvEscenarioConceptoP.DataSource = dtConceptoP;
                gvEscenarioConceptoP.DataBind();
            }
        }

        protected void ObtenConceptosPorEscenario(int Opcion)
        {

            Cliente = ddlCliente.SelectedItem.Value;
            Escnario = ddlEscenario.SelectedItem.Value;

            strQuery = string.Format("SP_ObtenEscenarioConceptos {0}, {1}, {2}", Cliente, Escnario, Opcion);

            if (Opcion == 1)
            {
                dtConceptoP = clsQuery.execQueryDataTable(strQuery);

                if (dtConceptoP.Rows.Count > 0)
                {
                    ViewState["ConceptoP"] = dtConceptoP;

                    gvEscenarioConceptoP.DataSource = dtConceptoP;
                    gvEscenarioConceptoP.DataBind();
                    gvEscenarioConceptoP.Visible = true;
                }
                else
                {
                    dtConceptoP.Clear();
                    ViewState["ConceptoP"] = dtConceptoP;
                    gvEscenarioConceptoP.DataSource = dtConceptoP;
                    gvEscenarioConceptoP.DataBind();
                    gvEscenarioConceptoP.Visible = false;
                }
            }
            else if (Opcion == 2)
            {
                dtConceptoH = clsQuery.execQueryDataTable(strQuery);

                //if (dtConceptoH.Rows.Count > 0)
                //{
                ViewState["ConceptoH"] = dtConceptoH;

                gvEscenarioConceptoH.DataSource = dtConceptoH;
                gvEscenarioConceptoH.DataBind();
                //}

                if (dtConceptoP.Rows.Count > 0 || dtConceptoH.Rows.Count > 0)
                {
                    contenedorconceptos.Visible = true;
                }
                else
                {
                    Mensaje("No se existen conceptos", CuadroMensaje.CuadroMensajeIcono.Error);
                }
            }

        }

        protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Id_Cliente = int.Parse(ddlCliente.SelectedItem.Value);
            ObtenEscenario(Id_Cliente);

            contenedorconceptos.Visible = false;
        }

        protected void ddlEscenario_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ObtenConceptos();
            ObtenConceptosPorEscenario(1);
            ObtenConceptosPorEscenario(2);
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {

            dtConceptoP = ViewState["ConceptoP"] as DataTable;
            dtConceptoH = ViewState["ConceptoH"] as DataTable;

            // List<GridViewRow> rowSelected = new List<GridViewRow>();

            if (dtConceptoP != null)
            {
                objdtListaP = new DataTable();
                objdtListaP = dtConceptoP.Clone();
                DataTable dt = objdtTablaP;
            }
            else
            {
                objdtListaH = new DataTable();
                objdtListaH = dtConceptoH.Clone();
                DataTable dt = objdtTablaH;
            }


            foreach (GridViewRow row in gvEscenarioConceptoP.Rows)
            {

                CheckBox cellSelecion = (CheckBox)row.FindControl("chkSeleccionP");

                if (Convert.ToBoolean(cellSelecion.Checked))
                {
                    DataRow dr = dtConceptoH.NewRow();

                    Id_Concepto = ((Label)row.Cells[0].FindControl("lbId_Conceptos")).Text;
                    NomCorto = ((Label)row.Cells[1].FindControl("lbNomCorto")).Text;
                    Descripcion = ((Label)row.Cells[2].FindControl("lbDescripcion")).Text;
                    Calculo = ((Label)row.Cells[3].FindControl("lbCalculo")).Text;

                    dr["Id_Conceptos"] = Id_Concepto;
                    dr["NomCorto"] = NomCorto;
                    dr["Descripcion"] = Descripcion;
                    dr["Calculo"] = Calculo;

                    dtConceptoH.Rows.Add(dr);

                    if (dtConceptoP != null)
                    {
                        for (int i = 0; dtConceptoP.Rows.Count - 1 >= i; i++)
                        {
                            DataRow drs = dtConceptoP.Rows[i];

                            if (drs["NomCorto"].ToString() == NomCorto)
                            {
                                dtConceptoP.Rows.RemoveAt(i);
                            }
                        }
                    }
                    else
                    {

                        for (int i = 0; objdtTablaP.Rows.Count - 1 >= i; i++)
                        {
                            DataRow drs = objdtTablaP.Rows[i];

                            if (drs["NomCorto"].ToString() == NomCorto)
                            {
                                objdtTablaP.Rows.RemoveAt(i);
                            }
                        }

                        dtConceptoP = objdtTablaP;
                    
                    }
                }

                objdtTablaH = dtConceptoH;
            }


            gvEscenarioConceptoP.DataSource = dtConceptoP;
            gvEscenarioConceptoP.DataBind();
            gvEscenarioConceptoP.Visible = true;

            gvEscenarioConceptoH.DataSource = objdtTablaH;
            gvEscenarioConceptoH.DataBind();

        }

        protected void btnQuitar_Click(object sender, EventArgs e)
        {
            dtConceptoP = ViewState["ConceptoP"] as DataTable;
            dtConceptoH = ViewState["ConceptoH"] as DataTable;

            List<GridViewRow> rowSelected = new List<GridViewRow>();

            if (dtConceptoP != null)
            {
                objdtListaH = new DataTable();
                objdtListaH = dtConceptoP.Clone();
                DataTable dt = objdtTablaH;
            }
            else
            {
                objdtListaP = new DataTable();
                objdtListaP = dtConceptoH.Clone();
                DataTable dt = objdtTablaP;
                dtConceptoP = dt;
            }
            foreach (GridViewRow row in gvEscenarioConceptoH.Rows)
            {

                CheckBox cellSelecion = (CheckBox)row.FindControl("chkSeleccionP");

                if (Convert.ToBoolean(cellSelecion.Checked))
                {
                    DataRow dr = dtConceptoP.NewRow();

                    Id_Concepto = ((Label)row.Cells[0].FindControl("lbId_Conceptos")).Text;
                    NomCorto = ((Label)row.Cells[1].FindControl("lbNomCorto")).Text;
                    Descripcion = ((Label)row.Cells[2].FindControl("lbDescripcion")).Text;
                    Calculo = ((Label)row.Cells[3].FindControl("lbCalculo")).Text;

                    dr["Id_Conceptos"] = Id_Concepto;
                    dr["NomCorto"] = NomCorto;
                    dr["Descripcion"] = Descripcion;
                    dr["Calculo"] = Calculo;

                    dtConceptoP.Rows.Add(dr);

                    for (int i = 0; dtConceptoH.Rows.Count - 1 >= i; i++)
                    {
                        DataRow drs = dtConceptoH.Rows[i];

                        if (drs["NomCorto"].ToString() == NomCorto)
                        {
                            dtConceptoH.Rows.RemoveAt(i);
                        }
                    }

                }

                objdtTablaP = dtConceptoP;

            }

            gvEscenarioConceptoP.DataSource = objdtTablaP;
            gvEscenarioConceptoP.DataBind();
            gvEscenarioConceptoP.Visible = true;

            gvEscenarioConceptoH.DataSource = dtConceptoH;
            gvEscenarioConceptoH.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

            Cliente = ddlCliente.SelectedItem.Value;
            Escnario = ddlEscenario.SelectedItem.Value;

            //string conceptos = string.Empty;

            if (gvEscenarioConceptoH.Rows.Count > 0)
            {
               
                foreach (GridViewRow row in gvEscenarioConceptoH.Rows)
                {
                    Id_Concepto = ((Label)row.Cells[1].FindControl("lbId_Conceptos")).Text;

                    strQuery = string.Format("SP_InsertaEscenarioConceptos {0}, {1}, {2}, {3}", Cliente, Escnario, Id_Concepto, 1);
                    clsQuery.execQueryString(strQuery);

                    //conceptos += Id_Concepto + ",";
                }              
              
            }

            if (gvEscenarioConceptoP.Rows.Count > 0)
            {                

                foreach (GridViewRow row in gvEscenarioConceptoP.Rows)
                {
                    Id_Concepto = ((Label)row.Cells[1].FindControl("lbId_Conceptos")).Text;

                    strQuery = string.Format("SP_InsertaEscenarioConceptos {0}, {1}, {2}, {3}", Cliente, Escnario, Id_Concepto, 2);
                    clsQuery.execQueryString(strQuery);
                }
                
            }

            Mensaje("GUARDADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
        }

        public void Mensaje(string textomensaje, CuadroMensaje.CuadroMensajeIcono icono)
        {
            CuadroMensaje Mensaje = new CuadroMensaje(textomensaje, "MENSAJE", icono, CuadroMensaje.CuadroMensajeBoton.Ok, CuadroMensaje.CuadroMensajeEstilo.EstiloB);
            Literal ltrMensaje = (Literal)this.Master.FindControl("ltrMensaje");
            ltrMensaje.Text = Mensaje.Mostrar(this);
        }

        protected DataTable objdtTablaP
        {

            get
            {
                if (ViewState["objdtTablaP"] != null)
                {
                    return (DataTable)ViewState["objdtTablaP"];
                }
                else
                {
                    return objdtListaP;
                }
            }
            set
            {
                ViewState["objdtTablaP"] = value;
            }

        }

        protected DataTable objdtTablaH
        {

            get
            {
                if (ViewState["objdtTablaH"] != null)
                {
                    return (DataTable)ViewState["objdtTablaH"];
                }
                else
                {
                    return objdtListaH;
                }
            }
            set
            {
                ViewState["objdtTablaH"] = value;
            }

        }

        protected void gvEscenarioConceptoP_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < e.Row.Cells.Count - 1; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }

            if ((e.Row.RowType == DataControlRowType.Header) && gvEscenarioConceptoP.EditIndex == e.Row.RowIndex)
            {
                //adding an attribut for onclick event on the check box in the hearder and passing the ClientID of the Select All checkbox 
                ((CheckBox)e.Row.FindControl("checkAllP")).Attributes.Add("onclick", "javascript:SelectAllP('" + ((CheckBox)e.Row.FindControl("checkAllP")).ClientID + "')");
            }
        }

        protected void gvEscenarioConceptoH_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < e.Row.Cells.Count - 1; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }

            if ((e.Row.RowType == DataControlRowType.Header) && gvEscenarioConceptoP.EditIndex == e.Row.RowIndex)
            {
                //adding an attribut for onclick event on the check box in the hearder and passing the ClientID of the Select All checkbox 
                ((CheckBox)e.Row.FindControl("checkAllH")).Attributes.Add("onclick", "javascript:SelectAllH('" + ((CheckBox)e.Row.FindControl("checkAllH")).ClientID + "')");
            }
        }

    }
}