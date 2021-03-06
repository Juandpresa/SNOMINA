﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EscenariosQnta.Data;

namespace EscenariosQnta
{
    public partial class wfrmEsquemaPago : System.Web.UI.Page
    {
        #region Variables

        string Id = string.Empty;
        string Descripcion = string.Empty;
        string strQuery = string.Empty;
        string strReturnValue = string.Empty;

        clsDatos clsQuery = new clsDatos();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenEsquemaPago();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {


                Descripcion = txtDescripcion.Text.ToString();

                strQuery = string.Format("SP_InsertaEsquemaPago '{0}'", Descripcion);

                strReturnValue = clsQuery.execQueryString(strQuery);

                if (strReturnValue == "1")
                {
                    ObtenEsquemaPago();
                    LimpiarControles();
                    Response.Write("<script>alert('Guardado');</script>");
                }
            }
            catch (Exception ex)
            {
                MensageError(ex.ToString());
            }
        }

        protected void gvEsquemaPago_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEsquemaPago.EditIndex = -1;
        }

        protected void gvEsquemaPago_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEsquemaPago.EditIndex = e.NewEditIndex;
        }

        protected void gvEsquemaPago_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Id = ((Label)gvEsquemaPago.Rows[e.RowIndex].FindControl("lblId_TpoEsq")).Text;
                Descripcion = ((TextBox)gvEsquemaPago.Rows[e.RowIndex].FindControl("txtDescripcion")).Text;

                strQuery = string.Format("SP_ActualizaEsquemaPago {0}, '{1}'", Id, Descripcion);

                strReturnValue = clsQuery.execQueryString(strQuery);

                if (strReturnValue == "1")
                {
                    gvEsquemaPago.EditIndex = -1;
                    ObtenEsquemaPago();
                    Response.Write("<script>alert('Actualizado');</script>");
                }
            }
            catch (Exception ex)
            {
                MensageError(ex.ToString());
            }
        }

        protected void ObtenEsquemaPago()
        {
            DataTable dtEsquemaPago = new DataTable();

            dtEsquemaPago = clsQuery.execQueryDataTable("SP_ObtenEsquemaPago");

            if (dtEsquemaPago.Rows.Count > 0)
            {
                gvEsquemaPago.DataSource = dtEsquemaPago;
                gvEsquemaPago.DataBind();
            }
        }

        public void MensageError(string Mensaje)
        {
            Response.Write("<script>alert('Hubo un Error en el siatemapor favor contacte a Soporte: " + Mensaje + "');</script>");
        }

        protected void LimpiarControles()
        {
            txtDescripcion.Text = "";
        }
    }
}