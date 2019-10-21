using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EscenariosQnta.Data;
using System.Data;
using EscenariosQnta.Clases;


namespace EscenariosQnta
{
    public partial class wfrmSalarioMinimoProfesionales : System.Web.UI.Page
    {
        #region Variables
        string Id = string.Empty;
        string Fecha = string.Empty;
        string Profesionales = string.Empty;
        string Valor = string.Empty;

        string strQuery = string.Empty;
        string returnValue = string.Empty;

        clsDatos clsQuery = new clsDatos();

        //public System.Web.UI.WebControls.Literal ltrMensaje = new Literal();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenSalarioMinimoProf();                
            }

            // Gets a reference to a TextBox control inside 
            // a ContentPlaceHolder
            ContentPlaceHolder mpContentPlaceHolder;
            TextBox mpTextBox;
            mpContentPlaceHolder =  (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            if (mpContentPlaceHolder != null)
            {
                mpTextBox = (TextBox)mpContentPlaceHolder.FindControl("txtDevtroce");
                if (mpTextBox != null)
                {
                    mpTextBox.Text = "TextBox found!";
                    Response.Write("<script>alert('TextBox found!);</script>");
                }
            }

            // Gets a reference to a Label control that not in 
            // a ContentPlaceHolder
            Label mpLabel = (Label)Master.FindControl("masterPageLabel");
            if (mpLabel != null)
            {
                //Label1.Text = "Master page label = " + mpLabel.Text;
                Response.Write("<script>alert('Master page label = " + mpLabel.Text + ");</script>");
            }

            ltrMensaje.Text = string.Empty;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

            Fecha = txtFecha.Text.ToString().Replace("-","");
            Profesionales = txtProfesionales.Text.ToString();
            Valor = txtValor.Text.ToString();

            strQuery = string.Format("SP_InsertaSalarioMinimoProfesional '{0}', '{1}', '{2}'", Fecha, Profesionales, Valor);

            returnValue = clsQuery.execQueryString(strQuery);

            if(returnValue == "1")
            {
                ObtenSalarioMinimoProf();
                LimpiarControloes();
                //Response.Write("<script>alert('Guardado');</script>");

                //MessageBoxs messageBox = new MessageBoxs();
                //messageBox.MessageTitle = "Information";
                //messageBox.MessageText = "Hello ";                
                //Literal1.Text = messageBox.Show(this);

                CuadroMensaje Mensaje = new CuadroMensaje();
                Mensaje.MensajeTitulo = "Prueba Titulo";
                Mensaje.MensajeTexto = "Prueba Texto";
               
                ltrMensaje.Text = Mensaje.Mostrar(this);

            }
        }

        protected void gvSalarioMinimoProf_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSalarioMinimoProf.EditIndex = -1;
            ObtenSalarioMinimoProf();
            
        }

        protected void gvSalarioMinimoProf_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvSalarioMinimoProf.EditIndex = e.NewEditIndex;
            ObtenSalarioMinimoProf();
        }

        protected void gvSalarioMinimoProf_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Id = ((Label)gvSalarioMinimoProf.Rows[e.RowIndex].FindControl("lblId_SalMinProf")).Text.Replace(",", ".");
            Fecha = ((TextBox)gvSalarioMinimoProf.Rows[e.RowIndex].FindControl("txtFecha")).Text.Replace("-","");
            Profesionales = ((TextBox)gvSalarioMinimoProf.Rows[e.RowIndex].FindControl("txtProfesionales")).Text.Replace(",", ".");
            Valor = ((TextBox)gvSalarioMinimoProf.Rows[e.RowIndex].FindControl("txtValor")).Text.Replace(",", ".");


            strQuery = string.Format("SP_ActualizaSalarioMinimoProfesional '{0}', '{1}', '{2}', '{3}'", Id, Fecha, Profesionales, Valor);

            returnValue = clsQuery.execQueryString(strQuery);

            if (returnValue == "1")
            {
                gvSalarioMinimoProf.EditIndex = -1;
                ObtenSalarioMinimoProf();
                //Response.Write("<script>alert('Actualizado');</script>");

                CuadroMensaje Mensaje = new CuadroMensaje("Prueba Titulo", "Prueba Texto", CuadroMensaje.CuadroMensajeIcono.Advertencia, CuadroMensaje.CuadroMensajeBoton.YesNoCancel, CuadroMensaje.CuadroMensajeEstilo.EstiloB);
                //Mensaje.MensajeTitulo = "Prueba Titulo";
                //Mensaje.MensajeTexto = "Prueba Texto";
                
                ltrMensaje.Text = Mensaje.Mostrar(this);

                
            }
        }

        protected void ObtenSalarioMinimoProf()
        {
            DataTable dtSalarioMinProf = new DataTable();
            dtSalarioMinProf = clsQuery.execQueryDataTable("SP_ObtenSalarioMinimoProfesional");

            if (dtSalarioMinProf.Rows.Count > 0)
            {
                gvSalarioMinimoProf.DataSource = dtSalarioMinProf;
                gvSalarioMinimoProf.DataBind();
            }
        }

        protected void LimpiarControloes()
        {
            txtFecha.Text = string.Empty;
            txtProfesionales.Text = string.Empty;
            txtValor.Text = string.Empty;
        }


    }
}