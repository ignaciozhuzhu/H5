using System;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.SessionState;
using Trip.JinJiang.H5;

namespace Trip.JinJiang.H5Web.ajax
{
    /// <summary>
    /// apihandler 的摘要说明
    /// </summary>
    public class apihandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                Type type = this.GetType();
                string fn = context.Request["fn"].ToString();
                MethodInfo method = type.GetMethod(fn, BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
                method.Invoke(this, null);
            }
            catch (Exception e)
            {
                HttpContext.Current.Response.Write("接口出错!");
            }
        }

        private void ResponseWriteEnd(HttpContext context, string msg)
        {
            context.Response.Write(msg);
            context.Response.End();
        }

        /// <summary>
        /// 获取线路列表(类型)
        /// </summary>
        /// <param:lineCategory-->GROUPTRIP("跟团游"), FREETRIP("自由行"), SHIP("邮轮");>
        public void getlines()
        {
            string lineCategory = "";
            try
            {
                lineCategory = (HttpContext.Current.Request["linecategory"]).ToString();
            }
            catch (Exception e)
            {
            }
            HttpContext.Current.Response.Write(JJH5Api.Find());
        }

        /// <summary>
        /// 获取线路列表(类型)
        /// </summary>
        /// <param:lineCategory-->GROUPTRIP("跟团游"), FREETRIP("自由行"), SHIP("邮轮");>
        public void getlinesbycategory()
        {
            string lineCategory = "";
            try
            {
                lineCategory = (HttpContext.Current.Request["linecategory"]).ToString();
            }
            catch (Exception e)
            {
            }
            string businessCategory = "";
            try
            {
                businessCategory = (HttpContext.Current.Request["businessCategory"]).ToString();
            }
            catch (Exception e)
            {
            }
            string keyWord = "";
            try
            {
                keyWord = (HttpContext.Current.Request["keyWord"]).ToString();
            }
            catch (Exception e)
            {
            }
            HttpContext.Current.Response.Write(JJH5Api.getlinesByCategory(lineCategory, businessCategory, keyWord));
        }

        /// <summary>
        /// 促销路线(暂时理解为热门路线)
        /// 新 修改为动态后台设置获取,S2样式归属
        /// </summary>
        public void getlinespromotion()
        {
            try
            {
                string pattern = (HttpContext.Current.Request["pattern"]).ToString();
                HttpContext.Current.Response.Write(JJH5Api.FindPromotion(pattern));
            }
            catch (Exception e)
            {
            }
        }

        public void getlinecategoriecrm()
        {
            try
            {
                string category = (HttpContext.Current.Request["category"]).ToString();
                HttpContext.Current.Response.Write(JJH5Api.Getlinecategoriecrm(category));
            }
            catch (Exception e)
            {
            }
        }
        public void getlinecategoriecrm2()
        {
            try
            {
                string category = (HttpContext.Current.Request["category"]).ToString();
                HttpContext.Current.Response.Write(JJH5Api.Getlinecategoriecrm2(category));
            }
            catch (Exception e)
            {
            }
        }

        /// <summary>
        /// 线路详情
        /// </summary>
        public void getlinedetail()
        {
            try
            {
                int lineid = Convert.ToInt32(HttpContext.Current.Request["lineid"]);
                HttpContext.Current.Response.Write(JJH5Api.LineDetail(lineid));
            }
            catch (Exception e)
            {
            }
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        public void createorder()
        {
            try
            {
                string json = HttpContext.Current.Request["json"];
                HttpContext.Current.Response.Write(JJH5Api.CreateOrder(json));
            }
            catch (Exception e)
            {
            }
        }


        /// <summary>
        /// 查询库存 ,实时价格
        /// </summary>
        public void queryrealtimerefresh()
        {
            try
            {
                int groupid = Convert.ToInt32(HttpContext.Current.Request["groupid"]);
                HttpContext.Current.Response.Write(JJH5Api.queryRealTimeRefresh(groupid));
            }
            catch (Exception e)
            {
            }
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        public void cancelorder()
        {
            string orderCode = "";
            try
            {
                orderCode = HttpContext.Current.Request["ordercode"];
            }
            catch (Exception e)
            {
            }
            try
            {
                HttpContext.Current.Response.Write(JJH5Api.cancelOrder(orderCode));
            }
            catch (Exception e)
            {
            }
        }
        //以下为后台功能方法.---------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// 后台线路库
        /// </summary>
        public void getlinesad()
        {
            string category = "";
            try
            {
                category = HttpContext.Current.Request["category"];
            }
            catch { }
            HttpContext.Current.Response.Write(Admin.getlinesAd(category));
        }
        /// <summary>
        /// 后台线路库(带筛选)
        /// </summary>
        public void getlinesadsearch()
        {
            string search = "";
            try
            {
                search = HttpContext.Current.Request["search"];
            }
            catch { }
            HttpContext.Current.Response.Write(Admin.getlinesadsearch(search));
        }
        /// <summary>
        /// 后台线路库(带筛选(推荐产品查询区))
        /// </summary>
        public void getlinesadsearch2()
        {
            var linenamesc = "";
            var agencysc = "";
            var lineidsc = "";
            var categorysc = "";
            var agencysc2 = "";
            try
            {
                linenamesc = HttpContext.Current.Request["linenamesc"];
                agencysc = HttpContext.Current.Request["agencysc"];
                lineidsc = HttpContext.Current.Request["lineidsc"];
                categorysc = HttpContext.Current.Request["categorysc"];
                agencysc2 = HttpContext.Current.Request["agencysc2"];
            }
            catch { }
            HttpContext.Current.Response.Write(Admin.getlinesadsearch2(linenamesc, agencysc, lineidsc, categorysc, agencysc2));
        }
        /// <summary>
        /// 更新线路的类型
        /// </summary>
        public void updatelinesad()
        {
            string lineCategory = HttpContext.Current.Request["lineCategory"];
            int lineid = Convert.ToInt32(HttpContext.Current.Request["lineid"]);
            HttpContext.Current.Response.Write(Admin.updatelinesAd(lineCategory, lineid));
        }

        //-----------------------------线路分类管理 bg
        /// <summary>
        /// 获取线路类型
        /// </summary>
        public void getlinecategorys0()
        {
            HttpContext.Current.Response.Write(Admin.getlinecategorys0());
        }
        /// <summary>
        /// 获取线路类型
        /// </summary>
        public void getlinecategorys()
        {
            string status = "";
            try
            {
                status = HttpContext.Current.Request["status"];
            }
            catch { }
            HttpContext.Current.Response.Write(Admin.getlinecategorys(status));
        }
        public void getlinecategorys2()
        {
            string status = "";
            try
            {
                status = HttpContext.Current.Request["status"];
            }
            catch { }
            string pattern = "";
            try
            {
                pattern = HttpContext.Current.Request["pattern"];
            }
            catch { }
            HttpContext.Current.Response.Write(Admin.getlinecategorys2(status, pattern));
        }
        public void getlinecategorys3()
        {
            string status = "";
            try
            {
                status = HttpContext.Current.Request["status"];
            }
            catch { }
            string pattern = "";
            try
            {
                pattern = HttpContext.Current.Request["pattern"];
            }
            catch { }
            HttpContext.Current.Response.Write(Admin.getlinecategorys3(status, pattern));
        }

        /// <summary>
        /// 线路类型禁(可)用
        /// </summary>
        public void enlinecategory()
        {
            int Id = Convert.ToInt32(HttpContext.Current.Request["Id"]);
            HttpContext.Current.Response.Write(Admin.enlinecategory(Id));
        }
        /// <summary>
        /// 线路类型删除
        /// </summary>
        public void dellinecategory()
        {
            int Id = Convert.ToInt32(HttpContext.Current.Request["Id"]);
            HttpContext.Current.Response.Write(Admin.dellinecategory(Id));
        }
        //-----------------------------线路分类管理   ed


        public void pbppayorder()
        {
            string orderNo = HttpContext.Current.Request["orderNo"].ToString();
            decimal payAmount = Convert.ToDecimal(HttpContext.Current.Request["payAmount"]);
            string accountName = HttpContext.Current.Request["accountName"].ToString();
            string txnTime = "";
            string smscode = "";
            int score = 0;
            string Membercode = "";
            try { txnTime = HttpContext.Current.Request["txnTime"].ToString(); }
            catch { }
            try { smscode = HttpContext.Current.Request["smscode"].ToString(); }
            catch { }
            try { score = Convert.ToInt32(HttpContext.Current.Request["score"]); }
            catch { }
            try { Membercode = HttpContext.Current.Request["Membercode"].ToString(); }
            catch { }
            HttpContext.Current.Response.Write(JJH5Api.pbppayorder(orderNo, payAmount, accountName, txnTime, smscode, score, Membercode));
        }
        /// <summary>
        /// 周边游查询
        /// </summary>
        public void getnearby()
        {
            HttpContext.Current.Response.Write(JJH5Api.GetNearBy());
        }


        /// <summary>
        /// 旅行社列表
        /// </summary>
        public void getagencies()
        {
            HttpContext.Current.Response.Write(JJH5Api.getagencies());
        }

        /// <summary>
        /// 取线路价格
        /// </summary>
        public void getlinesprice()
        {
            string lineArr = (HttpContext.Current.Request["linearr"]).ToString();
            HttpContext.Current.Response.Write(JJH5Api.getlinesprice(lineArr));
        }
        /// <summary>
        /// 支付回调
        /// </summary>
        public void funpaycallback()
        {
            string ordercode = (HttpContext.Current.Request["ordercode"]).ToString();
            string payamount = (HttpContext.Current.Request["payamount"]).ToString();

            HttpContext.Current.Response.Write(JJH5Api.funpaycallback(ordercode, payamount));
        }



        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}