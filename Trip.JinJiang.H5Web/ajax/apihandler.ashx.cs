using System;
using System.Reflection;
using System.Web;
using System.Web.SessionState;
using Trip.JinJiang.H5;

namespace Trip.JinJiang.H5Web.ajax
{
    /// <summary>
    /// apihandler 的摘要说明
    /// </summary>
    public class apihandler : IHttpHandler, IRequiresSessionState
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

        /// <summary>
        /// 获取线路列表(类型)
        /// </summary>
        /// <param:lineCategory-->GROUPTRIP("跟团游"), FREETRIP("自由行"), SHIP("邮轮");>
        public void getlines()
        {
            string lineCategory = "";
            try
            {
                lineCategory = (HttpContext.Current.Request["linecategory"]).ToString();
            }
            catch (Exception e)
            {
            }
            HttpContext.Current.Response.Write(JJH5Api.Find());
        }

        /// <summary>
        /// 获取线路列表(类型)
        /// </summary>
        /// <param:lineCategory-->GROUPTRIP("跟团游"), FREETRIP("自由行"), SHIP("邮轮");>
        public void getlinesbycategory()
        {
            string lineCategory = "";
            try
            {
                lineCategory = (HttpContext.Current.Request["linecategory"]).ToString();
            }
            catch (Exception e)
            {
            }
            HttpContext.Current.Response.Write(JJH5Api.getlinesByCategory(lineCategory));
        }

        /// <summary>
        /// 促销路线(暂时理解为热门路线)
        /// </summary>
        public void getlinespromotion()
        {
            try
            {
                HttpContext.Current.Response.Write(JJH5Api.FindPromotion());
            }
            catch (Exception e)
            {
            }
        }

        public void getlinecategoriecrm()
        {
            try
            {
                string category = (HttpContext.Current.Request["category"]).ToString();
                HttpContext.Current.Response.Write(JJH5Api.Getlinecategoriecrm(category));
            }
            catch (Exception e)
            {
            }
        }

        /// <summary>
        /// 线路详情
        /// </summary>
        public void getlinedetail()
        {
            try
            {
                int lineid = Convert.ToInt32(HttpContext.Current.Request["lineid"]);
                HttpContext.Current.Response.Write(JJH5Api.LineDetail(lineid));
            }
            catch (Exception e)
            {
            }
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        public void createorder()
        {
            try
            {
                string json = HttpContext.Current.Request["json"];
                HttpContext.Current.Response.Write(JJH5Api.CreateOrder(json));
            }
            catch (Exception e)
            {
            }
        }


        /// <summary>
        /// 查询库存 ,实时价格
        /// </summary>
        public void queryrealtimerefresh()
        {
            try
            {
                int groupid = Convert.ToInt32(HttpContext.Current.Request["groupid"]);
                HttpContext.Current.Response.Write(JJH5Api.queryRealTimeRefresh(groupid));
            }
            catch (Exception e)
            {
            }
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        public void cancelorder()
        {
            string orderCode= HttpContext.Current.Request["ordercode"];
            try
            {
                HttpContext.Current.Response.Write(JJH5Api.cancelOrder(orderCode));
            }
            catch (Exception e)
            {
            }
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