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
    public partial class wfrmImpuesto : System.Web.UI.Page
    {
        #region

        //string IdFactor = string.Empty;
        string Nombre = string.Empty;

        string Id;
        string LimInf = string.Empty;
        string LimSup = string.Empty;
        string CuotaFija = string.Empty;
        string Porcentaje = string.Empty;
       

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

                ddlPeriodo.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "0"));     
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                IdPeriodo = int.Parse(ddlPeriodo.SelectedItem.Value);
                LimInf = ((TextBox)gvImpuesto.FooterRow.FindControl("txtLimInf")).Text;
                LimSup = ((TextBox)gvImpuesto.FooterRow.FindControl("txtLimSup")).Text;
                CuotaFija = ((TextBox)gvImpuesto.FooterRow.FindControl("txtCuotaFija")).Text;
                Porcentaje = ((TextBox)gvImpuesto.FooterRow.FindControl("txtPorcentaje")).Text;


                strQuery = string.Format("SP_InsertaImpuesto {0}, '{1}', '{2}', '{3}', '{4}' ", IdPeriodo, LimInf, LimSup, CuotaFija, Porcentaje);

                returnValue = clsQuery.execQueryString(strQuery);

                if (returnValue == "1")
                {
                    gvImpuesto.EditIndex = -1;
                    ObtenImpuesto();
                    Response.Write("<script>alert('Actualizado');</script>");
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected void ObtenImpuesto()
        {

            try
            {
                IdPeriodo = int.Parse(ddlPeriodo.SelectedItem.Value);

                DataTable dtImporte = new DataTable();
                strQuery = string.Format("SP_ObtenImpuesto {0}", IdPeriodo);

                dtImporte = clsQuery.execQueryDataTable(strQuery);

                //if (dtImporte.Rows.Count > 0)
                //{
                    gvImpuesto.DataSource = dtImporte;
                    gvImpuesto.DataBind();
                    //gvImpuesto.Visible = true;
                //}
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }

        protected void ddlPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlPeriodo.SelectedItem.Value))
            {
                IdPeriodo = int.Parse(ddlPeriodo.SelectedItem.Value);

                if (IdPeriodo != 0)
                {
                    ObtenImpuesto();
                    gvImpuesto.Visible = true;
                }
                else
                {
                    gvImpuesto.Visible = false;

                }
            }
        }

        protected void gvImpuesto_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvImpuesto.EditIndex = -1;
            ObtenImpuesto();
        }

        protected void gvImpuesto_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvImpuesto.EditIndex = e.NewEditIndex;
            ObtenImpuesto();
        }

        protected void gvImpuesto_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Id = ((Label)gvImpuesto.Rows[e.RowIndex].FindControl("lblId")).Text;
                LimInf = ((TextBox)gvImpuesto.Rows[e.RowIndex].FindControl("txtLimInf")).Text.Replace(',','.');
                LimSup = ((TextBox)gvImpuesto.Rows[e.RowIndex].FindControl("txtLimSup")).Text.Replace(',', '.');
                CuotaFija = ((TextBox)gvImpuesto.Rows[e.RowIndex].FindControl("txtCuotaFija")).Text.Replace(',', '.');
                Porcentaje = ((TextBox)gvImpuesto.Rows[e.RowIndex].FindControl("txtPorcentaje")).Text.Replace(',', '.');


                strQuery = string.Format("SP_ActualizaImpuesto {0}, '{1}', '{2}', '{3}', '{4}' ", Id, LimInf, LimSup, CuotaFija, Porcentaje);

                returnValue = clsQuery.execQueryString(strQuery);

                if (returnValue == "1")
                {
                    gvImpuesto.EditIndex = -1;
                    ObtenImpuesto();
                    Response.Write("<script>alert('Actualizado');</script>");
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }
}