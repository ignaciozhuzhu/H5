using System;
namespace Trip.JinJiang.H5.Model
{
    /// <summary>
    /// emptySearchMod:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class emptySearchMod
    {
        public emptySearchMod()
        { }
        #region Model
        private int _id;
        private string _searchname;
        private bool _status = true;
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
        public string searchName
        {
            set { _searchname = value; }
            get { return _searchname; }
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
        public string H5Url
        {
            set { _H5Url = value; }
            get { return _H5Url; }
        }
        
        #endregion Model

    }
}

