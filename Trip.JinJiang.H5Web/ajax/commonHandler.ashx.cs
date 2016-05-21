using System;
using System.Reflection;
using System.Web;
using System.Web.SessionState;

namespace Trip.JinJiang.H5Web.ajax
{
    /// <summary>
    /// commonHandler 的摘要说明
    /// </summary>
    public class commonHandler : IHttpHandler, IRequiresSessionState
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
        private static void ResponseWriteEnd(HttpContext context, string msg)
        {
            context.Response.Write(msg);
            context.Response.End();
        }

        /// <summary>
        /// 添加图片文件到项目中
        /// </summary>
        public static string uploadimg()
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
                if (bytes > 1024 * 1024 * 2)
                    //    return "3";
                    ResponseWriteEnd(HttpContext.Current, "3"); //图片不能大于1M  

                _upfile.SaveAs(HttpContext.Current.Server.MapPath("~/modules/img/" + fileName));//保存图片 
                return fileName;
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