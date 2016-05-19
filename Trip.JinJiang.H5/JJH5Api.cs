using Trip.Core;
using System.Data;
using Newtonsoft.Json;
using System.Text;
using Maticsoft.DBUtility;
using Trip.JinJiang.H5.DAL;
using Trip.JinJiang.H5.Model;

namespace Trip.JinJiang.H5
{
    /// <summary>
    /// 锦江官网接口
    /// </summary>
    public class JJH5Api
    {
        private const string urllinesearch = "http://travelsearchservice.jinjiang.uat/travelsearchservice/travel/search/searchTravel";   //线路列表接口
        private const string urllinedetail = "http://travelbaseservice.jinjiang.uat/travelbaseservice/travel/line/";    //某线路详情
        private const string urlcmslinedetail = "http://travelbaseservice.jinjiang.uat/travelbaseservice/travel/line/queryCMSLineInfo";     //CMS线路详情接口
        private const string urlcrmlinesearch = "http://travelbaseservice.jinjiang.uat/travelbaseservice/travel/line/queryLinesForCMS";     //CMS线路列表接口
        private const string urlprepay = "http://travelbaseservice.jinjiang.uat/travelbaseservice/travel/payment/prepay";   //预支付接口
        private const string urlsearchorder = "http://travelbaseservice.jinjiang.uat/travelbaseservice/travel/order/queryOrderList";    //订单查询接口
        private const string urlcreateorder = "http://travelbaseservice.jinjiang.uat/travelbaseservice/travel/order/create";    //创建订单接口
        private const string urlinventory = "http://travelbaseservice.jinjiang.uat/travelbaseservice/travel/group/queryRealTimeRefresh/";   //查询库存 ,实时价格接口
        private const string urlcancelorder = "http://travelbaseservice.jinjiang.uat/travelbaseservice/travel/order/cancel";    //取消订单接口

        private const string urlcurlcreateorder = "http://116.236.229.43:8081/pbp/payment/createOrUpdatePayPreInfo";   //创建订单
        private const string urlcurlpc = "http://116.236.229.43:8081/pbp/ali/default/pay/12332";     //网页端支付
        private const string urlcurlwap = "http://116.236.229.43:8081/pbp/ali/wap/pay/xtsb";    //WAP端支付

        /// <summary>
        /// 查询路线
        /// </summary>
        public static string Find()
        {
            var data = "{\"page\":{\"endRow\":10,\"page\":1,\"records\":0,\"rows\":50,\"search\":false,\"startRow\":1,\"total\":8}}";
            var data3 = "{\"endBookingDate\":\"2016-05-14\",\"mcMemberCode\":\"1231234\",\"orderCode\":\"\",\"orderStatus\":\"\",\"pagination\":{\"endRow\":10,\"page\":2,\"records\":0,\"rows\":10,\"search\":false,\"startRow\":1,\"total\":1},\"payStatus\":\"PAY_WAITING\",\"startBookingDate\":\"2016-05-10\"}";
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
            string str = "select lineId,lineCategory from dbo.tbl_lineLists where lineCategory='" + category + "'";
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
            var data = "{\"lineCategory\":\"" + lineCategory + "\"}";
            data = "{\"page\":{\"endRow\":10,\"page\":1,\"records\":0,\"rows\":80,\"search\":false,\"startRow\":1,\"total\":8}}";
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
        public static string FindPromotion()
        {
            var data = "{\"isPromotion\":1}";
            data = "{\"page\":{\"endRow\":10,\"page\":1,\"records\":80,\"rows\":80,\"search\":false,\"startRow\":1,\"total\":80}}";
            var response = HttpUtil.Post(data, urllinesearch, contentType: "application/json");
            response = maps.mapAgencies(response);
            string tourl = "http://192.168.2.81:81/hbp/hotels/order/afterPayProcess";
            var data2 = "{\"bgUrl\":\"" + tourl + "\",\"callPart\":\"TRAVEL\",\"orderNo\":\"1000160323000023\",\"payMethod\":\"MONEY\",\"payType\":\"ONLINE\",\"productTitle\":\"旅游测试title\",\"payAmount\":\"10\",\"payChannel\":\"ALIPAY\"}";

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
        /// 取消订单
        /// </summary>
        public static string cancelOrder2(string orderCode)
        {
            // var data = "{\"lineCategory\":\"" + lineCategory + "\"}";
            //   if(lineCategory=="")
            var data = "{\"page\":{\"endRow\":10,\"page\":1,\"records\":0,\"rows\":25,\"search\":false,\"startRow\":1,\"total\":1}}";

            //先获取所有的线路,再与本地数据库做关联
            var json3 = HttpUtil.Post(data, urllinesearch, contentType: "application/json");

            var result3 = JsonConvert.DeserializeObject<LineListModel>(json3);

            //  string a = "insert into tbl_lineLists(lineId,lineName)";

            return null;

            //取消订单业务
            //string data = "{\"mcMemberCode\":\"1231234\",\"orderNo\":\""+ orderCode + "\",\"reson\":\"名额不足\"}";
            //var response = HttpUtil.Post(data, urlcancelorder, contentType: "application/json");
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
        public static string cancelOrder44(string json)
        {
            var data = "{\"page\":{\"endRow\":10,\"page\":1,\"records\":0,\"rows\":80,\"search\":false,\"startRow\":1,\"total\":8}}";
            var response = HttpUtil.Post(data, urllinesearch, contentType: "application/json");
            var result3 = JsonConvert.DeserializeObject<LineListModel>(response);
            lineListsFac lineListsFac = new lineListsFac();
            Line table = new Line();
            for (var i = 0; i < result3.lines.Length; i++)
            {
                table = result3.lines[i];
                try
                {
                    lineListsFac.Add(table);
                }
                catch { }
            }
            return "";
        }

        //(--)测支付报文.
        public static string cancelOrder(string json)
        {
            // var data = "{\"bgUrl\":{\"endRow\":10,\"page\":1,\"records\":0,\"rows\":50,\"search\":false,\"startRow\":1,\"total\":8}}";
            var data = "{\"bgUrl\":\"\",\"callPart\":\"HOTEL\",\"cardNo\":\"\",\"csId\":\"\",\"csName\":\"\",\"orderNo\":\"H1D52A754174\",\"orderPageUrlFroAdmin\":\"\",\"pageUrl\":\"\"}";
            data = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><payRequest>  <bankCode></bankCode>  <bgUrl></bgUrl>  <buyerId> 10056582 </buyerId>  <buyerIp> 192.168.2.51 </buyerIp>     <buyerName> 惊云 </buyerName>     <callPart> HOTEL </callPart>     <description> 锦江之星上海外滩滨江酒店 </description>     <orderNo> H1D52C3DA815 </orderNo>     <orderPageUrlFroAdmin> </orderPageUrlFroAdmin>  <pageUrl></pageUrl><payMethod > MONEY </payMethod>     <payType> ONLINE </payType>     <paymentPlatform> ALIPAY </paymentPlatform>     <price> 77 </price><score> 0 </score>     <subject> 锦江之星上海外滩滨江酒店 </subject> </payRequest> ";
            var response = HttpUtil.Post(data, urlcurlpc, contentType: "application/xml");

            var data2 = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><payRequest>  <bankCode></bankCode>  <bgUrl>http://192.168.2.81:81/hbp/hotels/order/afterPayProcess</bgUrl>  <buyerId>10056582</buyerId>  <buyerIp>192.168.2.51</buyerIp>  <buyerName>惊云</buyerName>  <callPart>HOTEL</callPart>  <description>锦江之星上海外滩滨江酒店</description>  <orderNo>H1D52C3DA815</orderNo>  <orderPageUrlFroAdmin> </orderPageUrlFroAdmin>  <pageUrl></pageUrl>  <payMethod>MONEY</payMethod>  <payType>ONLINE</payType>  <paymentPlatform>ALIPAY_WAP</paymentPlatform>  <price>77</price>  <score>0</score>  <subject>锦江之星上海外滩滨江酒店</subject></payRequest> ";
            var response2 = HttpUtil.Post(data2, urlcurlwap, contentType: "application/xml");

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

        //以下为后台功能方法.---------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// 后台线路库
        /// </summary>
        public static string getlinesAd()
        {
            lineListsFac lineListsFac = new lineListsFac();
            DataSet ds = lineListsFac.GetList("");
            ConvertJson ConvertJson = new ConvertJson();
            string json = ConvertJson.ToJson(ds);
            return json;

        }
        /// <summary>
        /// 更新线路类型
        /// </summary>
        public static string updatelinesAd(string lineCategory, int lineid)
        {
            Trip.JinJiang.H5.Line model = new Line();
            model.lineCategory = lineCategory;
            model.lineId = lineid;
            lineListsFac lineListsFac = new lineListsFac();
            lineListsFac.UpdatelineCategory(model);
            return "";
        }

        /// <summary>
        /// 获取线路类型
        /// </summary>
        public static string getlinecategorys()
        {
            lineCategoryFac lineCategoryFac = new lineCategoryFac();
            DataSet ds = lineCategoryFac.GetList("");
            ConvertJson ConvertJson = new ConvertJson();
            string json = ConvertJson.ToJson(ds);
            return json;
        }

        /// <summary>
        /// 线路类型编辑
        /// </summary>
        public static string editlinecategory(string categoryName, string lineCategory ,string imgUrl)
        {
            lineCategoryMod model = new lineCategoryMod();
            model.lineCategory = lineCategory;
            model.categoryName = categoryName;
            model.imgUrl = imgUrl;
            lineCategoryFac Fac = new lineCategoryFac();
            Fac.Add(model);
            return "";
        }

        /// <summary>
        /// 线路类型删除
        /// </summary>
        public static string dellinecategory(int Id)
        {
            lineCategoryFac Fac = new lineCategoryFac();
            Fac.Delete(Id);
            return "";
        }

    }
}
