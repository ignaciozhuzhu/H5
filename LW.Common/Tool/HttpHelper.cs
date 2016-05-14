using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LW.Common.Tool
{
    public static class HttpHelper
    {
        //public static string HttpPost(string Url, string postDataStr)
        //{
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
        //    request.Method = "POST";
        //    request.ContentType = "application/x-www-form-urlencoded";
        //    request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
        //    CookieContainer cookie = new CookieContainer();
        //    request.CookieContainer = cookie;
        //    Stream myRequestStream = request.GetRequestStream();
        //    StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
        //    myStreamWriter.Write(postDataStr);
        //    myStreamWriter.Close();

        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        //    response.Cookies = cookie.GetCookies(response.ResponseUri);
        //    Stream myResponseStream = response.GetResponseStream();
        //    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
        //    string retString = myStreamReader.ReadToEnd();
        //    myStreamReader.Close();
        //    myResponseStream.Close();

        //    return retString;
        //}

        public static string HttpPost(String url, String data)
        {
            string strReturn = string.Empty;
            byte[] byteData = Encoding.UTF8.GetBytes(data);
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = byteData.Length;

            try
            {
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(byteData, 0, byteData.Length);//发送数据
                }

                using (HttpWebResponse wr = (HttpWebResponse)req.GetResponse())
                {

                    Stream stream = wr.GetResponseStream();
                    strReturn += new StreamReader(stream, System.Text.Encoding.UTF8).ReadToEnd();//解决乱码：utf-8 + streamreader.readtoend


                }
            }
            catch (Exception ex)
            {
                throw ex;
                //LogFile.WriteLine("HttpSend.HttpPostData exception url=" + url);
                //LogFile.WriteLine("HttpSend.HttpPostData exception " + ex.Message);
            }

            return strReturn;
        }


        public static string HttpGet(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }
    }
}
