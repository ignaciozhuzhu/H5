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
    /// userHandler 的摘要说明
    /// </summary>
    public class userHandler : IHttpHandler, IRequiresSessionState
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
        /// 注册新用户
        /// </summary>
        public void regist()
        {
            string xml = HttpContext.Current.Request["xml"].ToString();
            HttpContext.Current.Response.Write(User.regist(xml));
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