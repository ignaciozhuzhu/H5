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
    /// bannerImgHandler 的摘要说明
    /// </summary>
    public class bannerImgHandler : IHttpHandler, IRequiresSessionState
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
        /// 获取线路类型
        /// </summary>
        public void getbannerimglist()
        {
            string status = "";
            try
            {
                status = HttpContext.Current.Request["status"];
            }
            catch { }
            HttpContext.Current.Response.Write(Admin.getbannerimglist(status));
        }

        /// <summary>
        /// 添加线路分类
        /// </summary>
        public void addbannerimg()
        {
            string fileName = commonHandler.uploadimg();
            //if (fileName == "0")
            //{
            //    ResponseWriteEnd(HttpContext.Current, "4");//请选择要上传的文件  
            //}
            //else
            //{

            string imgUrl = "../../../modules/img/" + fileName;
            string alt = HttpContext.Current.Request["alt"].ToString();
            //string imgUrl = "../../../modules/img/" + fileName;
            int lineId = 0;
            try
            {
                lineId = Convert.ToInt32(HttpContext.Current.Request["lineId"]);
            }
            catch { }
            int order = Convert.ToInt32(HttpContext.Current.Request["order"]);
            string H5Url = HttpContext.Current.Request["H5Url"].ToString();
            string beginDate = HttpContext.Current.Request["beginDate"].ToString();
            string endDate = HttpContext.Current.Request["endDate"].ToString();
            HttpContext.Current.Response.Write(Admin.addbannerimg(alt, imgUrl, lineId, order, H5Url, beginDate, endDate));
            //}
        }

        /// <summary>
        /// 线路类型编辑
        /// </summary>
        public void editbannerimg()
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
            string alt = HttpContext.Current.Request["alt"].ToString();
            //string imgUrl = "../../../modules/img/" + fileName;
            int Id = Convert.ToInt32(HttpContext.Current.Request["Id"]);
            int lineId = 0;
            try
            {
                lineId = Convert.ToInt32(HttpContext.Current.Request["lineId"]);
            }
            catch { }
            int order = Convert.ToInt32(HttpContext.Current.Request["order"]);
            string H5Url = HttpContext.Current.Request["H5Url"];
            string beginDate = HttpContext.Current.Request["beginDate"];
            string endDate = HttpContext.Current.Request["endDate"];
            HttpContext.Current.Response.Write(Admin.editbannerimg(alt, imgUrl, Id, lineId, order, H5Url, beginDate, endDate));
            //}
        }

        /// <summary>
        /// 轮播图禁(可)用
        /// </summary>
        public void enbannerimg()
        {
            int Id = Convert.ToInt32(HttpContext.Current.Request["Id"]);
            HttpContext.Current.Response.Write(Admin.enbannerimg(Id));
        }
        /// <summary>
        /// 轮播图删除
        /// </summary>
        public void delbannerimg()
        {
            int Id = Convert.ToInt32(HttpContext.Current.Request["Id"]);
            HttpContext.Current.Response.Write(Admin.delbannerimg(Id));
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

