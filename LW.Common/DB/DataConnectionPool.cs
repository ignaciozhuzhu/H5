using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace LW.Common.DB
{
    public class DataConnectionPool
    {
        public DataConnectionPool(string connectionstring)
        {
            connectionString = connectionstring;
            connection = new SqlConnection(connectionString);
            connection.Open();

        }
        public SqlConnection connection = new SqlConnection();
        public string connectionString = string.Empty;
        public int s = 0;



        #region 公用方法
        /// <summary>
        /// 判断是否存在某表的某个字段
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="columnName">列名称</param>
        /// <returns>是否存在</returns>
        public bool ColumnExists(string tableName, string columnName)
        {
            s = 1;
            string sql = "select count(1) from syscolumns where [id]=object_id('" + tableName + "') and [name]='" + columnName + "'";
            object res = GetSingle(sql);
            if (res == null)
            {
                return false;
            }
            s = 0;

            return Convert.ToInt32(res) > 0;
        }
        public int GetMaxID(string FieldName, string TableName)
        {
            s = 1;

            string strsql = "select max(" + FieldName + ")+1 from " + TableName;
            object obj = GetSingle(strsql);
            if (obj == null)
            {
                s = 0;

                return 1;
            }
            else
            {
                s = 0;

                return int.Parse(obj.ToString());
            }
        }
        public bool Exists(string strSql)
        {
            s = 1;

            object obj = GetSingle(strSql);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                s = 0;

                return false;
            }
            else
            {
                s = 0;

                return true;
            }
        }
        /// <summary>
        /// 表是否存在
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public bool TabExists(string TableName)
        {
            s = 1;

            string strsql = "select count(*) from sysobjects where id = object_id(N'[" + TableName + "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1";
            //string strsql = "SELECT count(*) FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + TableName + "]') AND type in (N'U')";
            object obj = GetSingle(strsql);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                s = 0;

                return false;
            }
            else
            {
                s = 0;

                return true;
            }
        }
        public bool Exists(string strSql, params SqlParameter[] cmdParms)
        {
            s = 1;

            object obj = GetSingle(strSql, cmdParms);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                s = 0;

                return false;
            }
            else
            {
                s = 0;

                return true;
            }
        }
        #endregion


        #region  执行简单SQL语句
        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSql(string SQLString)
        {
            s = 1;

            using (SqlCommand cmd = new SqlCommand(SQLString, connection))
            {
                try
                {
                    int rows = cmd.ExecuteNonQuery();
                    s = 0;

                    return rows;
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    s = 0;

                    throw e;
                }
            }
        }

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="Times">超时时间</param>
        /// <returns></returns>
        public int ExecuteSqlByTime(string SQLString, int Times)
        {
            using (SqlCommand cmd = new SqlCommand(SQLString, connection))
            {
                try
                {
                    s = 1;

                    cmd.CommandTimeout = Times;
                    int rows = cmd.ExecuteNonQuery();
                    s = 0;

                    return rows;
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    s = 0;

                    throw e;
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>		
        public int ExecuteSqlTran(List<String> SQLStringList)
        {
            s = 1;

            SqlConnection connection2 = new SqlConnection(connectionString);
            connection2.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection2;
            SqlTransaction tx = connection2.BeginTransaction();
            cmd.Transaction = tx;

            int errIndex = -1;
            try
            {
                int count = 0;
                for (int n = 0; n < SQLStringList.Count; n++)
                {
                    string strsql = SQLStringList[n];
                    if (strsql.Trim().Length > 1)
                    {
                        errIndex = n;
                        cmd.CommandText = strsql;
                        count += cmd.ExecuteNonQuery();
                    }
                }
                tx.Commit();
                connection2.Close();
                s = 0;

                return count;
            }
            catch (Exception e)
            {
                tx.Rollback();
                connection2.Close();
                s = 0;

                throw new Exception("错误行:" + errIndex + ",错误信息:" + e.Message);
            }

        }


        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSql(string SQLString, string content)
        {
            s = 1;

            SqlCommand cmd = new SqlCommand(SQLString, connection);
            System.Data.SqlClient.SqlParameter myParameter = new System.Data.SqlClient.SqlParameter("@content", SqlDbType.NText);
            myParameter.Value = content;
            cmd.Parameters.Add(myParameter);
            try
            {
                int rows = cmd.ExecuteNonQuery();
                s = 0;

                return rows;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
            finally
            {
                s = 0;

                cmd.Dispose();
            }
        }


        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public object ExecuteSqlGet(string SQLString, string content)
        {
            s = 1;

            SqlCommand cmd = new SqlCommand(SQLString, connection);
            System.Data.SqlClient.SqlParameter myParameter = new System.Data.SqlClient.SqlParameter("@content", SqlDbType.NText);
            myParameter.Value = content;
            cmd.Parameters.Add(myParameter);
            try
            {
                object obj = cmd.ExecuteScalar();
                if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                {
                    return null;
                }
                else
                {
                    return obj;
                }
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
            finally
            {
                s = 0;

                cmd.Dispose();
            }
        }


        /// <summary>
        /// 向数据库里插入图像格式的字段(和上面情况类似的另一种实例)
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSqlInsertImg(string strSQL, byte[] fs)
        {
            s = 1;

            SqlCommand cmd = new SqlCommand(strSQL, connection);
            System.Data.SqlClient.SqlParameter myParameter = new System.Data.SqlClient.SqlParameter("@fs", SqlDbType.Image);
            myParameter.Value = fs;
            cmd.Parameters.Add(myParameter);
            try
            {
                int rows = cmd.ExecuteNonQuery();
                return rows;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
            finally
            {
                s = 0;

                cmd.Dispose();
            }
        }



        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public object GetSingle(string SQLString)
        {
            using (SqlCommand cmd = new SqlCommand(SQLString, connection))
            {
                try
                {
                    s = 1;

                    object obj = cmd.ExecuteScalar();
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        s = 0;

                        return null;
                    }
                    else
                    {
                        s = 0;

                        return obj;
                    }
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    s = 0;

                    throw e;
                }
            }
        }

        public object GetSingle(string SQLString, int Times)
        {
            using (SqlCommand cmd = new SqlCommand(SQLString, connection))
            {
                try
                {
                    s = 1;

                    cmd.CommandTimeout = Times;
                    object obj = cmd.ExecuteScalar();
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        s = 0;

                        return null;
                    }
                    else
                    {
                        s = 0;

                        return obj;
                    }
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    s = 0;

                    throw e;
                }
            }
        }



        /// <summary>
        /// 执行查询语句，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader ExecuteReader(string strSQL)
        {
            s = 1;

            //SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(strSQL, connection);
            try
            {
                if (connection.State.ToString() != "Open")
                {
                    connection.Open();
                }
                SqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                s = 0;

                return myReader;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                s = 0;

                throw e;
            }
        }


        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public DataSet Query(SqlCommand cmd)
        {
            s = 1;

            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter command = new SqlDataAdapter();
                command.SelectCommand = cmd;
                command.Fill(ds, "ds"); return ds;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                s = 0;

                // connection.Close();
            }

        }

        public DataSet Query(string SQLString)
        {
            s = 1;

            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                command.Fill(ds, "ds");
                return ds;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                s = 0;

                // connection.Close();
            }

        }

        public DataSet Query(string SQLString, int startRow, int Rowcount)
        {
            s = 1;
            SQLString += " select @@rowcount ";
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                command.Fill(ds, startRow, Rowcount, "ds");
                return ds;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                s = 0;

                // connection.Close();
            }

        }

        public DataSet Query(string SQLString, int Timeout)
        {
            s = 1;

            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                command.SelectCommand.CommandTimeout = Timeout;
                command.Fill(ds, "ds");
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            s = 0;

            return ds;
        }
        #endregion


        #region 执行带参数的SQL语句


        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSql(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                try
                {
                    s = 1;

                    PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                    int rows = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    s = 0;

                    return rows;
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    s = 0;

                    throw e;
                }
            }
        }


        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public void ExecuteSqlTran(Hashtable SQLStringList)
        {
            s = 1;

            SqlConnection connection2 = new SqlConnection(connectionString);
            connection2.Open();
            using (SqlTransaction trans = connection2.BeginTransaction())
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    //循环
                    foreach (DictionaryEntry myDE in SQLStringList)
                    {
                        string cmdText = myDE.Key.ToString();
                        SqlParameter[] cmdParms = (SqlParameter[])myDE.Value;
                        PrepareCommand(cmd, connection2, trans, cmdText, cmdParms);
                        int val = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                    trans.Commit();
                    connection2.Close();

                }
                catch
                {
                    trans.Rollback();
                    connection2.Close();
                    throw;
                }
            }
            s = 0;

        }



        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public int ExecuteSqlTran(System.Collections.Generic.List<CommandInfo> cmdList)
        {
            s = 1;

            SqlConnection connection2 = new SqlConnection(connectionString);
            connection2.Open();
            using (SqlTransaction trans = connection2.BeginTransaction())
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    int count = 0;
                    //循环
                    foreach (CommandInfo myDE in cmdList)
                    {
                        string cmdText = myDE.CommandText;
                        SqlParameter[] cmdParms = (SqlParameter[])myDE.Parameters;
                        PrepareCommand(cmd, connection2, trans, cmdText, cmdParms);

                        if (myDE.EffentNextType == EffentNextType.WhenHaveContine || myDE.EffentNextType == EffentNextType.WhenNoHaveContine)
                        {
                            if (myDE.CommandText.ToLower().IndexOf("count(") == -1)
                            {
                                trans.Rollback();
                                s = 0;

                                return 0;
                            }

                            object obj = cmd.ExecuteScalar();
                            bool isHave = false;
                            if (obj == null && obj == DBNull.Value)
                            {
                                isHave = false;
                            }
                            isHave = Convert.ToInt32(obj) > 0;

                            if (myDE.EffentNextType == EffentNextType.WhenHaveContine && !isHave)
                            {
                                trans.Rollback();
                                s = 0;

                                return 0;
                            }
                            if (myDE.EffentNextType == EffentNextType.WhenNoHaveContine && isHave)
                            {
                                trans.Rollback();
                                s = 0;

                                return 0;
                            }
                            continue;
                        }
                        int val = cmd.ExecuteNonQuery();
                        count += val;
                        if (myDE.EffentNextType == EffentNextType.ExcuteEffectRows && val == 0)
                        {
                            trans.Rollback();
                            s = 0;

                            return 0;
                        }
                        cmd.Parameters.Clear();
                    }
                    trans.Commit();
                    connection2.Close();
                    s = 0;

                    return count;
                }
                catch
                {
                    trans.Rollback();
                    connection2.Close();
                    s = 0;

                    throw;
                }
            }
        }



        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public void ExecuteSqlTranWithIndentity(System.Collections.Generic.List<CommandInfo> SQLStringList)
        {
            s = 1;

            SqlConnection connection2 = new SqlConnection(connectionString);
            connection2.Open();
            using (SqlTransaction trans = connection2.BeginTransaction())
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    int indentity = 0;
                    //循环
                    foreach (CommandInfo myDE in SQLStringList)
                    {
                        string cmdText = myDE.CommandText;
                        SqlParameter[] cmdParms = (SqlParameter[])myDE.Parameters;
                        foreach (SqlParameter q in cmdParms)
                        {
                            if (q.Direction == ParameterDirection.InputOutput)
                            {
                                q.Value = indentity;
                            }
                        }
                        PrepareCommand(cmd, connection2, trans, cmdText, cmdParms);
                        int val = cmd.ExecuteNonQuery();
                        foreach (SqlParameter q in cmdParms)
                        {
                            if (q.Direction == ParameterDirection.Output)
                            {
                                indentity = Convert.ToInt32(q.Value);
                            }
                        }
                        cmd.Parameters.Clear();
                    }
                    trans.Commit();
                    connection2.Close();
                }
                catch
                {
                    trans.Rollback();
                    connection2.Close();
                    throw;
                }
            }
            s = 0;

        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public void ExecuteSqlTranWithIndentity(Hashtable SQLStringList)
        {
            s = 1;

            SqlConnection connection2 = new SqlConnection(connectionString);
            connection2.Open();

            using (SqlTransaction trans = connection2.BeginTransaction())
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    int indentity = 0;
                    //循环
                    foreach (DictionaryEntry myDE in SQLStringList)
                    {
                        string cmdText = myDE.Key.ToString();
                        SqlParameter[] cmdParms = (SqlParameter[])myDE.Value;
                        foreach (SqlParameter q in cmdParms)
                        {
                            if (q.Direction == ParameterDirection.InputOutput)
                            {
                                q.Value = indentity;
                            }
                        }
                        PrepareCommand(cmd, connection2, trans, cmdText, cmdParms);
                        int val = cmd.ExecuteNonQuery();
                        foreach (SqlParameter q in cmdParms)
                        {
                            if (q.Direction == ParameterDirection.Output)
                            {
                                indentity = Convert.ToInt32(q.Value);
                            }
                        }
                        cmd.Parameters.Clear();
                    }
                    trans.Commit();
                    connection2.Close();
                }
                catch
                {
                    trans.Rollback();
                    connection2.Close();
                    throw;
                }
            }
            s = 0;

        }


        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public object GetSingle(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                try
                {
                    s = 1;

                    PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                    object obj = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        s = 0;

                        return null;
                    }
                    else
                    {
                        s = 0;

                        return obj;
                    }
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    s = 0;

                    throw e;
                }
            }
        }


        /// <summary>
        /// 执行查询语句，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader ExecuteReader(string SQLString, params SqlParameter[] cmdParms)
        {
            s = 1;
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                SqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                s = 0;
                return myReader;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                s = 0;
                throw e;
            }
            //			finally
            //			{
            //				cmd.Dispose();
            //				connection.Close();
            //			}	

        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public DataSet Query(string SQLString, params SqlParameter[] cmdParms)
        {
            s = 1;
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, connection, null, SQLString, cmdParms);
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();
                try
                {
                    da.Fill(ds, "ds");
                    cmd.Parameters.Clear();
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                s = 0;
                return ds;
            }
        }

        public DataSet ProcedureQuery(string SQLString, params SqlParameter[] cmdParms)
        {
            s = 1;
            SqlCommand cmd = new SqlCommand();
            PrepareProcedureCommand(cmd, connection, null, SQLString, cmdParms);
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();
                try
                {
                    da.Fill(ds, "ds");
                    cmd.Parameters.Clear();
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                s = 0;
                return ds;
            }
        }

        private void PrepareProcedureCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            s = 1;
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.StoredProcedure;//cmdType;
            cmd.CommandTimeout = 150;
            if (cmdParms != null)
            {


                foreach (SqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
            s = 0;
        }

        private void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            s = 1;
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
            s = 0;
        }

        #endregion


        #region 存储过程操作

        /// <summary>
        /// 执行存储过程，返回SqlDataReader  ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            s = 1;
            SqlDataReader returnReader;
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.CommandType = CommandType.StoredProcedure;
            returnReader = command.ExecuteReader(CommandBehavior.CloseConnection);
            s = 0;
            return returnReader;
        }


        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            s = 1;
            DataSet dataSet = new DataSet();
            SqlDataAdapter sqlDA = new SqlDataAdapter();
            sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
            sqlDA.Fill(dataSet, tableName);
            s = 0;
            return dataSet;
        }
        public DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName, int Times)
        {
            s = 1;
            DataSet dataSet = new DataSet();
            SqlDataAdapter sqlDA = new SqlDataAdapter();
            sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
            sqlDA.SelectCommand.CommandTimeout = Times;
            sqlDA.Fill(dataSet, tableName);
            s = 0;
            return dataSet;
        }


        /// <summary>
        /// 构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand</returns>
        private SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            s = 1;
            SqlCommand command = new SqlCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
            {
                if (parameter != null)
                {
                    // 检查未分配值的输出参数,将其分配以DBNull.Value.
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    command.Parameters.Add(parameter);
                }
            }

            s = 0;
            return command;
        }

        /// <summary>
        /// 执行存储过程，返回影响的行数		
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="rowsAffected">影响的行数</param>
        /// <returns></returns>
        public int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            s = 1;
            int result;
            SqlCommand command = BuildIntCommand(connection, storedProcName, parameters);
            rowsAffected = command.ExecuteNonQuery();
            result = (int)command.Parameters["ReturnValue"].Value;
            s = 0;
            return result;
        }

        /// <summary>
        /// 创建 SqlCommand 对象实例(用来返回一个整数值)	
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand 对象实例</returns>
        private SqlCommand BuildIntCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            s = 1;
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.Parameters.Add(new SqlParameter("ReturnValue",
                SqlDbType.Int, 4, ParameterDirection.ReturnValue,
                false, 0, 0, string.Empty, DataRowVersion.Default, null));
            s = 0;
            return command;
        }
        #endregion

    }
}
