using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.SessionState;
using Trip.JinJiang.H5;

namespace Trip.JinJiang.H5Web.ajax
{
    /// <summary>
    /// userHandler 的摘要说明
    /// </summary>
    public class userHandler : IHttpHandler, IRequiresSessionState
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

        /// <summary>
        /// 注册新用户
        /// </summary>
        public void regist()
        {
            string xml = HttpContext.Current.Request["xml"].ToString();
            HttpContext.Current.Response.Write(User.regist(xml));
        }
        /// <summary>
        /// 登录
        /// </summary>
        public void login()
        {
            string xml = HttpContext.Current.Request["xml"].ToString();
            HttpContext.Current.Response.Write(User.login(xml));
        }
        /// <summary>
        /// 注册1(快速注册)
        /// </summary>
        public void quickregist()
        {
            string xml = HttpContext.Current.Request["xml"].ToString();
            HttpContext.Current.Response.Write(User.quickregist(xml));
        }
        //----------------------------------------------密码找回4个步骤 bg
        /// <summary>
        /// 忘记密码并设置新密码
        /// </summary>
        public void sendvalidatecode()
        {
            string loginName = HttpContext.Current.Request["loginName"].ToString();
            HttpContext.Current.Response.Write(User.sendvalidatecode(loginName));
        }
        public void checkmember()
        {
            string loginName = HttpContext.Current.Request["loginName"].ToString();
            HttpContext.Current.Response.Write(User.checkmember(loginName));
        }
        public void checkmemberfullname()
        {
            string loginName = HttpContext.Current.Request["loginName"].ToString();
            string fullName = HttpContext.Current.Request["fullName"].ToString();
            HttpContext.Current.Response.Write(User.checkmemberfullname(loginName, fullName));
        }
        public void forgetpwd()
        {
            string validateCode = HttpContext.Current.Request["validateCode"].ToString();
            string loginName = HttpContext.Current.Request["loginName"].ToString();
            string fullName = HttpContext.Current.Request["fullName"].ToString();
            string md5 = HttpContext.Current.Request["md5"].ToString();
            string sha1 = HttpContext.Current.Request["sha1"].ToString();
            HttpContext.Current.Response.Write(User.forgetpwd(loginName, fullName, validateCode, md5, sha1));
        }
        //----------------------------------------------密码找回4个步骤 ed

        public void queryorder()
        {
            string mcMemberCode = HttpContext.Current.Request["mcMemberCode"].ToString();
            string orderStatus = HttpContext.Current.Request["orderStatus"].ToString();
            string payStatus = HttpContext.Current.Request["payStatus"].ToString();
            HttpContext.Current.Response.Write(User.queryorder(mcMemberCode, orderStatus, payStatus));
        }
        public void queryorderdetail()
        {
            string code = HttpContext.Current.Request["code"].ToString();
            HttpContext.Current.Response.Write(User.queryorderdetail(code));
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        public void cancelorder()
        {
            string orderCode = HttpContext.Current.Request["ordercode"];
            string mcMemberCode = HttpContext.Current.Request["mcMemberCode"];
            try
            {
                HttpContext.Current.Response.Write(User.cancelOrder(orderCode, mcMemberCode));
            }
            catch (Exception e)
            {
            }
        }

        //以下短注册,1 获取验证码 2 核对验证码 3 允许快速注册.
        /// <summary>
        /// 获取验证码
        /// </summary>
        public void sendvalidatecode4reg()
        {
            string phone = HttpContext.Current.Request["phone"].ToString();
            HttpContext.Current.Response.Write(User.sendvalidatecode4reg(phone));
        }
        /// <summary>
        /// 核对验证码
        /// </summary>
        public void checkvalidatecode4reg()
        {
            string phone = HttpContext.Current.Request["phone"].ToString();
            string validate = HttpContext.Current.Request["code"].ToString();
            HttpContext.Current.Response.Write(User.checkvalidatecode4reg(phone, validate));
        }



        //以下 积分,优惠券-----------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// 查看个人积分
        /// </summary>
        public void getmemberscoreinfo()
        {
            try
            {
                string Membercode = HttpContext.Current.Request["Membercode"];
                HttpContext.Current.Response.Write(User.getMemberScoreInfo(Membercode));
            }
            catch (Exception e)
            {
            }
        }
        /// <summary>
        /// 查看个人优惠券
        /// </summary>
        public void getmembercoupinfo()
        {
            try
            {
                string Membercode = HttpContext.Current.Request["Membercode"];
                HttpContext.Current.Response.Write(User.getMemberCoupInfo(Membercode));
            }
            catch (Exception e)
            {
            }
        }
        /// <summary>
        /// 查看当前可下单的优惠券
        /// </summary>
        public void getmembercouporderinfo()
        {
            try
            {
                string Membercode = HttpContext.Current.Request["Membercode"];
                HttpContext.Current.Response.Write(User.getMemberCoupOrderInfo(Membercode));
            }
            catch (Exception e)
            {
            }
        }
        /// <summary>
        /// 14.判断手机、邮箱是否已存在
        /// </summary>
        public void getvalidateemailortel()
        {
            try
            {
                string phone = HttpContext.Current.Request["phone"];
                HttpContext.Current.Response.Write(User.getvalidateEmailOrTel(phone));
            }
            catch (Exception e)
            {
            }
        }
        /// <summary>
        /// 下单环节的快速注册
        /// </summary>
        public void quickregistorder()
        {
            try
            {
                string xml = HttpContext.Current.Request["xml"];
                HttpContext.Current.Response.Write(User.quickregistorder(xml));
            }
            catch (Exception e)
            {
            }
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