using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Trip.JinJiang.H5
{
    class Common
    {
        static string conn = System.Configuration.ConfigurationSettings.AppSettings["conn"];
        /// <summary>
        /// 为combox提供数据格式
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DataTable2Array(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                StringBuilder str = new StringBuilder();
                str.Append("[");
                foreach (DataRow dr in dt.Rows)
                {
                    str.Append("{");
                    foreach (DataColumn dc in dt.Columns)
                    {
                        str.Append("'" + dc.ColumnName + "':'" + Regex.Replace(dr[dc.ColumnName].ToString(), @"[\n\r]", "") + "',");
                    }
                    str.Remove(str.Length - 1, 1);
                    str.Append("},");
                }
                str.Remove(str.Length - 1, 1);
                str.Append("]");
                return str.ToString();
            }
            else
            {
                return "[]";
            }
        }

        public static DataSet fillds(string str)
        {
            string constr = conn;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand lo_cmd = new SqlCommand();
            lo_cmd.Connection = con;
            lo_cmd.CommandText = str;
            SqlDataAdapter dbAdapter = new SqlDataAdapter(lo_cmd);
            DataSet ds = new DataSet();
            dbAdapter.Fill(ds);
            return ds;
        }

    }
}
