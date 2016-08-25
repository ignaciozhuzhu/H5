using Newtonsoft.Json;
using System;
using Trip.Core;
using Trip.JinJiang.H5.Model;

namespace Trip.JinJiang.H5
{
    public class Pay
    {
        private static string xhserver = System.Configuration.ConfigurationManager.AppSettings["xhserver"];
        private static string userserver = System.Configuration.ConfigurationManager.AppSettings["userserver"];
        private static string jjh5Bserver = System.Configuration.ConfigurationManager.AppSettings["jjh5Bserver"];
        private static string usercoupserver = System.Configuration.ConfigurationManager.AppSettings["usercoupserver"];

        private static string urlbindquery = userserver + "/pbp/union/token/JJE_APP_UNION_PAY/bind/query/";    //绑定银行卡
        private static string urlbind = userserver + "/pbp/union/token/JJE_APP_UNION_PAY/bind";    //绑定银行卡
        private static string urlsms = userserver + "/pbp/union/token/JJE_APP_UNION_PAY/sms";    //银联发短信
        private static string urlunionpaywap = xhserver + "/pbp/union/token/JJE_APP_UNION_PAY/pay";    //银联WAP端支付

        //查看是否已经绑定银行卡
        public static string bindcardquery(string memberid)
        {
            string url = urlbindquery;
            url += memberid;
            var response = HttpUtil.Get(url);
            return response;
        }
        //绑定银行卡
        public static string bindcard(string memberid, string card)
        {
            //pageUrl 是回调地址.
            string xml = "<payRequestWithCustomer>  <memberId>" + memberid + "</memberId>  <card>" + card + "</card>  <pageUrl>http://192.168.1.32:8077/www/templates/card/cdxbind.html</pageUrl ></payRequestWithCustomer>";
            var response = HttpUtil.Post(xml, urlbind, contentType: "application/xml");
            return response;
        }

        //银联发短信
        public static string sms(string memberid, string orderNo, decimal price,string txnTime)
        {
            string xml = @"   <payRequestWithCustomer>
                              <memberId>" + memberid + @"</memberId>
                              <orderNo>" + orderNo + @"</orderNo>
                              <price>" + price + @"</price>
                              <txnTime>" + txnTime + @"</txnTime>
                              </payRequestWithCustomer>";
            var response = HttpUtil.Post(xml, urlsms, contentType: "application/xml");
            return response;
        }
    }


}
