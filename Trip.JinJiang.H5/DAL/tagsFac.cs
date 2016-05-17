using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
using LW.Common.DB;
namespace Trip.JinJiang.H5.DAL
{
    /// <summary>
    /// 数据访问类:tagsFac
    /// </summary>
    public partial class tagsFac
    {
        static string conn = System.Configuration.ConfigurationSettings.AppSettings["conn"];
        public tagsFac()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("id", "tagsFac");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from tagsFac");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)            };
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Trip.JinJiang.H5.Tag model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tagsFac(");
            strSql.Append("id,lineId,name,lineCount,createDate,updateDate,tagType,tagSource)");
            strSql.Append(" values (");
            strSql.Append("@id,@lineId,@name,@lineCount,@createDate,@updateDate,@tagType,@tagSource)");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4),
                    new SqlParameter("@lineId", SqlDbType.Int,4),
                    new SqlParameter("@name", SqlDbType.NVarChar,100),
                    new SqlParameter("@lineCount", SqlDbType.Int,4),
                    new SqlParameter("@createDate", SqlDbType.NVarChar,100),
                    new SqlParameter("@updateDate", SqlDbType.NVarChar,100),
                    new SqlParameter("@tagType", SqlDbType.NVarChar,200),
                    new SqlParameter("@tagSource", SqlDbType.NVarChar,200)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.lineId;
            parameters[2].Value = model.name;
            parameters[3].Value = model.lineCount;
            parameters[4].Value = model.createDate;
            parameters[5].Value = model.updateDate;
            parameters[6].Value = model.tagType;
            parameters[7].Value = model.tagSource;

            int rows = DataSource.ExecuteSql(strSql.ToString(), parameters, conn);
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
        public bool Update(Trip.JinJiang.H5.Tag model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tagsFac set ");
            strSql.Append("lineId=@lineId,");
            strSql.Append("name=@name,");
            strSql.Append("lineCount=@lineCount,");
            strSql.Append("createDate=@createDate,");
            strSql.Append("updateDate=@updateDate,");
            strSql.Append("tagType=@tagType,");
            strSql.Append("tagSource=@tagSource");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@lineId", SqlDbType.Int,4),
                    new SqlParameter("@name", SqlDbType.NVarChar,100),
                    new SqlParameter("@lineCount", SqlDbType.Int,4),
                    new SqlParameter("@createDate", SqlDbType.NVarChar,100),
                    new SqlParameter("@updateDate", SqlDbType.NVarChar,100),
                    new SqlParameter("@tagType", SqlDbType.NVarChar,200),
                    new SqlParameter("@tagSource", SqlDbType.NVarChar,200),
                    new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.lineId;
            parameters[1].Value = model.name;
            parameters[2].Value = model.lineCount;
            parameters[3].Value = model.createDate;
            parameters[4].Value = model.updateDate;
            parameters[5].Value = model.tagType;
            parameters[6].Value = model.tagSource;
            parameters[7].Value = model.id;

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
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tagsFac ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)            };
            parameters[0].Value = id;

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
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tagsFac ");
            strSql.Append(" where id in (" + idlist + ")  ");
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
        public Trip.JinJiang.H5.Tag GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,lineId,name,lineCount,createDate,updateDate,tagType,tagSource from tagsFac ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)            };
            parameters[0].Value = id;

            Trip.JinJiang.H5.Tag model = new Trip.JinJiang.H5.Tag();
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
        public Trip.JinJiang.H5.Tag DataRowToModel(DataRow row)
        {
            Trip.JinJiang.H5.Tag model = new Trip.JinJiang.H5.Tag();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["lineId"] != null && row["lineId"].ToString() != "")
                {
                    model.lineId = int.Parse(row["lineId"].ToString());
                }
                if (row["name"] != null)
                {
                    model.name = row["name"].ToString();
                }
                if (row["lineCount"] != null && row["lineCount"].ToString() != "")
                {
                    model.lineCount = int.Parse(row["lineCount"].ToString());
                }
                if (row["createDate"] != null)
                {
                    model.createDate = row["createDate"].ToString();
                }
                if (row["updateDate"] != null)
                {
                    model.updateDate = row["updateDate"].ToString();
                }
                if (row["tagType"] != null)
                {
                    model.tagType = row["tagType"].ToString();
                }
                if (row["tagSource"] != null)
                {
                    model.tagSource = row["tagSource"].ToString();
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
            strSql.Append("select id,lineId,name,lineCount,createDate,updateDate,tagType,tagSource ");
            strSql.Append(" FROM tagsFac ");
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
            strSql.Append(" id,lineId,name,lineCount,createDate,updateDate,tagType,tagSource ");
            strSql.Append(" FROM tagsFac ");
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
            strSql.Append("select count(1) FROM tagsFac ");
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
                strSql.Append("order by T.id desc");
            }
            strSql.Append(")AS Row, T.*  from tagsFac T ");
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
			parameters[0].Value = "tagsFac";
			parameters[1].Value = "id";
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

