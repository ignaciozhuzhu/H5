using LW.Common.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LW.Common.Tool
{
    public class SysConfig
    {
        /// <summary>
        /// 获取系统配置信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetValue(string key)
        {
            string SystemDB = System.Configuration.ConfigurationManager.AppSettings["SystemDB"].ToString();

            string defaultSetOfBook = string.Empty;
            try
            {
                defaultSetOfBook = System.Configuration.ConfigurationManager.AppSettings["DefaultSetsOfBooks"].ToString();
            }
            catch
            {
                throw new Exception("webconfig中缺失DefaultSetsOfBooks参数！");
            }

            string strSql = "select [connectionString] from [SetsOfBooks] where SID = '" + defaultSetOfBook + "'";
            object obj = DataSource.GetSingle(strSql, SystemDB);
            string conn = obj.ToString();

            strSql = "select sysvalue from tbl_SysConfig where syskey = '" + key + "'";
            obj = DataSource.GetSingle(strSql, conn);
            if (obj == null) return string.Empty;
            else return obj.ToString();
        }
    }
}
