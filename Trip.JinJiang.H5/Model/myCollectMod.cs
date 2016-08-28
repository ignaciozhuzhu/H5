using System;
namespace Trip.JinJiang.H5.Model
{
    /// <summary>
    /// myCollectMod:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class myCollectMod
    {
        public myCollectMod()
        { }
        #region Model
        private int _id;
        private string _title;
        private string _description;
        private int _price;
        private int? _date;
        private string _imgurl;
        private int _usermid;
        private int _lineid;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int price
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? date
        {
            set { _date = value; }
            get { return _date; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string imgurl
        {
            set { _imgurl = value; }
            get { return _imgurl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int userMID
        {
            set { _usermid = value; }
            get { return _usermid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int lineID
        {
            set { _lineid = value; }
            get { return _lineid; }
        }
        #endregion Model

    }
}

