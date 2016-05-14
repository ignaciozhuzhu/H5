using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Trip.Core;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;

using LW.Common.DB;
using LW.Common.Json;

namespace Trip.JinJiang.H5
{
    /// <summary>
    /// 
    /// </summary>
    public class JJH5Api
    {
        static string conn = System.Configuration.ConfigurationSettings.AppSettings["conn"]; //System.Configuration.ConfigurationManager.AppSettings["conn"];
        //线路搜索接口地址
        // private string URL1
        private const string URL0 = "http://travelsearchservice.jinjiang.uat/travelsearchservice/travel/search/searchTravel";   //线路列表接口
        private const string URL5 = "http://travelbaseservice.jinjiang.uat/travelbaseservice/travel/line/queryCMSLineInfo";     //CMS线路详情接口
        private const string URL6 = "http://travelbaseservice.jinjiang.uat/travelbaseservice/travel/line/queryLinesForCMS";     //CMS线路列表接口
        private const string urlprepay = "http://travelbaseservice.jinjiang.uat/travelbaseservice/travel/payment/prepay";   //预支付接口
        private const string urlsearchorder = "http://travelbaseservice.jinjiang.uat/travelbaseservice/travel/order/queryOrderList";    //订单查询接口
        private const string urlcreateorder = "http://travelbaseservice.jinjiang.uat/travelbaseservice/travel/order/create";    //创建订单接口
        private const string urlinventory = "http://travelbaseservice.jinjiang.uat/travelbaseservice/travel/group/queryRealTimeRefresh/";   //查询库存 ,实时价格接口
        private const string urlcancelorder = "http://travelbaseservice.jinjiang.uat/travelbaseservice/travel/order/cancel";    //取消订单接口
        /// <summary>
        /// 查询路线
        /// </summary>
        public static string Find()
        {
            var data = "{\"cluster\":1}";
            data = "{\"page\":{\"endRow\":10,\"page\":1,\"records\":0,\"rows\":50,\"search\":false,\"startRow\":1,\"total\":8}}";
            //   var data2 = "{\"bgUrl\":\"\",\"callPart\":\"TRAVEL\",\"orderNo\":\"1000160323000023\",\"payMethod\":\"MONEY\",\"payType\":\"ONLINE\",\"productTitle\":\"旅游测试title\",\"payAmount\":\"10\"}";

            var data3 = "{\"endBookingDate\":\"2016-05-14\",\"mcMemberCode\":\"1231234\",\"orderCode\":\"\",\"orderStatus\":\"\",\"pagination\":{\"endRow\":10,\"page\":2,\"records\":0,\"rows\":10,\"search\":false,\"startRow\":1,\"total\":1},\"payStatus\":\"PAY_WAITING\",\"startBookingDate\":\"2016-05-10\"}";
            // var response = HttpUtil.Post(data, URL0, contentType: "application/json");
            var response = HttpUtil.Post(data, URL0, contentType: "application/json");
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
            string str = "select lineId,lineCategory from dbo.linecategory where lineCategory='" + category + "'";
            DataSet ds = Common.fillds(str);
            string json = Common.DataTable2Array(ds.Tables[0]);
            json = "{\"rows\":" + json.Replace("'", "\"") + "}";
            return json;
        }

        /// <summary>
        /// 查询路线
        /// </summary>
        public static string getlinesByCategory(string lineCategory)
        {
            var data = "{\"lineCategory\":\"" + lineCategory + "\"}";
            //   if(lineCategory=="")
            data = "{\"page\":{\"endRow\":10,\"page\":1,\"records\":0,\"rows\":100,\"search\":false,\"startRow\":1,\"total\":8}}";

            //先获取所有的线路,再与本地数据库做关联
            var response = HttpUtil.Post(data, URL0, contentType: "application/json");

            //   response = "[{\"ViewId\":14,\"Name\":\"view 1\",\"IsValid\":true,\"Seq\":\"1\",\"ChangeType\":0}]";
            //  response = "[{\"lineId\": 10109,\"lineName\":\"5天4晚跟团游,YT哈尔滨/亚布力激情滑雪/中国雪乡双飞五日尊享游\",\"name\": \"5天4晚跟团游\"}]";

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
            string str = "select * from dbo.linecategory";
            DataSet ds = Common.fillds(str);

            var data = "{\"isPromotion\":1}";
            data = "{\"page\":{\"endRow\":10,\"page\":1,\"records\":80,\"rows\":80,\"search\":false,\"startRow\":1,\"total\":80}}";
            var response = HttpUtil.Post(data, URL0, contentType: "application/json");
            response = maps.mapAgencies(response);
            // string tourl = "https://openauth.alipay.com/oauth2/appToAppAuth.htm?app_id=APPID&redirect_uri=ENCODED_URL";
            //   var data2 = "{\"bgUrl\":\""+ tourl + "\",\"callPart\":\"TRAVEL\",\"orderNo\":\"1000160323000023\",\"payMethod\":\"MONEY\",\"payType\":\"ONLINE\",\"productTitle\":\"旅游测试title\",\"payAmount\":\"10\",\"payChannel\":\"ALIPAY\"}";

            //  response = HttpUtil.Post(data2, urlprepay, contentType: "application/json");

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
            string URL1 = "http://travelbaseservice.jinjiang.uat/travelbaseservice/travel/line/";    //某线路详情
            URL1 += lineid;
            var response = HttpUtil.Get(URL1);
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
            var json3 = HttpUtil.Post(data, URL0, contentType: "application/json");

            var result3 = JsonConvert.DeserializeObject<LineListModel>(json3);

            string a = "insert into ";

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
        public static string cancelOrder(string userid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from LineCategory ");
            DataTable dt = DataSource.ExecuteQuery(strSql.ToString(), conn);
            if ((Convert.ToInt32(dt.Rows[0][0])) > 0)
            {
                return "";
            }
            else
            {
                return "";
            }

            //strSql.Append("select * from LineCategory");
            //int j = DataSource.ExecuteSql(strSql.ToString(), conn);
            //if (j == 0)
            //{
            //    return JsonMessage.FailResponse("未改变任何数据集!");
            //}
            //else
            //{
            //    return JsonMessage.SuccessString("操作成功!");
            //}
        }


    }
}
