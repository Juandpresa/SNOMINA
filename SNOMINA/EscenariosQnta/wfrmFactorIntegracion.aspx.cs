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
    public partial class WebForm3 : System.Web.UI.Page
    {
        #region Variables

        //string IdFactor = string.Empty;
        string Nombre = string.Empty;

        string Id;
        string Antiguedad = string.Empty;
        string DiasAguin = string.Empty;
        string DiasVac = string.Empty;
        string PorcPrima = string.Empty;
        string OtraPrestacion = string.Empty;
        string Integracion = string.Empty;
        //float Integracion = 0;

        string strQuery = string.Empty;
        string returnValue = string.Empty;
        int IdFactor = 0;
        clsDatos clsQuery = new clsDatos();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenFactor();
            }
        }

        protected void btnAgregarEditar_Click(object sender, EventArgs e)
        {
            divFactor.Visible = true;
            gvDetalleFactor.Visible = false;
            ddlFactor.SelectedIndex = 0;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            divFactor.Visible = false;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Nombre = txtNombre.Text.ToString();

            if (string.IsNullOrEmpty(Nombre))
            {
                Response.Write("<script>alert('Debe ingresar el nombre de un Factor');</script>");
            }
            else
            {

                strQuery = string.Format("SP_InsertaFactor '{0}'", Nombre);
                returnValue = clsQuery.execQueryString(strQuery);


                if (returnValue == "1")
                {
                    Response.Write("<script>alert('Guardado');</script>");
                    ObtenFactor();
                    txtNombre.Text = string.Empty;
                }
                else
                {

                    Response.Write("<script>alert('Hubi un erro al intentar guardar, por favor contacte a sistemas');</script>");
                }
            }
        }

        protected void ObtenFactor()
        {
            DataTable dtFactor = new DataTable();

            dtFactor = clsQuery.execQueryDataTable("SP_ObtenFactor");

            if (dtFactor.Rows.Count > 0)
            {
                ddlFactor.DataSource = dtFactor;
                ddlFactor.DataTextField = "Nombre";
                ddlFactor.DataValueField = "Id_Factor";
                ddlFactor.DataBind();

                gvFactor.DataSource = dtFactor;
                gvFactor.DataBind();
            }

            ddlFactor.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "0"));

        }

        protected void ddlFactor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlFactor.SelectedItem.Value))
            {
                IdFactor = int.Parse(ddlFactor.SelectedItem.Value);

                if (IdFactor != 0)
                {
                    ObtenFactorDetalle();
                    divFactor.Visible = false;

                }
                else
                {

                    gvDetalleFactor.Visible = false;

                }
            }
        }

        protected void ObtenFactorDetalle()
        {
            IdFactor = int.Parse(ddlFactor.SelectedItem.Value);

            DataTable dtFactorDetalle = new DataTable();
            strQuery = string.Format("SP_ObtenFactorDetalle {0}", IdFactor);

            dtFactorDetalle = clsQuery.execQueryDataTable(strQuery);

            if (dtFactorDetalle.Rows.Count > 0)
            {
                gvDetalleFactor.DataSource = dtFactorDetalle;
                gvDetalleFactor.DataBind();
                gvDetalleFactor.Visible = true;
            }

        }

        protected void gvDetalleFactor_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDetalleFactor.EditIndex = -1;
            ObtenFactorDetalle();
        }

        protected void gvDetalleFactor_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDetalleFactor.EditIndex = e.NewEditIndex;
            ObtenFactorDetalle();
        }

        protected void gvDetalleFactor_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Id = ((Label)gvDetalleFactor.Rows[e.RowIndex]
                                .FindControl("lblId")).Text;
                Antiguedad = ((TextBox)gvDetalleFactor.Rows[e.RowIndex]
                                   .FindControl("txtAntiguedad")).Text;
                DiasAguin = ((TextBox)gvDetalleFactor.Rows[e.RowIndex]
                                  .FindControl("txtDiasAguin")).Text;
                DiasVac = ((TextBox)gvDetalleFactor.Rows[e.RowIndex]
                                   .FindControl("txtDiasVac")).Text;
                PorcPrima = ((TextBox)gvDetalleFactor.Rows[e.RowIndex]
                                   .FindControl("txtPorcPrima")).Text;
                OtraPrestacion = ((TextBox)gvDetalleFactor.Rows[e.RowIndex]
                                .FindControl("txtOtraPrestacion")).Text;
                Integracion = Integracion = (1 + ((float.Parse(DiasVac) / 365) * (float.Parse(PorcPrima) / 100)) + (float.Parse(DiasAguin) / 365) + (float.Parse(OtraPrestacion) / 365)).ToString().Replace(",", "."); 
                
                //Integracion = ((TextBox)gvDetalleFactor.Rows[e.RowIndex]
                //                  .FindControl("txtIntegracion")).Text;

                strQuery = string.Format("SP_ActualizaFactorDetalle {0}, {1}, {2}, {3}, '{4}', '{5}', '{6}'", Id, Antiguedad, DiasAguin, DiasVac, PorcPrima, OtraPrestacion, Integracion);

                returnValue = clsQuery.execQueryString(strQuery);

                if (returnValue == "1")
                {
                    gvDetalleFactor.EditIndex = -1;
                    ObtenFactorDetalle();
                    Response.Write("<script>alert('Actualizado');</script>");
                }
            }
            catch (Exception ex) { }
        }

        protected void gvFactor_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvFactor.EditIndex = -1;
            ObtenFactor();
        }

        protected void gvFactor_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvFactor.EditIndex = e.NewEditIndex;
            ObtenFactor();
        }

        protected void gvFactor_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Id = ((Label)gvFactor.Rows[e.RowIndex]
                                .FindControl("lblId_Factor")).Text;
                Nombre = ((TextBox)gvFactor.Rows[e.RowIndex]
                                   .FindControl("txtNombre")).Text;


                strQuery = string.Format("SP_ActualizaFactor {0}, '{1}'", Id, Nombre);

                returnValue = clsQuery.execQueryString(strQuery);

                if (returnValue == "1")
                {
                    gvFactor.EditIndex = -1;
                    ObtenFactor();
                    Response.Write("<script>alert('Actualizado');</script>");
                }
            }
            catch (Exception ex) { }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            IdFactor = int.Parse(ddlFactor.SelectedItem.Value);
            Antiguedad = ((TextBox)gvDetalleFactor.FooterRow.FindControl("txtAntiguedad")).Text;
            DiasAguin = ((TextBox)gvDetalleFactor.FooterRow.FindControl("txDiasAguin")).Text;
            DiasVac = ((TextBox)gvDetalleFactor.FooterRow.FindControl("txDiasVac")).Text;
            PorcPrima = ((TextBox)gvDetalleFactor.FooterRow.FindControl("txtPorcPrima")).Text;
            OtraPrestacion = ((TextBox)gvDetalleFactor.FooterRow.FindControl("txtOtraPrestacion")).Text;
            //Integracion = int.Parse( ((TextBox)gvDetalleFactor.FooterRow.FindControl("txtIntegracion")).Text);
            Integracion = (1 + ((float.Parse(DiasVac) / 365) * (float.Parse(PorcPrima)/100)) + (float.Parse(DiasAguin) / 365) + (float.Parse(OtraPrestacion) / 365)).ToString().Replace(",",".");
            
            strQuery = string.Format("SP_InsertaFactorDetalle {0}, {1}, {2}, {3}, '{4}', '{5}', '{6}'", IdFactor, Antiguedad, DiasAguin, DiasVac, PorcPrima, OtraPrestacion, Integracion);

            returnValue = clsQuery.execQueryString(strQuery);

            if (returnValue == "1")
            {
                gvDetalleFactor.EditIndex = -1;
                ObtenFactorDetalle();
                Response.Write("<script>alert('Actualizado');</script>");
            }

            
        }
    }
}