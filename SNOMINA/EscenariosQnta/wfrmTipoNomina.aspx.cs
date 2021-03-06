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
    public partial class wfrmTipoNomina : System.Web.UI.Page
    {
        clsDatos clsQuery = new clsDatos();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenTipoNomina();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string Nombre = string.Empty;                
                string strValue = string.Empty;

                Nombre = txtNombre.Text.ToString();

                strValue = clsQuery.execQueryString("SP_InsertaTipoNomina '" + Nombre + "'");

                if (strValue == "1")
                {
                    ObtenTipoNomina();
                    LimpiarControles();
                    Response.Write("<script>alert('Guardado');</script>");
                }
            }
            catch (Exception ex)
            {
                MensageError(ex.ToString());
            }
        }

        protected void gvTipoNomina_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvTipoNomina.EditIndex = -1;
        }

        protected void gvTipoNomina_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                string ID = ((Label)gvTipoNomina.Rows[e.RowIndex]
                                 .FindControl("lblId_TipoNom")).Text;
                string Nombre = ((TextBox)gvTipoNomina.Rows[e.RowIndex]
                                    .FindControl("txtNombre")).Text;                


                string strValue = string.Empty;

                strValue = clsQuery.execQueryString("SP_ActualizaTipoNomina " + ID + ",'" + Nombre + "'");

                if (strValue == "1")
                {
                    gvTipoNomina.EditIndex = -1;
                    ObtenTipoNomina();
                    Response.Write("<script>alert('Actualizado');</script>");
                }
            }
            catch (Exception ex)
            {
                MensageError(ex.ToString());
            }
        }

        protected void gvTipoNomina_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvTipoNomina.EditIndex = e.NewEditIndex;
        }

        protected void ObtenTipoNomina()
        {
            DataTable dtTipoNomina = new DataTable();

            dtTipoNomina = clsQuery.execQueryDataTable("SP_ObtenTipoNomina");

            if (dtTipoNomina.Rows.Count > 0)
            {
                gvTipoNomina.DataSource = dtTipoNomina;
                gvTipoNomina.DataBind();
            }
        }

        public void MensageError(string Mensaje)
        {
            Response.Write("<script>alert('Hubo un Error en el siatemapor favor contacte a Soporte: " + Mensaje + "');</script>");
        }

        protected void LimpiarControles()
        {
            txtNombre.Text = "";
           
        }

    }
}