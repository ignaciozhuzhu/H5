using System;
using Newtonsoft.Json;
using System.Data;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Text;
using System.Collections;
using LW.Common.Tool;
using System.Text.RegularExpressions;

namespace LW.Common.Json
{
    /// <summary>
    /// Json对象处理
    /// </summary>
    public class JsonMessage
    {
        /// <summary>
        /// 返回执行成功JSON字符串
        /// </summary>
        /// <returns></returns>
        public static string SuccessString()
        {
            return SuccessString(NoticeEnum.GetEnumDesc(NoticeEnum.NOTICE.SUCCESS));
        }
        /// <summary>
        /// 返回执行成功JSON字符串
        /// </summary>
        /// <returns></returns>
        public static string SuccessString(string info)
        {
            JsonHelper json = new JsonHelper();
            json.totlal = 0;
            json.success = true;
            json.AddItem("info", info);
            json.ItemOk();
            return json.ToString();
        }
        /// <summary>
        /// 返回失败JSON字符串
        /// </summary>
        /// <param name="FailNotice"></param>
        /// <returns></returns>
        public static string FailString(string FailNotice)
        {
            JsonHelper json = new JsonHelper();
            json.totlal = 0;
            json.success = false;
            FailNotice = Regex.Replace(FailNotice, @"\r\n", " ");
            json.AddItem("info", NoticeEnum.GetEnumDesc(NoticeEnum.NOTICE.FAILSURE) + "，可能原因为：" + FailNotice.Replace("/", "").Replace("\"", "'"));
            json.ItemOk();
            return json.ToString();
        }
        /// <summary>
        /// 返回执行成功JSON字符串
        /// </summary>
        /// <returns></returns>
        public static string SuccessResponse(string info)
        {
            JsonHelper json = new JsonHelper();
            json.totlal = 0;
            json.success = true;
            json.AddItem("info", info);
            json.ItemOk();
            return json.ToString();
        }
        /// <summary>
        /// 返回执行成功JSON字符串，带返回的自定义字符串
        /// </summary>
        /// <returns></returns>
        public static string SuccessResponse(string info,string returnStr)
        {
            JsonHelper json = new JsonHelper();
            json.totlal = 0;
            json.success = true;
            json.AddItem("info", info);
            json.AddItem("return", returnStr);
            json.ItemOk();
            return json.ToString();
        }
        /// <summary>
        /// 返回执行失败JSON字符串，存储错误信息到日志文件
        /// </summary>
        /// <returns></returns>
        public static string FailResponse(string FailNotice)
        {
            JsonHelper json = new JsonHelper();
            json.totlal = 0;
            json.success = false;
            json.AddItem("info", "当前操作失败：" + FailNotice);
            json.ItemOk();
            return json.ToString();
        }

        /// <summary>
        /// 返回执行失败JSON字符串，存储错误信息到日志文件,带返回的自定义字符串
        /// </summary>
        /// <returns></returns>
        public static string FailResponse(string FailNotice, string returnStr)
        {
            JsonHelper json = new JsonHelper();
            json.totlal = 0;
            json.success = false;
            json.AddItem("info", "当前操作失败：" + FailNotice);
            json.AddItem("return", returnStr);
            json.ItemOk();
            return json.ToString();
        }
    }
}
