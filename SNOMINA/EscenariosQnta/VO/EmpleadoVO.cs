using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace EscenariosQnta.VO
{
  public class EmpleadoVO
  {
    #region datos privados
    private int _Id_Empleado;
    private int _Id_Escenario;
    private int _Id_Cliente;
    private int _Id_PrimaRgo;
    private string _Nombre;
    private string _Paterno;
    private string _Materno;
    private string _Puesto;
    private string _DescriPto;
    private string _UbicaLabora;
    private DateTime _FechaIngreso;
    private DateTime _FechaNac;
    private float _PorcNomina;
    private float _PorcAsimilados;
    private float _PorcHonorarios;
    private float _PorcTN;
    private float _PorcEZWallet;
    private float _Sueldo;
    private float _SueldoBruto;
    private float _SueldoNeto;
    private float _SueldoHonorarios;
    private float _SueldoTN;
    private float _SueldoEZWallet;
    private double _Comisiones;
    private double _Bono;
    private double _OtrosIngresos;
    private string _ImpFonacot;
    private int _Id_Infonavit;
    private float _ImporteInfonavit;
    private int _Id_Prestac;
    private int _Id_Pension;
    private float _ImportePension;
    private int _Id_EsquemaActual;
    private int _Id_ClasifEmp;
    private string _Nacionalidad;
    private double _SueldoDiario;
    private int _Antiguedad;
    private int _TipoEsquema;
    private string _Clave;
    private string _ClaveAnterior;
    private int _RSocialPagadoraID;
    private int _SexoID;
    private int _TipoPagoID;
    private int _EstatusID;
    private string _Curp;
    private string _Rfc;
    private string _CorreoElectronico;
    private string _TelefonoLocal;
    private string _TelefonoMovil;
    private double _SueldoIntegracion;
    private DateTime _FechaUltimoPago;
    private int _PeriodoPagoID;
    #endregion

    #region Propiedades del objeto
    public int Id_Empleado { get => _Id_Empleado; set => _Id_Empleado = value; }
    public int Id_Escenario { get => _Id_Escenario; set => _Id_Escenario = value; }
    public int Id_Cliente { get => _Id_Cliente; set => _Id_Cliente = value; }
    public int Id_PrimaRgo { get => _Id_PrimaRgo; set => _Id_PrimaRgo = value; }
    public string Nombre { get => _Nombre; set => _Nombre = value; }
    public string Paterno { get => _Paterno; set => _Paterno = value; }
    public string Materno { get => _Materno; set => _Materno = value; }
    public string Puesto { get => _Puesto; set => _Puesto = value; }
    public string DescriPto { get => _DescriPto; set => _DescriPto = value; }
    public string UbicaLabora { get => _UbicaLabora; set => _UbicaLabora = value; }
    public DateTime FechaIngreso { get => _FechaIngreso; set => _FechaIngreso = value; }
    public DateTime FechaNac { get => _FechaNac; set => _FechaNac = value; }
    public float PorcNomina { get => _PorcNomina; set => _PorcNomina = value; }
    public float PorcAsimilados { get => _PorcAsimilados; set => _PorcAsimilados = value; }
    public float PorcHonorarios { get => _PorcHonorarios; set => _PorcHonorarios = value; }
    public float PorcTN { get => _PorcTN; set => _PorcTN = value; }
    public float PorcEZWallet { get => _PorcEZWallet; set => _PorcEZWallet = value; }
    public float Sueldo { get => _Sueldo; set => _Sueldo = value; }
    public float SueldoBruto { get => _SueldoBruto; set => _SueldoBruto = value; }
    public float SueldoNeto { get => _SueldoNeto; set => _SueldoNeto = value; }
    public float SueldoHonorarios { get => _SueldoHonorarios; set => _SueldoHonorarios = value; }
    public float SueldoTN { get => _SueldoTN; set => _SueldoTN = value; }
    public float SueldoEZWallet { get => _SueldoEZWallet; set => _SueldoEZWallet = value; }
    public double Comisiones { get => _Comisiones; set => _Comisiones = value; }
    public double Bono { get => _Bono; set => _Bono = value; }
    public double OtrosIngresos { get => _OtrosIngresos; set => _OtrosIngresos = value; }
    public string ImpFonacot { get => _ImpFonacot; set => _ImpFonacot = value; }
    public int Id_Infonavit { get => _Id_Infonavit; set => _Id_Infonavit = value; }
    public float ImporteInfonavit { get => _ImporteInfonavit; set => _ImporteInfonavit = value; }
    public int Id_Prestac { get => _Id_Prestac; set => _Id_Prestac = value; }
    public int Id_Pension { get => _Id_Pension; set => _Id_Pension = value; }
    public float ImportePension { get => _ImportePension; set => _ImportePension = value; }
    public int Id_EsquemaActual { get => _Id_EsquemaActual; set => _Id_EsquemaActual = value; }
    public int Id_ClasifEmp { get => _Id_ClasifEmp; set => _Id_ClasifEmp = value; }
    public string Nacionalidad { get => _Nacionalidad; set => _Nacionalidad = value; }
    public double SueldoDiario { get => _SueldoDiario; set => _SueldoDiario = value; }
    public int Antiguedad { get => _Antiguedad; set => _Antiguedad = value; }
    public int TipoEsquema { get => _TipoEsquema; set => _TipoEsquema = value; }
    public string Clave { get => _Clave; set => _Clave = value; }
    public string ClaveAnterior { get => _ClaveAnterior; set => _ClaveAnterior = value; }
    public int RSocialPagadoraID { get => _RSocialPagadoraID; set => _RSocialPagadoraID = value; }
    public int SexoID { get => _SexoID; set => _SexoID = value; }
    public int TipoPagoID { get => _TipoPagoID; set => _TipoPagoID = value; }
    public int EstatusID { get => _EstatusID; set => _EstatusID = value; }
    public string Curp { get => _Curp; set => _Curp = value; }
    public string Rfc { get => _Rfc; set => _Rfc = value; }
    public string CorreoElectronico { get => _CorreoElectronico; set => _CorreoElectronico = value; }
    public string TelefonoLocal { get => _TelefonoLocal; set => _TelefonoLocal = value; }
    public string TelefonoMovil { get => _TelefonoMovil; set => _TelefonoMovil = value; }
    public double SueldoIntegracion { get => _SueldoIntegracion; set => _SueldoIntegracion = value; }
    public DateTime FechaUltimoPago { get => _FechaUltimoPago; set => _FechaUltimoPago = value; }
    public int PeriodoPagoID { get => _PeriodoPagoID; set => _PeriodoPagoID = value; }
    #endregion

    #region Constructores
    public EmpleadoVO()
    {
      Id_Empleado = 0;
      Id_Escenario = 0;
      Id_Cliente = 0;
      Id_PrimaRgo = 0;
      Nombre = "";
      Paterno = "";
      Materno = "";
      Puesto = "";
      DescriPto = "";
      UbicaLabora = "";
      FechaIngreso = DateTime.Parse("1900-01-01");
      FechaNac = DateTime.Parse("1900-01-01");
      PorcNomina = 0;
      PorcAsimilados = 0;
      PorcHonorarios = 0;
      PorcTN = 0;
      PorcEZWallet = 0;
      Sueldo = 0;
      SueldoBruto = 0;
      SueldoNeto = 0;
      SueldoHonorarios = 0;
      SueldoTN = 0;
      SueldoEZWallet = 0;
      Comisiones = 0;
      Bono = 0;
      OtrosIngresos = 0;
      ImpFonacot = "";
      Id_Infonavit = 0;
      ImporteInfonavit = 0;
      Id_Prestac = 0;
      Id_Pension = 0;
      ImportePension = 0;
      Id_EsquemaActual = 0;
      Id_ClasifEmp = 0;
      Nacionalidad = "";
      SueldoDiario = 0;
      Antiguedad = 0;
      TipoEsquema = 0;
      Clave = "";
      ClaveAnterior = "";
      RSocialPagadoraID = 0;
      SexoID = 0;
      TipoPagoID = 0;
      EstatusID = 0;
      Curp = "";
      Rfc = "";
      CorreoElectronico = "";
      TelefonoLocal = "";
      TelefonoMovil = "";
      SueldoIntegracion = 0;
      FechaUltimoPago = DateTime.Parse("1900-01-01");
      PeriodoPagoID = 0;
    }

    public EmpleadoVO(DataRow dr)
    {
      Id_Empleado = int.Parse(dr["Id_Empleado"].ToString());
      Id_Escenario = int.Parse(dr["Id_Escenario"].ToString());
      Id_Cliente = int.Parse(dr["Id_Cliente"].ToString());
      Id_PrimaRgo = int.Parse(dr["Id_PrimaRgo"].ToString());
      Nombre = dr["Nombre"].ToString();
      Paterno = dr["Paterno"].ToString();
      Materno = dr["Materno"].ToString();
      Puesto = dr["Puesto"].ToString();
      DescriPto = dr["DescriPto"].ToString();
      UbicaLabora = dr["UbicaLabora"].ToString();
      FechaIngreso = DateTime.Parse("1900-01-01");
      FechaNac = DateTime.Parse("1900-01-01");
      PorcNomina = float.Parse(dr["PorcNomina"].ToString());
      PorcAsimilados = float.Parse(dr["PorcAsimilados"].ToString());
      PorcHonorarios = float.Parse(dr["PorcHonorarios"].ToString());
      PorcTN = float.Parse(dr["PorcTN"].ToString());
      PorcEZWallet = float.Parse(dr["PorcEZWallet"].ToString());
      Sueldo = float.Parse(dr["Sueldo"].ToString());
      SueldoBruto = float.Parse(dr["SueldoBruto"].ToString());
      SueldoNeto = float.Parse(dr["SueldoNeto"].ToString());
      SueldoHonorarios = float.Parse(dr["SueldoHonorarios"].ToString());
      SueldoTN = float.Parse(dr["SueldoTN"].ToString());
      SueldoEZWallet = float.Parse(dr["SueldoEZWallet"].ToString());
      Comisiones = double.Parse(dr["Comisiones"].ToString());
      Bono = double.Parse(dr["Bono"].ToString());
      OtrosIngresos = double.Parse(dr["OtrosIngresos"].ToString());
      ImpFonacot = dr["ImpFonacot"].ToString();
      Id_Infonavit = int.Parse(dr["Id_Infonavit"].ToString());
      ImporteInfonavit = float.Parse(dr["ImporteInfonavit"].ToString());
      Id_Prestac = int.Parse(dr["Id_Prestac"].ToString());
      Id_Pension = int.Parse(dr["Id_Pension"].ToString());
      ImportePension = float.Parse(dr["ImportePension"].ToString());
      Id_EsquemaActual = int.Parse(dr["Id_EsquemaActual"].ToString());
      Id_ClasifEmp = int.Parse(dr["Id_ClasifEmp"].ToString());
      Nacionalidad = dr["Nacionalidad"].ToString();
      SueldoDiario = double.Parse(dr["SueldoDiario"].ToString());
      Antiguedad = int.Parse(dr["Antiguedad"].ToString());
      TipoEsquema = int.Parse(dr["TipoEsquema"].ToString());
      Clave = dr["Clave"].ToString();
      ClaveAnterior = dr["ClaveAnterior"].ToString();
      RSocialPagadoraID = int.Parse(dr["RSocialPagadoraID"].ToString());
      SexoID = int.Parse(dr["SexoID"].ToString());
      TipoPagoID = int.Parse(dr["TipoPagoID"].ToString());
      EstatusID = int.Parse(dr["EstatusID"].ToString());
      Curp = dr["Curp"].ToString();
      Rfc = dr["Rfc"].ToString();
      CorreoElectronico = dr["CorreoElectronico"].ToString();
      TelefonoLocal = dr["TelefonoLocal"].ToString();
      TelefonoMovil = dr["TelefonoMovil"].ToString();
      SueldoIntegracion = double.Parse(dr["SueldoIntegracion"].ToString());
      FechaUltimoPago = DateTime.Parse("1900-01-01");
      PeriodoPagoID = int.Parse(dr["PeriodoPagoID"].ToString());
    }

    #endregion
  }
}