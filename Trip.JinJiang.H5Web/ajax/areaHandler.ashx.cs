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
    /// areaHandler 的摘要说明
    /// </summary>
    public class areaHandler : IHttpHandler, IRequiresSessionState
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
        /// 获取标签列表
        /// </summary>
        public void getarealist()
        {
            HttpContext.Current.Response.Write(JJH5Api.getarealist());
        }
        /// <summary>
        /// 目的地标签添加
        /// </summary>
        public void addarea()
        {
            string areaName = HttpContext.Current.Request["areaName"].ToString();
            int order = Convert.ToInt32(HttpContext.Current.Request["order"]);
            HttpContext.Current.Response.Write(JJH5Api.addarea(areaName, order));
        }
        /// <summary>
        /// 目的地标签编辑
        /// </summary>
        public void editarea()
        {
            string areaName = HttpContext.Current.Request["areaName"].ToString();
            int order = Convert.ToInt32(HttpContext.Current.Request["order"]);
            int Id = Convert.ToInt32(HttpContext.Current.Request["Id"]);
            HttpContext.Current.Response.Write(JJH5Api.editarea(areaName, order, Id));
        }
        /// <summary>
        /// 目的地标签管理禁用
        /// </summary>
        public void enarea()
        {
            int Id = Convert.ToInt32(HttpContext.Current.Request["Id"]);
            HttpContext.Current.Response.Write(JJH5Api.enarea(Id));
        }

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// 获取标签列表
        /// </summary>
        public void getarea2list()
        {
            int aId = Convert.ToInt32(HttpContext.Current.Request["aId"]);
            HttpContext.Current.Response.Write(JJH5Api.getarea2list(aId));
        }
        /// <summary>
        /// 目的地标签添加
        /// </summary>
        public void addarea2()
        {
            int aId = Convert.ToInt32(HttpContext.Current.Request["aId"]);
            string destName = HttpContext.Current.Request["destName"].ToString();
            int order = Convert.ToInt32(HttpContext.Current.Request["order"]);
            HttpContext.Current.Response.Write(JJH5Api.addarea2(aId, destName, order));
        }
        /// <summary>
        /// 目的地标签编辑
        /// </summary>
        public void editarea2()
        {
            int aId = Convert.ToInt32(HttpContext.Current.Request["aId"]);
            string destName = HttpContext.Current.Request["destName"].ToString();
            int order = Convert.ToInt32(HttpContext.Current.Request["order"]);
            int Id = Convert.ToInt32(HttpContext.Current.Request["Id"]);
            HttpContext.Current.Response.Write(JJH5Api.editarea2(aId, destName, order, Id));
        }
        /// <summary>
        /// 目的地标签管理禁用
        /// </summary>
        public void enarea2()
        {
            int Id = Convert.ToInt32(HttpContext.Current.Request["Id"]);
            HttpContext.Current.Response.Write(JJH5Api.enarea2(Id));
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

