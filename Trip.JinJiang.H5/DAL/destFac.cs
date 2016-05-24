using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace Trip.JinJiang.H5.DAL
{
    /// <summary>
    /// 数据访问类:destFac
    /// </summary>
    public partial class destFac
    {
        public destFac()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from tbl_destination");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Trip.JinJiang.H5.Model.destMod model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tbl_destination(");
            strSql.Append("A_Id,destName,status,[order])");
            strSql.Append(" values (");
            strSql.Append("@A_Id,@destName,@status,@order)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@A_Id", SqlDbType.Int,4),
                    new SqlParameter("@destName", SqlDbType.NVarChar,50),
                    new SqlParameter("@status", SqlDbType.Bit,1),
                    new SqlParameter("@order", SqlDbType.Int,4)};
            parameters[0].Value = model.A_Id;
            parameters[1].Value = model.destName;
            parameters[2].Value = model.status;
            parameters[3].Value = model.order;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新数据状态
        /// </summary>
        public bool ChangeStatus(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tbl_destination set ");
            strSql.Append("status=~status ");
            strSql.Append(" where Id=@Id and status='true'");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Trip.JinJiang.H5.Model.destMod model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tbl_destination set ");
            strSql.Append("A_Id=@A_Id,");
            strSql.Append("destName=@destName,");
            strSql.Append("status=@status,");
            strSql.Append("[order]=@order");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@A_Id", SqlDbType.Int,4),
                    new SqlParameter("@destName", SqlDbType.NVarChar,50),
                    new SqlParameter("@status", SqlDbType.Bit,1),
                    new SqlParameter("@order", SqlDbType.Int,4),
                    new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.A_Id;
            parameters[1].Value = model.destName;
            parameters[2].Value = model.status;
            parameters[3].Value = model.order;
            parameters[4].Value = model.Id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tbl_destination ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tbl_destination ");
            strSql.Append(" where Id in (" + Idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Trip.JinJiang.H5.Model.destMod GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,A_Id,destName,status,order from tbl_destination ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;

            Trip.JinJiang.H5.Model.destMod model = new Trip.JinJiang.H5.Model.destMod();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Trip.JinJiang.H5.Model.destMod DataRowToModel(DataRow row)
        {
            Trip.JinJiang.H5.Model.destMod model = new Trip.JinJiang.H5.Model.destMod();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = int.Parse(row["Id"].ToString());
                }
                if (row["A_Id"] != null && row["A_Id"].ToString() != "")
                {
                    model.A_Id = int.Parse(row["A_Id"].ToString());
                }
                if (row["destName"] != null)
                {
                    model.destName = row["destName"].ToString();
                }
                if (row["status"] != null && row["status"].ToString() != "")
                {
                    if ((row["status"].ToString() == "1") || (row["status"].ToString().ToLower() == "true"))
                    {
                        model.status = true;
                    }
                    else
                    {
                        model.status = false;
                    }
                }
                if (row["order"] != null && row["order"].ToString() != "")
                {
                    model.order = int.Parse(row["order"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,A_Id,destName,status,[order] ");
            strSql.Append(" FROM tbl_destination where 1=1 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            strSql.Append(" and status='true' order by [order]");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" Id,A_Id,destName,status,order ");
            strSql.Append(" FROM tbl_destination ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM tbl_destination ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.Id desc");
            }
            strSql.Append(")AS Row, T.*  from tbl_destination T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "tbl_destination";
			parameters[1].Value = "Id";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

