using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Web.Configuration;
using EscenariosQnta.Data;
using EscenariosQnta.Clases;
using System.Drawing;
using System.Data.SqlClient;

namespace EscenariosQnta
{
    public partial class wfrmEmpleadoCargaMasiva : System.Web.UI.Page
    {

        #region Variables

        string Id = string.Empty;
        string Id_Escenario = string.Empty;
        string Cliente = string.Empty;
        string Nombre = string.Empty;
        string Paterno = string.Empty;
        string Materno = string.Empty;
        string Puesto = string.Empty;
        string DescriPto = string.Empty;
        string PrimaRgo = string.Empty;
        string FechaIngreso = string.Empty;
        string FechaNac = string.Empty;
        string TipoEsquema = string.Empty;
        string Nomina = string.Empty;
        string Asimilados = string.Empty;
        string Honorarios = string.Empty;
        //string OtrosProductos = string.Empty;
        string TN = string.Empty;
        string EZWallet = string.Empty;
        string Sueldo = string.Empty;
        string SueldoBruto = string.Empty;
        string SueldoNeto = string.Empty;
        //string SueldoIntegrado = string.Empty;
        string SueldoHonorarios = string.Empty;
        string SueldoTN = string.Empty;
        string SueldoEZWallet = string.Empty;
        string Prestac = string.Empty;
        string UbicaLabora = string.Empty;
        string TipoInfonavit = string.Empty;
        string ImpInfonavit = string.Empty;
        string Bono = string.Empty;
        string ComisionEmpleado = string.Empty;
        string OtrosIngresos = string.Empty;
        string ImpFonacot = string.Empty;
        string Pension = string.Empty;
        string ImportePension = string.Empty;
        string EsquemaActual = string.Empty;
        string ClasifEmp = string.Empty;
        string Nacionalidad = string.Empty;

        DateTime FechIng;
        DateTime? FechNac;
        DataTable objdtLista;

        string strQuery = string.Empty;
        int RetunValue;
        clsDatos clsQuery = new clsDatos();

        #endregion

        private string className = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            leerExcel();
        }

        public bool leerExcel()
        {
            bool operacion = false;
            try
            {
                //Revisamos que exista hallan seleccionado un archivo
                if (!fulEmpleado.HasFile)
                {
                    Mensaje("Seleccione un archivo Excel", CuadroMensaje.CuadroMensajeIcono.Error);
                }

                else
                {
                    //Comprobamos que sea un archivo Excel con las siguientes extensiones
                    string type = Path.GetExtension(fulEmpleado.PostedFile.FileName);
                    if (type == ".xlsx" || type == ".xls")
                    {
                        //lsMsg.Text = string.Empty;

                        DataTable dtEmpleado = new DataTable();

                        int count = 0;
                        //Separamos los nombres de las Hojas 
                        foreach (string tabla in TablasEnExcel(rutaArchivo()))
                        {
                            DataTable dtExcel = new DataTable();

                            //Pasamos las Hojas Excel a un DataTable
                            ExcelADataTable(rutaArchivo(), tabla, ref dtExcel);

                            switch (tabla)
                            {
                                case "DatosEmpleado$":
                                    dtEmpleado = MapearDataTable(tabla, dtExcel);
                                    count = count + 1;
                                    break;
                            }
                        }

                        //Revisamos que existan los nombres de las Hojas
                        if (count == 1)
                        {
                            //Comprobamos si la operación tuvo éxito
                            if (InsertarEmpleado(dtEmpleado) != 0)
                            {
                                Mensaje("GUARDADO", CuadroMensaje.CuadroMensajeIcono.Exitoso);
                            }
                        }
                        else
                        {
                            Mensaje("ERROR: Nombre de Hojas Diferentes a la de la Plantilla", CuadroMensaje.CuadroMensajeIcono.Error);
                        }
                    }
                    else
                    {
                        Mensaje("ERROR: Seleccione un archivo Excel", CuadroMensaje.CuadroMensajeIcono.Error);
                    }

                }
            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
            }

            return operacion;
        }

        public string rutaArchivo()
        {
            string strConn = string.Empty;

            try
            {
                //Buscamos el Nombre y la Ruta del archivo en el servidor 
                string nombreExcel = Path.GetFileNameWithoutExtension(fulEmpleado.PostedFile.FileName);
                string rutaArchivo = Server.MapPath(("\\") + "\\UploadFiles\\" + Path.GetFileName(nombreExcel) + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx");
                VaciarDirectorio(Server.MapPath(("\\") + "\\UploadFiles\\"));

                using (FileStream fs = new FileStream(rutaArchivo, FileMode.Create, FileAccess.Write))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        bw.Write(fulEmpleado.FileBytes);
                        bw.Close();
                        bw.Dispose();
                    }
                    fs.Close();
                    fs.Dispose();
                }

                //Cadena de Conexión
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\"" + rutaArchivo + "\";" +
                    "Extended Properties=\"Excel 12.0 Xml;HDR=No;IMEX=1;\"";

            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
            }
            return strConn;

        }

        private void VaciarDirectorio(string ruta)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(ruta);
                foreach (FileInfo fi in dir.GetFiles())
                {
                    fi.Delete();
                }
            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
            }
        }

        public List<string> TablasEnExcel(string rutaArchivo)
        {
            List<string> tables = new List<string>();

            //Devolvemos el nombre de las Hojas dentro del Excel
            try
            {
                using (OleDbConnection conn = new OleDbConnection(rutaArchivo))
                {
                    conn.Open();
                    DataTable dtSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });


                    foreach (DataRow drSchema in dtSchema.Rows)
                    {
                        string sheet = drSchema.Field<string>("TABLE_NAME");
                        sheet = sheet.Replace("$'", "");
                        if (sheet.IndexOf('$') > 0)
                            //Agregamos el nombre de la Hoja a una lista
                            tables.Add(sheet);
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
            }

            return tables;
        }

        public bool ExcelADataTable(string rutaArchivo, string tabla, ref DataTable dtResultado)
        {
            bool resultado = false;

            try
            {
                using (OleDbConnection conn = new OleDbConnection(rutaArchivo))
                {
                    conn.Open();

                    //Extraemos los datos de las Hojas
                    using (OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM [" + tabla + "] WHERE F1 <> ' '", conn))
                    {
                        //Condicion para verificar que existan las Hojas Cotizacion y Unidades
                        if (tabla == "DatosEmpleado$")
                        {
                            da.Fill(dtResultado);
                            conn.Close();
                            conn.Dispose();
                        }
                        //Pasamos los datos de la Hoja a un DataTable
                        resultado = ReasignarColumnas(ref dtResultado);

                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
            }
            return resultado;
        }

        private bool ReasignarColumnas(ref DataTable dt)
        {
            //Comprobamos que existan datos en el DataTable
            if (dt.Rows.Count == 0) return false;
            DataRow dr = dt.Rows[0];

            //Asignamos el nombre a las columnas
            foreach (string columnName in dt.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToList())
            {
                if (dr[columnName].ToString().Length == 0)
                {
                    if (dt.Columns.Contains(columnName))
                        dt.Columns.Remove(columnName);
                }
                else
                {
                    dt.Columns[columnName].ColumnName = dr[columnName].ToString();
                }
            }

            dt.Rows.Remove(dr);
            return true;
        }

        private DataTable MapearDataTable(string hoja, DataTable dtExcel)
        {

            switch (hoja)
            {
                case "DatosEmpleado$":
                    try
                    {
                        //Se transfiere nombre de columnas idénticas como workaround para identificar si alguna de ellas no existe 
                        //Comprobamos que las las columnas estén dentro de la Hoja Correspondiente (DatosEmpleado)
                        //22 Columnas

                        if (dtExcel.Columns.Count == 35)
                        {
                            dtExcel.Columns["CLIENTE"].ColumnName = "CLIENTE";
                            dtExcel.Columns["ESCENARIO"].ColumnName = "ESCENARIO";
                            dtExcel.Columns["PRIMA RIESGO"].ColumnName = "PRIMA";
                            dtExcel.Columns["NOMBRE EMPLEADO"].ColumnName = "NOMBRE";
                            dtExcel.Columns["APELLIDO PATERNO"].ColumnName = "PATERNO";
                            dtExcel.Columns["APELLIDO MATERNO"].ColumnName = "MATERNO";
                            dtExcel.Columns["PUESTO"].ColumnName = "PUESTO";
                            dtExcel.Columns["DESCRIPCION PUESTO"].ColumnName = "DESCRIPCION";
                            dtExcel.Columns["UBICACION LABORAL"].ColumnName = "UBICACION";
                            dtExcel.Columns["FECHA INGRESO"].ColumnName = "INGRESO";
                            dtExcel.Columns["FECHA NACIMIENTO"].ColumnName = "NACIMIENTO";
                            //dtExcel.Columns["TIPO NOMINA"].ColumnName = "TIPONOMINA";
                            dtExcel.Columns["TIPO ESQUEMA"].ColumnName = "TIPOESQUEMA";
                            dtExcel.Columns["NOMINA"].ColumnName = "NOMINA";
                            dtExcel.Columns["ASIMILADOS"].ColumnName = "ASIMILADOS";
                            dtExcel.Columns["HONORARIOS"].ColumnName = "HONORARIOS";
                            dtExcel.Columns["TN"].ColumnName = "TN";
                            dtExcel.Columns["EZ WALLET"].ColumnName = "EZWALLET";
                            dtExcel.Columns["SUELDO"].ColumnName = "SUELDO";                            
                            //dtExcel.Columns["OTROS PRODUCTOS"].ColumnName = "OTROSPRODUCTOS";
                            //dtExcel.Columns["SUELDO MENSUAL"].ColumnName = "SUELDOMENSUAL";
                            dtExcel.Columns["SUELDO BRUTO"].ColumnName = "BRUTO";
                            dtExcel.Columns["SUELDO NETO"].ColumnName = "NETO";
                            //dtExcel.Columns["SUELDO INTEGRADO"].ColumnName = "INTEGRADO";
                            dtExcel.Columns["SUELDO HONORARIOS"].ColumnName = "SUELDOHONORARIOS";
                            dtExcel.Columns["SUELDO TN"].ColumnName = "SUELDOTN";
                            dtExcel.Columns["SUELDO EZ WALLET"].ColumnName = "SUELDOEZWALLET";                           
                            dtExcel.Columns["BONO"].ColumnName = "BONO";
                            dtExcel.Columns["COMISIONES"].ColumnName = "COMISIONES";
                            dtExcel.Columns["OTROS INGRESOS"].ColumnName = "OTROSINGRESOS";
                            dtExcel.Columns["TIPO INFONAVIT"].ColumnName = "TIPOINFONAVIT";
                            dtExcel.Columns["IMPORTE INFONAVIT"].ColumnName = "IMPORTEINFONAVIT";
                            dtExcel.Columns["IMPORTE FONACOT"].ColumnName = "FONACOT";
                            dtExcel.Columns["PRESTACION"].ColumnName = "PRESTACION";
                            dtExcel.Columns["TIPO PENSION ALIMENTICIA"].ColumnName = "PENSION";
                            dtExcel.Columns["IMPORTE PENSION ALIMENTICIA"].ColumnName = "IMPORTEPENSION";
                            dtExcel.Columns["ESQUEMA ACTUAL"].ColumnName = "ESQUEMA";
                            dtExcel.Columns["CLASIFICACION EMPLEADO"].ColumnName = "CLASIFICACION";
                            dtExcel.Columns["NACIONALIDAD"].ColumnName = "NACIONALIDAD";
                        }
                        else
                        {
                            Mensaje("ERROR: El Numero de celdas es diferente a la de la Plantilla", CuadroMensaje.CuadroMensajeIcono.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
                    }
                    break;

            }

            //Extraemos solo las columnas que vamos a ocupar para insertar 
            DataTable newdt = new DataTable();
            newdt = dtExcel.Clone();
            for (int i = 0; i < dtExcel.Rows.Count; i++)
            {
                newdt.ImportRow(dtExcel.Rows[i]);
            }

            return newdt;
        }

        public int InsertarEmpleado(DataTable dtEmpleado)
        {
            int insertado = 0;
            objdtLista = new DataTable();
            objdtLista = dtEmpleado.Clone();
            DataTable dtEmpleados = objdtTabla;

            objdtTabla.Rows.Clear();

            try
            {
                //Comprobamos que no este vacío el DataTable de Cotizacion
                if (dtEmpleado.Rows.Count > 0)
                {
                    for (int i = 0; i < dtEmpleado.Rows.Count; i++)
                    {
                        Id_Escenario = dtEmpleado.Rows[i]["ESCENARIO"].ToString();
                        Cliente = dtEmpleado.Rows[i]["CLIENTE"].ToString();
                        PrimaRgo = dtEmpleado.Rows[i]["PRIMA"].ToString();
                        Nombre = dtEmpleado.Rows[i]["NOMBRE"].ToString();
                        Paterno = dtEmpleado.Rows[i]["PATERNO"].ToString();
                        Materno = dtEmpleado.Rows[i]["MATERNO"].ToString();
                        Puesto = dtEmpleado.Rows[i]["PUESTO"].ToString();
                        DescriPto = dtEmpleado.Rows[i]["DESCRIPCION"].ToString();
                        UbicaLabora = dtEmpleado.Rows[i]["UBICACION"].ToString();
                        FechaIngreso = dtEmpleado.Rows[i]["INGRESO"].ToString();
                        FechaNac = dtEmpleado.Rows[i]["NACIMIENTO"].ToString();
                        //if (dtEmpleado.Rows[i]["TIPONOMINA"].ToString() == "BRUTOS")
                        //{
                        //    SueldoBruto = dtEmpleado.Rows[i]["SUELDOMENSUAL"].ToString();
                        //    SueldoNeto = "0";
                        //    Nomina = "100";
                        //    Asimilados = "0";
                        //}
                        //else if (dtEmpleado.Rows[i]["TIPONOMINA"].ToString() == "NETOS")
                        //{
                        //    SueldoBruto = "0";
                        //    SueldoNeto = dtEmpleado.Rows[i]["SUELDOMENSUAL"].ToString();
                        //    Nomina = "0";
                        //    Asimilados = "100";
                        //}
                        Nomina = string.IsNullOrEmpty(dtEmpleado.Rows[i]["NOMINA"].ToString()) ? "0" : dtEmpleado.Rows[i]["NOMINA"].ToString();
                        Asimilados = string.IsNullOrEmpty(dtEmpleado.Rows[i]["ASIMILADOS"].ToString()) ? "0" : dtEmpleado.Rows[i]["ASIMILADOS"].ToString();
                        Honorarios = string.IsNullOrEmpty(dtEmpleado.Rows[i]["HONORARIOS"].ToString()) ? "0" : dtEmpleado.Rows[i]["HONORARIOS"].ToString();
                        TN = string.IsNullOrEmpty(dtEmpleado.Rows[i]["TN"].ToString()) ? "0" : dtEmpleado.Rows[i]["TN"].ToString();
                        EZWallet = string.IsNullOrEmpty(dtEmpleado.Rows[i]["EZWALLET"].ToString()) ? "0" : dtEmpleado.Rows[i]["EZWALLET"].ToString();
                        Sueldo = string.IsNullOrEmpty(dtEmpleado.Rows[i]["SUELDO"].ToString()) ? "0" : dtEmpleado.Rows[i]["SUELDO"].ToString();
                        //OtrosProductos = dtEmpleado.Rows[i]["OTROSPRODUCTOS"].ToString();
                        SueldoBruto = string.IsNullOrEmpty(dtEmpleado.Rows[i]["BRUTO"].ToString()) ? "0" : dtEmpleado.Rows[i]["BRUTO"].ToString();
                        SueldoNeto = string.IsNullOrEmpty(dtEmpleado.Rows[i]["NETO"].ToString()) ? "0" : dtEmpleado.Rows[i]["NETO"].ToString();
                        SueldoHonorarios = string.IsNullOrEmpty(dtEmpleado.Rows[i]["SUELDOHONORARIOS"].ToString()) ? "0" : dtEmpleado.Rows[i]["SUELDOHONORARIOS"].ToString();
                        SueldoTN = string.IsNullOrEmpty(dtEmpleado.Rows[i]["SUELDOTN"].ToString()) ? "0" : dtEmpleado.Rows[i]["SUELDOTN"].ToString();
                        SueldoEZWallet = string.IsNullOrEmpty(dtEmpleado.Rows[i]["SUELDOEZWALLET"].ToString()) ? "0" : dtEmpleado.Rows[i]["SUELDOEZWALLET"].ToString();
                        //SueldoIntegrado = dtEmpleado.Rows[i]["INTEGRADO"].ToString();                                                
                        TipoInfonavit = dtEmpleado.Rows[i]["TIPOINFONAVIT"].ToString();
                        ImpInfonavit = dtEmpleado.Rows[i]["IMPORTEINFONAVIT"].ToString();
                        Bono = string.IsNullOrEmpty(dtEmpleado.Rows[i]["BONO"].ToString()) ? "0" : dtEmpleado.Rows[i]["BONO"].ToString();
                        ComisionEmpleado = string.IsNullOrEmpty(dtEmpleado.Rows[i]["COMISIONES"].ToString())? "0" : dtEmpleado.Rows[i]["COMISIONES"].ToString();
                        OtrosIngresos = string.IsNullOrEmpty(dtEmpleado.Rows[i]["OTROSINGRESOS"].ToString())? "0" : dtEmpleado.Rows[i]["OTROSINGRESOS"].ToString();
                        ImpFonacot = dtEmpleado.Rows[i]["FONACOT"].ToString();
                        Prestac = dtEmpleado.Rows[i]["PRESTACION"].ToString();
                        Pension = dtEmpleado.Rows[i]["PENSION"].ToString();
                        ImportePension = dtEmpleado.Rows[i]["IMPORTEPENSION"].ToString();
                        EsquemaActual = dtEmpleado.Rows[i]["ESQUEMA"].ToString();
                        ClasifEmp = dtEmpleado.Rows[i]["CLASIFICACION"].ToString();
                        Nacionalidad = dtEmpleado.Rows[i]["NACIONALIDAD"].ToString();

                        FechIng = DateTime.Parse(FechaIngreso);
                        FechNac = string.IsNullOrEmpty(FechaNac) ? (DateTime?)null : Convert.ToDateTime(FechaNac);

                        if (FechNac == null)
                        {
                            strQuery = string.Format("dbo.SP_InsertaEmpleadosMasivo '{0}', {1}, '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}', '{21}', '{22}', '{23}', '{24}', '{25}','{26}', '{27}', '{28}', '{29}', '{30}', '{31}', '{32}', '{33}', '{34}'",
                                 Cliente, Id_Escenario, PrimaRgo, Nombre, Paterno, Materno, Puesto, DescriPto, UbicaLabora, FechIng.ToString("yyyyMMdd"), FechNac, TipoEsquema, Nomina, Asimilados, Honorarios, TN, EZWallet, 
                                 Sueldo, SueldoBruto, SueldoNeto, SueldoHonorarios, SueldoTN, SueldoEZWallet,
                                Bono, ComisionEmpleado, OtrosIngresos, TipoInfonavit, ImpInfonavit, ImpFonacot, Prestac, Pension, ImportePension, EsquemaActual, ClasifEmp, Nacionalidad);
                        }
                        else
                        {
                            //strQuery = string.Format("dbo.SP_InsertaEmpleadosMasivo '{0}', {1}, '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}', '{21}', '{22}', '{23}', '{24}', '{25}', '{26}', '{27}', '{28}', '{29}', '{30}', '{31}', '{32}', '{33}', '{34}'",
                            //     Cliente, Id_Escenario, Nombre, Paterno, Materno, Puesto, DescriPto, PrimaRgo, FechIng.ToString("yyyyMMdd"), FechNac.Value.ToString("yyyyMMdd"), Nomina, Asimilados, Honorarios, OtrosProductos, SueldoBruto, SueldoNeto,
                            //    SueldoIntegrado, UbicaLabora, TipoInfonavit, ImpInfonavit, Bono, ComisionEmpleado, OtrosIngresos, ImpFonacot, Prestac, Pension, ImportePension, EsquemaActual, ClasifEmp, Nacionalidad);
                            strQuery = string.Format("dbo.SP_InsertaEmpleadosMasivo '{0}', {1}, '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}', '{21}', '{22}', '{23}', '{24}', '{25}', '{26}', '{27}', '{28}', '{29}', '{30}', '{31}', '{32}', '{33}', '{34}'",
                               Cliente, Id_Escenario, PrimaRgo, Nombre, Paterno, Materno, Puesto, DescriPto, UbicaLabora, FechIng.ToString("yyyyMMdd"), FechNac.Value.ToString("yyyyMMdd"), TipoEsquema, Nomina, Asimilados, Honorarios, TN, EZWallet,
                               Sueldo, SueldoBruto, SueldoNeto, SueldoHonorarios, SueldoTN, SueldoEZWallet,
                              Bono, ComisionEmpleado, OtrosIngresos, TipoInfonavit, ImpInfonavit, ImpFonacot, Prestac, Pension, ImportePension, EsquemaActual, ClasifEmp, Nacionalidad);
                        }

                        RetunValue = clsQuery.execQueryInt(strQuery);

                        if (RetunValue == 1)
                        {
                            insertado = insertado + 1;
                        }
                        else
                        {
                            DataRow drEmpleado = dtEmpleados.NewRow();

                            drEmpleado["ESCENARIO"] = Id_Escenario;
                            drEmpleado["CLIENTE"] = Cliente;
                            drEmpleado["PRIMA"] = PrimaRgo;
                            drEmpleado["NOMBRE"] = Nombre;
                            drEmpleado["PATERNO"] = Paterno;
                            drEmpleado["MATERNO"] = Materno;
                            drEmpleado["PUESTO"] = Puesto;
                            drEmpleado["DESCRIPCION"] = DescriPto;
                            drEmpleado["UBICACION"] = UbicaLabora;
                            drEmpleado["INGRESO"] = FechIng;
                            drEmpleado["NACIMIENTO"] = FechNac;

                            //if(SueldoBruto != "0")
                            //{
                            //    drEmpleado["TIPONOMINA"] = "BRUTOS";
                            //}
                            //else if (SueldoNeto != "0")
                            //{
                            //    drEmpleado["TIPONOMINA"] = "NETOS";
                            //}
                            drEmpleado["NOMINA"] = Nomina;
                            drEmpleado["ASIMILADOS"] = Asimilados;
                            drEmpleado["HONORARIOS"] = Honorarios;
                            //drEmpleado["OTROSPRODUCTOS"] = OtrosProductos;
                            drEmpleado["BRUTO"] = SueldoBruto;
                            drEmpleado["NETO"] = SueldoNeto;
                            //drEmpleado["INTEGRADO"] = SueldoIntegrado;
                            
                            drEmpleado["TIPOINFONAVIT"] = TipoInfonavit;
                            drEmpleado["IMPORTEINFONAVIT"] = ImpInfonavit;
                            drEmpleado["BONO"] = Bono;
                            drEmpleado["COMISIONES"] = ComisionEmpleado;
                            drEmpleado["OTROSINGRESOS"] = OtrosIngresos;
                            drEmpleado["FONACOT"] = ImpFonacot;
                            drEmpleado["PRESTACION"] = Prestac;
                            drEmpleado["PENSION"] = Pension;
                            drEmpleado["IMPORTEPENSION"] = ImportePension;
                            drEmpleado["ESQUEMA"] = EsquemaActual;
                            drEmpleado["CLASIFICACION"] = ClasifEmp;
                            drEmpleado["NACIONALIDAD"] = Nacionalidad;

                            dtEmpleados.Rows.Add(drEmpleado);

                        }

                        objdtTabla = dtEmpleados;
                    }


                    gvEmpleados.DataSource = objdtTabla;
                    gvEmpleados.DataBind();

                    if (objdtTabla.Rows.Count > 0)
                    {
                        btnExportar.Visible = true;
                    }
                    else
                    {
                        btnExportar.Visible = false;
                    }
                }


            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
                lbmsg.Text = ex.ToString();            
            }
            return insertado;
        }

        protected DataTable objdtTabla
        {
            get
            {

                if (ViewState["objdtTabla"] != null)
                {
                    return (DataTable)ViewState["objdtTabla"];
                }

                else
                {
                    return objdtLista;
                }

            }

            set
            {
                ViewState["objdtTabla"] = value;
            }

        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            ExportarArchivoExcel();
        }

        private void ExportarArchivoExcel()
        {
            try
            {
                //Response.ClearContent();
                //Response.Buffer = true;
                //Response.ClearContent();
                //Response.ClearHeaders();
                //Response.Charset = "";
                //string FileName = "Empleados_NO_Insertados" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                //string attachment = "attachment; filename = " + FileName;
                //Response.ClearContent();
                //Response.AddHeader("content-disposition", attachment);
                //Response.ContentType = "application/ms-excel";  //Excel 2003  
                //StringWriter strWrite = new StringWriter();
                //HtmlTextWriter htmWrite = new HtmlTextWriter(strWrite);
                ////HtmlForm htmfrm = new HtmlForm();
                ////htmfrm.Attributes["runat"] = "server";
                ////gvEmpleados.Parent.Controls.Add(htmWrite);
                //gvEmpleados.RenderControl(htmWrite);            
                ////htmfrm.Controls.Add(gvEmpleados);
                ////htmfrm.RenderControl(htmWrite);
                //Response.Write(strWrite.ToString());
                //Response.Flush();
                //Response.End();

                Response.Clear();
                Response.Buffer = true;
                //Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
                string FileName = "Empleados_NO_Insertados" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                string attachment = "attachment; filename = " + FileName;
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //To Export all pages
                    //GridView1.AllowPaging = false;
                    //this.BindGrid();

                    gvEmpleados.HeaderRow.BackColor = Color.White;
                    foreach (TableCell cell in gvEmpleados.HeaderRow.Cells)
                    {
                        cell.BackColor = gvEmpleados.HeaderStyle.BackColor;
                    }
                    foreach (GridViewRow row in gvEmpleados.Rows)
                    {
                        row.BackColor = Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = gvEmpleados.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = gvEmpleados.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    gvEmpleados.RenderControl(hw);

                    //style to format numbers to string
                    // string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                    Response.Write(sw.ToString());
                    // Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }

            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
            }

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        public void Mensaje(string textomensaje, CuadroMensaje.CuadroMensajeIcono icono)
        {
            CuadroMensaje Mensaje = new CuadroMensaje(textomensaje, "MENSAJE", icono, CuadroMensaje.CuadroMensajeBoton.Ok, CuadroMensaje.CuadroMensajeEstilo.EstiloB);
            Literal ltrMensaje = (Literal)this.Master.FindControl("ltrMensaje");
            ltrMensaje.Text = Mensaje.Mostrar(this);
        }

        protected void btnDescargar_Click(object sender, EventArgs e)
        {
            string rutaPlantilla = string.Empty, NombrePlantilla = string.Empty;

            rutaPlantilla = Server.MapPath("\\Layouts\\Layout_Alta_Empleado.xlsx");
            NombrePlantilla = "Layout_Alta_Empleado.xlsx"; //BL_NVPv3.ObtenerParametro("Plantilla_S&L", null).ToString().Split('\\').Last();

            try
            {
                DirectoryInfo df = new DirectoryInfo(Server.MapPath((".")));

                if (string.IsNullOrEmpty(rutaPlantilla))
                {
                    rutaPlantilla = Server.MapPath((".") + "\\Layouts\\Layout_Alta_Empleado.xlsx");

                    String patch = rutaPlantilla;
                    System.IO.FileInfo toDownload = new System.IO.FileInfo((patch));

                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + NombrePlantilla);
                    HttpContext.Current.Response.AddHeader("Content-Length", rutaPlantilla.Length.ToString());
                    HttpContext.Current.Response.ContentType = "application/octet-stream";
                    HttpContext.Current.Response.WriteFile(patch);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();

                }
                else
                {
                    String patch = rutaPlantilla;
                    System.IO.FileInfo toDownload = new System.IO.FileInfo((patch));

                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + NombrePlantilla);
                    HttpContext.Current.Response.AddHeader("Content-Length", toDownload.Length.ToString());
                    HttpContext.Current.Response.ContentType = "application/octet-stream";
                    HttpContext.Current.Response.WriteFile(patch);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();

                }

            }
            catch (Exception ex)
            {
                Mensaje("ERROR: " + ex.ToString(), CuadroMensaje.CuadroMensajeIcono.Error);
            }
        }

        protected void gvEmpleados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }
        }
    }
}