using System;
using EscenariosQnta.Clases;
using EscenariosQnta.Data;
using EscenariosQnta.Negocio;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EscenariosQnta.Clases;
using EscenariosQnta.Data;
using EscenariosQnta.Negocio;

namespace EscenariosQnta.Vista
{
  public partial class wfrmEdicionEmpleado : System.Web.UI.Page
  {
    clsDatos clsQuery = new clsDatos();
    int idEmp;
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        idEmp = (int)Session["IdEmpleado"];
        SpInformacion();


        ObtenTipoEsquema();
      }
    }
    protected void ObtenTipoEsquema()
    {
      DataTable dtTipoEsquema = new DataTable();

      dtTipoEsquema = clsQuery.execQueryDataTable("Sp_ObtenTipoEsquema");

      if (dtTipoEsquema.Rows.Count > 0)
      {
        rbtTipoEsquema.DataSource = dtTipoEsquema;
        rbtTipoEsquema.DataTextField = "TipoEsquema";
        rbtTipoEsquema.DataValueField = "Id_TipoEsquema";
        rbtTipoEsquema.DataBind();
      }
    }
    public void SpInformacion()
    {
      string[] es = new string[38];
      DataTable dtEMP = new DataTable();
      dtEMP = BLLEmpleado.ObtenEmpleadoById(idEmp);
      for (int i = 0; i <= dtEMP.Rows.Count; i++)
      {
        for (int j = 0; j < 37; j++)
        {
          es[j] = dtEMP.Rows[0][j].ToString();
        }
      }
      //ddlEmpleadora.Text = es[1].ToString();
      ddlCliente.Text = es[2].ToString();
      txtClave.Text = es[3].ToString();
      txtNombre.Text = es[4].ToString();
      txtPaterno.Text = es[5].ToString();
      txtMaterno.Text = es[6].ToString();
      //ddlSexo.Text = es[7].ToString();
      txtFechaNacimiento.Text = es[8].ToString();
      txtCurp.Text = es[9].ToString();
      txtRfc.Text = es[10].ToString();
      txtCorreo.Text = es[11].ToString();
      txtTelefonoMovil.Text = es[12].ToString();
      txtTelefonoLocal.Text = es[13].ToString();
    }
  }
}