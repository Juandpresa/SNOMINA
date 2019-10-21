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
//using NVP.Entities;
//using NVP.Common;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Globalization;
//using VersionComponent.Logging;
using System.Web.Configuration;
using EscenariosQnta.Data;
using System.Configuration;

namespace EscenariosQnta
{
    public partial class wfrmEmpleadoCargaMasiva : System.Web.UI.Page
    {

        #region Variables

        string Id = string.Empty;
        string Id_Escenario = string.Empty;
        string Id_Cliente = string.Empty;
        string Nombre = string.Empty;
        string Paterno = string.Empty;
        string Materno = string.Empty;
        string Puesto = string.Empty;
        string DescriPto = string.Empty;
        string Id_PrimaRgo = string.Empty;
        string FechaIngreso = string.Empty;
        string FechaNac = string.Empty;
        string SueldoBruto = string.Empty;
        string SueldoNeto = string.Empty;
        string SueldoIntegrado = string.Empty;
        string Id_Prestac = string.Empty;
        string UbicaLabora = string.Empty;
        string Id_Infonavit = string.Empty;
        string ImpFonacot = string.Empty;
        string Id_Pension = string.Empty;
        string ImportePension = string.Empty;
        string Id_EsquemaActual = string.Empty;
        string Id_ClasifEmp = string.Empty;
        string Nacionalidad = string.Empty;

        string strQuery = string.Empty;
        string RetunValue;
        clsDatos clsQuery = new clsDatos();

        #endregion
        private string className = string.Empty;
        //BL_Quote BL_QuoteObj = new BL_Quote();
        DataTable objdtLista;

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
                    //lsMsg.ForeColor = System.Drawing.Color.Red;
                    //lsMsg.Text = "Seleccione un Archivo Excel";
                    //btnExportar.Visible = false;
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
                            if (operacion = InsertarEmpleado(dtEmpleado))
                            {
                                //Utilities.msjGritter("Cotizaciones Insertadas Correctamente", Utilities.TipoError.Exito, "mensajeGritter");
                            }
                        }
                        else
                        {
                            //Utilities.msjGritter("Nombre de Hojas Diferentes a la de la Plantilla", Utilities.TipoError.Advertencia, "mensajeGritter");
                        }
                    }
                    else
                    {
                        //lsMsg.ForeColor = System.Drawing.Color.Red;
                        //lsMsg.Text = "Seleccione un Archivo Excel";
                        //btnExportar.Visible = false;
                    }

                }
            }
            catch (Exception ex)
            {
                //log.WriteErrorLog(className, ex.Message + ", Stack: " + ex.StackTrace, Utilities.UserSSO);
                //Utilities.msjGritter("Error : Consulta a tu administrador.", Utilities.TipoError.Error, "mensajeGritter");
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
                string rutaArchivo = Server.MapPath((".") + "\\UploadFiles\\" + Path.GetFileName(nombreExcel) + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx");
                VaciarDirectorio(Server.MapPath((".") + "\\UploadFiles\\"));

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

                //log.WriteErrorLog(className, ex.Message + ", Stack: " + ex.StackTrace, Utilities.UserSSO);
                //Utilities.msjGritter("Error en la Conexion: Consulta a tu administrador.", Utilities.TipoError.Error, "mensajeGritter");
                //return strConn;
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
                //log.WriteErrorLog(className, ex.Message + ", Stack: " + ex.StackTrace, Utilities.UserSSO);
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
                //log.WriteErrorLog(className, ex.Message + ", Stack: " + ex.StackTrace, Utilities.UserSSO);
                //Utilities.msjGritter("Error : Consulta a tu administrador.", Utilities.TipoError.Error, "mensajeGritter");
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
                //log.WriteErrorLog(className, ex.Message + ", Stack: " + ex.StackTrace, Utilities.UserSSO);
                //Utilities.msjGritter("Error Al exportar los datos a un DataTable : Consulta a tu administrador.", Utilities.TipoError.Error, "mensajeGritter");
                //return false;
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

                        if (dtExcel.Columns.Count == 22)
                        {
                            dtExcel.Columns["ESCENARIO"].ColumnName = "ESCENARIO";
                            dtExcel.Columns["CLIENTE"].ColumnName = "CLIENTE";
                            dtExcel.Columns["NOMBRE"].ColumnName = "NOMBRE";
                            dtExcel.Columns["PATERNO"].ColumnName = "PATERNO";
                            dtExcel.Columns["MATERNO"].ColumnName = "MATERNO";
                            dtExcel.Columns["PUESTO"].ColumnName = "PUESTO";
                            dtExcel.Columns["DESCRIPCION"].ColumnName = "DESCRIPCION";
                            dtExcel.Columns["PRIMA"].ColumnName = "PRIMA";
                            dtExcel.Columns["INGRESO"].ColumnName = "INGRESO ";
                            dtExcel.Columns["NACIMIENTO"].ColumnName = "NACIMIENTO";
                            dtExcel.Columns["BRUTO"].ColumnName = "BRUTO";
                            dtExcel.Columns["NETO"].ColumnName = "NETO";
                            dtExcel.Columns["INTEGRADO"].ColumnName = "INTEGRADO";
                            dtExcel.Columns["UBICACION"].ColumnName = "UBICACION";
                            dtExcel.Columns["INFONAVIT"].ColumnName = "INFONAVIT";
                            dtExcel.Columns["FONACOT"].ColumnName = "FONACOT";
                            dtExcel.Columns["PRESTACION"].ColumnName = "PRESTACION";
                            dtExcel.Columns["PENSION"].ColumnName = "PENSION";
                            dtExcel.Columns["IMPORTE"].ColumnName = "IMPORTE";
                            dtExcel.Columns["ESQUEMA"].ColumnName = "ESQUEMA";
                            dtExcel.Columns["CLASIFICACION"].ColumnName = "CLASIFICACION";
                            dtExcel.Columns["NACIONALIDAD"].ColumnName = "NACIONALIDAD";


                        }
                        else
                        {
                            //Utilities.msjGritter("El Nombre de la Hoja Cotizacion es diferente a la de la Plantilla", Utilities.TipoError.Advertencia, "mensajeGritter");
                        }
                    }
                    catch (Exception ex)
                    {
                        //log.WriteErrorLog(className, ex.Message + ", Stack: " + ex.StackTrace, Utilities.UserSSO);
                        //Utilities.msjGritter("Error al Mapear las columnas Cotizacion.", Utilities.TipoError.Error, "mensajeGritter");
                    }
                    break;

            }

            //Extraemos solo las columnas que vamos a ocupar para insertar 
            DataTable newdt = new DataTable();
            newdt = dtExcel.Clone();
            for (int i = 2; i < dtExcel.Rows.Count; i++)
            {
                newdt.ImportRow(dtExcel.Rows[i]);
            }

            return newdt;
        }

        public bool InsertarEmpleado(DataTable dtEmpleado)
        {
            bool insertado = false;

            try
            {

                //Comprobamos que no este vacío el DataTable de Cotizacion
                if (dtEmpleado.Rows.Count > 0)
                {
                    for (int i = 0; i < dtEmpleado.Rows.Count; i++)
                    {
                        Id_Escenario = dtEmpleado.Rows[i]["ESCENARIO"].ToString();
                        Id_Cliente = dtEmpleado.Rows[i]["CLIENTE"].ToString();
                        Nombre = dtEmpleado.Rows[i]["NOMBRE"].ToString();
                        Paterno = dtEmpleado.Rows[i]["PATERNO"].ToString();
                        Materno = dtEmpleado.Rows[i]["MATERNO"].ToString();
                        Puesto = dtEmpleado.Rows[i]["PUESTO"].ToString();
                        DescriPto = dtEmpleado.Rows[i]["DESCRIPCION_PUESTO"].ToString();
                        Id_PrimaRgo = dtEmpleado.Rows[i]["PRIMA_RIESGO"].ToString();
                        FechaIngreso = dtEmpleado.Rows[i]["INGRESO"].ToString();
                        FechaNac = dtEmpleado.Rows[i]["NACIMIENTO"].ToString();
                        SueldoBruto = dtEmpleado.Rows[i]["SUELDO_BRUTO"].ToString();
                        SueldoNeto = dtEmpleado.Rows[i]["SUELDO_NETO"].ToString();
                        SueldoIntegrado = dtEmpleado.Rows[i]["SUELDO_INTEGRADO"].ToString();
                        Id_Prestac = dtEmpleado.Rows[i]["PRESTACION"].ToString();
                        UbicaLabora = dtEmpleado.Rows[i]["UBICACIÓN_LABORAL"].ToString();
                        Id_Infonavit = dtEmpleado.Rows[i]["INFONAVIT"].ToString();
                        ImpFonacot = dtEmpleado.Rows[i]["IMPORTE_FONACOT"].ToString();
                        Id_Pension = dtEmpleado.Rows[i]["PENSION_ALIMENTICIA"].ToString();
                        ImportePension = dtEmpleado.Rows[i]["IMPORTE_PENSION"].ToString();
                        Id_EsquemaActual = dtEmpleado.Rows[i]["ESQUEMA_ACTUAL"].ToString();
                        Id_ClasifEmp = dtEmpleado.Rows[i]["CLASIFICACION_EMPLEADO"].ToString();
                        Nacionalidad = dtEmpleado.Rows[i]["NACIONALIDAD"].ToString();

                        strQuery = string.Format("dbo.SP_InsertaEmpleado {0}, {1}, '{2}', '{3}', '{4}', '{5}', '{6}', {7}, '{8}', '{9}', '{10}', '{11}', '{12}', {13},'{14}',{15},'{16}',{17},'{18}',{19},{20},'{21}'",
                            Id_Escenario, Id_Cliente, Nombre, Paterno, Materno, Puesto, DescriPto, Id_PrimaRgo, FechaIngreso, FechaNac, SueldoBruto, SueldoNeto,
            SueldoIntegrado, Id_Prestac, UbicaLabora, Id_Infonavit, ImpFonacot, Id_Pension, ImportePension, Id_EsquemaActual, Id_ClasifEmp, Nacionalidad);

                        //RetunValue = clsQuery.execQueryString(strQuery);

                        //if (RetunValue == "1")
                        //{

                        //    LimpiarControles();
                        //    Response.Write("<script>alert('Guardado');</script>");
                        //    ObtenEmpleado();
                        //}
                    }
                }


                insertado = true;

            }
            catch (Exception ex)
            {
                //log.WriteErrorLog(className, ex.Message + ", Stack: " + ex.StackTrace, Utilities.UserSSO);
                //Utilities.msjGritter("Error al Insertar las Cotizaciones y/o Unidades : Consulte a su Administrador .", Utilities.TipoError.Error, "mensajeGritter");

            }
            return insertado;
        }

        //public string ValidarCotizacion(DataTable dtEmpleado)
        //{
        //    string error = string.Empty;

        //    for (int i = 0; i < dtEmpleado.Rows.Count; i++)
        //    {
        //        if (dtEmpleado.Rows[i]["Fleet"].ToString().Trim().Length > 25 || string.IsNullOrEmpty(dtEmpleado.Rows[i]["Fleet"].ToString()))
        //        { error = "Revise Columna Fleet, Fila " + (i + 5) + " de la Hoja de Cotizaciones, porfavor."; }

        //        if (dtEmpleado.Rows[i]["Contract"].ToString().Trim().Length > 4 || string.IsNullOrEmpty(dtEmpleado.Rows[i]["Contract"].ToString()))
        //        { error = "Revise Columna Contract, Fila " + (i + 5) + " de la Hoja de Cotizaciones, porfavor."; }

        //        if (dtEmpleado.Rows[i]["TipoOperacion"].ToString().Trim().Length > 25 || string.IsNullOrEmpty(dtEmpleado.Rows[i]["TipoOperacion"].ToString()))
        //        { error = "Revise Columna TipoOperacion, Fila " + (i + 5) + " de la Hoja de Cotizaciones, porfavor."; }

        //        if (dtEmpleado.Rows[i]["TipoUnidad"].ToString().Trim().Length > 25 || string.IsNullOrEmpty(dtEmpleado.Rows[i]["TipoUnidad"].ToString()))
        //        { error = "Revise Columna TipoUnidad, Fila " + (i + 5) + " de la Hoja de Cotizaciones, porfavor."; }

        //        if (dtEmpleado.Rows[i]["TipoTasa"].ToString().Trim().Length > 150 || string.IsNullOrEmpty(dtEmpleado.Rows[i]["TipoTasa"].ToString()))
        //        { error = "Revise Columna TipoTasa, Fila " + (i + 5) + " de la Hoja de Cotizaciones, porfavor."; }

        //        if (dtEmpleado.Rows[i]["TasaBase"].ToString().Trim().Length > 8 || string.IsNullOrEmpty(dtEmpleado.Rows[i]["TasaBase"].ToString()))
        //        { error = "Revise Columna TasaBase, Fila " + (i + 5) + " de la Hoja de Cotizaciones, porfavor."; }

        //        if (dtEmpleado.Rows[i]["TipoCambio"].ToString().Trim().Length > 8 || string.IsNullOrEmpty(dtEmpleado.Rows[i]["TipoCambio"].ToString()))
        //        { error = "Revise Columna TipoCambio, Fila " + (i + 5) + " de la Hoja de Cotizaciones, porfavor."; }

        //        if (dtEmpleado.Rows[i]["Iva"].ToString().Trim().Length > 2 || string.IsNullOrEmpty(dtEmpleado.Rows[i]["Iva"].ToString()))
        //        { error = "Revise Columna Iva, Fila " + (i + 5) + " de la Hoja de Cotizaciones, porfavor."; }

        //        if (dtEmpleado.Rows[i]["AmortTerm"].ToString().Trim().Length > 8 || string.IsNullOrEmpty(dtEmpleado.Rows[i]["AmortTerm"].ToString()))
        //        { error = "Revise Columna AmortTerm, Fila " + (i + 5) + " de la Hoja de Cotizaciones, porfavor."; }

        //        if (dtEmpleado.Rows[i]["Residual"].ToString().Trim().Length > 8 || string.IsNullOrEmpty(dtEmpleado.Rows[i]["Residual"].ToString()))
        //        { error = "Revise Columna Residual, Fila " + (i + 5) + " de la Hoja de Cotizaciones, porfavor."; }

        //        if (dtEmpleado.Rows[i]["Marca"].ToString().Trim().Length > 25 || string.IsNullOrEmpty(dtEmpleado.Rows[i]["Marca"].ToString()))
        //        { error = "Revise Columna Marca, Fila " + (i + 5) + " de la Hoja de Cotizaciones, porfavor."; }

        //        if (dtEmpleado.Rows[i]["Modelo"].ToString().Trim().Length > 30 || string.IsNullOrEmpty(dtEmpleado.Rows[i]["Modelo"].ToString()))
        //        { error = "Revise Columna Modelo, Fila " + (i + 5) + " de la Hoja de Cotizaciones, porfavor."; }

        //        if (dtEmpleado.Rows[i]["Version"].ToString().Trim().Length > 80 || string.IsNullOrEmpty(dtEmpleado.Rows[i]["Version"].ToString()))
        //        { error = "Revise Columna Version, Fila " + (i + 5) + " de la Hoja de Cotizaciones, porfavor."; }

        //        if (dtEmpleado.Rows[i]["Year"].ToString().Trim().Length > 4 || string.IsNullOrEmpty(dtEmpleado.Rows[i]["Year"].ToString()))
        //        { error = "Revise Columna Year, Fila " + (i + 5) + " de la Hoja de Cotizaciones, porfavor."; }

        //        if (dtEmpleado.Rows[i]["Costo_Seguro"].ToString().Trim().Length > 8 || string.IsNullOrEmpty(dtEmpleado.Rows[i]["Costo_Seguro"].ToString()))
        //        { error = "Revise Columna Costo_Seguro, Fila " + (i + 5) + " de la Hoja de Cotizaciones, porfavor."; }

        //        if (dtEmpleado.Rows[i]["Moneda_Costo_Seguro"].ToString().Trim().Length > 4 || string.IsNullOrEmpty(dtEmpleado.Rows[i]["Moneda_Costo_Seguro"].ToString()))
        //        { error = "Revise Columna Moneda_Costo_Seguro, Fila " + (i + 5) + " de la Hoja de Cotizaciones, porfavor."; }

        //        if (dtEmpleado.Rows[i]["Precio_Definitivo"].ToString().Trim().Length > 8 || string.IsNullOrEmpty(dtEmpleado.Rows[i]["Precio_Definitivo"].ToString()))
        //        { error = "Revise Columna Precio_Definitivo, Fila " + (i + 5) + " de la Hoja de Cotizaciones, porfavor."; }

        //        if (dtEmpleado.Rows[i]["Moneda_Precio_Definitivo"].ToString().Trim().Length > 4 || string.IsNullOrEmpty(dtEmpleado.Rows[i]["Moneda_Precio_Definitivo"].ToString()))
        //        { error = "Revise Columna Moneda_Precio_Definitivo, Fila " + (i + 5) + " de la Hoja de Cotizaciones, porfavor."; }

        //        if (dtEmpleado.Rows[i]["Limite_Empleado"].ToString().Trim().Length > 8 || string.IsNullOrEmpty(dtEmpleado.Rows[i]["Limite_Empleado"].ToString()))
        //        { error = "Revise Columna Limite_Empleado, Fila " + (i + 5) + " de la Hoja de Cotizaciones, porfavor."; }

        //        if (dtEmpleado.Rows[i]["Valor_EBO"].ToString().Trim().Length > 8 || string.IsNullOrEmpty(dtEmpleado.Rows[i]["Valor_EBO"].ToString()))
        //        { error = "Revise Columna Valor_EBO, Fila " + (i + 5) + " de la Hoja de Cotizaciones, porfavor."; }

        //        if (dtEmpleado.Rows[i]["Plazo_EBO"].ToString().Trim().Length > 4 || string.IsNullOrEmpty(dtEmpleado.Rows[i]["Plazo_EBO"].ToString()))
        //        { error = "Revise Columna Plazo_EBO, Fila " + (i + 5) + " de la Hoja de Cotizaciones, porfavor."; }

        //        if (dtEmpleado.Rows[i]["SobrePrecio"].ToString().Trim().Length > 8 || string.IsNullOrEmpty(dtEmpleado.Rows[i]["SobrePrecio"].ToString()))
        //        { error = "Revise Columna SobrePrecio, Fila " + (i + 5) + " de la Hoja de Cotizaciones, porfavor."; }

        //        if (dtEmpleado.Rows[i]["Costo_Originacion"].ToString().Trim().Length > 8 || string.IsNullOrEmpty(dtEmpleado.Rows[i]["Costo_Originacion"].ToString()))
        //        { error = "Revise Columna Costo_Originacion, Fila " + (i + 5) + " de la Hoja de Cotizaciones, porfavor."; }

        //        if (dtEmpleado.Rows[i]["Renta_Anticipada"].ToString().Trim().Length > 8 || string.IsNullOrEmpty(dtEmpleado.Rows[i]["Renta_Anticipada"].ToString()))
        //        { error = "Revise Columna Renta_Anticipada, Fila " + (i + 5) + " de la Hoja de Cotizaciones, porfavor."; }
        //        string count = Convert.ToString(dtEmpleado.Rows[i]["CapCost"].ToString().ToString().Trim().Length);
        //        if (dtEmpleado.Rows[i]["CapCost"].ToString().ToString().Trim().Length > 10 || string.IsNullOrEmpty(dtEmpleado.Rows[i]["CapCost"].ToString()))
        //        { error = "Revise Columna CapCost, Fila " + (i + 5) + " de la Hoja de Cotizaciones, porfavor."; }

        //        if (dtEmpleado.Rows[i]["Plazo"].ToString().Trim().Length > 4 || string.IsNullOrEmpty(dtEmpleado.Rows[i]["Plazo"].ToString()))
        //        { error = "Revise Columna Plazo, Fila " + (i + 5) + " de la Hoja de Cotizaciones, porfavor."; }

        //        if (dtEmpleado.Rows[i]["KM_Permitidos"].ToString().Trim().Length > 4 || string.IsNullOrEmpty(dtEmpleado.Rows[i]["KM_Permitidos"].ToString()))
        //        { error = "Revise Columna KM_Permitidos, Fila " + (i + 5) + " de la Hoja de Cotizaciones, porfavor."; }

        //        if (dtEmpleado.Rows[i]["Cargo_KM_Adicional"].ToString().Trim().Length > 8 || string.IsNullOrEmpty(dtEmpleado.Rows[i]["Cargo_KM_Adicional"].ToString()))
        //        { error = "Revise Columna Cargo_KM_Adicional, Fila " + (i + 5) + " de la Hoja de Cotizaciones, porfavor."; }

        //        if (dtEmpleado.Rows[i]["Moneda_KM_Adicional"].ToString().Trim().Length > 50 || string.IsNullOrEmpty(dtEmpleado.Rows[i]["Moneda_KM_Adicional"].ToString()))
        //        { error = "Revise Columna Moneda_KM_Adicional, Fila " + (i + 5) + " de la Hoja de Cotizaciones, porfavor."; }

        //        if (dtEmpleado.Rows[i]["ModeloCotizacion"].ToString().Trim().Length > 4 || string.IsNullOrEmpty(dtEmpleado.Rows[i]["ModeloCotizacion"].ToString()))
        //        { error = "Revise Columna ModeloCotizacion, Fila " + (i + 5) + " de la Hoja de Cotizaciones, porfavor."; }

        //        if (dtEmpleado.Rows[i]["MARK_UP_FEE_"].ToString().Trim().Length > 8 || string.IsNullOrEmpty(dtEmpleado.Rows[i]["MARK_UP_FEE_"].ToString()))
        //        { error = "Revise Columna MARK_UP_FEE_, Fila " + (i + 5) + " de la Hoja de Cotizaciones, porfavor."; }

        //        if (dtEmpleado.Rows[i]["MARK_UP_FEE_AFFECT"].ToString().Trim().Length > 2 || string.IsNullOrEmpty(dtEmpleado.Rows[i]["MARK_UP_FEE_AFFECT"].ToString()))
        //        { error = "Revise Columna MARK_UP_FEE_AFFECT, Fila " + (i + 5) + " de la Hoja de Cotizaciones, porfavor."; }

        //        if (long.Parse(dtEmpleado.Rows[i]["JatoId"].ToString()).ToString().Trim().Length > 9 || string.IsNullOrEmpty(dtEmpleado.Rows[i]["JatoId"].ToString()))
        //        { error = "Revise Columna JatoId, Fila " + (i + 5) + " de la Hoja de Cotizaciones, porfavor."; }

        //        if (dtEmpleado.Rows[i]["Fintest13"].ToString().Trim().Length > 4 || string.IsNullOrEmpty(dtEmpleado.Rows[i]["Fintest13"].ToString()))
        //        { error = "Revise Columna Fintest13, Fila " + (i + 5) + " de la Hoja de Cotizaciones, porfavor."; }

        //        if (dtEmpleado.Rows[i]["UnidadesCotizadas"].ToString().Trim().Length > 4 || string.IsNullOrEmpty(dtEmpleado.Rows[i]["UnidadesCotizadas"].ToString()))
        //        { error = "Revise Columna UnidadesCotizadas, Fila " + (i + 5) + " de la Hoja de Cotizaciones, porfavor."; }

        //        if (dtEmpleado.Rows[i]["Id_Insurance_company"].ToString().Trim().Length > 4 || string.IsNullOrEmpty(dtEmpleado.Rows[i]["Id_Insurance_company"].ToString()))
        //        { error = "Revise Columna Id_Insurance_company, Fila " + (i + 5) + " de la Hoja de Cotizaciones, porfavor."; }

        //    }
        //    return error;
        //}

        //public string ValidarUnidad(DataTable dtUnidad)
        //{
        //    string error = string.Empty;

        //    for (int i = 0; i < dtUnidad.Rows.Count; i++)
        //    {
        //        if (dtUnidad.Rows[i]["ID_NVP_QUOTE_UNITS"].ToString().Trim().Length > 5 || string.IsNullOrEmpty(dtUnidad.Rows[i]["ID_NVP_QUOTE_UNITS"].ToString()))
        //        { error = "Revise Columna ID_NVP_QUOTE_UNITS, Fila " + (i + 5) + " de la Hoja de Unidades, porfavor"; }

        //        if (dtUnidad.Rows[i]["ID_QUOTE"].ToString().Trim().Length > 5 || string.IsNullOrEmpty(dtUnidad.Rows[i]["ID_QUOTE"].ToString()))
        //        { error = "Revise Columna ID_QUOTE, Fila " + (i + 5) + " de la Hoja de Unidades, porfavor"; }

        //    }
        //    return error;

        //}

        //public EntidadCotizacionExcel CotizacionExcel(DataTable dtEmpleado, int i)
        //{
        //    //Asignamos los Datos a las Entidades de Cotizacion
        //    NVP.Entities.EntidadCotizacionExcel Cotizacion = new NVP.Entities.EntidadCotizacionExcel();

        //    Cotizacion.fleet = dtEmpleado.Rows[i]["Fleet"].ToString();
        //    Cotizacion.contract = dtEmpleado.Rows[i]["Contract"].ToString();
        //    Cotizacion.tipoOperacion = dtEmpleado.Rows[i]["TipoOperacion"].ToString();
        //    Cotizacion.tipoUnidad = dtEmpleado.Rows[i]["TipoUnidad"].ToString();
        //    Cotizacion.tipoTasa = dtEmpleado.Rows[i]["TipoTasa"].ToString();
        //    Cotizacion.tasaBase = Convert.ToDecimal(dtEmpleado.Rows[i]["TasaBase"].ToString());
        //    Cotizacion.tipoCambio = Convert.ToDecimal(dtEmpleado.Rows[i]["TipoCambio"].ToString());
        //    Cotizacion.iva = int.Parse(dtEmpleado.Rows[i]["Iva"].ToString());
        //    Cotizacion.amortTerm = Convert.ToDecimal(dtEmpleado.Rows[i]["AmortTerm"].ToString());
        //    Cotizacion.residual = Convert.ToDecimal(dtEmpleado.Rows[i]["Residual"].ToString());
        //    Cotizacion.marca = dtEmpleado.Rows[i]["Marca"].ToString();
        //    Cotizacion.modelo = dtEmpleado.Rows[i]["Modelo"].ToString();
        //    Cotizacion.version = dtEmpleado.Rows[i]["Version"].ToString();
        //    Cotizacion.year = int.Parse(dtEmpleado.Rows[i]["Year"].ToString());
        //    Cotizacion.costo_Seguro = Convert.ToDecimal(dtEmpleado.Rows[i]["Costo_Seguro"].ToString());
        //    Cotizacion.moneda_Costo_Seguro = int.Parse(dtEmpleado.Rows[i]["Moneda_Costo_Seguro"].ToString());
        //    Cotizacion.precio_Definitivo = Convert.ToDecimal(dtEmpleado.Rows[i]["Precio_Definitivo"].ToString());
        //    Cotizacion.moneda_Precio_Definitivo = int.Parse(dtEmpleado.Rows[i]["Moneda_Precio_Definitivo"].ToString());
        //    Cotizacion.limite_Empleado = Convert.ToDecimal(dtEmpleado.Rows[i]["Limite_Empleado"].ToString());
        //    Cotizacion.id_User = Session["IdUser"].ToString();
        //    Cotizacion.valor_EBO = Convert.ToDecimal(dtEmpleado.Rows[i]["Valor_EBO"].ToString());
        //    Cotizacion.plazo_EBO = int.Parse(dtEmpleado.Rows[i]["Plazo_EBO"].ToString());
        //    Cotizacion.activa = 2; //int.Parse(dtEmpleado.Rows[i]["Activa"].ToString());
        //    Cotizacion.sobrePrecio = Convert.ToDecimal(dtEmpleado.Rows[i]["SobrePrecio"].ToString());
        //    Cotizacion.costo_Originacion = Convert.ToDecimal(dtEmpleado.Rows[i]["Costo_Originacion"].ToString());
        //    Cotizacion.renta_Anticipada = Convert.ToDecimal(dtEmpleado.Rows[i]["Renta_Anticipada"].ToString());
        //    Cotizacion.capCost = Convert.ToDecimal(dtEmpleado.Rows[i]["CapCost"].ToString());
        //    Cotizacion.plazo = int.Parse(dtEmpleado.Rows[i]["Plazo"].ToString());
        //    Cotizacion.KM_Permitidos = Convert.ToDecimal(dtEmpleado.Rows[i]["KM_Permitidos"].ToString());
        //    Cotizacion.cargo_KM_Adicional = Convert.ToDecimal(dtEmpleado.Rows[i]["Cargo_KM_Adicional"].ToString());
        //    Cotizacion.moneda_KM_Adicional = dtEmpleado.Rows[i]["Moneda_KM_Adicional"].ToString();
        //    Cotizacion.Ext_Opc1 = dtEmpleado.Rows[i]["Ext_Opc1"].ToString();
        //    Cotizacion.Ext_Opc2 = dtEmpleado.Rows[i]["Ext_Opc2"].ToString();
        //    Cotizacion.Int_Opc1 = dtEmpleado.Rows[i]["Int_Opc1"].ToString();
        //    Cotizacion.Int_Opc2 = dtEmpleado.Rows[i]["Int_Opc2"].ToString();
        //    string FechaOrdenCompra = dtEmpleado.Rows[i]["Fecha_OrdenCompra"].ToString();
        //    Cotizacion.Fecha_OrdenCompra = string.IsNullOrEmpty(dtEmpleado.Rows[i]["Fecha_OrdenCompra"].ToString()) ? (DateTime?)null : DateTime.Parse(FechaOrdenCompra);
        //    string idUserOrdenCompra = dtEmpleado.Rows[i]["idUserOrdenCompra"].ToString();
        //    Cotizacion.idUserOrdenCompra = string.IsNullOrEmpty(dtEmpleado.Rows[i]["idUserOrdenCompra"].ToString()) ? (int?)null : int.Parse(idUserOrdenCompra);
        //    string Proveedor = dtEmpleado.Rows[i]["Proveedor"].ToString();
        //    Cotizacion.Proveedor = string.IsNullOrEmpty(dtEmpleado.Rows[i]["Proveedor"].ToString()) ? (int?)null : int.Parse(Proveedor);
        //    Cotizacion.ContactoProveedor = string.IsNullOrEmpty(dtEmpleado.Rows[i]["ContactoProveedor"].ToString()) ? null : (dtEmpleado.Rows[i]["Proveedor"].ToString());
        //    string Id_Riesgo = dtEmpleado.Rows[i]["Id_Riesgo"].ToString();
        //    Cotizacion.Id_Riesgo = string.IsNullOrEmpty(dtEmpleado.Rows[i]["Id_Riesgo"].ToString()) ? (int?)null : int.Parse(Id_Riesgo);
        //    string Fecha_AproboRiesgo = dtEmpleado.Rows[i]["Fecha_AproboRiesgo"].ToString();
        //    Cotizacion.Fecha_AproboRiesgo = string.IsNullOrEmpty(dtEmpleado.Rows[i]["Fecha_AproboRiesgo"].ToString()) ? (DateTime?)null : DateTime.Parse(Fecha_AproboRiesgo);
        //    string Id_AnnexoA = dtEmpleado.Rows[i]["Id_AnnexoA"].ToString();
        //    Cotizacion.Id_AnnexoA = string.IsNullOrEmpty(dtEmpleado.Rows[i]["Id_AnnexoA"].ToString()) ? (int?)null : int.Parse(Id_AnnexoA);
        //    string Fecha_AnnexoA = dtEmpleado.Rows[i]["Fecha_AnnexoA"].ToString();
        //    Cotizacion.Fecha_AnnexoA = string.IsNullOrEmpty(dtEmpleado.Rows[i]["Fecha_AnnexoA"].ToString()) ? (DateTime?)null : DateTime.Parse(Fecha_AnnexoA);
        //    Cotizacion.modeloCotizacion = int.Parse(dtEmpleado.Rows[i]["ModeloCotizacion"].ToString());
        //    Cotizacion.MARK_UP_FEE_ = Convert.ToDecimal(dtEmpleado.Rows[i]["MARK_UP_FEE_"].ToString());
        //    Cotizacion.MARK_UP_FEE_AFFECT = int.Parse(dtEmpleado.Rows[i]["MARK_UP_FEE_AFFECT"].ToString());
        //    string Procurement_Fee_Type = dtEmpleado.Rows[i]["Procurement_Fee_Type"].ToString();
        //    Cotizacion.Procurement_Fee_Type = string.IsNullOrEmpty(dtEmpleado.Rows[i]["Procurement_Fee_Type"].ToString()) ? (int?)null : int.Parse(Procurement_Fee_Type);
        //    Cotizacion.jatoId = long.Parse(dtEmpleado.Rows[i]["JatoId"].ToString());
        //    Cotizacion.fintest13 = int.Parse(dtEmpleado.Rows[i]["Fintest13"].ToString());
        //    Cotizacion.UnidadesCotizadas = int.Parse(dtEmpleado.Rows[i]["UnidadesCotizadas"].ToString());
        //    Cotizacion.Id_Insurance_Company = int.Parse(dtEmpleado.Rows[i]["Id_Insurance_company"].ToString());

        //    return Cotizacion;
        //}

        //public EntidadUnidadExcel UnidadExcel(DataTable dtUnidad, int m, int idCotizacion)
        //{
        //    NVP.Entities.EntidadUnidadExcel Unidad = new NVP.Entities.EntidadUnidadExcel();

        //    Unidad.ID_QUOTE = idCotizacion;
        //    Unidad.UNIT = dtUnidad.Rows[m]["UNIT"].ToString();
        //    Unidad.SERIAL_NUMBER = dtUnidad.Rows[m]["SERIAL_NUMBER"].ToString();
        //    Unidad.DATE_ENTREGA = string.IsNullOrEmpty(dtUnidad.Rows[m]["DATE_ENTREGA"].ToString()) ? (DateTime?)null : Convert.ToDateTime(dtUnidad.Rows[m]["DATE_ENTREGA"].ToString());
        //    Unidad.DELIVERY_ADDRESS = dtUnidad.Rows[m]["DELIVERY_ADDRESS"].ToString();
        //    Unidad.USER_NAME = dtUnidad.Rows[m]["USER_NAME"].ToString();
        //    Unidad.RECEIVING_PERSON = dtUnidad.Rows[m]["RECEIVING_PERSON"].ToString();
        //    Unidad.EXT_COLOR1 = dtUnidad.Rows[m]["EXT_COLOR1"].ToString();
        //    Unidad.INT_COLOR1 = dtUnidad.Rows[m]["INT_COLOR1"].ToString();
        //    Unidad.LOG_NUMBER = dtUnidad.Rows[m]["LOG_NUMBER"].ToString();
        //    Unidad.ID_DEALER = string.IsNullOrEmpty(dtUnidad.Rows[m]["DATE_ENTREGA"].ToString()) ? (int?)null : int.Parse(dtUnidad.Rows[m]["ID_DEALER"].ToString());
        //    Unidad.DEALER_CONTACT = dtUnidad.Rows[m]["DEALER_CONTACT"].ToString();
        //    Unidad.PLATES_REQUIRED = string.IsNullOrEmpty(dtUnidad.Rows[m]["DATE_ENTREGA"].ToString()) ? (int?)null : int.Parse(dtUnidad.Rows[m]["PLATES_REQUIRED"].ToString());
        //    Unidad.ID_PLATES_STATE = string.IsNullOrEmpty(dtUnidad.Rows[m]["DATE_ENTREGA"].ToString()) ? (int?)null : int.Parse(dtUnidad.Rows[m]["ID_PLATES_STATE"].ToString());
        //    Unidad.VERIFICATION_REQUIRED = string.IsNullOrEmpty(dtUnidad.Rows[m]["DATE_ENTREGA"].ToString()) ? (int?)null : int.Parse(dtUnidad.Rows[m]["VERIFICATION_REQUIRED"].ToString());
        //    Unidad.EXT_COLOR2 = dtUnidad.Rows[m]["EXT_COLOR2"].ToString();
        //    Unidad.INT_COLOR2 = dtUnidad.Rows[m]["INT_COLOR2"].ToString();
        //    Unidad.ID_TRANSPORT_TYPE = string.IsNullOrEmpty(dtUnidad.Rows[m]["DATE_ENTREGA"].ToString()) ? (int?)null : int.Parse(dtUnidad.Rows[m]["ID_TRANSPORT_TYPE"].ToString());
        //    Unidad.EMAIL_USER_NAME = dtUnidad.Rows[m]["EMAIL_USER_NAME"].ToString();
        //    Unidad.USER_PHONE = dtUnidad.Rows[m]["USER_PHONE"].ToString();
        //    Unidad.COST_CENTER = dtUnidad.Rows[m]["COST_CENTER"].ToString();
        //    Unidad.DEALER_PHONE = dtUnidad.Rows[m]["DEALER_PHONE"].ToString();
        //    Unidad.Purchase_Order_Date = string.IsNullOrEmpty(dtUnidad.Rows[m]["DATE_ENTREGA"].ToString()) ? (DateTime?)null : Convert.ToDateTime(dtUnidad.Rows[m]["Purchase_Order_Date"].ToString());
        //    Unidad.Id_User_Purchase_Order = string.IsNullOrEmpty(dtUnidad.Rows[m]["DATE_ENTREGA"].ToString()) ? (int?)null : int.Parse(dtUnidad.Rows[m]["Id_User_Purchase_Order"].ToString());
        //    Unidad.Id_AnnexoB = string.IsNullOrEmpty(dtUnidad.Rows[m]["DATE_ENTREGA"].ToString()) ? (int?)null : int.Parse(dtUnidad.Rows[m]["Id_AnnexoB"].ToString());
        //    Unidad.Fecha_AnnexoB = string.IsNullOrEmpty(dtUnidad.Rows[m]["DATE_ENTREGA"].ToString()) ? (DateTime?)null : Convert.ToDateTime(dtUnidad.Rows[m]["Fecha_AnnexoB"].ToString());
        //    Unidad.ColorDefinitivo = dtUnidad.Rows[m]["ColorDefinitivo"].ToString();
        //    Unidad.Status = 10;
        //    Unidad.PolizaSeguro = dtUnidad.Rows[m]["PolizaSeguro"].ToString();
        //    Unidad.ComentariosPolizaSeguro = dtUnidad.Rows[m]["ComentariosPolizaSeguro"].ToString();
        //    Unidad.FechaGeneracionPolizaSeguro = string.IsNullOrEmpty(dtUnidad.Rows[m]["FechaGeneracionPolizaSeguro"].ToString()) ? (DateTime?)null : DateTime.Parse(dtUnidad.Rows[m]["FechaGeneracionPolizaSeguro"].ToString());
        //    Unidad.Id_UserPolizaSeguro = string.IsNullOrEmpty(dtUnidad.Rows[m]["Id_UserPolizaSeguro"].ToString()) ? (int?)null : int.Parse(dtUnidad.Rows[m]["Id_UserPolizaSeguro"].ToString());
        //    Unidad.CalificacionAudit = dtUnidad.Rows[m]["CalificacionAudit"].ToString();
        //    Unidad.FechaDocAudit = string.IsNullOrEmpty(dtUnidad.Rows[m]["DATE_ENTREGA"].ToString()) ? (DateTime?)null : Convert.ToDateTime(dtUnidad.Rows[m]["FechaDocAudit"].ToString());
        //    Unidad.FechaRealEntregaAudit = string.IsNullOrEmpty(dtUnidad.Rows[m]["DATE_ENTREGA"].ToString()) ? (DateTime?)null : Convert.ToDateTime(dtUnidad.Rows[m]["FechaRealEntregaAudit"].ToString());
        //    Unidad.ComentariosAudit = dtUnidad.Rows[m]["ComentariosAudit"].ToString();
        //    Unidad.Id_UserAudit = string.IsNullOrEmpty(dtUnidad.Rows[m]["DATE_ENTREGA"].ToString()) ? (int?)null : int.Parse(dtUnidad.Rows[m]["Id_UserAudit"].ToString());
        //    Unidad.FechaGeneracionAudit = string.IsNullOrEmpty(dtUnidad.Rows[m]["DATE_ENTREGA"].ToString()) ? (DateTime?)null : Convert.ToDateTime(dtUnidad.Rows[m]["FechaGeneracionAudit"].ToString());
        //    Unidad.Id_Booking = string.IsNullOrEmpty(dtUnidad.Rows[m]["DATE_ENTREGA"].ToString()) ? (int?)null : int.Parse(dtUnidad.Rows[m]["Id_Booking"].ToString());
        //    Unidad.Fecha_Booking = string.IsNullOrEmpty(dtUnidad.Rows[m]["DATE_ENTREGA"].ToString()) ? (DateTime?)null : Convert.ToDateTime(dtUnidad.Rows[m]["Fecha_Booking"].ToString());
        //    Unidad.NoFichaPago = dtUnidad.Rows[m]["NoFichaPago"].ToString();
        //    Unidad.FechaPago = string.IsNullOrEmpty(dtUnidad.Rows[m]["DATE_ENTREGA"].ToString()) ? (DateTime?)null : Convert.ToDateTime(dtUnidad.Rows[m]["FechaPago"].ToString());
        //    Unidad.Id_Payment = string.IsNullOrEmpty(dtUnidad.Rows[m]["Id_Payment"].ToString()) ? (int?)null : int.Parse(dtUnidad.Rows[m]["Id_Payment"].ToString());
        //    Unidad.Fecha_Payment = string.IsNullOrEmpty(dtUnidad.Rows[m]["Fecha_Payment"].ToString()) ? (DateTime?)null : DateTime.Parse(dtUnidad.Rows[m]["Fecha_Payment"].ToString());
        //    Unidad.Fecha_Promesa_Entrega = string.IsNullOrEmpty(dtUnidad.Rows[m]["Fecha_Promesa_Entrega"].ToString()) ? (DateTime?)null : DateTime.Parse(dtUnidad.Rows[m]["Fecha_Promesa_Entrega"].ToString());
        //    Unidad.Monto_Poliza = string.IsNullOrEmpty(dtUnidad.Rows[m]["Monto_Poliza"].ToString()) ? (Decimal?)null : Decimal.Parse(dtUnidad.Rows[m]["Monto_Poliza"].ToString());
        //    Unidad.Fecha_Entrega_AnexoB = string.IsNullOrEmpty(dtUnidad.Rows[m]["Fecha_Entrega_AnexoB"].ToString()) ? (DateTime?)null : DateTime.Parse(dtUnidad.Rows[m]["Fecha_Entrega_AnexoB"].ToString());
        //    Unidad.Fecha_Tentativa_Entrega = string.IsNullOrEmpty(dtUnidad.Rows[m]["DATE_ENTREGA"].ToString()) ? (DateTime?)null : Convert.ToDateTime(dtUnidad.Rows[m]["Fecha_Tentativa_Entrega"].ToString());
        //    Unidad.CausaEntrega = dtUnidad.Rows[m]["CausaEntrega"].ToString();
        //    Unidad.CausaDemora = dtUnidad.Rows[m]["CausaDemora"].ToString();
        //    Unidad.Placas = dtUnidad.Rows[m]["Placas"].ToString();
        //    Unidad.Id_User_OrdenCompra_2 = string.IsNullOrEmpty(dtUnidad.Rows[m]["DATE_ENTREGA"].ToString()) ? (int?)null : int.Parse(dtUnidad.Rows[m]["Id_User_OrdenCompra_2"].ToString());
        //    Unidad.FechaGeneracionOrdenCompra2 = string.IsNullOrEmpty(dtUnidad.Rows[m]["DATE_ENTREGA"].ToString()) ? (DateTime?)null : Convert.ToDateTime(dtUnidad.Rows[m]["FechaGeneracionOrdenCompra2"].ToString());
        //    Unidad.Id_Payment_2 = string.IsNullOrEmpty(dtUnidad.Rows[m]["DATE_ENTREGA"].ToString()) ? (int?)null : int.Parse(dtUnidad.Rows[m]["Id_Payment_2"].ToString());
        //    Unidad.Fecha_Payment_2 = string.IsNullOrEmpty(dtUnidad.Rows[m]["DATE_ENTREGA"].ToString()) ? (DateTime?)null : Convert.ToDateTime(dtUnidad.Rows[m]["Fecha_Payment_2"].ToString());
        //    Unidad.Id_Expediente = string.IsNullOrEmpty(dtUnidad.Rows[m]["DATE_ENTREGA"].ToString()) ? (int?)null : int.Parse(dtUnidad.Rows[m]["Id_Expediente"].ToString());
        //    Unidad.Fecha_Expediente = string.IsNullOrEmpty(dtUnidad.Rows[m]["DATE_ENTREGA"].ToString()) ? (DateTime?)null : Convert.ToDateTime(dtUnidad.Rows[m]["Fecha_Expediente"].ToString());
        //    Unidad.Fecha_Entrega_Expediente = string.IsNullOrEmpty(dtUnidad.Rows[m]["DATE_ENTREGA"].ToString()) ? (DateTime?)null : Convert.ToDateTime(dtUnidad.Rows[m]["Fecha_Entrega_Expediente"].ToString());
        //    Unidad.Id_Doc_Mgmnt = string.IsNullOrEmpty(dtUnidad.Rows[m]["Id_Doc_Mgmnt"].ToString()) ? (int?)null : int.Parse(dtUnidad.Rows[m]["Id_Doc_Mgmnt"].ToString());
        //    Unidad.Fecha_Validacion_Doc_Mgmnt = string.IsNullOrEmpty(dtUnidad.Rows[m]["Fecha_Validacion_Doc_Mgmnt"].ToString()) ? (DateTime?)null : DateTime.Parse(dtUnidad.Rows[m]["Fecha_Validacion_Doc_Mgmnt"].ToString());
        //    Unidad.Ubicacion_Unidad = dtUnidad.Rows[m]["Ubicacion_Unidad"].ToString();
        //    Unidad.EstadoPlacas = dtUnidad.Rows[m]["EstadoPlacas"].ToString();
        //    Unidad.Apellido_User = dtUnidad.Rows[m]["Apellido_User"].ToString();
        //    Unidad.No_Motor = dtUnidad.Rows[m]["No_Motor"].ToString();
        //    Unidad.Client_Unit = dtUnidad.Rows[m]["Client_Unit"].ToString();
        //    Unidad.Cel_User = string.IsNullOrEmpty(dtUnidad.Rows[m]["DATE_ENTREGA"].ToString()) ? (decimal?)null : Convert.ToDecimal(dtUnidad.Rows[m]["Cel_User"].ToString());
        //    Unidad.Ext_TelUser = string.IsNullOrEmpty(dtUnidad.Rows[m]["DATE_ENTREGA"].ToString()) ? (decimal?)null : Convert.ToDecimal(dtUnidad.Rows[m]["Ext_TelUser"].ToString());
        //    Unidad.fecha_real_booking = string.IsNullOrEmpty(dtUnidad.Rows[m]["DATE_ENTREGA"].ToString()) ? (DateTime?)null : Convert.ToDateTime(dtUnidad.Rows[m]["fecha_real_booking"].ToString());
        //    Unidad.close_ticket_date = string.IsNullOrEmpty(dtUnidad.Rows[m]["close_ticket_date"].ToString()) ? (DateTime?)null : DateTime.Parse(dtUnidad.Rows[m]["close_ticket_date"].ToString());
        //    Unidad.ClaveVehicular = dtUnidad.Rows[m]["ClaveVehicular"].ToString();
        //    Unidad.Policy_Number = dtUnidad.Rows[m]["Policy_Number"].ToString();
        //    Unidad.Expiration_Date = string.IsNullOrEmpty(dtUnidad.Rows[m]["Expiration_Date"].ToString()) ? (DateTime?)null : DateTime.Parse(dtUnidad.Rows[m]["Expiration_Date"].ToString());
        //    Unidad.Id_Broker = string.IsNullOrEmpty(dtUnidad.Rows[m]["Id_Broker"].ToString()) ? (int?)null : int.Parse(dtUnidad.Rows[m]["Id_Broker"].ToString());

        //    return Unidad;
        //}

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

        //protected void btnExportar_Click(object sender, EventArgs e)
        //{
        //    ExportarArchivoExcel();
        //}

        //private void ExportarArchivoExcel()
        //{
        //    try
        //    {
        //        Response.ClearContent();
        //        Response.Buffer = true;
        //        Response.ClearContent();
        //        Response.ClearHeaders();
        //        Response.Charset = "";
        //        string FileName = "Reporte_cotizacion" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        //        string attachment = "attachment; filename = " + FileName;
        //        Response.ClearContent();
        //        Response.AddHeader("content-disposition", attachment);
        //        Response.ContentType = "application/ms-excel";  //Excel 2003  
        //        StringWriter strWrite = new StringWriter();
        //        HtmlTextWriter htmWrite = new HtmlTextWriter(strWrite);
        //        HtmlForm htmfrm = new HtmlForm();
        //        htmfrm.Attributes["runat"] = "server";
        //        gdvSaleLease.Parent.Controls.Add(htmfrm);
        //        //gdvSalesLease.RenderControl(htmWrite);            
        //        htmfrm.Controls.Add(gdvSaleLease);
        //        htmfrm.RenderControl(htmWrite);
        //        Response.Write(strWrite.ToString());
        //        Response.Flush();
        //        Response.End();
        //    }
        //    catch (Exception ex)
        //    {
        //        log.WriteErrorLog(className, ex.Message + ", Stack: " + ex.StackTrace, Utilities.UserSSO);
        //        Utilities.msjGritter("Error al Descargar el Excel: Consulta a tu administrador.", Utilities.TipoError.Error, "mensajeGritter");
        //    }

        //}

        //protected void linkDescargarExcel_Click(object sender, EventArgs e)
        //{
        //    string rutaPlantilla = string.Empty, NombrePlantilla = string.Empty;

        //    rutaPlantilla = Server.MapPath(BL_NVPv3.ObtenerParametro("Plantilla_S&L", null));
        //    NombrePlantilla = BL_NVPv3.ObtenerParametro("Plantilla_S&L", null).ToString().Split('\\').Last();

        //    try
        //    {
        //        DirectoryInfo df = new DirectoryInfo(Server.MapPath((".")));

        //        if (string.IsNullOrEmpty(rutaPlantilla))
        //        {
        //            rutaPlantilla = Server.MapPath((".") + "\\PLantillas\\Plantilla Sale and Lease.xlsx");

        //            String patch = rutaPlantilla;
        //            System.IO.FileInfo toDownload = new System.IO.FileInfo((patch));

        //            HttpContext.Current.Response.Clear();
        //            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + NombrePlantilla);
        //            HttpContext.Current.Response.AddHeader("Content-Length", rutaPlantilla.Length.ToString());
        //            HttpContext.Current.Response.ContentType = "application/octet-stream";
        //            HttpContext.Current.Response.WriteFile(patch);
        //            HttpContext.Current.ApplicationInstance.CompleteRequest();

        //        }
        //        else
        //        {
        //            String patch = rutaPlantilla;
        //            System.IO.FileInfo toDownload = new System.IO.FileInfo((patch));

        //            HttpContext.Current.Response.Clear();
        //            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + NombrePlantilla);
        //            HttpContext.Current.Response.AddHeader("Content-Length", toDownload.Length.ToString());
        //            HttpContext.Current.Response.ContentType = "application/octet-stream";
        //            HttpContext.Current.Response.WriteFile(patch);
        //            HttpContext.Current.ApplicationInstance.CompleteRequest();

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        //log.WriteErrorLog(className, ex.Message + ", Stack: " + ex.StackTrace, Utilities.UserSSO);
        //        //Utilities.msjGritter("Error al descargar Plantilla: Consulta a tu administrador.", Utilities.TipoError.Error, "mensajeGritter");
        //    }
        //}


    }
}