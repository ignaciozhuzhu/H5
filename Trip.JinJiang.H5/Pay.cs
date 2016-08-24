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

        /// <summary>
        /// 银联支付
        /// </summary>
        public static string pbppayorder(string orderNo, int payAmount, string accountName)
        {
            //测试,将订单金额改为0.
            //  payAmount = 1;
            string paymentPlatform = "";
            if ( JJH5Api.pbppaypre(orderNo, payAmount))
            {

                var url = "";
                if (accountName == "JJE_APP_CLIENT_ALI_WAP_PAY")
                {
                    url = urlunionpaywap + accountName;
                    paymentPlatform = "ALIPAY_WAP";
                }
                else if (accountName == "JJE_APP_WECHAT_PAY")
                {
                    url = urlunionpaywap;
                    paymentPlatform = "WEIXIN";
                }
                var data = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><payRequest>  <bankCode></bankCode>  <bgUrl></bgUrl>  <buyerId></buyerId>  <buyerIp></buyerIp>  <buyerName></buyerName>  <callPart>TRAVEL</callPart>  <description>锦江手机官网</description>  <orderNo>" + orderNo + "</orderNo>  <orderPageUrlFroAdmin> </orderPageUrlFroAdmin>  <pageUrl></pageUrl>  <payMethod>MONEY</payMethod>  <payType>ONLINE</payType>  <paymentPlatform>" + paymentPlatform + "</paymentPlatform>  <price>" + payAmount + "</price>  <score>0</score>  <subject>锦江手机官网</subject></payRequest> ";
                var response = HttpUtil.Post(data, url, contentType: "application/xml");
                return response;
            }
            else
                return "预支付订单失败";
        }



    }


}
