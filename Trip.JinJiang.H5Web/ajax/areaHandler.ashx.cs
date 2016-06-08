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
            HttpContext.Current.Response.Write(Admin.getarealist());
        }
        /// <summary>
        /// 目的地标签添加
        /// </summary>
        public void addarea()
        {
            string areaName = HttpContext.Current.Request["areaName"].ToString();
            int order = Convert.ToInt32(HttpContext.Current.Request["order"]);
            HttpContext.Current.Response.Write(Admin.addarea(areaName, order));
        }
        /// <summary>
        /// 目的地标签编辑
        /// </summary>
        public void editarea()
        {
            string areaName = HttpContext.Current.Request["areaName"].ToString();
            int order = Convert.ToInt32(HttpContext.Current.Request["order"]);
            int Id = Convert.ToInt32(HttpContext.Current.Request["Id"]);
            HttpContext.Current.Response.Write(Admin.editarea(areaName, order, Id));
        }
        /// <summary>
        /// 目的地标签管理禁用
        /// </summary>
        public void enarea()
        {
            int Id = Convert.ToInt32(HttpContext.Current.Request["Id"]);
            HttpContext.Current.Response.Write(Admin.enarea(Id));
        }

        //----二级标签----------------------------------------------------------------------------------
        /// <summary>
        /// 获取标签列表
        /// </summary>
        public void getarea2list()
        {
            int aId = Convert.ToInt32(HttpContext.Current.Request["aId"]);
            HttpContext.Current.Response.Write(Admin.getarea2list(aId));
        }
        /// <summary>
        /// 目的地标签添加
        /// </summary>
        public void addarea2()
        {
            int aId = Convert.ToInt32(HttpContext.Current.Request["aId"]);
            string destName = HttpContext.Current.Request["destName"].ToString();
            int order = Convert.ToInt32(HttpContext.Current.Request["order"]);
            string H5Url = HttpContext.Current.Request["H5Url"].ToString();
            HttpContext.Current.Response.Write(Admin.addarea2(aId, destName, order,H5Url));
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
            string H5Url = HttpContext.Current.Request["H5Url"].ToString();
            HttpContext.Current.Response.Write(Admin.editarea2(aId, destName, order, Id, H5Url));
        }
        /// <summary>
        /// 目的地标签管理禁用
        /// </summary>
        public void enarea2()
        {
            int Id = Convert.ToInt32(HttpContext.Current.Request["Id"]);
            HttpContext.Current.Response.Write(Admin.enarea2(Id));
        }

        //----空搜----------------------------------------------------------------------------------
        /// <summary>
        /// 获取空搜关键词列表
        /// </summary>
        public void getarea3list()
        {
            HttpContext.Current.Response.Write(Admin.getarea3list());
        }
        /// <summary>
        /// 空搜关键词添加
        /// </summary>
        public void addarea3()
        {
            string searchName = HttpContext.Current.Request["searchName"].ToString(); 
            string H5Url = HttpContext.Current.Request["H5Url"].ToString();
            HttpContext.Current.Response.Write(Admin.addarea3(searchName, H5Url));
        }
        /// <summary>
        /// 空搜关键词编辑
        /// </summary>
        public void editarea3()
        {
            string searchName = HttpContext.Current.Request["searchName"].ToString();
            int Id = Convert.ToInt32(HttpContext.Current.Request["Id"]); 
            string H5Url = HttpContext.Current.Request["H5Url"].ToString();
            HttpContext.Current.Response.Write(Admin.editarea3(searchName, Id, H5Url));
        }
        /// <summary>
        /// 空搜关键词禁用
        /// </summary>
        public void enarea3()
        {
            int Id = Convert.ToInt32(HttpContext.Current.Request["Id"]);
            HttpContext.Current.Response.Write(Admin.enarea3(Id));
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

