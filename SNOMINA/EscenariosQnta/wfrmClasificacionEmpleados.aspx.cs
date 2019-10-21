using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EscenariosQnta.Data;

namespace EscenariosQnta
{
    public partial class wfrmClasificacionEmpleados : System.Web.UI.Page
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
                ObtenClasificacionEmpleado();
            }

            txtDescripcion.Attributes.Add("onkeypress", "return clickButton('btnGuardar')");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {


                Descripcion = txtDescripcion.Text.ToString();

                strQuery = string.Format("SP_InsertaClasificacionEmpleado '{0}'", Descripcion);

                strReturnValue = clsQuery.execQueryString(strQuery);

                if (strReturnValue == "1")
                {
                    ObtenClasificacionEmpleado();
                    LimpiarControles();
                    Response.Write("<script>alert('Guardado');</script>");
                }
            }
            catch (Exception ex)
            {
                MensageError(ex.ToString());
            }
        }

        protected void gvClasificacionEmpleado_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvClasificacionEmpleado.EditIndex = -1;
        }

        protected void gvClasificacionEmpleado_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvClasificacionEmpleado.EditIndex = e.NewEditIndex;
        }

        protected void gvClasificacionEmpleado_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Id = ((Label)gvClasificacionEmpleado.Rows[e.RowIndex].FindControl("lblId_TpoClasEmp")).Text;
                Descripcion = ((TextBox)gvClasificacionEmpleado.Rows[e.RowIndex].FindControl("txtDescripcion")).Text;

                strQuery = string.Format("SP_ActualizaClasificacionEmpleado {0}, '{1}'", Id, Descripcion);

                strReturnValue = clsQuery.execQueryString(strQuery);

                if (strReturnValue == "1")
                {
                    gvClasificacionEmpleado.EditIndex = -1;
                    ObtenClasificacionEmpleado();
                    Response.Write("<script>alert('Actualizado');</script>");
                }
            }
            catch (Exception ex)
            {
                MensageError(ex.ToString());
            }
        }

        protected void ObtenClasificacionEmpleado()
        {
            DataTable dtClasificacionEmpleado = new DataTable();

            dtClasificacionEmpleado = clsQuery.execQueryDataTable("SP_ObtenClasificacionEmpleado");

            if (dtClasificacionEmpleado.Rows.Count > 0)
            {
                gvClasificacionEmpleado.DataSource = dtClasificacionEmpleado;
                gvClasificacionEmpleado.DataBind();
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