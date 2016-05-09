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

namespace Trip.JinJiang.H5
{
    /// <summary>
    /// 
    /// </summary>
    public class JJH5Api
    {
        string conn = System.Configuration.ConfigurationSettings.AppSettings["conn"];
        //线路搜索接口地址
        // private string URL1
        private const string URL0 = "http://travelsearchservice.jinjiang.uat/travelsearchservice/travel/search/searchTravel";   //线路列表接口
        private const string URL5 = "http://travelbaseservice.jinjiang.uat/travelbaseservice/travel/line/queryCMSLineInfo";     //CMS线路详情接口
        private const string URL6 = "http://travelbaseservice.jinjiang.uat/travelbaseservice/travel/line/queryLinesForCMS";     //CMS线路列表接口
        private const string urlprepay = "http://travelbaseservice.jinjiang.uat/travelbaseservice/travel/payment/prepay";
        private const string urlsearchorder = "http://travelbaseservice.jinjiang.uat/travelbaseservice/travel/order/queryOrderList";
        /// <summary>
        /// 查询路线
        /// </summary>
        public static string Find()
        {
            var data = "{\"cluster\":1}";
            data = "{\"page\":{\"endRow\":10,\"page\":1,\"records\":0,\"rows\":50,\"search\":false,\"startRow\":1,\"total\":8}}";
            //   var data2 = "{\"bgUrl\":\"\",\"callPart\":\"TRAVEL\",\"orderNo\":\"1000160323000023\",\"payMethod\":\"MONEY\",\"payType\":\"ONLINE\",\"productTitle\":\"旅游测试title\",\"payAmount\":\"10\"}";

            //   var data3="{\"endBookingDate\":\"2016-03-22\",\"mcMemberCode\":\"\",\"orderCode\":\"\",\"orderStatus\":\"\",\"pagination\":{\"endRow\":10,\"page\":2,\"records\":0,\"rows\":10,\"search\":false,\"startRow\":1,\"total\":1},\"payStatus\":\"PAY_WAITING\",\"startBookingDate\":\"2016-03-21\"}";
            // var response = HttpUtil.Post(data, URL0, contentType: "application/json");
            var response = HttpUtil.Post(data, URL0, contentType: "application/json");
            //   var response3 = HttpUtil.Post(data3, urlsearchorder, contentType: "application/json");

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
            string constr = "Data Source=119.37.192.106,14330;Initial Catalog=devtrip;User ID=trip;Password=Travel@2016";

            SqlConnection con = new SqlConnection(constr); //注意与上面的区分开
            con.Open();
            //  DataSet ds = new DataSet(); //创建数据集对象
            //  dbAdapter.Fill(ds);
            SqlCommand lo_cmd = new SqlCommand();   //创建命令对象
            lo_cmd.CommandText = "select * from dbo.linecategory";   //写SQL语句
            lo_cmd.Connection = con;             //指定连接对象，即上面创建的
                                                 // SqlDataReader lo_reader = lo_cmd.ExecuteReader();//返回结果集
            SqlDataAdapter dbAdapter = new SqlDataAdapter(lo_cmd); //注意与上面的区分开
            DataSet ds = new DataSet(); //创建数据集对象
            dbAdapter.Fill(ds);

            var data = "{\"isPromotion\":1}";
            data = "{\"page\":{\"endRow\":10,\"page\":1,\"records\":0,\"rows\":50,\"search\":false,\"startRow\":1,\"total\":8}}";
            var response = HttpUtil.Post(data, URL0, contentType: "application/json");
            response = maps.mapAgencies(response);

            //  var data2 = "{\"bgUrl\":\"www.baidu.com\",\"callPart\":\"TRAVEL\",\"orderNo\":\"1000160323000023\",\"payMethod\":\"MONEY\",\"payType\":\"ONLINE\",\"productTitle\":\"旅游测试title\",\"payAmount\":\"10\",\"payChannel\":\"ALIPAY\"}";

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






    }
}
