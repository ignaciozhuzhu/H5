using System;
namespace Trip.JinJiang.H5.Model
{
    /// <summary>
    /// lineCategoryMod:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class lineCategoryMod
    {
        public lineCategoryMod()
        { }
        #region Model
        private string _linecode;
        private string _linecategory;
        /// <summary>
        /// 
        /// </summary>
        public string lineCode
        {
            set { _linecode = value; }
            get { return _linecode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string lineCategory
        {
            set { _linecategory = value; }
            get { return _linecategory; }
        }
        #endregion Model

    }
}

