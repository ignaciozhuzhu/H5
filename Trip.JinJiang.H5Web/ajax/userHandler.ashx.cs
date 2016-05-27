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
        /// <summary>
        /// 登录
        /// </summary>
        public void login()
        {
            string xml = HttpContext.Current.Request["xml"].ToString();
            HttpContext.Current.Response.Write(User.login(xml));
        }
        /// <summary>
        /// 注册1(快速注册)
        /// </summary>
        public void quickregist()
        {
            string xml = HttpContext.Current.Request["xml"].ToString();
            HttpContext.Current.Response.Write(User.quickregist(xml));
        }
        //----------------------------------------------密码找回4个步骤 bg
        /// <summary>
        /// 忘记密码并设置新密码
        /// </summary>
        public void sendvalidatecode()
        {
            string loginName = HttpContext.Current.Request["loginName"].ToString();
            HttpContext.Current.Response.Write(User.sendvalidatecode(loginName));
        }
        public void checkmember()
        {
            string loginName = HttpContext.Current.Request["loginName"].ToString();
            HttpContext.Current.Response.Write(User.checkmember(loginName));
        }
        public void checkmemberfullname()
        {
            string loginName = HttpContext.Current.Request["loginName"].ToString();
            string fullName = HttpContext.Current.Request["fullName"].ToString();
            HttpContext.Current.Response.Write(User.checkmemberfullname(loginName, fullName));
        }
        public void forgetpwd()
        {
            string validateCode = HttpContext.Current.Request["validateCode"].ToString();
            string loginName = HttpContext.Current.Request["loginName"].ToString();
            string fullName = HttpContext.Current.Request["fullName"].ToString();
            string md5 = HttpContext.Current.Request["md5"].ToString();
            string sha1 = HttpContext.Current.Request["sha1"].ToString();
            HttpContext.Current.Response.Write(User.forgetpwd(loginName, fullName, validateCode, md5, sha1));
        }
        //----------------------------------------------密码找回4个步骤 ed


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}