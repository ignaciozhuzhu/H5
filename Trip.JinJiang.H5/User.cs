using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Trip.Core;
using Trip.JinJiang.H5.Model;

namespace Trip.JinJiang.H5
{
    public class User
    {
        private static string userserver = System.Configuration.ConfigurationManager.AppSettings["userserver"];
        private static string jjh5Bserver = System.Configuration.ConfigurationManager.AppSettings["jjh5Bserver"];

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

        //注册2(完整注册)
        public static string regist(string xml)
        {
            var response = HttpUtil.Post(xml, urlregist, contentType: "application/xml");
            return response;
        }

        //1.登录
        public static string login(string xml)
        {
            var response = HttpUtil.Post(xml, urllogin, contentType: "application/xml");
            return response;
        }
        //注册1(快速注册)
        public static string quickregist(string xml)
        {
            var response = HttpUtil.Post(xml, urlquickregist, contentType: "application/xml");
            return response;
        }

        //忘记密码 功能步骤:
        //0.发送验证码至手机
        //1.验证手机/邮箱
        //2.验证姓名
        //3.设置新密码----------------------------------------------------------------------bg
        public static string sendvalidatecode(string loginName)
        {
            //测试时暂时没加发送短信限制,后需要将 periodTimes 从-1 改为3
            string xml = "<validateCodeDto>       <channel>website</channel>       <interval>-1</interval>       <ip></ip>       <ipCheck></ipCheck>       <ipPeriod></ipPeriod>       <ipTimes></ipTimes>       <memberInfoId></memberInfoId>       <period></period>       <periodTimes>-1</periodTimes>       <receiver>" + loginName + "</receiver>       <target>会员找回密码</target>      <type>SMS</type>      <json>{pwd}</json>    <jsonCode>pwd</jsonCode>    <templateNo>M_Forgot Password_SMS</templateNo></validateCodeDto>";
            var response = HttpUtil.Post(xml, urlsendValidateCode, contentType: "application/xml");
            return response;
        }
        public static string checkmember(string loginName)
        {
            string xml = "<forgetPwdDto><loginName>" + loginName + "</loginName></forgetPwdDto>";
            var response = HttpUtil.Post(xml, urlcheckMember, contentType: "application/xml");
            return response;
        }
        public static string checkmemberfullname(string loginName, string fullName)
        {
            string xml = "<forgetPwdDto><loginName>" + loginName + "</loginName><fullName>" + fullName + "</fullName></forgetPwdDto>";
            var response = HttpUtil.Post(xml, urlcheckMemberFullName, contentType: "application/xml");
            return response;
        }
        public static string forgetpwd(string loginName, string fullName, string validateCode, string md5, string sha1)
        {
            string xml = "<forgetPwdDto><loginName>" + loginName + "</loginName><fullName>" + fullName + "</fullName><validateCode>" + validateCode + "</validateCode><md5>" + md5 + "</md5><sha1>" + sha1 + "</sha1></forgetPwdDto>";
            var response = HttpUtil.Post(xml, urlforgetPwd, contentType: "application/xml");
            return response;
        }

        //查看我的订单
        public static string queryorder(string mcMemberCode, string orderStatus, string payStatus)
        {
            string startBookingDate = "2016-03-01";
            string endBookingDate = "2017-01-01";
            //mcMemberCode = "10059054";
            string json = "{\"endBookingDate\":\"" + endBookingDate + "\",\"mcMemberCode\":\"" + mcMemberCode + "\",\"orderCode\":\"\",\"orderStatus\":\"" + orderStatus + "\",\"pagination\":{ \"endRow\":10,\"page\":1,\"records\":0,\"rows\":10,\"search\":false,\"startRow\":1,\"total\":999},\"payStatus\":\"" + payStatus + "\",\"startBookingDate\":\"" + startBookingDate + "\"}";

            var response = HttpUtil.Post(json, urlqueryorder, contentType: "application/json");
            var result = JsonConvert.DeserializeObject<OrderdetailMod>(response);

            //需要两个表做关联,得到3个明细字段的数据
            for (int i = 0; i < result.orders.Length; i++)
            {
                var response2 = queryorderdetail(result.orders[i].code);
                var result2 = JsonConvert.DeserializeObject<OrderdetailMod>(response2);
                result.orders[i].adultNum = result2.adultNum;
                result.orders[i].departrueDate = GetTime(result2.departrueDate).ToString("yyyy-MM-dd");
                result.orders[i].createTime = result2.createTime;
                result.orders[i].paymentAmount = result2.paymentAmount;
            }
            response = ConvertJson.ToJSON(result);
            return response;
        }


        //查看某订单号的更多详情
        public static string queryorderdetail(string code)
        {
            var response = HttpUtil.Get(urlqueryorderdetail + code);
            return response;
        }

        //时间戳转换
        private static DateTime GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
        /// <summary>
        /// 取消订单
        /// </summary>
        public static string cancelOrder(string orderCode, string mcMemberCode)
        {
            string data = "{\"mcMemberCode\":\"" + mcMemberCode + "\",\"orderNo\":\"" + orderCode + "\",\"reson\":\"客户取消\"}";
            var response = HttpUtil.Post(data, urlcancelorder, contentType: "application/json");
            if (response != null)
            {
                return response;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 发送验证码
        /// </summary>
        public static string sendvalidatecode4reg(string phone)
        {
            //测试时暂时没加发送短信限制,后需要将 periodTimes 从-1 改为3
            string xml = "<validateCodeDto>       <channel>website</channel>       <interval>-1</interval>       <ip></ip>       <ipCheck></ipCheck>       <ipPeriod></ipPeriod>       <ipTimes></ipTimes>       <memberInfoId></memberInfoId>       <period></period>       <periodTimes>-1</periodTimes>       <receiver>" + phone + "</receiver>       <target>会员注册获取验证码</target>      <type>SMS</type>     <json>{code}</json><jsonCode>code</jsonCode>   <templateNo>validateCodeTemplate2</templateNo></validateCodeDto>";
            var response = HttpUtil.Post(xml, urlsendValidateCode, contentType: "application/xml");
            return response;
        }
        /// <summary>
        /// 核对验证码
        /// </summary>
        public static string checkvalidatecode4reg(string phone,string validate)
        {
            string xml = "<validateCodeVerifyDto>       <receiver>"+ phone + "</receiver>       <code>"+ validate + "</code></validateCodeVerifyDto>";
            var response = HttpUtil.Post(xml, urlcheckValidateCode, contentType: "application/xml");
            return response;
        }

        //以下 积分,优惠券-----------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// 查看个人积分
        /// </summary>
        public static string getMemberScoreInfo(string Membercode)
        {
            var response = HttpUtil.Get(urlMemberScoreInfo + Membercode);
            return response;
        }

    }


}



//public static string forgetmodifypwd(string validateCode, string loginName, string fullName, string md5, string sha1)
//{
//    string response;
//    sendvalidatecode(loginName);
//    checkmember(loginName);
//    checkmemberfullname(loginName, fullName);
//    response = forgetpwd(loginName, fullName, validateCode, md5, sha1);
//    return response;
//}
//----------------------------------------------------------------------ed


//完整注册demoxml "<memberRegisterDto><memberInfoDto><memberType>Silver Card</memberType><certificateNo>332510198211020626</certificateNo><certificateType>ID</certificateType><email>119414860@qq.com</email><mobile>18505793685</mobile><scoreType>1</scoreType><title>Mr.</title><passsword>9c7d4168c18e1aee29721134e27697cd</passsword><sha1pwd>d1805c2146c9627a8180a378cfe24a53b2c4a257</sha1pwd><surname>龙鸿轩</surname><memberScoreType>SCORE</memberScoreType><ipAddress>"+ localIP + "</ipAddress><registerSource>Website</registerSource></memberInfoDto></memberRegisterDto>";
