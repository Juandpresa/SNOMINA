﻿using EscenariosQnta.Clases;
using EscenariosQnta.Data;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace EscenariosQnta
{
  public partial class wfrmEsquemaPago : System.Web.UI.Page
  {
    #region Variables

    string Id = string.Empty;
    string nombre = string.Empty;
    string spu = string.Empty;
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


        nombre = txtNEsquema.Text.ToString();
        spu = txtSPutilizado.Text.ToString();

        strQuery = string.Format("SP_InsertaEsquemaPago '{0}','{1}'", nombre, spu);

        strReturnValue = clsQuery.execQueryString(strQuery);

        if (strReturnValue == "1")
        {
          ObtenEsquemaPago();
          LimpiarControles();
          Mensaje("GUARDADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
        }
      }
      catch (Exception ex)
      {
        Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);

      }
    }

    protected void gvEsquemaPago_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
      gvEsquemaPago.EditIndex = -1;
      ObtenEsquemaPago();
    }

    protected void gvEsquemaPago_RowEditing(object sender, GridViewEditEventArgs e)
    {
      gvEsquemaPago.EditIndex = e.NewEditIndex;
      ObtenEsquemaPago();
    }

    protected void gvEsquemaPago_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
      try
      {
        Id = ((Label)gvEsquemaPago.Rows[e.RowIndex].FindControl("lblId_TpoEsq")).Text;
        nombre = ((TextBox)gvEsquemaPago.Rows[e.RowIndex].FindControl("txtNombreEsquema")).Text;
        spu = ((TextBox)gvEsquemaPago.Rows[e.RowIndex].FindControl("txtSPNombre")).Text;

        strQuery = string.Format("SP_ActualizaEsquemaPago {0}, '{1}','{2}'", Id, nombre, spu);

        strReturnValue = clsQuery.execQueryString(strQuery);

        if (strReturnValue == "1")
        {
          gvEsquemaPago.EditIndex = -1;
          ObtenEsquemaPago();
          Mensaje("ACTUALIZADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
        }
      }
      catch (Exception ex)
      {
        Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);

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

    protected void LimpiarControles()
    {
      txtNEsquema.Text = string.Empty;
      txtSPutilizado.Text = string.Empty;
    }

    public void Mensaje(string textomensaje, CuadroMensaje.CuadroMensajeIcono icono)
    {
      CuadroMensaje Mensaje = new CuadroMensaje(textomensaje, "MENSAJE", icono, CuadroMensaje.CuadroMensajeBoton.Ok, CuadroMensaje.CuadroMensajeEstilo.EstiloB);
      Literal ltrMensaje = (Literal)this.Master.FindControl("ltrMensaje");
      ltrMensaje.Text = Mensaje.Mostrar(this);
    }

  }
}