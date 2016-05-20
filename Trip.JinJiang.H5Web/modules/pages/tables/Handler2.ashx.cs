﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trip.JinJiang.H5Web.modules.pages.tables
{
    /// <summary>
    /// Handler 的摘要说明
    /// </summary>
    public class Handler2 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpPostedFile _upfile = context.Request.Files["File1"];
            if (_upfile == null)
            {
                ResponseWriteEnd(context, "4");//请选择要上传的文件  
            }
            else
            {
                string fileName = _upfile.FileName;/*获取文件名： C:\Documents and Settings\Administrator\桌面\123.jpg*/
                string suffix = fileName.Substring(fileName.LastIndexOf(".") + 1).ToLower();/*获取后缀名并转为小写： jpg*/
                int bytes = _upfile.ContentLength;//获取文件的字节大小  

                if (suffix != "jpg"&&suffix != "png")
                    ResponseWriteEnd(context, "2"); //只能上传JPG格式图片  
                if (bytes > 1024 * 1024)
                    ResponseWriteEnd(context, "3"); //图片不能大于1M  

                _upfile.SaveAs(HttpContext.Current.Server.MapPath("~/modules/img/" + fileName));//保存图片  
                ResponseWriteEnd(context, "1"); //上传成功  
            }
        }

        private void ResponseWriteEnd(HttpContext context, string msg)
        {
            context.Response.Write(msg);
            context.Response.End();
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
