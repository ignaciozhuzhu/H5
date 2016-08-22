using Newtonsoft.Json;
using System;
using Trip.Core;
using Trip.JinJiang.H5.Model;

namespace Trip.JinJiang.H5
{
    public class Pay
    {
        private static string userserver = System.Configuration.ConfigurationManager.AppSettings["userserver"];
        private static string jjh5Bserver = System.Configuration.ConfigurationManager.AppSettings["jjh5Bserver"];
        private static string usercoupserver = System.Configuration.ConfigurationManager.AppSettings["usercoupserver"];

        private static string urlregist = userserver + "/vbp/merge/completeRegist";    //注册2(完整注册)
        private static string urllogin = userserver + "/vbp/merge/login";    //登录
        private static string urlquickregist = userserver + "/vbp/merge/quickRegist";    //注册1(快速注册)

        private static string urlsendValidateCode = userserver + "/vbp/validateCode/sendValidateCode"; //忘记密码
        private static string urlcheckMember = userserver + "/vbp/merge/checkMember";
        private static string urlcheckMemberFullName = userserver + "/vbp/merge/checkMemberFullName";
        private static string urlforgetPwd = userserver + "/vbp/merge/forgetPwd";

        private static string urlqueryorder = jjh5Bserver + "/travel/order/queryOrderList";
        private static string urlqueryorderdetail = jjh5Bserver + "/travel/order/orderDetail/";

        private static string urlcancelorder = jjh5Bserver + "/travel/order/cancel";    //取消订单接口

        private static string urlcheckValidateCode = userserver + "/vbp/validateCode/verifyValidateCode"; //核对验证短信码

        //以下 积分,优惠券
        private static string urlMemberScoreInfo = userserver + "/vbp/score/getMemberScoreInfo/"; //查看积分接口
        private static string urlMemberCoupInfo = usercoupserver + "/couponservice/couponTwoInOne/querycoupons/"; //查看优惠券接口
        private static string urlMemberCoupOrderInfo = userserver + "/cbp/coupon/queryNewCouponRule"; //查看当前可下单优惠券接口

        //绑定银行卡
        public static string bindcard(string memberid,string card)
        {
            string xml = "<payRequestWithCustomer>  <memberId>" + memberid + "</memberId>  <card>" + card + "</card>  <pageUrl>http://192.168.1.32:8077/www/templates/card/bind.html</pageUrl ></payRequestWithCustomer>";
            var url = "http://116.236.229.43:8081/pbp/union/token/JJE_APP_UNION_PAY/bind";
            var response = HttpUtil.Post(xml, url, contentType: "application/xml");
            return response;
        }
        

    }


}
