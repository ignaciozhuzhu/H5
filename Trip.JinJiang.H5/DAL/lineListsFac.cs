using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
//using LW.Common.DB;
namespace Trip.JinJiang.H5.DAL
{
    /// <summary>
    /// 数据访问类:lineListsFac
    /// </summary>
    public partial class lineListsFac
    {
        static string conn = System.Configuration.ConfigurationSettings.AppSettings["conn"];
        public lineListsFac()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("lineId", "tbl_lineLists");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int lineId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from tbl_lineLists");
            strSql.Append(" where lineId=@lineId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@lineId", SqlDbType.Int,4)            };
            parameters[0].Value = lineId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Trip.JinJiang.H5.Line model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tbl_lineLists(");
            strSql.Append("lineId,lineName,name,title,destinationInfo,spotInfo,lineSubjectInfo,destination,spot,lineSubject,manualRecommend,imageNo,brand,agency,days,isPromotion,departureMonth,priceScope,point,imageUrls,lineCategory,minPrice,productCode,priceScopeAndLine,lineDistrict,originalPrice,priceExplain,recommend)");
            strSql.Append(" values (");
            strSql.Append("@lineId,@lineName,@name,@title,@destinationInfo,@spotInfo,@lineSubjectInfo,@destination,@spot,@lineSubject,@manualRecommend,@imageNo,@brand,@agency,@days,@isPromotion,@departureMonth,@priceScope,@point,@imageUrls,@lineCategory,@minPrice,@productCode,@priceScopeAndLine,@lineDistrict,@originalPrice,@priceExplain,@recommend)");
            SqlParameter[] parameters = {
                    new SqlParameter("@lineId", SqlDbType.Int,4),
                    new SqlParameter("@lineName", SqlDbType.NVarChar,100),
                    new SqlParameter("@name", SqlDbType.NVarChar,100),
                    new SqlParameter("@title", SqlDbType.NVarChar,100),
                    new SqlParameter("@destinationInfo", SqlDbType.NVarChar,100),
                    new SqlParameter("@spotInfo", SqlDbType.NVarChar,100),
                    new SqlParameter("@lineSubjectInfo", SqlDbType.NVarChar,100),
                    new SqlParameter("@destination", SqlDbType.NVarChar,-1),
                    new SqlParameter("@spot", SqlDbType.NVarChar,100),
                    new SqlParameter("@lineSubject", SqlDbType.NVarChar,-1),
                    new SqlParameter("@manualRecommend", SqlDbType.Int,4),
                    new SqlParameter("@imageNo", SqlDbType.Int,4),
                    new SqlParameter("@brand", SqlDbType.NVarChar,100),
                    new SqlParameter("@agency", SqlDbType.NVarChar,100),
                    new SqlParameter("@days", SqlDbType.Int,4),
                    new SqlParameter("@isPromotion", SqlDbType.Int,4),
                    new SqlParameter("@departureMonth", SqlDbType.NVarChar,100),
                    new SqlParameter("@priceScope", SqlDbType.NVarChar,100),
                    new SqlParameter("@point", SqlDbType.Decimal,9),
                    new SqlParameter("@imageUrls", SqlDbType.NVarChar,-1),
                    new SqlParameter("@lineCategory", SqlDbType.NVarChar,100),
                    new SqlParameter("@minPrice", SqlDbType.Decimal,9),
                    new SqlParameter("@productCode", SqlDbType.NVarChar,100),
                    new SqlParameter("@priceScopeAndLine", SqlDbType.NVarChar,-1),
                    new SqlParameter("@lineDistrict", SqlDbType.NVarChar,100),
                    new SqlParameter("@originalPrice", SqlDbType.Decimal,9),
                    new SqlParameter("@priceExplain", SqlDbType.NVarChar,-1),
                    new SqlParameter("@recommend", SqlDbType.NVarChar,-1)};
            parameters[0].Value = model.lineId;
            parameters[1].Value = model.lineName;
            parameters[2].Value = model.name;
            parameters[3].Value = model.title;
            parameters[4].Value = model.destinationInfo;
            parameters[5].Value = model.spotInfo;
            parameters[6].Value = model.lineSubjectInfo;
            parameters[7].Value = arr2string(model.destination);
            parameters[8].Value = arr2string(model.spot);
            parameters[9].Value = arr2string(model.lineSubject);
            parameters[10].Value = model.manualRecommend;
            parameters[11].Value = model.imageNo;
            parameters[12].Value = model.brand;
            parameters[13].Value = model.agency;
            parameters[14].Value = model.days;
            parameters[15].Value = model.isPromotion;
            parameters[16].Value = arri2string(model.departureMonth);
            parameters[17].Value = arri2string(model.priceScope);
            parameters[18].Value = model.point;
            parameters[19].Value = arr2string(model.imageUrls);
            parameters[20].Value = model.lineCategory;
            parameters[21].Value = model.minPrice;
            parameters[22].Value = model.productCode;
            parameters[23].Value = arr2string(model.priceScopeAndLine);
            parameters[24].Value = model.lineDistrict;
            parameters[25].Value = model.originalPrice;
            parameters[26].Value = model.priceExplain;
            parameters[27].Value = model.recommend;

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
        private static string arr2string(string[] arr)
        {
            string str = "";
            for (var i = 0; i < arr.Length; i++)
            {
                str += arr[i] + "|";
            }
            return str;
        }
        private static string arri2string(int[] arr)
        {
            string str = "";
            for (var i = 0; i < arr.Length; i++)
            {
                str += arr[i] + "|";
            }
            return str;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Trip.JinJiang.H5.Line model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tbl_lineLists set ");
            strSql.Append("lineName=@lineName,");
            strSql.Append("name=@name,");
            strSql.Append("title=@title,");
            strSql.Append("destinationInfo=@destinationInfo,");
            strSql.Append("spotInfo=@spotInfo,");
            strSql.Append("lineSubjectInfo=@lineSubjectInfo,");
            strSql.Append("destination=@destination,");
            strSql.Append("spot=@spot,");
            strSql.Append("lineSubject=@lineSubject,");
            strSql.Append("manualRecommend=@manualRecommend,");
            strSql.Append("imageNo=@imageNo,");
            strSql.Append("brand=@brand,");
            strSql.Append("agency=@agency,");
            strSql.Append("days=@days,");
            strSql.Append("isPromotion=@isPromotion,");
            strSql.Append("departureMonth=@departureMonth,");
            strSql.Append("priceScope=@priceScope,");
            strSql.Append("point=@point,");
            strSql.Append("imageUrls=@imageUrls,");
            strSql.Append("lineCategory=@lineCategory,");
            strSql.Append("minPrice=@minPrice,");
            strSql.Append("productCode=@productCode,");
            strSql.Append("priceScopeAndLine=@priceScopeAndLine,");
            strSql.Append("lineDistrict=@lineDistrict,");
            strSql.Append("originalPrice=@originalPrice,");
            strSql.Append("priceExplain=@priceExplain,");
            strSql.Append("recommend=@recommend");
            strSql.Append(" where lineId=@lineId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@lineName", SqlDbType.NVarChar,100),
                    new SqlParameter("@name", SqlDbType.NVarChar,100),
                    new SqlParameter("@title", SqlDbType.NVarChar,100),
                    new SqlParameter("@destinationInfo", SqlDbType.NVarChar,100),
                    new SqlParameter("@spotInfo", SqlDbType.NVarChar,100),
                    new SqlParameter("@lineSubjectInfo", SqlDbType.NVarChar,100),
                    new SqlParameter("@destination", SqlDbType.NVarChar,-1),
                    new SqlParameter("@spot", SqlDbType.NVarChar,100),
                    new SqlParameter("@lineSubject", SqlDbType.NVarChar,-1),
                    new SqlParameter("@manualRecommend", SqlDbType.Int,4),
                    new SqlParameter("@imageNo", SqlDbType.Int,4),
                    new SqlParameter("@brand", SqlDbType.NVarChar,100),
                    new SqlParameter("@agency", SqlDbType.NVarChar,100),
                    new SqlParameter("@days", SqlDbType.Int,4),
                    new SqlParameter("@isPromotion", SqlDbType.Int,4),
                    new SqlParameter("@departureMonth", SqlDbType.NVarChar,100),
                    new SqlParameter("@priceScope", SqlDbType.NVarChar,100),
                    new SqlParameter("@point", SqlDbType.Decimal,9),
                    new SqlParameter("@imageUrls", SqlDbType.NVarChar,-1),
                    new SqlParameter("@lineCategory", SqlDbType.NVarChar,100),
                    new SqlParameter("@minPrice", SqlDbType.Decimal,9),
                    new SqlParameter("@productCode", SqlDbType.NVarChar,100),
                    new SqlParameter("@priceScopeAndLine", SqlDbType.NVarChar,-1),
                    new SqlParameter("@lineDistrict", SqlDbType.NVarChar,100),
                    new SqlParameter("@originalPrice", SqlDbType.Decimal,9),
                    new SqlParameter("@priceExplain", SqlDbType.NVarChar,-1),
                    new SqlParameter("@recommend", SqlDbType.NVarChar,-1),
                    new SqlParameter("@lineId", SqlDbType.Int,4)};
            parameters[0].Value = model.lineName;
            parameters[1].Value = model.name;
            parameters[2].Value = model.title;
            parameters[3].Value = model.destinationInfo;
            parameters[4].Value = model.spotInfo;
            parameters[5].Value = model.lineSubjectInfo;
            parameters[6].Value = model.destination;
            parameters[7].Value = model.spot;
            parameters[8].Value = model.lineSubject;
            parameters[9].Value = model.manualRecommend;
            parameters[10].Value = model.imageNo;
            parameters[11].Value = model.brand;
            parameters[12].Value = model.agency;
            parameters[13].Value = model.days;
            parameters[14].Value = model.isPromotion;
            parameters[15].Value = model.departureMonth;
            parameters[16].Value = model.priceScope;
            parameters[17].Value = model.point;
            parameters[18].Value = model.imageUrls;
            parameters[19].Value = model.lineCategory;
            parameters[20].Value = model.minPrice;
            parameters[21].Value = model.productCode;
            parameters[22].Value = model.priceScopeAndLine;
            parameters[23].Value = model.lineDistrict;
            parameters[24].Value = model.originalPrice;
            parameters[25].Value = model.priceExplain;
            parameters[26].Value = model.recommend;
            parameters[27].Value = model.lineId;

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
        /// 仅更新一条数据的线路类型
        /// </summary>
        public bool UpdatelineCategory(Trip.JinJiang.H5.Line model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tbl_lineLists set ");
            strSql.Append("lineCategory=@lineCategory");
            strSql.Append(" where lineId=@lineId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@lineCategory", SqlDbType.NVarChar,100),
                    new SqlParameter("@lineId", SqlDbType.Int,4)};
            parameters[0].Value = model.lineCategory;
            parameters[1].Value = model.lineId;

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
        public bool Delete(int lineId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tbl_lineLists ");
            strSql.Append(" where lineId=@lineId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@lineId", SqlDbType.Int,4)            };
            parameters[0].Value = lineId;

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
        public bool DeleteList(string lineIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tbl_lineLists ");
            strSql.Append(" where lineId in (" + lineIdlist + ")  ");
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
        //public Trip.JinJiang.H5.Line GetModel(int lineId)
        //{

        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select  top 1 lineId,lineName,name,title,destinationInfo,spotInfo,lineSubjectInfo,destination,spot,lineSubject,manualRecommend,imageNo,brand,agency,days,isPromotion,departureMonth,priceScope,point,imageUrls,lineCategory,minPrice,productCode,priceScopeAndLine,lineDistrict,originalPrice,priceExplain,recommend from tbl_lineLists ");
        //    strSql.Append(" where lineId=@lineId ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@lineId", SqlDbType.Int,4)            };
        //    parameters[0].Value = lineId;

        //    Trip.JinJiang.H5.Line model = new Trip.JinJiang.H5.Line();
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
        //public Trip.JinJiang.H5.Line DataRowToModel(DataRow row)
        //{
        //    Trip.JinJiang.H5.Line model = new Trip.JinJiang.H5.Line();
        //    if (row != null)
        //    {
        //        if (row["lineId"] != null && row["lineId"].ToString() != "")
        //        {
        //            model.lineId = int.Parse(row["lineId"].ToString());
        //        }
        //        if (row["lineName"] != null)
        //        {
        //            model.lineName = row["lineName"].ToString();
        //        }
        //        if (row["name"] != null)
        //        {
        //            model.name = row["name"].ToString();
        //        }
        //        if (row["title"] != null)
        //        {
        //            model.title = row["title"].ToString();
        //        }
        //        if (row["destinationInfo"] != null)
        //        {
        //            model.destinationInfo = row["destinationInfo"].ToString();
        //        }
        //        if (row["spotInfo"] != null)
        //        {
        //            model.spotInfo = row["spotInfo"].ToString();
        //        }
        //        if (row["lineSubjectInfo"] != null)
        //        {
        //            model.lineSubjectInfo = row["lineSubjectInfo"].ToString();
        //        }
        //        if (row["destination"] != null)
        //        {
        //            model.destination = row["destination"].ToString();
        //        }
        //        if (row["spot"] != null)
        //        {
        //            model.spot = row["spot"].ToString();
        //        }
        //        if (row["lineSubject"] != null)
        //        {
        //            model.lineSubject = row["lineSubject"].ToString();
        //        }
        //        if (row["manualRecommend"] != null && row["manualRecommend"].ToString() != "")
        //        {
        //            model.manualRecommend = int.Parse(row["manualRecommend"].ToString());
        //        }
        //        if (row["imageNo"] != null && row["imageNo"].ToString() != "")
        //        {
        //            model.imageNo = int.Parse(row["imageNo"].ToString());
        //        }
        //        if (row["brand"] != null)
        //        {
        //            model.brand = row["brand"].ToString();
        //        }
        //        if (row["agency"] != null)
        //        {
        //            model.agency = row["agency"].ToString();
        //        }
        //        if (row["days"] != null && row["days"].ToString() != "")
        //        {
        //            model.days = int.Parse(row["days"].ToString());
        //        }
        //        if (row["isPromotion"] != null && row["isPromotion"].ToString() != "")
        //        {
        //            model.isPromotion = int.Parse(row["isPromotion"].ToString());
        //        }
        //        if (row["departureMonth"] != null)
        //        {
        //            model.departureMonth = row["departureMonth"].ToString();
        //        }
        //        if (row["priceScope"] != null)
        //        {
        //            model.priceScope = row["priceScope"].ToString();
        //        }
        //        if (row["point"] != null && row["point"].ToString() != "")
        //        {
        //            model.point = decimal.Parse(row["point"].ToString());
        //        }
        //        if (row["imageUrls"] != null)
        //        {
        //            model.imageUrls = row["imageUrls"].ToString();
        //        }
        //        if (row["lineCategory"] != null)
        //        {
        //            model.lineCategory = row["lineCategory"].ToString();
        //        }
        //        if (row["minPrice"] != null && row["minPrice"].ToString() != "")
        //        {
        //            model.minPrice = decimal.Parse(row["minPrice"].ToString());
        //        }
        //        if (row["productCode"] != null)
        //        {
        //            model.productCode = row["productCode"].ToString();
        //        }
        //        if (row["priceScopeAndLine"] != null)
        //        {
        //            model.priceScopeAndLine = row["priceScopeAndLine"].ToString();
        //        }
        //        if (row["lineDistrict"] != null)
        //        {
        //            model.lineDistrict = row["lineDistrict"].ToString();
        //        }
        //        if (row["originalPrice"] != null && row["originalPrice"].ToString() != "")
        //        {
        //            model.originalPrice = decimal.Parse(row["originalPrice"].ToString());
        //        }
        //        if (row["priceExplain"] != null)
        //        {
        //            model.priceExplain = row["priceExplain"].ToString();
        //        }
        //        if (row["recommend"] != null)
        //        {
        //            model.recommend = row["recommend"].ToString();
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
            strSql.Append("select lineId,lineName,name,title,destinationInfo,spotInfo,lineSubjectInfo,destination,spot,lineSubject,manualRecommend,imageNo,brand,agency,days,isPromotion,departureMonth,priceScope,point,imageUrls,b.categoryName as lineCategory,minPrice,productCode,priceScopeAndLine,lineDistrict,originalPrice,priceExplain,recommend ");
            strSql.Append(" FROM tbl_lineLists a left join tbl_lineCategory b on a.lineCategory=b.lineCategory");
            if (strWhere != null && strWhere.Trim() != "")
            {
                strWhere = " b.categoryName='" + strWhere + "'";
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得数据列表(带筛选)
        /// </summary>
        public DataSet GetListsearch(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select lineId,lineName,name,title,destinationInfo,spotInfo,lineSubjectInfo,destination,spot,lineSubject,manualRecommend,imageNo,brand,agency,days,isPromotion,departureMonth,priceScope,point,imageUrls,b.categoryName as lineCategory,minPrice,productCode,priceScopeAndLine,lineDistrict,originalPrice,priceExplain,recommend ");
            strSql.Append(" FROM tbl_lineLists a left join tbl_lineCategory b on a.lineCategory=b.lineCategory");
            if (strWhere != null && strWhere.Trim() != "")
            {
                int lineid = 0;
                try
                {
                    lineid = Convert.ToInt32(strWhere);
                }
                catch { }
                strWhere = " b.categoryName like '%" + strWhere + "%' or a.lineName like '%" + strWhere + "%' or a.lineId = '" + lineid + "' or a.name like '%" + strWhere + "%'";
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListFront4(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select lineId,lineName,name,title,destinationInfo,spotInfo,lineSubjectInfo,destination,spot,lineSubject,manualRecommend,imageNo,brand,agency,days,isPromotion,departureMonth,priceScope,point,imageUrls,b.categoryName as lineCategory,minPrice,productCode,priceScopeAndLine,lineDistrict,originalPrice,priceExplain,recommend,b.pattern ");
            strSql.Append(" FROM tbl_lineLists a left join tbl_lineCategory b on a.lineCategory=b.lineCategory");
            if (strWhere != null && strWhere.Trim() != "")
            {
                strWhere = " b.pattern='" + strWhere + "'";
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
            strSql.Append(" lineId,lineName,name,title,destinationInfo,spotInfo,lineSubjectInfo,destination,spot,lineSubject,manualRecommend,imageNo,brand,agency,days,isPromotion,departureMonth,priceScope,point,imageUrls,lineCategory,minPrice,productCode,priceScopeAndLine,lineDistrict,originalPrice,priceExplain,recommend ");
            strSql.Append(" FROM tbl_lineLists ");
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
            strSql.Append("select count(1) FROM tbl_lineLists ");
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
                strSql.Append("order by T.lineId desc");
            }
            strSql.Append(")AS Row, T.*  from tbl_lineLists T ");
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
			parameters[0].Value = "tbl_lineLists";
			parameters[1].Value = "lineId";
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

