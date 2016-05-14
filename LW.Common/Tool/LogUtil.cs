using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LW.Common.Tool
{
    public static class LogUtil
    {
        //默认数据库连接
        private static string GetDataBaseConn()
        {
            string strSql = "select [conn] from [tbl_SysOutDatabase] where name = 'DBConStr_LYDB'";
            object obj = DB.DataSource.GetSingle(strSql);
            return obj.ToString();
        }

        public static void saveLog(Exception e,string module,string action,int notification) {
            string strSql = string.Empty;
            strSql = "EXEC PROC_LOG_SAVE '"+e.Message.Replace("'","''")+"','"+module+"','"+action+"','"+notification.ToString()+"' ";
            DB.DataSource.ExecuteSql(strSql, GetDataBaseConn());
            //TODO 根据notification判断是否需要提醒开发人员处理问题
        }

        //默认不做任何操作的情况
        public static void saveLog(Exception e, string module, string action)
        {
            string strSql = string.Empty;
            strSql = "EXEC PROC_LOG_SAVE '" + e.Message.Replace("'", "''") + "','" + module + "','" + action + "','default' ";
            DB.DataSource.ExecuteSql(strSql, GetDataBaseConn());
        }
    }
}
