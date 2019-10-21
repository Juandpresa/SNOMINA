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
    public partial class wfrmClienteActulizar : System.Web.UI.Page
    {

        #region Variables

          string Nombre = string.Empty;
                string Denominacion = string.Empty;
                string Giro = string.Empty;
                string Calle = string.Empty;
                string Colonia = string.Empty;
                string Delegacion = string.Empty;
                int Entidad = 0;
                string CP = string.Empty;
                string Pais = string.Empty; 
                string Telefono = string.Empty;
                string NombreContacto = string.Empty;
                string CorreoContacto = string.Empty;
                int Ejecutivo = 0;
                int Socio = 0;
                int Asociado = 0;
                string Notas = string.Empty;
                string strQuery = string.Empty; 
        clsDatos clsQuery = new clsDatos();
        string IdCliente= string.Empty;

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

                IdCliente = Request.QueryString["Id_Cliente"].ToString();
                Session["Id_Cliente"] = Request.QueryString["Id_Cliente"].ToString();
                ObtenDatosNomina(IdCliente);
            }

            IdCliente = Session["Id_Cliente"].ToString();
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

                    ddlSocio.DataSource = dtEjecutivo;
                    ddlSocio.DataTextField = "Nombre";
                    ddlSocio.DataValueField = "Id_EjecComer";
                    ddlSocio.DataBind();

                    ddlAsociado.DataSource = dtEjecutivo;
                    ddlAsociado.DataTextField = "Nombre";
                    ddlAsociado.DataValueField = "Id_EjecComer";
                    ddlAsociado.DataBind();
                }

                ddlEjecutivo.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
                ddlSocio.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
                ddlAsociado.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);

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
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);

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
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);


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
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);


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
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);


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
                }

                ddlEntidad.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));

            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);


            }

        }

        protected void ObtenDatosNomina(string Id_Cliente)
        {
            DataTable dtDatosCliente = new DataTable();
            DataTable dtDatosNomina = new DataTable();

            dtDatosCliente = clsQuery.execQueryDataTable("SP_ObtenClienteID " + Id_Cliente);

            if (dtDatosCliente.Rows.Count > 0)
            {
                txtNombre.Text = dtDatosCliente.Rows[0]["Nombre_RazonSocial"].ToString();
                txtDenominacion.Text = dtDatosCliente.Rows[0]["Denominacion"].ToString();
                txtGiro.Text = dtDatosCliente.Rows[0]["Giro"].ToString();
                txtCalle.Text = dtDatosCliente.Rows[0]["CalleNum"].ToString();
                txtColonia.Text = dtDatosCliente.Rows[0]["Colonia"].ToString();
                txtDelegacion.Text = dtDatosCliente.Rows[0]["Delegacion"].ToString();
                ddlEntidad.SelectedValue = dtDatosCliente.Rows[0]["ID_entFed"].ToString();
                txtCP.Text = dtDatosCliente.Rows[0]["CP"].ToString();
                txtPais.Text = dtDatosCliente.Rows[0]["Pais"].ToString();
                txtTelefono.Text = dtDatosCliente.Rows[0]["Telefono"].ToString();
                txtNombreContacto.Text = dtDatosCliente.Rows[0]["NomContacto"].ToString();
                txtCorreoContacto.Text = dtDatosCliente.Rows[0]["MailContacto"].ToString();
                ddlEjecutivo.SelectedValue = dtDatosCliente.Rows[0]["Id_EjecComer"].ToString();
                ddlSocio.SelectedValue = dtDatosCliente.Rows[0]["Id_Socio"].ToString();
                ddlAsociado.SelectedValue = dtDatosCliente.Rows[0]["Id_Asociado"].ToString();
                txtNotas.Text = dtDatosCliente.Rows[0]["Notas"].ToString();

                dtDatosNomina = clsQuery.execQueryDataTable("SP_ObtenClienteDatosNomina " + Id_Cliente);

                if (dtDatosNomina.Rows.Count > 0)
                {

                    for (int i = 0; dtDatosNomina.Rows.Count > i; i++)
                    {
                        string strDatoNomina = dtDatosNomina.Rows[i]["Nombre"].ToString();


                        for (int y = 0; chkTipoPersonal.Items.Count > y; y++)
                        {
                            if (chkTipoPersonal.Items[y].Text == strDatoNomina)
                            {
                                ListItem listItem = this.chkTipoPersonal.Items.FindByText(strDatoNomina);
                                if (listItem != null) listItem.Selected = true;
                            }
                        }


                        for (int y = 0; chkPeriocidadNomina.Items.Count > y; y++)
                        {
                            if (chkPeriocidadNomina.Items[y].Text == strDatoNomina)
                            {

                                ListItem listItem = this.chkPeriocidadNomina.Items.FindByText(strDatoNomina);

                                if (listItem != null) listItem.Selected = true;
                            }
                        }

                        for (int y = 0; chkPrimaRiesgo.Items.Count > y; y++)
                        {
                            if (chkPrimaRiesgo.Items[y].Text == strDatoNomina)
                            {

                                ListItem listItem = this.chkPrimaRiesgo.Items.FindByText(strDatoNomina);

                                if (listItem != null) listItem.Selected = true;
                            }
                        }

                        for (int y = 0; chkTipoNomina.Items.Count > y; y++)
                        {
                            if (chkTipoNomina.Items[y].Text == strDatoNomina)
                            {

                                ListItem listItem = this.chkTipoNomina.Items.FindByText(strDatoNomina);

                                if (listItem != null) listItem.Selected = true;
                            }
                        }
                    }
                }
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
                CP = txtCP.Text.ToString();
                Pais = txtPais.Text.ToString();
                Telefono = txtTelefono.Text.ToString();
                NombreContacto = txtNombreContacto.Text.ToString();
                CorreoContacto = txtCorreoContacto.Text.ToString();
                Ejecutivo = int.Parse(ddlEjecutivo.SelectedItem.Value);
                Socio = int.Parse(ddlSocio.SelectedItem.Value);
                Asociado = int.Parse(ddlAsociado.SelectedItem.Value);
                Notas = txtNotas.Text.ToString();


                strQuery = string.Format("dbo.SP_ActualizaCliente {0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}, '{8}', '{9}', '{10}', '{11}', '{12}', {13}, {14}, {15}, '{16}'", 
                    IdCliente, Nombre, Denominacion, Giro, Calle, Colonia, Delegacion, Entidad, CP, Pais, Telefono, NombreContacto, CorreoContacto, Ejecutivo, Socio, Asociado, Notas);

                clsQuery.execQueryString(strQuery);

                    if (chkTipoPersonal.Items.Count != 0)
                    {
                        for (int x = 0; x <= chkTipoPersonal.Items.Count - 1; x++)
                        {
                            int intChecked;

                            if (chkTipoPersonal.Items[x].Selected == true)
                            {
                                intChecked = 1;                               
                            }
                            else
                            {
                                intChecked = 0;
                            }


                            int itemValue = int.Parse(chkTipoPersonal.Items[x].Value);
                                clsQuery.execQueryString("SP_ActualizaClienteTipoPersonal " + IdCliente + "," + itemValue + "," + intChecked);
                        }

                    }


                    if (chkPeriocidadNomina.Items.Count != 0)
                    {
                        for (int x = 0; x <= chkPeriocidadNomina.Items.Count - 1; x++)
                        {

                            int intChecked;

                            if (chkPeriocidadNomina.Items[x].Selected == true)
                            {
                                intChecked = 1;                               
                            }
                            else
                            {
                                intChecked = 0;
                            }

                            int itemValue = int.Parse(chkPeriocidadNomina.Items[x].Value);
                             clsQuery.execQueryString("SP_ActualizaClientePeriocidad " + IdCliente + "," + itemValue + "," + intChecked);
                        }

                    }

                    if (chkPrimaRiesgo.Items.Count != 0)
                    {
                        for (int x = 0; x <= chkPrimaRiesgo.Items.Count - 1; x++)
                        {
                            int intChecked;

                            if (chkPrimaRiesgo.Items[x].Selected == true)
                            {
                               intChecked = 1;
                            }
                            else
                            {
                                intChecked = 0;
                            }

                            int itemValue = int.Parse(chkPrimaRiesgo.Items[x].Value);
                            clsQuery.execQueryString("SP_ActualizaClientePrimaRiesgo " + IdCliente + "," + itemValue + "," + intChecked);

                        }

                    }

                    if (chkTipoNomina.Items.Count != 0)
                    {
                        for (int x = 0; x <= chkTipoNomina.Items.Count - 1; x++)
                        {
                            int intChecked;

                            if (chkTipoNomina.Items[x].Selected == true)
                            {
                                intChecked = 1;
                            }
                            else
                            {
                                intChecked = 0;
                            }

                            int itemValue = int.Parse(chkTipoNomina.Items[x].Value);
                                clsQuery.execQueryString("SP_ActualizaClienteTpoNomina " + IdCliente + "," + itemValue + "," + intChecked);
                        }

                    }


                    Mensaje("GUARDADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);

                    ObtenDatosNomina(IdCliente);
            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Vista/wfrmClienteConsulta.aspx");
        }

        public void Mensaje(string textomensaje, CuadroMensaje.CuadroMensajeIcono icono)
        {
            CuadroMensaje Mensaje = new CuadroMensaje(textomensaje, "MENSAJE", icono, CuadroMensaje.CuadroMensajeBoton.Ok, CuadroMensaje.CuadroMensajeEstilo.EstiloB);
            Literal ltrMensaje = (Literal)this.Master.FindControl("ltrMensaje");
            ltrMensaje.Text = Mensaje.Mostrar(this);
        }

    }
}