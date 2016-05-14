using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Web.SessionState;
using System.Web;
using LW.Common.Model;

namespace LW.Common.DB
{
    public class DataSource
    {
        //public static 
        public DataSource() { }

        /// <summary>
        /// 池中池
        /// </summary>
        private static List<DataConnectionPool> DcList = new List<DataConnectionPool>();


        /// <summary>
        /// 新建连接池并加入池中池
        /// </summary>
        /// <param name="connectionstring"></param>
        /// <returns></returns>
        public static DataConnectionPool AddDcList(string connectionstring)
        {
            try
            {
                DataConnectionPool dc = new DataConnectionPool(connectionstring);
                DcList.Add(dc);
                return dc;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取池中池里面的连接池
        /// </summary>
        /// <param name="connectionstring"></param>
        /// <returns></returns>
        public static DataConnectionPool GetDcp(string connectionstring)
        {
            try
            {
                foreach (DataConnectionPool dc in DcList)
                {
                    //if (dc.connectionString.Equals(connectionstring))
                    if (dc.connectionString.Equals(connectionstring) && dc.connection.State == System.Data.ConnectionState.Open && dc.s == 0)
                    {
                        return dc;
                    }
                }
                return AddDcList(connectionstring);
            }
            catch (Exception e)
            {
               
                return null;
            }
        }

        public static DataConnectionPool GetDc(string connectionstring)
        {
            try
            {
                DataConnectionPool dc = GetDcp(connectionstring.Replace('|', '\\'));
                if (dc.connection.State != System.Data.ConnectionState.Open)
                {
                    dc.connection.Open();
                }
                return dc;

            }
            catch (Exception e)
            {
                DcList.Clear();//链接池不知何故会挂掉，在这里加一个判断，如果挂了就清空连接池重新来过。
                return null;
            }

        }


        private static string Getconnectionstring()
        {
            OLUsers oLUsers = ((OLUsers)HttpContext.Current.Session["OLUser"]);
            return oLUsers.ConnectionString;
        }

        #region 公用方法
        /// <summary>
        /// 判断是否存在某表的某个字段
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="columnName">列名称</param>
        /// <returns>是否存在</returns>
        public static bool ColumnExists(string tableName, string columnName, string connectionstring)
        {
            return GetDc(connectionstring).ColumnExists(tableName, columnName);
        }
        public static bool ColumnExists(string tableName, string columnName)
        {
            return GetDc(Getconnectionstring()).ColumnExists(tableName, columnName);
        }
        public static int GetMaxID(string FieldName, string TableName, string connectionstring)
        {
            return GetDc(connectionstring).GetMaxID(FieldName, TableName);
        }
        public static int GetMaxID(string FieldName, string TableName)
        {
            return GetDc(Getconnectionstring()).GetMaxID(FieldName, TableName);
        }
        public static bool Exists(string strSql, string connectionstring)
        {
            return GetDc(connectionstring).Exists(strSql);
        }
        public static bool Exists(string strSql)
        {
            return GetDc(Getconnectionstring()).Exists(strSql);
        }
        /// <summary>
        /// 表是否存在
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public static bool TabExists(string TableName, string connectionstring)
        {
            return GetDc(connectionstring).TabExists(TableName);
        }
        public static bool TabExists(string TableName)
        {
            return GetDc(Getconnectionstring()).TabExists(TableName);
        }
        public static bool Exists(string strSql, SqlParameter[] cmdParms, string connectionstring)
        {
            return GetDc(connectionstring).Exists(strSql, cmdParms);
        }
        public static bool Exists(string strSql, SqlParameter[] cmdParms)
        {
            return GetDc(Getconnectionstring()).Exists(strSql, cmdParms);
        }
        #endregion


        #region  执行简单SQL语句
        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="connectionstring">连接字符串</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, string connectionstring)
        {
            return GetDc(connectionstring).ExecuteSql(SQLString);
        }
        public static int ExecuteSql(string SQLString)
        {
            return GetDc(Getconnectionstring()).ExecuteSql(SQLString);
        }


        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="Times">超时时间</param>
        /// <returns></returns>
        public static int ExecuteSqlByTime(string SQLString, int Times, string connectionstring)
        {
            return GetDc(connectionstring).ExecuteSqlByTime(SQLString, Times);
        }
        public static int ExecuteSqlByTime(string SQLString, int Times)
        {
            return GetDc(Getconnectionstring()).ExecuteSqlByTime(SQLString, Times);
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>		
        public static int ExecuteSqlTran(List<String> SQLStringList, string connectionstring)
        {
            return GetDc(connectionstring).ExecuteSqlTran(SQLStringList);
        }
        public static int ExecuteSqlTran(List<String> SQLStringList)
        {
            return GetDc(Getconnectionstring()).ExecuteSqlTran(SQLStringList);
        }


        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, string content, string connectionstring)
        {
            return GetDc(connectionstring).ExecuteSql(SQLString, content);
        }
        public static int ExecuteSql2(string SQLString, string content)
        {
            return GetDc(Getconnectionstring()).ExecuteSql(SQLString, content);
        }


        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public static object ExecuteSqlGet(string SQLString, string content, string connectionstring)
        {
            return GetDc(connectionstring).ExecuteSqlGet(SQLString, content);
        }
        public static object ExecuteSqlGet(string SQLString, string content)
        {
            return GetDc(Getconnectionstring()).ExecuteSqlGet(SQLString, content);
        }

        /// <summary>
        /// 向数据库里插入图像格式的字段(和上面情况类似的另一种实例)
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSqlInsertImg(string strSQL, byte[] fs, string connectionstring)
        {
            return GetDc(connectionstring).ExecuteSqlInsertImg(strSQL, fs);
        }
        public static int ExecuteSqlInsertImg(string strSQL, byte[] fs)
        {
            return GetDc(Getconnectionstring()).ExecuteSqlInsertImg(strSQL, fs);
        }


        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString, string connectionstring)
        {
            return GetDc(connectionstring).GetSingle(SQLString);
        }
        public static object GetSingle(string SQLString)
        {
            return GetDc(Getconnectionstring()).GetSingle(SQLString);
        }


        public static object GetSingle(string SQLString, int Times, string connectionstring)
        {
            return GetDc(connectionstring).GetSingle(SQLString, Times);
        }
        public static object GetSingle(string SQLString, int Times)
        {
            return GetDc(Getconnectionstring()).GetSingle(SQLString, Times);
        }


        /// <summary>
        /// 执行查询语句，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string strSQL, string connectionstring)
        {
            return GetDc(connectionstring).ExecuteReader(strSQL);
        }
        public static SqlDataReader ExecuteReader(string strSQL)
        {
            return GetDc(Getconnectionstring()).ExecuteReader(strSQL);
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString, string connectionstring)
        {
            return GetDc(connectionstring).Query(SQLString);
        }
        public static DataSet Query(string SQLString)
        {
            return GetDc(Getconnectionstring()).Query(SQLString);
        }

        public static DataTable ExecuteProcedure(string SQLString, string connectionstring, params SqlParameter[] parameters)
        {
            return GetDc(connectionstring).ProcedureQuery(SQLString, parameters).Tables[0];
        }

        public static DataTable ExecuteProcedure(string SQLString, params SqlParameter[] parameters)
        {
            return GetDc(Getconnectionstring()).ProcedureQuery(SQLString, parameters).Tables[0];
        }

        public static DataSet Procedure(string SQLString, string connectionstring, params SqlParameter[] parameters)
        {
            return GetDc(connectionstring).ProcedureQuery(SQLString, parameters);
        }

        public static DataSet Procedure(string SQLString, params SqlParameter[] parameters)
        {
            return GetDc(Getconnectionstring()).ProcedureQuery(SQLString, parameters);
        }

        public static DataTable ExecuteQuery(string SQLString, string connectionstring, params SqlParameter[] parameters)
        {
            return GetDc(connectionstring).Query(SQLString, parameters).Tables[0];
        }
        public static DataTable ExecuteQuery(string SQLString, string connectionstring)
        {
            return GetDc(connectionstring).Query(SQLString).Tables[0];
        }
        public static DataTable ExecuteQuery(string SQLString)
        {
            return GetDc(Getconnectionstring()).Query(SQLString).Tables[0];
        }
        public static DataTableCollection ExecuteQuery(string SQLString, int startRow, int Rowcount, string connectionstring)
        {
            return GetDc(connectionstring).Query(SQLString, startRow, Rowcount).Tables;
        }

        public static DataSet Query(string SQLString, int Times, string connectionstring)
        {
            return GetDc(connectionstring).Query(SQLString, Times);
        }
        public static DataSet Query(string SQLString, int Times)
        {
            return GetDc(Getconnectionstring()).Query(SQLString, Times);
        }

        #endregion

        #region 执行带参数的SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, SqlParameter[] cmdParms, string connectionstring)
        {
            return GetDc(connectionstring).ExecuteSql(SQLString, cmdParms);
        }
        public static int ExecuteSql(string SQLString, SqlParameter[] cmdParms)
        {
            return GetDc(Getconnectionstring()).ExecuteSql(SQLString, cmdParms);
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public static void ExecuteSqlTran(Hashtable SQLStringList, string connectionstring)
        {
            GetDc(connectionstring).ExecuteSqlTran(SQLStringList);
        }
        public static void ExecuteSqlTran(Hashtable SQLStringList)
        {
            GetDc(Getconnectionstring()).ExecuteSqlTran(SQLStringList);
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public static int ExecuteSqlTran(System.Collections.Generic.List<CommandInfo> cmdList, string connectionstring)
        {
            return GetDc(connectionstring).ExecuteSqlTran(cmdList);
        }
        public static int ExecuteSqlTran(System.Collections.Generic.List<CommandInfo> cmdList)
        {
            return GetDc(Getconnectionstring()).ExecuteSqlTran(cmdList);
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public static void ExecuteSqlTranWithIndentity(System.Collections.Generic.List<CommandInfo> SQLStringList, string connectionstring)
        {
            GetDc(connectionstring).ExecuteSqlTranWithIndentity(SQLStringList);
        }

        public static void ExecuteSqlTranWithIndentity(System.Collections.Generic.List<CommandInfo> SQLStringList)
        {
            GetDc(Getconnectionstring()).ExecuteSqlTranWithIndentity(SQLStringList);
        }
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public static void ExecuteSqlTranWithIndentity(Hashtable SQLStringList, string connectionstring)
        {
            GetDc(connectionstring).ExecuteSqlTranWithIndentity(SQLStringList);
        }
        public static void ExecuteSqlTranWithIndentity(Hashtable SQLStringList)
        {
            GetDc(Getconnectionstring()).ExecuteSqlTranWithIndentity(SQLStringList);
        }


        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString, SqlParameter[] cmdParms, string connectionstring)
        {
            return GetDc(connectionstring).GetSingle(SQLString, cmdParms);
        }
        public static object GetSingle(string SQLString, SqlParameter[] cmdParms)
        {
            return GetDc(Getconnectionstring()).GetSingle(SQLString, cmdParms);
        }



        /// <summary>
        /// 执行查询语句，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string SQLString, SqlParameter[] cmdParms, string connectionstring)
        {
            return GetDc(connectionstring).ExecuteReader(SQLString, cmdParms);
        }
        public static SqlDataReader ExecuteReader(string SQLString, SqlParameter[] cmdParms)
        {
            return GetDc(Getconnectionstring()).ExecuteReader(SQLString, cmdParms);
        }



        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString, SqlParameter[] cmdParms, string connectionstring)
        {
            return GetDc(connectionstring).Query(SQLString, cmdParms);
        }
        public static DataSet Query(string SQLString, SqlParameter[] cmdParms)
        {
            return GetDc(Getconnectionstring()).Query(SQLString, cmdParms);
        }


        #endregion



        #region 存储过程操作

        /// <summary>
        /// 执行存储过程，返回SqlDataReader  ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters, string connectionstring)
        {
            return GetDc(connectionstring).RunProcedure(storedProcName, parameters);
        }
        public static SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            return GetDc(Getconnectionstring()).RunProcedure(storedProcName, parameters);
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName, string connectionstring)
        {
            return GetDc(connectionstring).RunProcedure(storedProcName, parameters, tableName);
        }

        public static DataSet RunProcedure2(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            return GetDc(Getconnectionstring()).RunProcedure(storedProcName, parameters, tableName);
        }


        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName, int Times, string connectionstring)
        {
            return GetDc(connectionstring).RunProcedure(storedProcName, parameters, tableName, Times);
        }
        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName, int Times)
        {
            return GetDc(Getconnectionstring()).RunProcedure(storedProcName, parameters, tableName, Times);
        }


        /// <summary>
        /// 执行存储过程，返回影响的行数		
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="rowsAffected">影响的行数</param>
        /// <returns></returns>
        public int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected, string connectionstring)
        {
            return GetDc(connectionstring).RunProcedure(storedProcName, parameters, out  rowsAffected);
        }

        public int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            return GetDc(Getconnectionstring()).RunProcedure(storedProcName, parameters, out  rowsAffected);
        }



        #endregion

    }
}
