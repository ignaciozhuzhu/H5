using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections;
using System.Globalization;
using System.Web;

namespace LW.Common.Tool
{
    public class SmsHelper
    {
        /// <summary>
        /// 信息发送
        /// </summary>
        /// <param name="SmsNo">信息接收号码</param>
        /// <param name="SmsContent">信息内容</param>
        /// <param name="SmsDate">信息发送时间</param>
        /// <returns>true:执行成功；false:执行失败</returns>
        public static bool doSms(string SmsNo, string SmsContent, string SmsDate, ref string errInfo)
        {
            errInfo = "该方法暂停使用!";
            return false;
            #region old method
            //string strSql = string.Empty;
            //errInfo = string.Empty;
            //switch (getMobileType(SmsNo))
            //{
            //    case "1":
            //        //联通
            //        strSql = "insert sms_send(mobile,content) values('" + SmsNo + "','" + SmsContent + "');";
            //        break;
            //    case "2":
            //        //移动
            //        strSql = "insert sm(user_name,SRC_TELE_NUM,DEST_TELE_NUM,MSG,SET_SEND_TIME) values('SYSTEM','106575257901','" + SmsNo + "','" + SmsContent + "','" + SmsDate + "');";
            //        break;
            //    case "3":
            //        //电信
            //        errInfo = "暂时不支持电信的号码!";
            //        return false;
            //}
            //return DataSource.ExecuteNonQuery(strSql, "DBConStr_Sms") > 0;
            #endregion
        }
        /// <summary>
        /// 判断号码是联通，移动，电信中的哪个
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns>返回相应类型：1代表联通；2代表移动；3代表电信 </returns>
        public static string getMobileType(string mobile)
        {
            if (mobile.StartsWith("0") || mobile.StartsWith("+860"))
            {
                mobile = mobile.Substring(mobile.IndexOf("0") + 1, mobile.Length);
            }
            string[] chinaUnicom = new string[] { "130", "131", "132", "133" };
            string[] chinaMobile1 = new string[] { "135", "136", "137", "138", "139", "158", "159" };
            string[] chinaMobile2 = new string[] { "1340", "1341", "1342", "1343", "1344", "1345", "1346", "1347", "1348" };

            bool bChinaUnicom = (chinaUnicom.Contains<string>(mobile.Substring(0, 3)));
            bool bChinaMobile1 = (chinaMobile1.Contains<string>(mobile.Substring(0, 3)));
            bool bChinaMobile2 = (chinaMobile2.Contains<string>(mobile.Substring(0, 4)));

            if (bChinaUnicom)
                return "1";//联通 
            if (bChinaMobile1 || bChinaMobile2)
                return "2"; //移动 
            return "3";     //其他为电信 
        }
        /// <summary>
        /// 发送短信方法
        /// </summary>
        /// <param name="ReceivePhone"></param>
        /// <param name="MsgContent"></param>
        /// <param name="SendDate"></param>
        /// <param name="MsgType"></param>
        public static Hashtable UnitSms(string ReceivePhone, string MsgContent, DateTime SendDate, string MsgType)
        {
            string url = "http://112.65.228.88:8888/sms/Api/Send.do";
            Encoding encoding = Encoding.GetEncoding("gb2312");
            byte[] bytesToPost = encoding.GetBytes(MakeParams(ReceivePhone, MsgContent, SendDate, MsgType));

            string cookieheader = string.Empty;

            CookieContainer cookieCon = new CookieContainer();

            #region 创建HttpWebRequest对象
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
            #endregion

            #region 初始化HtppWebRequest对象
            httpRequest.CookieContainer = cookieCon;
            httpRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
            httpRequest.ContentType = "application/x-www-form-urlencoded";
            httpRequest.Method = "POST";

            if (cookieheader.Equals(string.Empty))
            {
                cookieheader = httpRequest.CookieContainer.GetCookieHeader(new Uri(url));
            }
            else
            {
                httpRequest.CookieContainer.SetCookies(new Uri(url), cookieheader);
            }
            #endregion

            #region 附加Post给服务器的数据到HttpWebRequest对象
            httpRequest.ContentLength = bytesToPost.Length;
            System.IO.Stream requestStream = httpRequest.GetRequestStream();
            requestStream.Write(bytesToPost, 0, bytesToPost.Length);
            requestStream.Close();
            #endregion

            #region 读取服务器返回信息
            System.IO.Stream responseStream = httpRequest.GetResponse().GetResponseStream();
            string stringResponse = string.Empty;
            using (System.IO.StreamReader responseReader = new System.IO.StreamReader(responseStream, Encoding.GetEncoding("gb2312")))
            {
                stringResponse = responseReader.ReadToEnd();
            }

            Hashtable ht = new Hashtable();
            if (!string.IsNullOrEmpty(stringResponse))
            {
                stringResponse = HttpUtility.UrlDecode(stringResponse);
                string[] arr = stringResponse.Split('&');
                for (int i = 0; i < arr.Length; i++)
                {
                    string[] arr1 = arr[i].Split('=');
                    if (!ht.Contains(arr1[0]))
                    {
                        ht.Add(arr1[0], arr1[1]);
                    }
                    else
                    {
                        ht[arr1[0]] = ht[arr1[0]] + arr1[1];
                    }
                }
            }
            responseStream.Close();
            #endregion

            return ht;
        }
        /// <summary>
        /// 一信通消息发送方式
        /// </summary>
        /// <returns></returns>
        public static Hashtable UnitSms(SMSOBJECT obj)
        {
            Hashtable ht = new Hashtable();
            if (obj.RECEIVEPHONELIST == null && string.IsNullOrEmpty(obj.RECEIVEPHONE))
            {
                throw new Exception("接收方号码不能为空,操作失败!");
            }
            if (string.IsNullOrEmpty(obj.MSGCONTENT))
            {
                throw new Exception("短信息内容不能为空,操作失败!");
            }
            if (obj.RECEIVEPHONELIST != null)
            {
                ArrayList array = new ArrayList();
                string stemp = string.Empty;
                int itemp = 0;
                foreach (string o in obj.RECEIVEPHONELIST)
                {
                    itemp++;
                    stemp += o + ",";
                    if (itemp == 1000)
                    {
                        itemp = 0;
                        array.Add(stemp);
                    }
                }
                if (itemp < 1000)
                {
                    array.Add(stemp);
                }
                Hashtable _ht = new Hashtable();
                for (int i = 0; i < array.Count; i++)
                {
                    _ht = UnitSms(array[i].ToString().TrimEnd(','), obj.MSGCONTENT, obj.SENDDATE, obj.MSGTYPE);
                    if (ht.ContainsKey("description"))
                    {
                        ht["description"] = ht["description"] + "号码:【" + array[i].ToString().TrimEnd(',') + "】,结果:" + _ht["description"].ToString() + ";";
                    }
                    else
                    {
                        ht.Add("description", "号码:【" + array[i].ToString().TrimEnd(',') + "】,结果:" + _ht["description"].ToString() + ";");
                    }
                }
                return ht;
            }
            else
            {
                return UnitSms(obj.RECEIVEPHONE, obj.MSGCONTENT, obj.SENDDATE, obj.MSGTYPE);
            }
        }
        /// <summary>
        /// 生成POST参数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static string MakeParams(string ReceivePhone, string MsgContent, DateTime SendDate, string MsgType)
        {
            string param = string.Empty;
            param += "SpCode=" + SysConfig.GetValue("SMSCOMPANY");
            param += "&LoginName=" + SysConfig.GetValue("SMSACCOUNT");
            param += "&Password=" + SysConfig.GetValue("SMSPWD");
            param += "&MessageContent=" + MsgContent.Trim().Replace("【", "(").Replace("】", ")");
            param += "&UserNumber=" + ReceivePhone;
            if (SendDate == null) SendDate = DateTime.Now;
            if (SendDate.Year == 1)
            {
                param += "&ScheduleTime=";
            }
            else
            {
                param += "&ScheduleTime=" + SendDate.ToString("yyyyMMddhhmmss");
            }
            param += "&f=1";
            return param;
        }
    }
    /// <summary>
    /// 短消息对象
    /// </summary>
    public class SMSOBJECT
    {
        private string _MSGCONTENT;
        /// <summary>
        /// 发送内容
        /// </summary>
        public string MSGCONTENT
        {
            set { _MSGCONTENT = value; }
            get { return _MSGCONTENT; }
        }

        private DateTime _SENDDATE;
        /// <summary>
        /// 发送时间,为空则为立即发送
        /// </summary>
        public DateTime SENDDATE
        {
            set { _SENDDATE = value; }
            get { return _SENDDATE; }
        }

        private string _MSGTYPE;
        /// <summary>
        /// 信息类型,为活动短信、广告短信、发货短信等
        /// </summary>
        public string MSGTYPE
        {
            set { _MSGTYPE = value; }
            get { return _MSGTYPE; }
        }
        private List<string> _RECEIVEPHONELIST;
        /// <summary>
        /// 接收方手机号码列表
        /// </summary>
        public List<string> RECEIVEPHONELIST
        {
            set { _RECEIVEPHONELIST = value; }
            get { return _RECEIVEPHONELIST; }
        }
        private string _RECEIVEPHONE;
        /// <summary>
        /// 接收方手机号码
        /// </summary>
        public string RECEIVEPHONE
        {
            set { _RECEIVEPHONE = value; }
            get { return _RECEIVEPHONE; }
        }
    }
}
