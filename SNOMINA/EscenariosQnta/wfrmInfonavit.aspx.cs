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
    public partial class wfrmInfonavit : System.Web.UI.Page
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
                ObtenInfonavit();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {


                Descripcion = txtDescripcion.Text.ToString();

                strQuery = string.Format("SP_InsertaInfonavit '{0}'", Descripcion);

                strReturnValue = clsQuery.execQueryString(strQuery);

                if (strReturnValue == "1")
                {
                    ObtenInfonavit();
                    LimpiarControles();
                    Response.Write("<script>alert('Guardado');</script>");
                }
            }
            catch (Exception ex)
            {
                MensageError(ex.ToString());
            }
        }

        protected void gvInfonavit_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvInfonavit.EditIndex = -1;
        }

        protected void gvInfonavit_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvInfonavit.EditIndex = e.NewEditIndex;
        }

        protected void gvInfonavit_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Id = ((Label)gvInfonavit.Rows[e.RowIndex].FindControl("lblId_TpoInfo")).Text;
                Descripcion = ((TextBox)gvInfonavit.Rows[e.RowIndex].FindControl("txtDescripcion")).Text;

                strQuery = string.Format("SP_ActualizaInfonavit {0}, '{1}'",  Id , Descripcion);

                strReturnValue = clsQuery.execQueryString(strQuery);

                if (strReturnValue == "1")
                {
                    gvInfonavit.EditIndex = -1;
                    ObtenInfonavit();
                    Response.Write("<script>alert('Actualizado');</script>");
                }
            }
            catch (Exception ex)
            {
                MensageError(ex.ToString());
            }
        }

        protected void ObtenInfonavit()
        {
            DataTable dtInfonavit = new DataTable();

            dtInfonavit = clsQuery.execQueryDataTable("SP_ObtenInfonavit");

            if (dtInfonavit.Rows.Count > 0)
            {
                gvInfonavit.DataSource = dtInfonavit;
                gvInfonavit.DataBind();
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