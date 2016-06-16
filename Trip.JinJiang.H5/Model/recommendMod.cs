using System;
namespace Trip.JinJiang.H5.Model
{
    /// <summary>
    /// recommendMod:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class recommendMod
    {
        public recommendMod()
        { }
        #region Model
        private int _id;
        private int _lineid;
        private string _linetitle;
        private string _travelagency;
        private int _order;
        private string _imgurl;
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
        public int lineId
        {
            set { _lineid = value; }
            get { return _lineid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string lineTitle
        {
            set { _linetitle = value; }
            get { return _linetitle; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string travelAgency
        {
            set { _travelagency = value; }
            get { return _travelagency; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int order
        {
            set { _order = value; }
            get { return _order; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string imgUrl
        {
            set { _imgurl = value; }
            get { return _imgurl; }
        }
        #endregion Model

    }
}

