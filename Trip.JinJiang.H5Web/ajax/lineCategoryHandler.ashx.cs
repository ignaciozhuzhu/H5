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
        /// 添加图片文件到项目中
        /// </summary>
        private string uploadimg()
        {
            HttpPostedFile _upfile = HttpContext.Current.Request.Files["File1"];
            if (_upfile == null)
            {
                return "0";
            }
            else
            {
                string fileName = _upfile.FileName;//获取文件名
                string suffix = fileName.Substring(fileName.LastIndexOf(".") + 1).ToLower();
                int bytes = _upfile.ContentLength;//获取文件的字节大小  

                if (suffix != "jpg" && suffix != "png")
                    ResponseWriteEnd(HttpContext.Current, "2"); //只能上传JPG获取png格式图片  
                if (bytes > 1024 * 1024)
                    ResponseWriteEnd(HttpContext.Current, "3"); //图片不能大于1M  

                _upfile.SaveAs(HttpContext.Current.Server.MapPath("~/modules/img/" + fileName));//保存图片 
                return fileName;
            }
        }

        /// <summary>
        /// 添加线路分类
        /// </summary>
        public void addcategory()
        {
            string fileName = uploadimg();
            if (fileName == "0")
            {
                ResponseWriteEnd(HttpContext.Current, "4");//请选择要上传的文件  
            }
            else
            {
                string categoryName = HttpContext.Current.Request["categoryName"].ToString();
                string lineCategory = HttpContext.Current.Request["lineCategory"].ToString();
                string imgUrl = "../../../modules/img/" + fileName;
                HttpContext.Current.Response.Write(JJH5Api.addlinecategory(categoryName, lineCategory, imgUrl));
            }
        }

        /// <summary>
        /// 线路类型编辑
        /// </summary>
        public void editlinecategory()
        {
            string fileName = uploadimg();
            if (fileName == "0")
            {
                ResponseWriteEnd(HttpContext.Current, "4");//请选择要上传的文件  
            }
            else
            {
                string categoryName = HttpContext.Current.Request["categoryName"].ToString();
                string lineCategory = HttpContext.Current.Request["lineCategory"].ToString();
                string imgUrl = "../../../modules/img/" + fileName;
                int Id = Convert.ToInt32(HttpContext.Current.Request["Id"]);
                HttpContext.Current.Response.Write(JJH5Api.editlinecategory(categoryName, lineCategory, imgUrl, Id));
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

