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
                HttpContext.Current.Response.Write("接口出错!");
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

            HttpContext.Current.Response.Write(Admin.addrecommend(imgUrl, lineTitle, order, lineId, agency));
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