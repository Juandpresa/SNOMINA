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
    public partial class wfrmDatosClienteAlta : System.Web.UI.Page
    {
        #region Variables

            string Nombre = string.Empty;
            string Denominacion = string.Empty;
            string Giro = string.Empty;
            string Calle = string.Empty;
            string Colonia = string.Empty;
            string Delegacion = string.Empty;
            int Entidad = 0;
            int CP = 0;
            string Pais = string.Empty; 
            string Telefono = string.Empty;
            string NombreContacto = string.Empty;
            string CorreoContacto = string.Empty;
            int Ejecutivo = 0;
            string Notas = string.Empty;
            string strQuery = string.Empty;
            int intValue;
            clsDatos clsQuery = new clsDatos();
        #endregion 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenEntidad();
                ObtenEjecutivos();
                ObtenTipoPersonal();
                ObtenPeriocidadNomina();
                ObtenPrimaRiesgo();
                ObtenTipoNominaActual();

            }
        }

        protected void ObtenEjecutivos()
        {
            try
            {
                DataTable dtEjecutivo = new DataTable();

                dtEjecutivo = clsQuery.execQueryDataTable("SP_ObtenEjecutivosActivos");

                if (dtEjecutivo.Rows.Count > 0)
                {
                    ddlEjecutivo.DataSource = dtEjecutivo;
                    ddlEjecutivo.DataTextField = "Nombre";
                    ddlEjecutivo.DataValueField = "Id_EjecComer";
                    ddlEjecutivo.DataBind();                    
                }

                ddlEjecutivo.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "0"));

            }
            catch (Exception ex)
            {

            }
        }

        protected void ObtenTipoPersonal()
        {
            try
            {
                DataTable dtTipoPersonal = new DataTable();

                dtTipoPersonal = clsQuery.execQueryDataTable("dbo.SP_ObtenTipoPersonal");

                if (dtTipoPersonal.Rows.Count > 0)
                {
                    for (int i = 0; i < dtTipoPersonal.Rows.Count; i++)
                    {                      
                        chkTipoPersonal.Items.Add(new ListItem(dtTipoPersonal.Rows[i]["Nombre"].ToString(), dtTipoPersonal.Rows[i]["Id_TpoPersonal"].ToString()));

                    }
                    chkTipoPersonal.RepeatColumns = dtTipoPersonal.Rows.Count;
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void ObtenPeriocidadNomina()
        {
            try
            {
                DataTable dtPeriocidad = new DataTable();

                dtPeriocidad = clsQuery.execQueryDataTable("dbo.SP_ObtenPeriocidadNomina");

                if (dtPeriocidad.Rows.Count > 0)
                {
                    for (int i = 0; i < dtPeriocidad.Rows.Count; i++)
                    {
                        chkPeriocidadNomina.Items.Add(new ListItem(dtPeriocidad.Rows[i]["Nombre"].ToString(), dtPeriocidad.Rows[i]["Id_Periodo"].ToString()));
                    }
                    chkPeriocidadNomina.RepeatColumns = dtPeriocidad.Rows.Count;
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

                dtPrimaRiesgo = clsQuery.execQueryDataTable("dbo.SP_ObtenPrimaRiesgo");

                if (dtPrimaRiesgo.Rows.Count > 0)
                {
                    for (int i = 0; i < dtPrimaRiesgo.Rows.Count; i++)
                    {
                        chkPrimaRiesgo.Items.Add(new ListItem(dtPrimaRiesgo.Rows[i]["Clase"].ToString(), dtPrimaRiesgo.Rows[i]["Id_Clase"].ToString()));
                        
                    }
                    chkPrimaRiesgo.RepeatColumns = dtPrimaRiesgo.Rows.Count;
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void ObtenTipoNominaActual()
        {

            try
            {
                DataTable dtTipoNomina = new DataTable();

                dtTipoNomina = clsQuery.execQueryDataTable("dbo.SP_ObtenTipoNomina");

                if (dtTipoNomina.Rows.Count > 0)
                {
                    for (int i = 0; i < dtTipoNomina.Rows.Count; i++)
                    {
                        chkTipoNomina.Items.Add(new ListItem(dtTipoNomina.Rows[i]["Nombre"].ToString(), dtTipoNomina.Rows[i]["Id_TipoNom"].ToString()));
                        
                    }
                    chkTipoNomina.RepeatColumns = dtTipoNomina.Rows.Count;
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void ObtenEntidad()
        {

            try
            {
                DataTable dtEntidad = new DataTable();

                dtEntidad = clsQuery.execQueryDataTable("SP_ObtenEntidad");

                if (dtEntidad.Rows.Count > 0)
                {
                    ddlEntidad.DataSource = dtEntidad;
                    ddlEntidad.DataTextField = "Nombre";
                    ddlEntidad.DataValueField = "Id_EntFed";
                    ddlEntidad.DataBind();

                    ddlEntidad.Items.Insert(0,new ListItem( ">> Seleccione una Opcion <<","0"));
                }

            }
            catch (Exception ex)
            {

            }

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
        

                Nombre = txtNombre.Text.ToString();
                Denominacion = txtDenominacion.Text.ToString();
                Giro = txtGiro.Text.ToString();
                Calle = txtCalle.Text.ToString();
                Colonia = txtColonia.Text.ToString();
                Delegacion = txtDelegacion.Text.ToString();
                Entidad = int.Parse(ddlEntidad.SelectedValue);
                CP = int.Parse(txtCP.Text.ToString());
                Pais = txtPais.Text.ToString();
                Telefono = txtTelefono.Text.ToString();
                NombreContacto = txtNombreContacto.Text.ToString();
                CorreoContacto = txtCorreoContacto.Text.ToString();
                Ejecutivo = int.Parse(ddlEjecutivo.SelectedItem.Value);
                Notas = txtNotas.Text.ToString();

                strQuery = string.Format("dbo.SP_InsertaCliente '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', {6}, '{7}', '{8}', '{9}', '{10}', '{11}', {12}, '{13}'", 
                    Nombre , Denominacion , Giro , Calle , Colonia, Delegacion , Entidad , CP , Pais , Telefono, NombreContacto , CorreoContacto , Ejecutivo , Notas );

                intValue = clsQuery.execQueryInt(strQuery);

                if (intValue != 0)
                {


                    if (chkTipoPersonal.Items.Count != 0)
                    {
                        for (int x = 0; x <= chkTipoPersonal.Items.Count - 1; x++)
                        {
                            if (chkTipoPersonal.Items[x].Selected == true)
                            {

                                int itemValue = int.Parse(chkTipoPersonal.Items[x].Value);
                                clsQuery.execQueryString("dbo.SP_InsertaClienteTipoPersonal " + intValue + "," + itemValue);                                                        
                            }
                        }

                    }


                    if (chkPeriocidadNomina.Items.Count != 0)
                    {
                        for (int x = 0; x <= chkPeriocidadNomina.Items.Count - 1; x++)
                        {
                            if (chkPeriocidadNomina.Items[x].Selected == true)
                            {

                                int itemValue = int.Parse(chkPeriocidadNomina.Items[x].Value);
                                clsQuery.execQueryString("dbo.SP_InsertaClientePeriocidad " + intValue + "," + itemValue);
                            }
                        }

                    }

                    if (chkPrimaRiesgo.Items.Count != 0)
                    {
                        for (int x = 0; x <= chkPrimaRiesgo.Items.Count - 1; x++)
                        {
                            if (chkPrimaRiesgo.Items[x].Selected == true)
                            {
                                int itemValue = int.Parse(chkPrimaRiesgo.Items[x].Value);
                                clsQuery.execQueryString("dbo.SP_InsertaClientePrimaRiesgo " + intValue + "," + itemValue);
                            }
                        }

                    }

                    if (chkTipoNomina.Items.Count != 0)
                    {
                        for (int x = 0; x <= chkTipoNomina.Items.Count - 1; x++)
                        {
                            if (chkTipoNomina.Items[x].Selected == true)
                            {

                                int itemValue = int.Parse(chkTipoNomina.Items[x].Value);
                                clsQuery.execQueryString("dbo.SP_InsertaClienteTpoNomina " + intValue + "," + itemValue);
                            }
                        }

                    }

                   
                    LimpiarControles();
                    Response.Write("<script>alert('Guardado');</script>");
                }

            }
            catch (Exception ex)
            { 
            
                ex.ToString();
                
            }
        }

        private void LimpiarControles()
        {
            txtNombre.Text = string.Empty;
            txtDenominacion.Text = string.Empty;
            txtGiro.Text = string.Empty;
            txtCalle.Text = string.Empty;
            txtColonia.Text = string.Empty;
            txtDelegacion.Text = string.Empty;            
            txtCP.Text = string.Empty;
            txtPais.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtNombreContacto.Text = string.Empty;
            txtCorreoContacto.Text = string.Empty;            
            txtNotas.Text = string.Empty;


            ObtenEntidad();
            ObtenEjecutivos();
            chkTipoPersonal.SelectedIndex = -1;
            chkPeriocidadNomina.SelectedIndex = -1;
            chkPrimaRiesgo.SelectedIndex = -1;
            chkTipoNomina.SelectedIndex = -1;
        }



    }
}