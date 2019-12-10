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
using EscenariosQnta.Negocio;

namespace EscenariosQnta.Vista
{
  public partial class wfrmEdicionEmpleado : System.Web.UI.Page
  {
    int idEmp;
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        idEmp = (int)Session["IdEmpleado"];
        SpInformacion();
      }
    }

    public void SpInformacion()
    {
      DataTable dtEMP = new DataTable();
      dtEMP = BLLEmpleado.ObtenEmpleadoById(idEmp);
      for (int i = 0; i <= dtEMP.Rows.Count; i++)
      {
        for (int j = 2; j < 30; j++)
        {
          string es = dtEMP.Rows[0][j].ToString();
        }
      }
    }
  }
}