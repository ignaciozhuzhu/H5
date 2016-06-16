using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace Trip.JinJiang.H5.DAL
{
    /// <summary>
    /// 数据访问类:recommendFac
    /// </summary>
    public partial class recommendFac
    {
        public recommendFac()
        { }
        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Trip.JinJiang.H5.Model.recommendMod model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tbl_recommend(");
            strSql.Append("lineId,lineTitle,travelAgency,order,imgUrl)");
            strSql.Append(" values (");
            strSql.Append("@lineId,@lineTitle,@travelAgency,@order,@imgUrl)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@lineId", SqlDbType.Int,4),
                    new SqlParameter("@lineTitle", SqlDbType.NVarChar,50),
                    new SqlParameter("@travelAgency", SqlDbType.NVarChar,50),
                    new SqlParameter("@order", SqlDbType.Int,4),
                    new SqlParameter("@imgUrl", SqlDbType.NVarChar,200)};
            parameters[0].Value = model.lineId;
            parameters[1].Value = model.lineTitle;
            parameters[2].Value = model.travelAgency;
            parameters[3].Value = model.order;
            parameters[4].Value = model.imgUrl;

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
        public bool Update(Trip.JinJiang.H5.Model.recommendMod model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tbl_recommend set ");
            strSql.Append("lineId=@lineId,");
            strSql.Append("lineTitle=@lineTitle,");
            strSql.Append("travelAgency=@travelAgency,");
            strSql.Append("order=@order,");
            strSql.Append("imgUrl=@imgUrl");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@lineId", SqlDbType.Int,4),
                    new SqlParameter("@lineTitle", SqlDbType.NVarChar,50),
                    new SqlParameter("@travelAgency", SqlDbType.NVarChar,50),
                    new SqlParameter("@order", SqlDbType.Int,4),
                    new SqlParameter("@imgUrl", SqlDbType.NVarChar,200),
                    new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.lineId;
            parameters[1].Value = model.lineTitle;
            parameters[2].Value = model.travelAgency;
            parameters[3].Value = model.order;
            parameters[4].Value = model.imgUrl;
            parameters[5].Value = model.Id;

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
            strSql.Append("delete from tbl_recommend ");
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
            strSql.Append("delete from tbl_recommend ");
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

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

