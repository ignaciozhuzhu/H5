using System;
namespace Trip.JinJiang.H5.Model
{
    /// <summary>
    /// areaMod:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class areaMod
    {
        public areaMod()
        { }
        #region Model
        private int _id;
        private string _areaname;
        private bool _status = true;
        private int _order = 1;
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
        public string areaName
        {
            set { _areaname = value; }
            get { return _areaname; }
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
        #endregion Model

    }
}

