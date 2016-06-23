using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.SessionState;
using Trip.JinJiang.H5;

namespace Trip.JinJiang.H5Web.ajax
{
    /// <summary>
    /// recommendHandler 的摘要说明
    /// </summary>
    public class recommendHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                Type type = this.GetType();
                string fn = context.Request["fn"].ToString();
                MethodInfo method = type.GetMethod(fn, BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
                method.Invoke(this, null);
            }
            catch (Exception e)
            {
                HttpContext.Current.Response.Write(e.InnerException.Message);
            }
        }
        private void ResponseWriteEnd(HttpContext context, string msg)
        {
            context.Response.Write(msg);
            context.Response.End();
        }
        /// <summary>
        /// 推荐/修改旅游营销产品
        /// </summary>
        public void addrecommend()
        {
            string fileName = commonHandler.uploadimg();
            string imgUrl = "../../../modules/img/" + fileName;
            string lineTitle = HttpContext.Current.Request["lineTitle"].ToString();
            int order = Convert.ToInt32(HttpContext.Current.Request["order"]);
            int lineId = Convert.ToInt32(HttpContext.Current.Request["lineId"]);
            string agency = HttpContext.Current.Request["travelAgency"].ToString();
            string lineCategory = HttpContext.Current.Request["lineCategory"].ToString();

            HttpContext.Current.Response.Write(Admin.addrecommend(imgUrl, lineTitle, order, lineId, agency, lineCategory));
        }
        /// <summary>
        /// 推荐产品删除
        /// </summary>
        public void delrecommend()
        {
            int Id = Convert.ToInt32(HttpContext.Current.Request["Id"]);
            HttpContext.Current.Response.Write(Admin.delrecommend(Id));
        }

        /// <summary>
        /// 推荐产品编辑
        /// </summary>
        public void editrecommend()
        {
            string fileName = commonHandler.uploadimg();
            string imgUrl = "";
            string path = HttpContext.Current.Request["path"];
            if (path.IndexOf("/modules/img/") > -1)
            {
                imgUrl = "../../../modules/img" + path.Substring(path.LastIndexOf("/"));
            }
            else {
                imgUrl = "../../../modules/img/" + fileName;
            }
            string lineTitle = HttpContext.Current.Request["lineTitle"].ToString();
            int Id = Convert.ToInt32(HttpContext.Current.Request["Id"]);
            int order = Convert.ToInt32(HttpContext.Current.Request["order"]);
            HttpContext.Current.Response.Write(Admin.editrecommend(lineTitle, imgUrl, Id, order));
        }

        /// <summary>
        /// 获取
        /// </summary>
        public void getrecommendlist()
        {
            string search = "";
            try
            {
                search = HttpContext.Current.Request["search"];
            }
            catch { }
            HttpContext.Current.Response.Write(Admin.getrecommendlist(search));
        }
        /// <summary>
        /// 后台线路cms查询
        /// </summary>
        public void getlinescmssearch()
        {
            string json = HttpContext.Current.Request["json"];
            HttpContext.Current.Response.Write(Admin.getlinescmssearch(json));
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}