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

        //注册2(完整注册)
        public static string regist(string xml)
        {
            // "<memberRegisterDto><memberInfoDto><memberType>Silver Card</memberType><certificateNo>332510198211020626</certificateNo><certificateType>ID</certificateType><email>119414860@qq.com</email><mobile>18505793685</mobile><scoreType>1</scoreType><title>Mr.</title><passsword>9c7d4168c18e1aee29721134e27697cd</passsword><sha1pwd>d1805c2146c9627a8180a378cfe24a53b2c4a257</sha1pwd><surname>龙鸿轩</surname><memberScoreType>SCORE</memberScoreType><ipAddress>"+ localIP + "</ipAddress><registerSource>Website</registerSource></memberInfoDto></memberRegisterDto>";
            var response = HttpUtil.Post(xml, urlregist, contentType: "application/xml");
            return "";
        }
    }


}
