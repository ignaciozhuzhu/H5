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
        private string _categoryName;
        private string _linecategory;
        private string _imgUrl;
        /// <summary>
        /// 
        /// </summary>
        public string categoryName
        {
            set { _categoryName = value; }
            get { return _categoryName; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string lineCategory
        {
            set { _linecategory = value; }
            get { return _linecategory; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string imgUrl
        {
            set { _imgUrl = value; }
            get { return _imgUrl; }
        }
        #endregion Model

    }
}

