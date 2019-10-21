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
    public partial class wfrmSalarioMinimoProfesionales : System.Web.UI.Page
    {
        #region Variables
        string Id = string.Empty;
        string Fecha = string.Empty;
        DateTime FormatoFecha;
        string Profesionales = string.Empty;
        string Valor = string.Empty;

        string strQuery = string.Empty;
        string returnValue = string.Empty;

        clsDatos clsQuery = new clsDatos();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenSalarioMinimoProf();
            }
            
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

            Fecha = txtFecha.Text.ToString().Replace("-", "");
            Profesionales = txtProfesionales.Text.ToString();
            Valor = txtValor.Text.ToString();

            strQuery = string.Format("SP_InsertaSalarioMinimoProfesional '{0}', '{1}', '{2}'", Fecha, Profesionales, Valor);

            returnValue = clsQuery.execQueryString(strQuery);

            if (returnValue == "1")
            {
                ObtenSalarioMinimoProf();
                LimpiarControloes();
                Mensaje("GUARDADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
            }
        }

        protected void gvSalarioMinimoProf_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSalarioMinimoProf.EditIndex = -1;
            ObtenSalarioMinimoProf();

        }

        protected void gvSalarioMinimoProf_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvSalarioMinimoProf.EditIndex = e.NewEditIndex;
            ObtenSalarioMinimoProf();
        }

        protected void gvSalarioMinimoProf_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Id = ((Label)gvSalarioMinimoProf.Rows[e.RowIndex].FindControl("lblId_SalMinProf")).Text.Replace(",", ".");
            Fecha = ((TextBox)gvSalarioMinimoProf.Rows[e.RowIndex].FindControl("txtFecha")).Text.Replace("-", "");
            Profesionales = ((TextBox)gvSalarioMinimoProf.Rows[e.RowIndex].FindControl("txtProfesionales")).Text.Replace(",", ".");
            Valor = ((TextBox)gvSalarioMinimoProf.Rows[e.RowIndex].FindControl("txtValor")).Text.Replace(",", ".");
            FormatoFecha = DateTime.Parse(Fecha);

            strQuery = string.Format("SP_ActualizaSalarioMinimoProfesional '{0}', '{1}', '{2}', '{3}'", Id, FormatoFecha.ToString("yyyyMMdd HH:mm:ss"), Profesionales, Valor);

            returnValue = clsQuery.execQueryString(strQuery);

            if (returnValue == "1")
            {
                gvSalarioMinimoProf.EditIndex = -1;
                ObtenSalarioMinimoProf();
                Mensaje("ACTUALIZADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
            }
        }

        protected void ObtenSalarioMinimoProf()
        {
            DataTable dtSalarioMinProf = new DataTable();
            dtSalarioMinProf = clsQuery.execQueryDataTable("SP_ObtenSalarioMinimoProfesional");

            if (dtSalarioMinProf.Rows.Count > 0)
            {
                gvSalarioMinimoProf.DataSource = dtSalarioMinProf;
                gvSalarioMinimoProf.DataBind();
            }
        }

        protected void LimpiarControloes()
        {
            txtFecha.Text = string.Empty;
            txtProfesionales.Text = string.Empty;
            txtValor.Text = string.Empty;
        }

        public void Mensaje(string textomensaje, CuadroMensaje.CuadroMensajeIcono icono)
        {
            CuadroMensaje Mensaje = new CuadroMensaje(textomensaje, "MENSAJE", icono, CuadroMensaje.CuadroMensajeBoton.Ok, CuadroMensaje.CuadroMensajeEstilo.EstiloB);
            Literal ltrMensaje = (Literal)this.Master.FindControl("ltrMensaje");
            ltrMensaje.Text = Mensaje.Mostrar(this);
        }


    }
}