using System;
namespace Trip.JinJiang.H5.Model
{
    /// <summary>
    /// destMod:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class destMod
    {
        public destMod()
        { }
        #region Model
        private int _id;
        private int? _a_id;
        private string _destname;
        private bool _status = true;
        private int _order = 1;
        private string _H5Url;
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
        public int? A_Id
        {
            set { _a_id = value; }
            get { return _a_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string destName
        {
            set { _destname = value; }
            get { return _destname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int order
        {
            set { _order = value; }
            get { return _order; }
        }

        public string H5Url
        {
            set { _H5Url = value; }
            get { return _H5Url; }
        }
        #endregion Model

    }
}

