﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.SessionState;
using Trip.JinJiang.H5;

namespace Trip.JinJiang.H5Web.ajax
{
    /// <summary>
    /// lineCategoryHandler 的摘要说明
    /// </summary>
    public class lineCategoryHandler : IHttpHandler, IRequiresSessionState
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
        /// 添加线路分类
        /// </summary>
        public void addcategory()
        {
            string fileName = commonHandler.uploadimg();
            // if (fileName == "0")
            //  {
            //ResponseWriteEnd(HttpContext.Current, "4");//请选择要上传的文件  
            //  }
            //  else
            // {
            string categoryName = HttpContext.Current.Request["categoryName"].ToString();
            string lineCategory = HttpContext.Current.Request["lineCategory"].ToString();
            string imgUrl = "../../../modules/img/" + fileName;
            string pattern = HttpContext.Current.Request["pattern"].ToString();
            int order = Convert.ToInt32(HttpContext.Current.Request["order"]);
            HttpContext.Current.Response.Write(Admin.addlinecategory(categoryName, lineCategory, imgUrl, pattern, order));
            //   }
        }

        /// <summary>
        /// 线路类型编辑
        /// </summary>
        public void editlinecategory()
        {
            string fileName = commonHandler.uploadimg();
            //if (fileName == "0")
            //{
            //    ResponseWriteEnd(HttpContext.Current, "4");//请选择要上传的文件  
            //}
            //else
            //{
            string imgUrl = "";
            string path = HttpContext.Current.Request["path"];
            if (path.IndexOf("/modules/img/") > -1)
            {
                imgUrl = "../../../modules/img" + path.Substring(path.LastIndexOf("/"));
            }
            else {
                imgUrl = "../../../modules/img/" + fileName;
            }
            string categoryName = HttpContext.Current.Request["categoryName"].ToString();
            string lineCategory = HttpContext.Current.Request["lineCategory"].ToString();
            int Id = Convert.ToInt32(HttpContext.Current.Request["Id"]);
            int order = Convert.ToInt32(HttpContext.Current.Request["order"]);
            string pattern = HttpContext.Current.Request["pattern"].ToString();
            HttpContext.Current.Response.Write(Admin.editlinecategory(categoryName, lineCategory, imgUrl, Id, order, pattern));
            //}
        }
        /// <summary>
        /// 获得所有样式
        /// </summary>
        public void getpatterns()
        {
            HttpContext.Current.Response.Write(Admin.getpatterns());
        }

        /// <summary>
        /// 获得S2所有分类
        /// </summary>
        public void getpatternss2()
        {
            HttpContext.Current.Response.Write(Admin.getpatternss2());
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

