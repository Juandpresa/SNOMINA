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
    public partial class wfrmSubsEmp : System.Web.UI.Page
    {
        #region

        //string IdFactor = string.Empty;
        string Nombre = string.Empty;

        string Id;
        string LimInf = string.Empty;
        string LimSup = string.Empty;
        string Subsidio = string.Empty;



        string strQuery = string.Empty;
        string returnValue = string.Empty;
        int IdPeriodo = 0;
        clsDatos clsQuery = new clsDatos();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenPeriodo();
            }
        }

        protected void ObtenPeriodo()
        {
            try
            {
                DataTable dtPeriodo = new DataTable();

                dtPeriodo = clsQuery.execQueryDataTable("dbo.SP_ObtenPeriocidadNomina");

                if (dtPeriodo.Rows.Count > 0)
                {
                    ddlPeriodo.DataSource = dtPeriodo;
                    ddlPeriodo.DataTextField = "Nombre";
                    ddlPeriodo.DataValueField = "Id_Periodo";
                    ddlPeriodo.DataBind();
                }
                ddlPeriodo.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "-1"));
            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                IdPeriodo = int.Parse(ddlPeriodo.SelectedItem.Value);
                LimInf = ((TextBox)gvSudsidio.FooterRow.FindControl("txtLimInf")).Text;
                LimSup = ((TextBox)gvSudsidio.FooterRow.FindControl("txtLimSup")).Text;
                Subsidio = ((TextBox)gvSudsidio.FooterRow.FindControl("txtSubsidio")).Text;


                strQuery = string.Format("SP_InsertaSubsidio {0}, '{1}', '{2}', '{3}' ", IdPeriodo, LimInf, LimSup, Subsidio);
                returnValue = clsQuery.execQueryString(strQuery);

                if (returnValue == "1")
                {
                    gvSudsidio.EditIndex = -1;
                    ObtenSubsidio();
                    Mensaje("ACTUALIZADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
                }
            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
            }
        }

        protected void ObtenSubsidio()
        {

            try
            {
                IdPeriodo = int.Parse(ddlPeriodo.SelectedItem.Value);

                DataTable dtImporte = new DataTable();
                strQuery = string.Format("SP_ObtenSubsidio {0}", IdPeriodo);

                dtImporte = clsQuery.execQueryDataTable(strQuery);

                //if (dtImporte.Rows.Count > 0)
                //{
                gvSudsidio.DataSource = dtImporte;
                gvSudsidio.DataBind();
                //gvSudsidio.Visible = true;
                //}
            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
            }

        }

        protected void ddlPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlPeriodo.SelectedItem.Value))
            {
                IdPeriodo = int.Parse(ddlPeriodo.SelectedItem.Value);

                if (IdPeriodo != 0)
                {
                    ObtenSubsidio();
                    gvSudsidio.Visible = true;
                }
                else
                {
                    gvSudsidio.Visible = false;

                }
            }
        }

        protected void gvSudsidio_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSudsidio.EditIndex = -1;
            ObtenSubsidio();
        }

        protected void gvSudsidio_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvSudsidio.EditIndex = e.NewEditIndex;
            ObtenSubsidio();
        }

        protected void gvSudsidio_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Id = ((Label)gvSudsidio.Rows[e.RowIndex].FindControl("lblId")).Text;
                LimInf = ((TextBox)gvSudsidio.Rows[e.RowIndex].FindControl("txtLimInf")).Text.Replace(',', '.');
                LimSup = ((TextBox)gvSudsidio.Rows[e.RowIndex].FindControl("txtLimSup")).Text.Replace(',', '.');
                Subsidio = ((TextBox)gvSudsidio.Rows[e.RowIndex].FindControl("txtSubsidio")).Text.Replace(',', '.');



                strQuery = string.Format("SP_ActualizaSubsidio {0}, '{1}', '{2}', '{3}' ", Id, LimInf, LimSup, Subsidio);

                returnValue = clsQuery.execQueryString(strQuery);

                if (returnValue == "1")
                {
                    gvSudsidio.EditIndex = -1;
                    ObtenSubsidio();
                    Mensaje("ACTUALIZADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
                }
            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
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