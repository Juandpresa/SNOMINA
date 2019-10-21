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
    public partial class wfrmCuotasIMSS : System.Web.UI.Page
    {

        #region Variables

        string Id = string.Empty;
        string Fecha = string.Empty;
        string PatCuotaFija = string.Empty;
        string PatExcedente = string.Empty;
        string PatDinero = string.Empty;
        string PatIV = string.Empty;
        string PatRetiro = string.Empty;
        string PatCV = string.Empty;        
        string PatGastosMedicos = string.Empty;
        string PatGuarderia = string.Empty;
        string PatInfonavit = string.Empty;
        string ObrCuotaFija = string.Empty;
        string ObrDinero = string.Empty;
        string ObrMedicos = string.Empty;
        string ObrIV = string.Empty;
        string ObrCV = string.Empty;
        string ObrGuarderia = string.Empty;
        string ObrExcedente = string.Empty;
        string strQuery = string.Empty;
        string strRetunValue = string.Empty;

        clsDatos clsQuery = new clsDatos();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenCuotasIMSS();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

            Fecha = txtFecha.Text.ToString();
            
            PatCuotaFija = txtPatCuotaFija.Text.ToString();
            PatExcedente = txtPatExcedente.Text.ToString();
            PatDinero = txtPatDinero.Text.ToString();
            PatIV = txtPatIV.Text.ToString();
            PatRetiro = txtPatRetiro.Text.ToString();
            PatCV = txtPatCV.Text.ToString();
            PatGastosMedicos = txtPatGastosMedicos.Text.ToString();
            PatGuarderia = txtPatGuarderia.Text.ToString();
            PatInfonavit = txtPatInfonavit.Text.ToString();

            ObrCuotaFija = txtObrCuotaFija.Text.ToString();
            ObrDinero = txtObrDinero.Text.ToString();
            ObrMedicos = txtObrMedicos.Text.ToString();            
            ObrIV = txtObrIV.Text.ToString();
            ObrCV = txtObrCV.Text.ToString();
            ObrGuarderia = txtObrGuarderia.Text.ToString();
            ObrExcedente = txtObrExcedente.Text.ToString();
            


            strQuery = string.Format("SP_InsertaCuotasIMSS '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}'",
             Fecha, PatCuotaFija, PatExcedente, PatDinero, PatIV, PatRetiro, PatCV, PatGastosMedicos, PatGuarderia, PatInfonavit, ObrCuotaFija, ObrDinero, ObrMedicos, ObrIV, ObrCV, ObrGuarderia, ObrExcedente);

           strRetunValue= clsQuery.execQueryString(strQuery);

           if (strRetunValue == "1")
            {
                ObtenCuotasIMSS();
                Mensaje("GUARDADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
                LimpiarControlos();
            }
        }

        protected void ObtenCuotasIMSS()
        {

            DataTable dtCuotaIMSS = new DataTable();

            dtCuotaIMSS = clsQuery.execQueryDataTable("SP_ObtenCuotasIMSS");

            if (dtCuotaIMSS.Rows.Count > 0)
            {
                grvCuotaIMSS.DataSource = dtCuotaIMSS;
                grvCuotaIMSS.DataBind();
                

            }

        }

        protected void grvCuotaIMSS_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grvCuotaIMSS.EditIndex = -1;
            ObtenCuotasIMSS();
        }

        protected void grvCuotaIMSS_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grvCuotaIMSS.EditIndex = e.NewEditIndex;
            ObtenCuotasIMSS();
        }

        protected void grvCuotaIMSS_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Id = ((Label)grvCuotaIMSS.Rows[e.RowIndex].FindControl("lblId_Cuota")).Text;
                Fecha = ((TextBox)grvCuotaIMSS.Rows[e.RowIndex].FindControl("txtFecha")).Text;
                PatCuotaFija = ((TextBox)grvCuotaIMSS.Rows[e.RowIndex].FindControl("txtPatCuotaFija")).Text;
                PatExcedente = ((TextBox)grvCuotaIMSS.Rows[e.RowIndex].FindControl("txtPatExcedente")).Text;
                PatDinero = ((TextBox)grvCuotaIMSS.Rows[e.RowIndex].FindControl("txtPatDinero")).Text;
                PatIV = ((TextBox)grvCuotaIMSS.Rows[e.RowIndex].FindControl("txtPatIV")).Text;
                PatRetiro = ((TextBox)grvCuotaIMSS.Rows[e.RowIndex].FindControl("txtPatRetiro")).Text;
                PatCV = ((TextBox)grvCuotaIMSS.Rows[e.RowIndex].FindControl("txtPatCV")).Text;
                PatGastosMedicos = ((TextBox)grvCuotaIMSS.Rows[e.RowIndex].FindControl("txtPatGastosMedicos")).Text;
                PatGuarderia = ((TextBox)grvCuotaIMSS.Rows[e.RowIndex].FindControl("txtPatGuarderia")).Text;
                PatInfonavit = ((TextBox)grvCuotaIMSS.Rows[e.RowIndex].FindControl("txtPatInfonavit")).Text;

                ObrCuotaFija = ((TextBox)grvCuotaIMSS.Rows[e.RowIndex].FindControl("txtObrCuotaFija")).Text;
                ObrDinero = ((TextBox)grvCuotaIMSS.Rows[e.RowIndex].FindControl("txtObrDinero")).Text;
                ObrMedicos = ((TextBox)grvCuotaIMSS.Rows[e.RowIndex].FindControl("txtObrMedicos")).Text;
                ObrIV = ((TextBox)grvCuotaIMSS.Rows[e.RowIndex].FindControl("txtObrIV")).Text;
                ObrCV = ((TextBox)grvCuotaIMSS.Rows[e.RowIndex].FindControl("txtObrCV")).Text;
                ObrGuarderia = ((TextBox)grvCuotaIMSS.Rows[e.RowIndex].FindControl("txtObrGuarderia")).Text;
                ObrExcedente = ((TextBox)grvCuotaIMSS.Rows[e.RowIndex].FindControl("txtObrExcedente")).Text;             

                strQuery = string.Format("SP_ActualizaCuotasIMSS {0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}'",
             Id, Fecha, PatCuotaFija,  PatExcedente, PatDinero, PatIV, PatRetiro, PatCV, PatGastosMedicos, PatGuarderia, PatInfonavit, ObrCuotaFija, ObrDinero, ObrMedicos, ObrIV, ObrCV, ObrGuarderia, ObrExcedente);

                strRetunValue = clsQuery.execQueryString(strQuery);

                if (strRetunValue == "1")
                {
                    grvCuotaIMSS.EditIndex = -1;
                    ObtenCuotasIMSS();
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

        protected void LimpiarControlos()
        {
            txtFecha.Text = string.Empty;
            txtPatCuotaFija.Text = string.Empty;
            txtPatExcedente.Text = string.Empty;
            txtPatDinero.Text = string.Empty;
            txtPatIV.Text = string.Empty;
            txtPatRetiro.Text = string.Empty;
            txtPatCV.Text = string.Empty;
            txtPatGastosMedicos.Text = string.Empty;
            txtPatGuarderia.Text = string.Empty;
            txtPatInfonavit.Text = string.Empty;
            txtObrCuotaFija.Text = string.Empty;            
            txtObrDinero.Text = string.Empty;
            txtObrMedicos.Text = string.Empty;
            txtObrIV.Text = string.Empty;
            txtObrCV.Text = string.Empty;
            txtObrGuarderia.Text = string.Empty;
            txtObrExcedente.Text = string.Empty;
        }

        protected void grvCuotaIMSS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }
        }

    }
}