using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace Trip.JinJiang.H5.DAL
{
    /// <summary>
    /// 数据访问类:tbl_myCollect
    /// </summary>
    public partial class myCollectFac
    {
        public myCollectFac()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from tbl_myCollect");
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
        public int Add(Trip.JinJiang.H5.Model.myCollectMod model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tbl_myCollect(");
            strSql.Append("title,description,price,date,imgurl,userMID,lineID)");
            strSql.Append(" values (");
            strSql.Append("@title,@description,@price,@date,@imgurl,@userMID,@lineID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@title", SqlDbType.NVarChar,50),
                    new SqlParameter("@description", SqlDbType.NVarChar,200),
                    new SqlParameter("@price", SqlDbType.Int,4),
                    new SqlParameter("@date", SqlDbType.Int,4),
                    new SqlParameter("@imgurl", SqlDbType.NVarChar,300),
                    new SqlParameter("@userMID", SqlDbType.Int,4),
                    new SqlParameter("@lineID", SqlDbType.Int,4)};
            parameters[0].Value = model.title;
            parameters[1].Value = model.description;
            parameters[2].Value = model.price;
            parameters[3].Value = model.date;
            parameters[4].Value = model.imgurl;
            parameters[5].Value = model.userMID;
            parameters[6].Value = model.lineID;

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
        /// 更新一条数据
        /// </summary>
        public bool Update(Trip.JinJiang.H5.Model.myCollectMod model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tbl_myCollect set ");
            strSql.Append("title=@title,");
            strSql.Append("description=@description,");
            strSql.Append("price=@price,");
            strSql.Append("date=@date,");
            strSql.Append("imgurl=@imgurl,");
            strSql.Append("userMID=@userMID,");
            strSql.Append("lineID=@lineID");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@title", SqlDbType.NVarChar,50),
                    new SqlParameter("@description", SqlDbType.NVarChar,200),
                    new SqlParameter("@price", SqlDbType.Int,4),
                    new SqlParameter("@date", SqlDbType.Int,4),
                    new SqlParameter("@imgurl", SqlDbType.NVarChar,300),
                    new SqlParameter("@userMID", SqlDbType.Int,4),
                    new SqlParameter("@lineID", SqlDbType.Int,4),
                    new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.title;
            parameters[1].Value = model.description;
            parameters[2].Value = model.price;
            parameters[3].Value = model.date;
            parameters[4].Value = model.imgurl;
            parameters[5].Value = model.userMID;
            parameters[6].Value = model.lineID;
            parameters[7].Value = model.Id;

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
        public bool Delete(int lineID, int userMID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tbl_myCollect ");
            strSql.Append(" where lineID=@lineID and userMID=@userMID");
            SqlParameter[] parameters = {
                    new SqlParameter("@lineID", SqlDbType.Int,4),
                    new SqlParameter("@userMID", SqlDbType.Int,4)
            };
            parameters[0].Value = lineID;
            parameters[1].Value = userMID;

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
        /// 获取该条线路是否已经被收藏
        /// </summary>
        public bool GetIfCollect(int lineID, int userMID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from tbl_myCollect ");
            strSql.Append(" where lineID=@lineID and userMID=@userMID");
            SqlParameter[] parameters = {
                    new SqlParameter("@lineID", SqlDbType.Int,4),
                    new SqlParameter("@userMID", SqlDbType.Int,4)
            };
            parameters[0].Value = lineID;
            parameters[1].Value = userMID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tbl_myCollect ");
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
        public Trip.JinJiang.H5.Model.myCollectMod GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,title,description,price,date,imgurl,userMID,lineID from tbl_myCollect ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;

            Trip.JinJiang.H5.Model.myCollectMod model = new Trip.JinJiang.H5.Model.myCollectMod();
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
        public Trip.JinJiang.H5.Model.myCollectMod DataRowToModel(DataRow row)
        {
            Trip.JinJiang.H5.Model.myCollectMod model = new Trip.JinJiang.H5.Model.myCollectMod();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = int.Parse(row["Id"].ToString());
                }
                if (row["title"] != null)
                {
                    model.title = row["title"].ToString();
                }
                if (row["description"] != null)
                {
                    model.description = row["description"].ToString();
                }
                if (row["price"] != null && row["price"].ToString() != "")
                {
                    model.price = int.Parse(row["price"].ToString());
                }
                if (row["date"] != null && row["date"].ToString() != "")
                {
                    model.date = int.Parse(row["date"].ToString());
                }
                if (row["imgurl"] != null)
                {
                    model.imgurl = row["imgurl"].ToString();
                }
                if (row["userMID"] != null && row["userMID"].ToString() != "")
                {
                    model.userMID = int.Parse(row["userMID"].ToString());
                }
                if (row["lineID"] != null && row["lineID"].ToString() != "")
                {
                    model.lineID = int.Parse(row["lineID"].ToString());
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
            strSql.Append("select Id,title,description,price,date,imgurl,userMID,lineID ");
            strSql.Append(" FROM tbl_myCollect ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where userMID = " + strWhere);
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
            strSql.Append(" Id,title,description,price,date,imgurl,userMID,lineID ");
            strSql.Append(" FROM tbl_myCollect ");
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
            strSql.Append("select count(1) FROM tbl_myCollect ");
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
            strSql.Append(")AS Row, T.*  from tbl_myCollect T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }


        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

