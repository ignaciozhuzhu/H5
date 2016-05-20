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
        private int _Id;
        /// <summary>
        /// 分类名称
        /// </summary>
        public string categoryName
        {
            set { _categoryName = value; }
            get { return _categoryName; }
        }
        /// <summary>
        /// 分类编码
        /// </summary>
        public string lineCategory
        {
            set { _linecategory = value; }
            get { return _linecategory; }
        }
        /// <summary>
        /// 图标
        /// </summary>
        public string imgUrl
        {
            set { _imgUrl = value; }
            get { return _imgUrl; }
        }
        /// <summary>
        /// 自增Id
        /// </summary>
        public int Id
        {
            set { _Id = value; }
            get { return _Id; }
        }
        #endregion Model

    }
}

