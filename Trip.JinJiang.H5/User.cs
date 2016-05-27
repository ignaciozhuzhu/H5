using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trip.Core;

namespace Trip.JinJiang.H5
{
    public class User
    {
        private static string userserver = System.Configuration.ConfigurationManager.AppSettings["userserver"];

        private static string urlregist = userserver + "/vbp/merge/completeRegist";    //注册2(完整注册)
        private static string urllogin = userserver + "/vbp/merge/login";    //登录
        private static string urlquickregist = userserver + "/vbp/merge/quickRegist";    //注册1(快速注册)

        private static string urlsendValidateCode = userserver + "/vbp/validateCode/sendValidateCode";
        private static string urlcheckMember = userserver + "/vbp/merge/checkMember";
        private static string urlcheckMemberFullName = userserver + "/vbp/merge/checkMemberFullName";
        private static string urlforgetPwd = userserver + "/vbp/merge/forgetPwd";



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

    }


}


//完整注册demoxml "<memberRegisterDto><memberInfoDto><memberType>Silver Card</memberType><certificateNo>332510198211020626</certificateNo><certificateType>ID</certificateType><email>119414860@qq.com</email><mobile>18505793685</mobile><scoreType>1</scoreType><title>Mr.</title><passsword>9c7d4168c18e1aee29721134e27697cd</passsword><sha1pwd>d1805c2146c9627a8180a378cfe24a53b2c4a257</sha1pwd><surname>龙鸿轩</surname><memberScoreType>SCORE</memberScoreType><ipAddress>"+ localIP + "</ipAddress><registerSource>Website</registerSource></memberInfoDto></memberRegisterDto>";
