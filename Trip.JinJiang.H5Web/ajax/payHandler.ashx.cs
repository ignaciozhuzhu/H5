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
    /// payHandler 的摘要说明
    /// </summary>
    public class payHandler : IHttpHandler, IRequiresSessionState
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
        /// 绑定银行卡
        /// </summary>
        public void bindcard()
        {
            string memberid = HttpContext.Current.Request["memberid"].ToString();
            string card = HttpContext.Current.Request["card"].ToString();
            HttpContext.Current.Response.Write(Pay.bindcard(memberid, card));
        }
        /// <summary>
        /// 银联发短信
        /// </summary>
        public void sms()
        {
            string memberid = HttpContext.Current.Request["memberid"].ToString();
            string orderNo = HttpContext.Current.Request["orderNo"].ToString();
            decimal price = Convert.ToDecimal(HttpContext.Current.Request["price"]);
            string txnTime = HttpContext.Current.Request["txnTime"].ToString();
            HttpContext.Current.Response.Write(Pay.sms(memberid, orderNo, price, txnTime));
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