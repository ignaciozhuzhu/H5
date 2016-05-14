/*
 * 在线用户模型
 * 作者：金君耀
 * 时间：2013-2-11 3：19
 * 作用：登录用户的基本模型用于记录用户状态。
 */

using System;
using System.Collections.Generic;
namespace LW.Common.Model
{
    /// <summary>
    /// Users:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    /// 
    [Serializable]

    public partial class Org
    {
        public Org() { }
        #region Model
        private string _UnitID;
        private string _UnitName;
        private string _UnitCode;
        private string _bDepID;
        private string _bDepName;
        private string _bJobID;
        private string _bJobName;
        private string _heID;

        public string UnitCode
        {
            get { return _UnitCode; }
            set { _UnitCode = value; }
        }

        public string UnitID
        {
            get { return _UnitID; }
            set { _UnitID = value; }
        }

        public string UnitName
        {
            get { return _UnitName; }
            set { _UnitName = value; }
        }


        public string BDepID
        {
            get { return _bDepID; }
            set { _bDepID = value; }
        }

        public string BDepName
        {
            get { return _bDepName; }
            set { _bDepName = value; }
        }


        public string BJobID
        {
            get { return _bJobID; }
            set { _bJobID = value; }
        }

        public string BJobName
        {
            get { return _bJobName; }
            set { _bJobName = value; }
        }

        public string code
        {
            set;
            get;
        }
        public string codeTree { set; get; }
        public string StCode { set; get; }
        public string stID { set; get; }

        public string HeID
        {
            get { return _heID; }
            set { _heID = value; }
        }

        #endregion Model

    }

    [Serializable]

    public partial class OLUsers
    {
        public OLUsers()
        {
            _errorBout = 0;
        }
        #region Model
        private string _userid;
        private string _loginname;
        private string _email;
        private string _phone;
        private string _connectionString;
        private DateTime _loginTime;
        private int _errorBout = 0;
        private string _errorAbout;
        private string _UserName;
        private string _Sub;
        private string _UnitID;
        private string _UnitName;
        private string _bDepID;
        private string _bDepName;
        private string _bJobID;
        private string _bJobName;
        private string _heID;
        private string _sob;
        private string _stID;
        private string _UnitCode;

        public string UnitCode
        {
            get { return _UnitCode; }
            set { _UnitCode = value; }
        }

        public string stID
        {
            get { return _stID; }
            set { _stID = value; }
        }

        public string code { set; get; }

        /// <summary>
        /// 账套
        /// </summary>
        public string Sob
        {
            get { return _sob; }
            set { _sob = value; }
        }
        private List<Org> _OrgList;


        public List<Org> OrgList
        {
            get { return _OrgList; }
            set { _OrgList = value; }
        }


        public string UnitID
        {
            get { return _UnitID; }
            set { _UnitID = value; }
        }

        public string UnitName
        {
            get { return _UnitName; }
            set { _UnitName = value; }
        }


        public string BDepID
        {
            get { return _bDepID; }
            set { _bDepID = value; }
        }

        public string BDepName
        {
            get { return _bDepName; }
            set { _bDepName = value; }
        }


        public string BJobID
        {
            get { return _bJobID; }
            set { _bJobID = value; }
        }

        public string BJobName
        {
            get { return _bJobName; }
            set { _bJobName = value; }
        }


        //public string StCode
        //{
        //    get { return _stCode; }
        //    set { _stCode = value; }
        //}

        public string HeID
        {
            get { return _heID; }
            set { _heID = value; }
        }

        /// <summary>
        /// 隶属关系
        /// </summary>
        public string Sub
        {
            get { return _Sub; }
            set { _Sub = value; }
        }
        /// <summary>
        /// 员工信息
        /// </summary>
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        /// <summary>
        /// 错误原因
        /// </summary>
        public string ErrorAbout
        {
            get { return _errorAbout; }
            set { _errorAbout = value; }
        }

        /// <summary>
        /// 错误次数
        /// </summary>
        public int ErrorBout
        {
            get { return _errorBout; }
            set { _errorBout = value; }
        }

        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime LoginTime
        {
            get { return _loginTime; }
            set { _loginTime = value; }
        }

        /// <summary>
        /// 登录帐套连接字符串
        /// </summary>
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }
        /// <summary>
        /// ID
        /// </summary>
        public string UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName
        {
            set { _loginname = value; }
            get { return _loginname; }
        }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }

        #endregion Model

    }
}

