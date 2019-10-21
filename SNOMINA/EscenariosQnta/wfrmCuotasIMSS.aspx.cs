using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EscenariosQnta.Data;
using System.Data;

namespace EscenariosQnta
{
    public partial class wfrmCuotasIMSS : System.Web.UI.Page
    {

        #region Variables
        
        string Id = string.Empty;
        string Fecha = string.Empty;
        string PatCuota = string.Empty;
        string PatExced = string.Empty;
        string PatDinero = string.Empty;
        string PatEspecie = string.Empty;
        string PatIV = string.Empty;
        string PatRetiro = string.Empty;
        string PatCV = string.Empty;
        string ObrExc = string.Empty;
        string ObrDinero = string.Empty;
        string ObrEspecie = string.Empty;
        string ObrIV = string.Empty;
        string ObrCV = string.Empty;
        string Infonavit = string.Empty;
        string Excedente = string.Empty;
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
            PatCuota = txtPatCuotaFija.Text.ToString();
            PatExced = txtPatExced.Text.ToString();
            PatDinero = txtPatDinero.Text.ToString();
            PatEspecie = txtPatEspecie.Text.ToString();
            PatIV = txtPatIV.Text.ToString();
            PatRetiro = txtPatRetiro.Text.ToString();
            PatCV = txtPatCV.Text.ToString();
            ObrExc = txtObrExc.Text.ToString();
            ObrDinero = txtObrDinero.Text.ToString();
            ObrEspecie = txtObrEspecie.Text.ToString();
            ObrIV = txtObrIV.Text.ToString();
            ObrCV = txtObrCV.Text.ToString();
            Infonavit = txtInfonavit.Text.ToString();
            Excedente = txtExcedente.Text.ToString();

            string query = string.Format("SP_InsertaCuotasIMSS '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}'",
              Fecha, PatCuota, PatExced, PatDinero, PatEspecie, PatIV, PatRetiro, PatCV, ObrExc, ObrDinero, ObrEspecie, ObrIV, ObrCV, Infonavit, Excedente);

            clsQuery.execQueryString(query);
            
            ObtenCuotasIMSS();
            Response.Write("<script>alert('Guardo');</script>");
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
        }

        protected void grvCuotaIMSS_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Id = ((Label)grvCuotaIMSS.Rows[e.RowIndex].FindControl("lblId_Cuota")).Text;
                Fecha = ((TextBox)grvCuotaIMSS.Rows[e.RowIndex].FindControl("txtFecha")).Text;
                PatCuota = ((TextBox)grvCuotaIMSS.Rows[e.RowIndex].FindControl("txtPatCuotaFija")).Text;
                PatExced = ((TextBox)grvCuotaIMSS.Rows[e.RowIndex].FindControl("txtPatExced")).Text;
                PatDinero = ((TextBox)grvCuotaIMSS.Rows[e.RowIndex].FindControl("txtPatDinero")).Text;
                PatEspecie = ((TextBox)grvCuotaIMSS.Rows[e.RowIndex].FindControl("txtPatEspecie")).Text;
                PatIV = ((TextBox)grvCuotaIMSS.Rows[e.RowIndex].FindControl("txtPatIV")).Text;
                PatRetiro = ((TextBox)grvCuotaIMSS.Rows[e.RowIndex].FindControl("txtPatRetiro")).Text;
                PatCV = ((TextBox)grvCuotaIMSS.Rows[e.RowIndex].FindControl("txtPatCV")).Text;
                ObrExc = ((TextBox)grvCuotaIMSS.Rows[e.RowIndex].FindControl("txtObrExc")).Text;
                ObrDinero = ((TextBox)grvCuotaIMSS.Rows[e.RowIndex].FindControl("txtObrDinero")).Text;
                ObrEspecie = ((TextBox)grvCuotaIMSS.Rows[e.RowIndex].FindControl("txtObrEspecie")).Text;
                ObrIV = ((TextBox)grvCuotaIMSS.Rows[e.RowIndex].FindControl("txtObrIV")).Text;
                ObrCV = ((TextBox)grvCuotaIMSS.Rows[e.RowIndex].FindControl("txtObrCV")).Text;
                Infonavit = ((TextBox)grvCuotaIMSS.Rows[e.RowIndex].FindControl("txtInfonavit")).Text;
                Excedente = ((TextBox)grvCuotaIMSS.Rows[e.RowIndex].FindControl("txtExcedente")).Text;

                string strQuery = string.Empty;

                strQuery = string.Format("SP_ActualizaCuotasIMSS '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}'",
                                    ID, Fecha, PatCuota, PatExced, PatDinero, PatEspecie, PatIV, PatRetiro, PatCV, ObrExc, ObrDinero, ObrEspecie, ObrIV, ObrCV, Infonavit, Excedente);

                clsQuery.execQueryString(strQuery);

                //if (strQuery == "1")
                //{
                grvCuotaIMSS.EditIndex = -1;
                ObtenCuotasIMSS();
                Response.Write("<script>alert('Actualizado');</script>");
                //}
            }
            catch (Exception ex)
            {
                //MensageError(ex.ToString());
            }
        }
            

    }
}