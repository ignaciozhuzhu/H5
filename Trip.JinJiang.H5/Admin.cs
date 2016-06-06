using Trip.Core;
using System.Data;
using Newtonsoft.Json;
using System.Text;
using Maticsoft.DBUtility;
using Trip.JinJiang.H5.DAL;
using Trip.JinJiang.H5.Model;

namespace Trip.JinJiang.H5
{
    public class Admin
    {
        /// <summary>
        /// 获取后台线路库列表
        /// </summary>
        public static string getlinesAd(string category)
        {
            lineListsFac lineListsFac = new lineListsFac();
            DataSet ds = lineListsFac.GetList(category);
            ConvertJson ConvertJson = new ConvertJson();
            string json = ConvertJson.ToJson(ds);
            return json;

        }
        /// <summary>
        /// 更改线路的类型
        /// </summary>
        public static string updatelinesAd(string lineCategory, int lineid)
        {
            Trip.JinJiang.H5.Line model = new Line();
            model.lineCategory = lineCategory;
            model.lineId = lineid;
            lineListsFac lineListsFac = new lineListsFac();
            lineListsFac.UpdatelineCategory(model);
            return "";
        }

        //-----------------------------线路分类管理 bg
        /// <summary>
        /// 获取线路类型
        /// </summary>
        public static string getlinecategorys(string status)
        {
            string str = "";
            if (status != "" && status != null)
            {
                str = " status='true' ";
            }
            lineCategoryFac lineCategoryFac = new lineCategoryFac();
            DataSet ds = lineCategoryFac.GetList(str);
            ConvertJson ConvertJson = new ConvertJson();
            string json = ConvertJson.ToJson(ds);
            return json;
        }

        /// <summary>
        /// 线路类型添加
        /// </summary>
        public static string addlinecategory(string categoryName, string lineCategory, string imgUrl)
        {
            lineCategoryMod model = new lineCategoryMod();
            model.lineCategory = lineCategory;
            model.categoryName = categoryName;
            model.imgUrl = imgUrl;
            lineCategoryFac Fac = new lineCategoryFac();
            if (Fac.Add(model))
            {
                return "操作成功!";
            }
            else {
                return "操作失败!";
            }
        }
        /// <summary>
        /// 线路类型编辑
        /// </summary>
        public static string editlinecategory(string categoryName, string lineCategory, string imgUrl, int Id)
        {
            lineCategoryMod model = new lineCategoryMod();
            model.lineCategory = lineCategory;
            model.categoryName = categoryName;
            model.imgUrl = imgUrl;
            model.Id = Id;
            lineCategoryFac Fac = new lineCategoryFac();
            if (Fac.Update(model))
            {
                return "操作成功!";
            }
            else {
                return "操作失败!";
            }
        }

        /// <summary>
        /// 线路类型禁(可)用
        /// </summary>
        public static string enlinecategory(int Id)
        {
            lineCategoryFac Fac = new lineCategoryFac();
            Fac.ChangeStatus(Id);
            return "";
        }
        /// <summary>
        /// 线路类型删除
        /// </summary>
        public static string dellinecategory(int Id)
        {
            lineCategoryFac Fac = new lineCategoryFac();
            Fac.Delete(Id);
            return "";
        }
        //-----------------------------线路分类管理   ed

        //-----------------------------轮播图管理 bg
        /// <summary>
        /// 获取轮播图
        /// </summary>
        public static string getbannerimglist(string status)
        {
            string str = "";
            if (status != "" && status != null)
            {
                str = " status='true' ";
            }
            bannerImgFac Fac = new bannerImgFac();
            DataSet ds = Fac.GetList(str);
            ConvertJson ConvertJson = new ConvertJson();
            string json = ConvertJson.ToJson(ds);
            return json;
        }

        /// <summary>
        /// 轮播图添加
        /// </summary>
        public static string addbannerimg(string alt, string imgUrl,int lineId)
        {
            bannerImgMod model = new bannerImgMod();
            model.alt = alt;
            model.imgUrl = imgUrl;
            model.lineId = lineId;
            bannerImgFac Fac = new bannerImgFac();
            if (Fac.Add(model) > 0)
            {
                return "操作成功!";
            }
            else {
                return "操作失败!";
            }
        }
        /// <summary>
        /// 轮播图编辑
        /// </summary>
        public static string editbannerimg(string alt, string imgUrl, int Id,int lineId)
        {
            bannerImgMod model = new bannerImgMod();
            model.alt = alt;
            model.imgUrl = imgUrl;
            model.Id = Id;
            model.lineId = lineId;
            bannerImgFac Fac = new bannerImgFac();
            if (Fac.Update(model))
            {
                return "操作成功!";
            }
            else {
                return "操作失败!";
            }
        }
        /// <summary>
        /// 轮播图管理禁(可)用
        /// </summary>
        public static string enbannerimg(int Id)
        {
            bannerImgFac Fac = new bannerImgFac();
            Fac.ChangeStatus(Id);
            return "";
        }
        /// <summary>
        /// 轮播图管理删除
        /// </summary>
        public static string delbannerimg(int Id)
        {
            bannerImgFac Fac = new bannerImgFac();
            Fac.Delete(Id);
            return "";
        }
        //-----------------------------轮播图管理   ed

        //-----------------------------目的地标签管理 bg
        /// <summary>
        /// 获取目的地标签
        /// </summary>
        public static string getarealist()
        {
            areaFac Fac = new areaFac();
            DataSet ds = Fac.GetList("");
            ConvertJson ConvertJson = new ConvertJson();
            string json = ConvertJson.ToJson(ds);
            return json;
        }

        /// <summary>
        /// 目的地标签添加
        /// </summary>
        public static string addarea(string areaName, int order)
        {
            areaMod model = new areaMod();
            model.areaName = areaName;
            model.order = order;
            areaFac Fac = new areaFac();
            if (Fac.Add(model) > 0)
            {
                return "操作成功!";
            }
            else {
                return "操作失败!";
            }
        }
        /// <summary>
        /// 目的地标签编辑
        /// </summary>
        public static string editarea(string areaName, int order, int Id)
        {
            areaMod model = new areaMod();
            model.areaName = areaName;
            model.order = order;
            model.Id = Id;
            areaFac Fac = new areaFac();
            if (Fac.Update(model))
            {
                return "操作成功!";
            }
            else {
                return "操作失败!";
            }
        }
        /// <summary>
        /// 目的地标签管理禁用
        /// </summary>
        public static string enarea(int Id)
        {
            areaFac Fac = new areaFac();
            Fac.ChangeStatus(Id);
            return "";
        }
        //-----------------------------目的地标签管理 ed

        //-----------------------------目的地标签二级管理 bg
        /// <summary>
        /// 获取目的地标签
        /// </summary>
        public static string getarea2list(int aid)
        {
            destFac Fac = new destFac();
            DataSet ds = Fac.GetList("A_Id=" + aid + "");
            ConvertJson ConvertJson = new ConvertJson();
            string json = ConvertJson.ToJson(ds);
            return json;
        }

        /// <summary>
        /// 目的地标签添加
        /// </summary>
        public static string addarea2(int aId, string destName, int order)
        {
            destMod model = new destMod();
            model.A_Id = aId;
            model.destName = destName;
            model.order = order;
            destFac Fac = new destFac();
            if (Fac.Add(model) > 0)
            {
                return "操作成功!";
            }
            else {
                return "操作失败!";
            }
        }
        /// <summary>
        /// 目的地标签编辑
        /// </summary>
        public static string editarea2(int aId, string destName, int order, int Id)
        {
            destMod model = new destMod();
            model.A_Id = aId;
            model.destName = destName;
            model.order = order;
            model.Id = Id;
            destFac Fac = new destFac();
            if (Fac.Update(model))
            {
                return "操作成功!";
            }
            else {
                return "操作失败!";
            }
        }
        /// <summary>
        /// 目的地标签管理禁用
        /// </summary>
        public static string enarea2(int Id)
        {
            destFac Fac = new destFac();
            Fac.ChangeStatus(Id);
            return "";
        }
        //-----------------------------目的地标签二级管理 ed

        //-----------------------------空搜关键词管理 bg
        /// <summary>
        /// 获取空搜关键词
        /// </summary>
        public static string getarea3list()
        {
            emptySearchFac Fac = new emptySearchFac();
            DataSet ds = Fac.GetList("");
            ConvertJson ConvertJson = new ConvertJson();
            string json = ConvertJson.ToJson(ds);
            return json;
        }

        /// <summary>
        /// 空搜关键词添加
        /// </summary>
        public static string addarea3(string searchName)
        {
            emptySearchMod model = new emptySearchMod();
            model.searchName = searchName;
            emptySearchFac Fac = new emptySearchFac();
            if (Fac.Add(model) > 0)
            {
                return "操作成功!";
            }
            else {
                return "操作失败!";
            }
        }
        /// <summary>
        /// 空搜关键词编辑
        /// </summary>
        public static string editarea3(string searchName , int Id)
        {
            emptySearchMod model = new emptySearchMod();
            model.searchName = searchName;
            model.Id = Id;
            emptySearchFac Fac = new emptySearchFac();
            if (Fac.Update(model))
            {
                return "操作成功!";
            }
            else {
                return "操作失败!";
            }
        }
        /// <summary>
        /// 空搜关键词禁用
        /// </summary>
        public static string enarea3(int Id)
        {
            emptySearchFac Fac = new emptySearchFac();
            Fac.ChangeStatus(Id);
            return "";
        }
        //-----------------------------空搜关键词管理 ed
    }
}
