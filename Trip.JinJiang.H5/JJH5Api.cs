using Trip.Core;
using System.Data;
using Newtonsoft.Json;
using System.Text;
using Maticsoft.DBUtility;
using Trip.JinJiang.H5.DAL;

namespace Trip.JinJiang.H5
{
    /// <summary>
    /// 锦江官网接口
    /// </summary>
    public class JJH5Api
    {
        private static string xhserver = System.Configuration.ConfigurationManager.AppSettings["xhserver"];
        private static string jjh5Sserver = System.Configuration.ConfigurationManager.AppSettings["jjh5Sserver"];
        private static string jjh5Bserver = System.Configuration.ConfigurationManager.AppSettings["jjh5Bserver"];

        private static string urllinesearch = jjh5Sserver + "/travel/search/searchTravel";   //线路列表接口
        private static string urllinedetail = jjh5Bserver + "/travel/line/";    //某线路详情
        private static string urlcmslinedetail = jjh5Bserver + "/travel/line/queryCMSLineInfo";     //CMS线路详情接口
        private static string urlcrmlinesearch = jjh5Bserver + "/travel/line/queryLinesForCMS";     //CMS线路列表接口
        private static string urlprepay = jjh5Bserver + "/travel/payment/prepay";   //预支付接口
        private static string urlsearchorder = jjh5Bserver + "/travel/order/queryOrderList";    //订单查询接口
        private static string urlcreateorder = jjh5Bserver + "/travel/order/create";    //创建订单接口
        private static string urlinventory = jjh5Bserver + "/travel/group/queryRealTimeRefresh/";   //查询库存 ,实时价格接口

        private static string urlcurlcreateorder = xhserver + "/pbp/payment/createOrUpdatePayPreInfo";   //创建订单(预支付)
        private static string urlcurlpc = xhserver + "/pbp/ali/default/pay/";     //网页端支付
        private static string urlcurlwap = xhserver + "/pbp/ali/wap/pay/";    //WAP端支付

        private static string urlcurlh5 = xhserver + "/pbp/wechat/jsapi/pay/JJE_APP_WECHAT_PAY";    //WAP H5 微信

        /// <summary>
        /// 查询路线
        /// </summary>
        public static string Find()
        {
            var data = "{\"page\":{\"endRow\":10,\"page\":1,\"records\":0,\"rows\":50,\"search\":false,\"startRow\":1,\"total\":8}}";
            var data3 = "{\"endBookingDate\":\"2016-05-21\",\"mcMemberCode\":\"1231234\",\"orderCode\":\"\",\"orderStatus\":\"\",\"pagination\":{\"endRow\":10,\"page\":2,\"records\":0,\"rows\":10,\"search\":false,\"startRow\":1,\"total\":1},\"payStatus\":\"PAY_WAITING\",\"startBookingDate\":\"2016-05-19\"}";
            var response = HttpUtil.Post(data, urllinesearch, contentType: "application/json");
            var response3 = HttpUtil.Post(data3, urlsearchorder, contentType: "application/json");
            response = maps.mapAgencies(response);
            if (response != null)
            {
                return response;
            }
            else
            {
                return null;
            }
        }

        public static string Getlinecategoriecrm(string category)
        {
            string str = @"select lineId,lineCategory from dbo.tbl_lineLists where lineCategory='" + category
                + @"' union 
                select a.lineId,b.lineCategory from tbl_recommend a inner join tbl_lineCategory b on a.lineCategory=b.categoryName  where b.lineCategory='" + category + "'";
            DataSet ds = DbHelperSQL.Query(str);
            string json = ConvertJson.DataTable2Array(ds.Tables[0]);
            json = "{\"rows\":" + json.Replace("'", "\"") + "}";
            return json;
        }
        /// <summary>
        /// 查询线路业务类型
        /// </summary>
        public static string Getlinecategoriecrm2(string category)
        {
            string str = @"select lineId,businessCategory as lineCategory from dbo.tbl_lineLists where businessCategory='" + category
                + @"' union 
                select a.lineId,b.lineCategory from tbl_recommend a inner join tbl_lineCategory b on a.lineCategory=b.categoryName  where b.lineCategory='" + category + "'";
            DataSet ds = DbHelperSQL.Query(str);
            string json = ConvertJson.DataTable2Array(ds.Tables[0]);
            json = "{\"rows\":" + json.Replace("'", "\"") + "}";
            return json;
        }

        /// <summary>
        /// 查询路线
        /// </summary>
        public static string getlinesByCategory(string lineCategory)
        {
            //string str = @"select * from dbo.tbl_lineLists ";
            //DataSet ds = DbHelperSQL.Query(str);
            //string json = ConvertJson.DataTable2Array(ds.Tables[0]);
            //json = "{\"rows\":" + json.Replace("'", "\"") + "}";
            //return json;

            //   var data = "{\"lineCategory\":\"\",\"lineName\":\"\",\"travelAgency\":\"\",\"travelBrand\":\"\",\"lineId\":\"\"}";
            //   var response = HttpUtil.Post(data, urlcrmlinesearch, contentType: "application/json");

            var data = "{\"lineCategory\":\"" + lineCategory + "\"}";
            data = "{\"page\":{\"endRow\":10,\"page\":1,\"records\":0,\"rows\":9999,\"search\":false,\"startRow\":1,\"total\":1}}";
            //先获取所有的线路,再与本地数据库做关联
            var response = HttpUtil.Post(data, urllinesearch, contentType: "application/json");

            response = maps.mapAgencies(response);
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
        /// 促销路线(暂时理解为热门路线)
        /// </summary>
        public static string FindPromotion(string pattern)
        {
            lineListsFac lineListsFac = new lineListsFac();
            DataSet ds = lineListsFac.GetListFront4(pattern);
            ConvertJson ConvertJson = new ConvertJson();
            string json = ConvertJson.ToJson(ds);
            return json;

            //lineListsFac lineListsFac=new lineListsFac();
            //var data = "{\"isPromotion\":1}";
            //data = "{\"page\":{\"endRow\":10,\"page\":1,\"records\":80,\"rows\":80,\"search\":false,\"startRow\":1,\"total\":80}}";
            //var response = HttpUtil.Post(data, urllinesearch, contentType: "application/json");
            //response = maps.mapAgencies(response);

            //if (response != null)
            //{
            //    return response;
            //}
            //else
            //{
            //    return null;
            //}
        }

        /// <summary>
        /// 促销路线(暂时理解为热门路线)
        /// </summary>
        public static string LineDetail(int lineid)
        {
            string url = urllinedetail;
            url += lineid;
            var response = HttpUtil.Get(url);
            response = maps.mapAgencies(response);
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
        /// 创建订单
        /// </summary>
        public static string CreateOrder(string json)
        {
            //var data = "{\"adultNum\":1,\"amount\":16500,\"channel\":\"E_BUSINESS_PLATFORM\",\"childNum\":0,\"contact\":{\"mobile\":\"18688880009\",\"name\":\"leesa\"},\"couponAmount\":0,\"groupId\":12266,\"guests\":[{\"category\":\"ADULT\",\"name\":\"guest1\"}],\"mcMemberCode\":\"123123\",\"cardNo\":\"123123\",\"onLinePay\":true,\"receivables\":[{\"copies\":1,\"discountAmount\":0,\"priceId\":15240,\"singlePrice\":16500}],\"scorePay\":false}";
            var response = HttpUtil.Post(json, urlcreateorder, contentType: "application/json");
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
        /// 查询库存 ,实时价格
        /// </summary>
        public static string queryRealTimeRefresh(int groupid)
        {
            string url = urlinventory;    //某线路详情
            url += groupid;
            var response = HttpUtil.Get(url);
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
        /// 
        /// </summary>
        public static string cancelOrder67(string userid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from tbl_lineLists ");
            int i = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (i > 0)
            {
                return "";
            }
            else
            {
                return "";
            }

            //strSql.Append("select * from LineCategory");
            //int j = DataSource.ExecuteSql(strSql.ToString());
            //if (j == 0)
            //{
            //    return JsonMessage.FailResponse("未改变任何数据集!");
            //}
            //else
            //{
            //    return JsonMessage.SuccessString("操作成功!");
            //}
        }


        //(1)line[0]的线路库esGroup先全存好.
        public static string cancelOrder4(string json)
        {
            var data = "{\"page\":{\"endRow\":10,\"page\":1,\"records\":0,\"rows\":50,\"search\":false,\"startRow\":1,\"total\":8}}";
            var response = HttpUtil.Post(data, urllinesearch, contentType: "application/json");
            var result3 = JsonConvert.DeserializeObject<LineListModel>(response);
            var len = result3.lines[0].esGroup.Length;
            esGroupsFac esGroupsFac = new esGroupsFac();
            EsGroup table = new EsGroup();
            for (var i = 0; i < len; i++)
            {
                table = result3.lines[0].esGroup[i];
                esGroupsFac.Add(table);
            }
            return "";
        }

        //(2)line[0]的线路库tags先全存好.
        //public static string cancelOrder(string json)
        //{
        //    var data = "{\"page\":{\"endRow\":10,\"page\":1,\"records\":0,\"rows\":50,\"search\":false,\"startRow\":1,\"total\":8}}";
        //    var response = HttpUtil.Post(data, urllinesearch, contentType: "application/json");
        //    var result3 = JsonConvert.DeserializeObject<LineListModel>(response);
        //    var len = result3.lines[0].tags.Length;
        //    tagsFac tagsFac = new tagsFac();
        //    Tag table = new Tag();
        //    for (var i = 0; i < len; i++)
        //    {
        //        table = result3.lines[0].tags[i];
        //        tagsFac.Add(table);
        //    }
        //    return "";
        //}

        //(3)line[0]的线路库line[]先全存好.
        public static string cancelOrder(string json)
        {
            var data = "{\"page\":{\"endRow\":10,\"page\":1,\"records\":0,\"rows\":9999,\"search\":false,\"startRow\":1,\"total\":1}}";
            var response = HttpUtil.Post(data, urllinesearch, contentType: "application/json");
            var result = JsonConvert.DeserializeObject<LineListModel>(response);
            lineListsFac lineListsFac = new lineListsFac();
            lineListsFac.Truncate();
            Line table = new Line();
            for (var i = 0; i < result.lines.Length; i++)
            {
                table = result.lines[i];
                try
                {
                    lineListsFac.Add(table);
                }
                catch { }
            }

            var data2 = "{\"lineCategory\":\"\",\"lineName\":\"\",\"travelAgency\":\"\",\"travelBrand\":\"\",\"lineId\":\"\"}";
            var response2 = HttpUtil.Post(data2, urlcrmlinesearch, contentType: "application/json");

            response2 = "{\"page\":{\"page\":1,\"rows\":9999,\"records\":4,\"total\":1,\"sidx\":null,\"sord\":null,\"search\":false,\"startRow\":1,\"endRow\":4},\"lines\":" + response2 + "}";
            var result2 = JsonConvert.DeserializeObject<LineListModel2>(response2);

            Line2 table2 = new Line2();
            for (var i = 0; i < result2.lines.Length; i++)
            {
                table2 = result2.lines[i];
                try
                {
                    lineListsFac.Add2(table2);
                }
                catch { }
            }

            return "操作完成";
        }

        /// <summary>
        /// pbp预支付
        /// </summary>
        public static bool pbppaypre(string orderNo, int payAmount)
        {
            var data = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><payPreInfoDto>    <bgUrl>http://gatewayqa.jje.com</bgUrl>    <callPart>TRAVEL</callPart>    <cardNo></cardNo>    <csId></csId>    <csName></csName><orderNo>" + orderNo + "</orderNo><orderPageUrlFroAdmin></orderPageUrlFroAdmin><pageUrl></pageUrl>    <payAmount>" + payAmount + "</payAmount>    <payMethod>MONEY</payMethod>    <payType>ONLINE</payType>    <productTitle>锦江手机官网</productTitle>    <scoreAmount>0</scoreAmount>    <sign></sign>    <userId></userId>    <userName></userName>  </payPreInfoDto>";
            var response = HttpUtil.Post(data, urlcurlcreateorder, contentType: "application/xml");

            if (response == "")
                return true;
            else
                return false;
        }

        /// <summary>
        /// 支付宝支付
        /// </summary>
        public static string pbppayorder(string orderNo, int payAmount, string accountName)
        {
            //测试,将订单金额改为0.
            //payAmount = 0;
            string paymentPlatform = "";
            if (pbppaypre(orderNo, payAmount))
            {

                var url = "";
                if (accountName == "INNS_APP_CLIENT_ALI_WAP_PAY")
                {
                    url = urlcurlwap + accountName;
                    paymentPlatform = "ALIPAY_WAP";
                }
                else if (accountName == "JJE_APP_WECHAT_PAY")
                {
                    url = urlcurlh5;
                    paymentPlatform = "WEIXIN";
                }
                var data = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><payRequest>  <bankCode></bankCode>  <bgUrl>http://gatewayqa.jje.com</bgUrl>  <buyerId></buyerId>  <buyerIp></buyerIp>  <buyerName></buyerName>  <callPart>TRAVEL</callPart>  <description>锦江手机官网</description>  <orderNo>" + orderNo + "</orderNo>  <orderPageUrlFroAdmin> </orderPageUrlFroAdmin>  <pageUrl></pageUrl>  <payMethod>MONEY</payMethod>  <payType>ONLINE</payType>  <paymentPlatform>" + paymentPlatform + "</paymentPlatform>  <price>" + payAmount + "</price>  <score>0</score>  <subject>锦江手机官网</subject></payRequest> ";
                var response = HttpUtil.Post(data, url, contentType: "application/xml");
                return response;
            }
            else
                return "预支付订单失败";
        }

        //(--)测支付报文.
        public static string cancelOrder556(string json)
        {
            //11. 直接修改密码
            var data10 = "<memberDto><mcMemberCode>10059054</mcMemberCode><password>aa3f9313cab1c23764df04d831295c0f</password><sha1pwd>2ca9d3aa9dc4aaf65a2a6b7c054a0e3c58455b8b</sha1pwd></memberDto>";
            var url10 = "http://116.236.229.43:8081/vbp/merge/modifyPwd";
            var response10 = HttpUtil.Post(data10, url10, contentType: "application/xml");
            return "";
            //8. 修改密码（第三步）
            var data08 = "<forgetPwdDto><loginName>18505793685</loginName><fullName>龙鸿轩</fullName><validateCode>154546</validateCode><md5>a91fb0d16d3d3180c8004306df9d6615</md5><sha1>cbe0d52e4132cfca924a051d2d0d97e42a633ee6</sha1></forgetPwdDto>";
            var url08 = "http://116.236.229.43:8081/vbp/merge/forgetPwd";
            var response08 = HttpUtil.Post(data08, url08, contentType: "application/xml");  //成功


            //2. 新验证码（第一步）--发送接口
            var data09 = "<validateCodeDto>       <channel>website</channel>       <interval>-1</interval>       <ip></ip>       <ipCheck></ipCheck>       <ipPeriod></ipPeriod>       <ipTimes></ipTimes>       <memberInfoId></memberInfoId>       <period></period>       <periodTimes>-1</periodTimes>       <receiver>18505793685</receiver>       <target>会员找回密码</target>      <type>SMS</type>      <json>{pwd}</json>    <jsonCode>pwd</jsonCode>    <templateNo>M_Forgot Password_SMS</templateNo></validateCodeDto>";
            var url09 = "http://116.236.229.43:8081/vbp/validateCode/sendValidateCode";
            var response09 = HttpUtil.Post(data09, url09, contentType: "application/xml");



            //注册2(完整注册)
            var data01 = "<memberRegisterDto><memberInfoDto><memberType>Silver Card</memberType><certificateNo>332510198211020626</certificateNo><certificateType>ID</certificateType><email>119414860@qq.com</email><mobile>18505793685</mobile><scoreType>1</scoreType><title>Mr.</title><passsword>9c7d4168c18e1aee29721134e27697cd</passsword><sha1pwd>d1805c2146c9627a8180a378cfe24a53b2c4a257</sha1pwd><surname>龙鸿轩</surname><memberScoreType>SCORE</memberScoreType><ipAddress>192.168.10.20</ipAddress><registerSource>Website</registerSource></memberInfoDto></memberRegisterDto>";
            var url01 = "http://116.236.229.43:8081/vbp/merge/completeRegist";
            var response01 = HttpUtil.Post(data01, url01, contentType: "application/xml");


            //4. 验证身份信息（第一步）-- 验证姓名 //118415262@qq.com该用户名不存在
            var data02 = "<memberAuthentidateDto>       <fullName>龙鸿轩</fullName>       <loginName>1850512</loginName>       <md5>2f51d2bdaad6a56bfd8d6ddbdd4837c3</md5>       <sha1>48e0b82e61a41b29cf0c2bdcb06c82c90959fa8d</sha1></memberAuthentidateDto>";
            var url02 = "http://116.236.229.43:8081/vbp/merge/checkName";
            var response02 = HttpUtil.Post(data02, url02, contentType: "application/xml");


            //7. 新忘记密码（第二步）
            var data07 = "<forgetPwdDto><loginName>1850512</loginName><fullName>龙鸿轩</fullName></forgetPwdDto>";
            var url07 = "http://116.236.229.43:8081/vbp/merge/checkMemberFullName";
            var response07 = HttpUtil.Post(data07, url07, contentType: "application/xml"); //成功

            //6. 新忘记密码（第一步）
            var data06 = "<forgetPwdDto><loginName>1850512</loginName></forgetPwdDto>";
            var url06 = "http://116.236.229.43:8081/vbp/merge/checkMember";
            var response06 = HttpUtil.Post(data06, url06, contentType: "application/xml"); //成功

            //1.登录
            var data05 = "<mergeLoginDto><loginName>18505793685</loginName><md5>aa3f9313cab1c23764df04d831295c0f</md5><sha1>2ca9d3aa9dc4aaf65a2a6b7c054a0e3c58455b8b</sha1></mergeLoginDto>";
            var url05 = "http://116.236.229.43:8081/vbp/merge/login";
            var response05 = HttpUtil.Post(data05, url05, contentType: "application/xml"); //成功

            //14.判断手机,邮箱是否已存在
            var data04 = "<validateEmailOrPhoneRespDto><phone>1850512</phone></validateEmailOrPhoneRespDto>";
            var url04 = "http://116.236.229.43:8081/vbp/merge/validateEmailOrTel";
            var response04 = HttpUtil.Post(data04, url04, contentType: "application/xml"); //119@qq.com该邮箱不存在

            //注册1
            //这个证明可以使用,返回了<?xml version="1.0" encoding="UTF-8" standalone="yes"?><webMemberRegisterReturnDto><cdsid>80012234</cdsid><mcMemberCode>10059052</mcMemberCode></webMemberRegisterReturnDto> 
            var data00 = "<webMemberDto><email>1194@qq.com</email><phone>1850512</phone><pwd>2f51d2bdaad6a56bfd8d6ddbdd4837c3</pwd><sha1pwd>48e0b82e61a41b29cf0c2bdcb06c82c90959fa8d</sha1pwd><registChannel>Mobile</registChannel><registTag>IOS|JJTRAVEL_IOS_1|JinJiang</registTag></webMemberDto>";//密码的明文是 xtsbwe 118415262@qq.com</email><phone>18550250152
            var url00 = "http://116.236.229.43:8081/vbp/merge/quickRegist";
            var response00 = HttpUtil.Post(data00, url00, contentType: "application/xml");






            var orderNo = "";
            var payAmount = 0;

            var data = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><payPreInfoDto>    <bgUrl></bgUrl>    <callPart>TRAVEL</callPart>    <cardNo></cardNo>    <csId></csId>    <csName></csName><orderNo>" + orderNo + "</orderNo><orderPageUrlFroAdmin></orderPageUrlFroAdmin><pageUrl></pageUrl>    <payAmount>" + payAmount + "</payAmount>    <payMethod>MONEY</payMethod>    <payType>ONLINE</payType>    <productTitle>锦江手机官网</productTitle>    <scoreAmount>0</scoreAmount>    <sign></sign>    <userId></userId>    <userName></userName>  </payPreInfoDto>";
            var response = HttpUtil.Post(data, urlcurlcreateorder, contentType: "application/xml");

            var data0 = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><payPreInfoDto>    <bgUrl>http://172.24.88.73:6066/hotelservice/hotels/order/afterPayProcess</bgUrl>    <callPart>TRAVEL</callPart>    <cardNo></cardNo>    <csId></csId>    <csName></csName><orderNo>" + orderNo + "</orderNo><orderPageUrlFroAdmin></orderPageUrlFroAdmin><pageUrl></pageUrl>    <payAmount>" + payAmount + "</payAmount>    <payMethod>MONEY</payMethod>    <payType>ONLINE</payType>    <productTitle>锦江手机官网</productTitle>    <scoreAmount>0</scoreAmount>    <sign></sign>    <userId></userId>    <userName></userName>  </payPreInfoDto>";
            var response0 = HttpUtil.Post(data0, urlcurlcreateorder, contentType: "application/xml");

            // var data = "{\"bgUrl\":{\"endRow\":10,\"page\":1,\"records\":0,\"rows\":50,\"search\":false,\"startRow\":1,\"total\":8}}";
            //var data = "{\"bgUrl\":\"\",\"callPart\":\"HOTEL\",\"cardNo\":\"\",\"csId\":\"\",\"csName\":\"\",\"orderNo\":\"H1D52A754174\",\"orderPageUrlFroAdmin\":\"\",\"pageUrl\":\"\"}";
            //data = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><payRequest>  <bankCode></bankCode>  <bgUrl></bgUrl>  <buyerId> 10056582 </buyerId>  <buyerIp> 192.168.2.51 </buyerIp>     <buyerName> 惊云 </buyerName>     <callPart> HOTEL </callPart>     <description> 锦江之星上海外滩滨江酒店 </description>     <orderNo> H1D52C3DA815 </orderNo>     <orderPageUrlFroAdmin> </orderPageUrlFroAdmin>  <pageUrl></pageUrl><payMethod > MONEY </payMethod>     <payType> ONLINE </payType>     <paymentPlatform> ALIPAY </paymentPlatform>     <price> 77 </price><score> 0 </score>     <subject> 锦江之星上海外滩滨江酒店 </subject> </payRequest> ";
            //var response = HttpUtil.Post(data, urlcurlpc, contentType: "application/xml");


            //  var data2 = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><payRequest>  <bankCode></bankCode>  <bgUrl>http://192.168.2.81:81/hbp/hotels/order/afterPayProcess</bgUrl>  <buyerId>10056582</buyerId>  <buyerIp>192.168.2.51</buyerIp>  <buyerName>惊云</buyerName>  <callPart>HOTEL</callPart>  <description>锦江之星上海外滩滨江酒店</description>  <orderNo>1000160520000001</orderNo>  <orderPageUrlFroAdmin> </orderPageUrlFroAdmin>  <pageUrl></pageUrl>  <payMethod>MONEY</payMethod>  <payType>ONLINE</payType>  <paymentPlatform>ALIPAY_WAP</paymentPlatform>  <price>1</price>  <score>0</score>  <subject>锦江之星上海外滩滨江酒店</subject></payRequest> ";
            // var response2 = HttpUtil.Post(data2, urlcurlwap, contentType: "application/xml");
            // var response2 = HttpUtil.Post(data2, urlcurlwap3, contentType: "application/xml");

            return "";

            //var data = "{\"page\":{\"endRow\":10,\"page\":1,\"records\":0,\"rows\":50,\"search\":false,\"startRow\":1,\"total\":8}}";
            //var response = HttpUtil.Post(data, urllinesearch, contentType: "application/json");
            //var result3 = JsonConvert.DeserializeObject<LineListModel>(response);
            //lineListsFac lineListsFac = new lineListsFac();
            //Line table = new Line();
            //table = result3.lines[1];
            //lineListsFac.Add(table);
            //return "";
        }

        /// <summary>
        /// 周边游查询
        /// </summary>
        public static string GetNearBy()
        {
            string str = "select * from tbl_lineLists where lineDistrict in('上海','江苏','浙江','安徽','江西') and days<=3";
            DataSet ds = DbHelperSQL.Query(str);
            string json = ConvertJson.DataTable2Array(ds.Tables[0]);
            json = "{\"rows\":" + json.Replace("'", "\"") + "}";
            return json;
        }
        /// <summary>
        /// 旅行社列表
        /// </summary>
        public static string getagencies()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select '' as agency,'' as code 
             union all
             select agency,code from tbl_agencies");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            ConvertJson ConvertJson = new ConvertJson();
            string json = ConvertJson.ToJson(ds);
            return json;
        }
    }
}
