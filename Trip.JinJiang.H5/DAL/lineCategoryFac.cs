using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace Trip.JinJiang.H5.DAL
{
    /// <summary>
    /// 数据访问类:lineCategoryFac
    /// </summary>
    public partial class lineCategoryFac
    {
        public lineCategoryFac()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string lineCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from tbl_lineCategory");
            strSql.Append(" where lineCode=@lineCode ");
            SqlParameter[] parameters = {
                    new SqlParameter("@lineCode", SqlDbType.NVarChar,50)            };
            parameters[0].Value = lineCode;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Trip.JinJiang.H5.Model.lineCategoryMod model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tbl_lineCategory(");
            strSql.Append("categoryName,lineCategory,imgUrl)");
            strSql.Append(" values (");
            strSql.Append("@categoryName,@lineCategory,@imgUrl)");
            SqlParameter[] parameters = {
                    new SqlParameter("@categoryName", SqlDbType.NVarChar,50),
                    new SqlParameter("@lineCategory", SqlDbType.NVarChar,50),
                    new SqlParameter("@imgUrl", SqlDbType.NVarChar,300)};
            parameters[0].Value = model.categoryName;
            parameters[1].Value = model.lineCategory;
            parameters[2].Value = model.imgUrl;

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
        public bool Update(Trip.JinJiang.H5.Model.lineCategoryMod model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tbl_lineCategory set ");
            strSql.Append("categoryName=@categoryName, ");
            strSql.Append("lineCategory=@lineCategory, ");
            strSql.Append("imgUrl=@imgUrl");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@categoryName", SqlDbType.NVarChar,50),
                    new SqlParameter("@lineCategory", SqlDbType.NVarChar,50),
                    new SqlParameter("@imgUrl", SqlDbType.NVarChar,200),
                    new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.categoryName;
            parameters[1].Value = model.lineCategory;
            parameters[2].Value = model.imgUrl;
            parameters[3].Value = model.Id;

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
        /// 更新数据状态
        /// </summary>
        public bool ChangeStatus(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tbl_lineCategory set ");
            strSql.Append("status=status ");
            strSql.Append(" where Id=@Id ");
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
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tbl_lineCategory ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)           };
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
        public bool DeleteList(string lineCodelist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tbl_lineCategory ");
            strSql.Append(" where lineCode in (" + lineCodelist + ")  ");
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


        ///// <summary>
        ///// 得到一个对象实体
        ///// </summary>
        //public Trip.JinJiang.H5.Model.lineCategoryMod GetModel(string lineCode)
        //{

        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select  top 1 Id,lineCategory,categoryName from tbl_lineCategory ");
        //    strSql.Append(" where lineCode=@lineCode ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@lineCode", SqlDbType.NVarChar,50)            };
        //    parameters[0].Value = lineCode;

        //    Trip.JinJiang.H5.Model.lineCategoryMod model = new Trip.JinJiang.H5.Model.lineCategoryMod();
        //    DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        return DataRowToModel(ds.Tables[0].Rows[0]);
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}


        ///// <summary>
        ///// 得到一个对象实体
        ///// </summary>
        //public Trip.JinJiang.H5.Model.lineCategoryMod DataRowToModel(DataRow row)
        //{
        //    Trip.JinJiang.H5.Model.lineCategoryMod model = new Trip.JinJiang.H5.Model.lineCategoryMod();
        //    if (row != null)
        //    {
        //        if (row["lineCode"] != null)
        //        {
        //            model.lineCode = row["lineCode"].ToString();
        //        }
        //        if (row["lineCategory"] != null)
        //        {
        //            model.lineCategory = row["lineCategory"].ToString();
        //        }
        //    }
        //    return model;
        //}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,lineCategory,categoryName,imgUrl,status ");
            strSql.Append(" FROM tbl_lineCategory ");
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
            strSql.Append(" Id,lineCategory,categoryName,imgUrl,status ");
            strSql.Append(" FROM tbl_lineCategory ");
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
            strSql.Append("select count(1) FROM tbl_lineCategory ");
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
                strSql.Append("order by T.lineCode desc");
            }
            strSql.Append(")AS Row, T.*  from tbl_lineCategory T ");
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
			parameters[0].Value = "tbl_lineCategory";
			parameters[1].Value = "lineCode";
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

