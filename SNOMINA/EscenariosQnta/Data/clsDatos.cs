using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace EscenariosQnta.Data
{
    public class clsDatos
    {        

        public DataTable execQueryDataTable(string QueryString)
        {

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            try
            {
                SqlConnection sqlConnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnxSQL"].ConnectionString);
                SqlCommand cmd = new SqlCommand(QueryString, sqlConnn);
                //cmd.CommandTimeout = ConfigurationManager.AppSettings["CommandTimeOut"];
                cmd.CommandTimeout = 180;
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                sqlConnn.Open();
                da.Fill(ds, "TB");

                if (ds.Tables.Count > 0)
                    dt = ds.Tables[0];

                sqlConnn.Close();
                sqlConnn.Dispose();

            }
            catch (Exception ExString)
            {
                ExString.ToString();               
            }

            return dt;
        }

        public string execQueryString(string QueryString)
        {
            string strValue = string.Empty;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            try
            {
                SqlConnection sqlConnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnxSQL"].ConnectionString);
                SqlCommand cmd = new SqlCommand(QueryString, sqlConnn);

                cmd.CommandTimeout = 180;
    
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                cmd.Connection.Dispose();

                strValue = "1";

            }
            catch (Exception ExString)
            {

                ExString.ToString();
                strValue = "-1";
            }

            return strValue;
        }

        public int execQueryInt(string QueryString)
        {
            int intValue = 0;         

            try
            {
                SqlConnection sqlConnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnxSQL"].ConnectionString);

                using (SqlCommand cmd = new SqlCommand(QueryString, sqlConnn))
                {

                    sqlConnn.Open();

                    intValue = (int)cmd.ExecuteScalar();

                    if (sqlConnn.State == System.Data.ConnectionState.Open)
                        sqlConnn.Close();
                    sqlConnn.Dispose();

                }


            }
            catch (Exception ExString)
            {

                ExString.ToString();
                intValue = -1;
            }

            return intValue;
        }

    }
}