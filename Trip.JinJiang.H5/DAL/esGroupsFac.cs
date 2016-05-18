using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
//using LW.Common.DB;
namespace Trip.JinJiang.H5.DAL
{
    /// <summary>
    /// 数据访问类:esGroupsFac
    /// </summary>
    public partial class esGroupsFac
    {
        static string conn = System.Configuration.ConfigurationSettings.AppSettings["conn"]; 
        public esGroupsFac()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("groupId", "esGroupsFac");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int groupId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from esGroupsFac");
            strSql.Append(" where groupId=@groupId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@groupId", SqlDbType.Int,4)           };
            parameters[0].Value = groupId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Trip.JinJiang.H5.EsGroup model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tbl_esGroups(");
            strSql.Append("groupId,lineId,salePrice,clusterType,deadLineTime,departDate,deadLineTimeVal)");
            strSql.Append(" values (");
            strSql.Append("@groupId,@lineId,@salePrice,@clusterType,@deadLineTime,@departDate,@deadLineTimeVal)");
            SqlParameter[] parameters = {
                    new SqlParameter("@groupId", SqlDbType.Int,4),
                    new SqlParameter("@lineId", SqlDbType.Int,4),
                    new SqlParameter("@salePrice", SqlDbType.Decimal,9),
                    new SqlParameter("@clusterType", SqlDbType.Int,4),
                    new SqlParameter("@deadLineTime", SqlDbType.BigInt,8),
                    new SqlParameter("@departDate", SqlDbType.NVarChar,100),
                    new SqlParameter("@deadLineTimeVal", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.groupId;
            parameters[1].Value = model.lineId;
            parameters[2].Value = model.salePrice;
            parameters[3].Value = model.clusterType;
            parameters[4].Value = model.deadLineTime;
            parameters[5].Value = model.departDate;
            parameters[6].Value = model.deadLineTimeVal;

          //  int rows = DataSource.ExecuteSql(strSql.ToString(), parameters, conn);

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
        public bool Update(Trip.JinJiang.H5.EsGroup model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update esGroupsFac set ");
            strSql.Append("lineId=@lineId,");
            strSql.Append("salePrice=@salePrice,");
            strSql.Append("clusterType=@clusterType,");
            strSql.Append("deadLineTime=@deadLineTime,");
            strSql.Append("departDate=@departDate,");
            strSql.Append("deadLineTimeVal=@deadLineTimeVal");
            strSql.Append(" where groupId=@groupId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@lineId", SqlDbType.Int,4),
                    new SqlParameter("@salePrice", SqlDbType.Decimal,9),
                    new SqlParameter("@clusterType", SqlDbType.Int,4),
                    new SqlParameter("@deadLineTime", SqlDbType.BigInt,8),
                    new SqlParameter("@departDate", SqlDbType.NVarChar,100),
                    new SqlParameter("@deadLineTimeVal", SqlDbType.NVarChar,100),
                    new SqlParameter("@groupId", SqlDbType.Int,4)};
            parameters[0].Value = model.lineId;
            parameters[1].Value = model.salePrice;
            parameters[2].Value = model.clusterType;
            parameters[3].Value = model.deadLineTime;
            parameters[4].Value = model.departDate;
            parameters[5].Value = model.deadLineTimeVal;
            parameters[6].Value = model.groupId;

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
        public bool Delete(int groupId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from esGroupsFac ");
            strSql.Append(" where groupId=@groupId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@groupId", SqlDbType.Int,4)           };
            parameters[0].Value = groupId;

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
        public bool DeleteList(string groupIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from esGroupsFac ");
            strSql.Append(" where groupId in (" + groupIdlist + ")  ");
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
        public Trip.JinJiang.H5.EsGroup GetModel(int groupId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 groupId,lineId,salePrice,clusterType,deadLineTime,departDate,deadLineTimeVal from esGroupsFac ");
            strSql.Append(" where groupId=@groupId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@groupId", SqlDbType.Int,4)           };
            parameters[0].Value = groupId;

            Trip.JinJiang.H5.EsGroup model = new Trip.JinJiang.H5.EsGroup();
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
        public Trip.JinJiang.H5.EsGroup DataRowToModel(DataRow row)
        {
            Trip.JinJiang.H5.EsGroup model = new Trip.JinJiang.H5.EsGroup();
            if (row != null)
            {
                if (row["groupId"] != null && row["groupId"].ToString() != "")
                {
                    model.groupId = int.Parse(row["groupId"].ToString());
                }
                if (row["lineId"] != null && row["lineId"].ToString() != "")
                {
                    model.lineId = int.Parse(row["lineId"].ToString());
                }
                if (row["salePrice"] != null && row["salePrice"].ToString() != "")
                {
                    model.salePrice = decimal.Parse(row["salePrice"].ToString());
                }
                if (row["clusterType"] != null && row["clusterType"].ToString() != "")
                {
                    model.clusterType = int.Parse(row["clusterType"].ToString());
                }
                if (row["deadLineTime"] != null && row["deadLineTime"].ToString() != "")
                {
                    model.deadLineTime = long.Parse(row["deadLineTime"].ToString());
                }
                if (row["departDate"] != null)
                {
                    model.departDate = row["departDate"].ToString();
                }
                if (row["deadLineTimeVal"] != null)
                {
                    model.deadLineTimeVal = row["deadLineTimeVal"].ToString();
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
            strSql.Append("select groupId,lineId,salePrice,clusterType,deadLineTime,departDate,deadLineTimeVal ");
            strSql.Append(" FROM esGroupsFac ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
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
            strSql.Append(" groupId,lineId,salePrice,clusterType,deadLineTime,departDate,deadLineTimeVal ");
            strSql.Append(" FROM esGroupsFac ");
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
            strSql.Append("select count(1) FROM esGroupsFac ");
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
                strSql.Append("order by T.groupId desc");
            }
            strSql.Append(")AS Row, T.*  from esGroupsFac T ");
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
			parameters[0].Value = "esGroupsFac";
			parameters[1].Value = "groupId";
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

