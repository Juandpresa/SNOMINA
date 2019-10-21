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
    public partial class wfrmPrestacion : System.Web.UI.Page
    {
        #region

        //string IdFactor = string.Empty;
        string Nombre = string.Empty;
        string Descripcion = string.Empty;

        string Id;
        string TipoValor = string.Empty;
        string Valor = string.Empty;       

        string strQuery = string.Empty;
        string returnValue = string.Empty;
        int IdPrestacion = 0;
        clsDatos clsQuery = new clsDatos();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenPrestaciones();
            }
        }

        protected void btnAgregarEditar_Click(object sender, EventArgs e)
        {
            divFactor.Visible = true;
            gvPrestacionesDetalle.Visible = false;
            ddlPrestaciones.SelectedIndex = 0;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            divFactor.Visible = false;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Nombre = txtNombre.Text.ToString();
            Descripcion = txtDescripcion.Text.ToString();

            if (string.IsNullOrEmpty(Nombre))
            {
                Response.Write("<script>alert('Debe ingresar el nombre de una Prestacion');</script>");
            }
            else
            {

                strQuery = string.Format("SP_InsertaPrestaciones '{0}', '{1}'", Nombre, Descripcion);
                returnValue = clsQuery.execQueryString(strQuery);


                if (returnValue == "1")
                {
                    Response.Write("<script>alert('Guardado');</script>");
                    ObtenPrestaciones();
                    txtNombre.Text = string.Empty;
                    txtDescripcion.Text = string.Empty;
                }
                else
                {

                    Response.Write("<script>alert('Hubi un erro al intentar guardar, por favor contacte a sistemas');</script>");
                }
            }
        }

        protected void ObtenPrestaciones()
        {
            DataTable dtFactor = new DataTable();

            dtFactor = clsQuery.execQueryDataTable("SP_ObtenPrestaciones");

            if (dtFactor.Rows.Count > 0)
            {
                ddlPrestaciones.DataSource = dtFactor;
                ddlPrestaciones.DataTextField = "Nombre";
                ddlPrestaciones.DataValueField = "Id_Prest";
                ddlPrestaciones.DataBind();

                gvPrestaciones.DataSource = dtFactor;
                gvPrestaciones.DataBind();
            }

            ddlPrestaciones.Items.Insert(0, new ListItem(">> Seleccione una Opcion <<", "0"));

        }

        protected void ddlPrestaciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlPrestaciones.SelectedItem.Value))
            {
                IdPrestacion = int.Parse(ddlPrestaciones.SelectedItem.Value);

                if (IdPrestacion != 0)
                {
                    ObtenPrestacionesDetalle();
                    divFactor.Visible = false;

                }
                else
                {

                    gvPrestacionesDetalle.Visible = false;

                }
            }
        }

        protected void ObtenPrestacionesDetalle()
        {
            IdPrestacion = int.Parse(ddlPrestaciones.SelectedItem.Value);

            DataTable dtFactorDetalle = new DataTable();
            strQuery = string.Format("SP_ObtenPrestacionesDetalle {0}", IdPrestacion);

            dtFactorDetalle = clsQuery.execQueryDataTable(strQuery);

            if (dtFactorDetalle.Rows.Count > 0)
            {
                gvPrestacionesDetalle.DataSource = dtFactorDetalle;
                gvPrestacionesDetalle.DataBind();
                gvPrestacionesDetalle.Visible = true;
            }

        }

        protected void gvPrestacionesDetalle_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPrestacionesDetalle.EditIndex = -1;
            ObtenPrestacionesDetalle();
        }

        protected void gvPrestacionesDetalle_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvPrestacionesDetalle.EditIndex = e.NewEditIndex;
            ObtenPrestacionesDetalle();
        }

        protected void gvPrestacionesDetalle_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Id = ((Label)gvPrestacionesDetalle.Rows[e.RowIndex]
                                .FindControl("lblId")).Text;
                TipoValor = ((TextBox)gvPrestacionesDetalle.Rows[e.RowIndex]
                                   .FindControl("txtTipoValor")).Text;
                Valor = ((TextBox)gvPrestacionesDetalle.Rows[e.RowIndex]
                                  .FindControl("txtValor")).Text;


                strQuery = string.Format("SP_ActualizaPrestacionesDetalle {0}, '{1}', '{2}'", Id, TipoValor, Valor);

                returnValue = clsQuery.execQueryString(strQuery);

                if (returnValue == "1")
                {
                    gvPrestacionesDetalle.EditIndex = -1;
                    ObtenPrestacionesDetalle();
                    Response.Write("<script>alert('Actualizado');</script>");
                }
            }
            catch (Exception ex) { }
        }

        protected void gvPrestaciones_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPrestaciones.EditIndex = -1;
            ObtenPrestaciones();
        }

        protected void gvPrestaciones_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvPrestaciones.EditIndex = e.NewEditIndex;
            ObtenPrestaciones();
        }

        protected void gvPrestaciones_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Id = ((Label)gvPrestaciones.Rows[e.RowIndex]
                                .FindControl("lblId_Prest")).Text;
                Nombre = ((TextBox)gvPrestaciones.Rows[e.RowIndex]
                                   .FindControl("txtNombre")).Text;
                Descripcion = ((TextBox)gvPrestaciones.Rows[e.RowIndex]
                                   .FindControl("txtDescripcion")).Text;


                strQuery = string.Format("SP_ActualizaPrestaciones {0}, '{1}', '{2}'", Id, Nombre, Descripcion);

                returnValue = clsQuery.execQueryString(strQuery);

                if (returnValue == "1")
                {
                    gvPrestaciones.EditIndex = -1;
                    ObtenPrestaciones();
                    Response.Write("<script>alert('Actualizado');</script>");
                }
            }
            catch (Exception ex) { }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            IdPrestacion = int.Parse(ddlPrestaciones.SelectedItem.Value);
            TipoValor = ((TextBox)gvPrestacionesDetalle.FooterRow.FindControl("txtTipoValor")).Text;
            Valor = ((TextBox)gvPrestacionesDetalle.FooterRow.FindControl("txtValor")).Text;


            strQuery = string.Format("SP_InsertaPrestacionesDetalle {0}, '{1}', '{2}'", IdPrestacion, TipoValor, Valor);

            returnValue = clsQuery.execQueryString(strQuery);

            if (returnValue == "1")
            {
                gvPrestacionesDetalle.EditIndex = -1;
                ObtenPrestacionesDetalle();
                Response.Write("<script>alert('Actualizado');</script>");
            }


        }
    }
}