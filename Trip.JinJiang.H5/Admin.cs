﻿using Trip.Core;
using System.Data;
using Newtonsoft.Json;
using System.Text;
using Maticsoft.DBUtility;
using Trip.JinJiang.H5.DAL;
using Trip.JinJiang.H5.Model;
using System;

namespace Trip.JinJiang.H5
{
    public class Admin
    {
        private static string jjh5Sserver = System.Configuration.ConfigurationManager.AppSettings["jjh5Sserver"];
        private static string jjh5Bserver = System.Configuration.ConfigurationManager.AppSettings["jjh5Bserver"];
        private static string urllinesearch = jjh5Sserver + "/travel/search/searchTravel";   //线路列表接口
        private static string urlcmslinesearch = jjh5Bserver + "/travel/line/queryLinesForCMS";   //CMS线路列表接口
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
        /// 获取后台线路库列表
        /// </summary>
        public static string getlinesadsearch(string search)
        {
            lineListsFac lineListsFac = new lineListsFac();
            DataSet ds = lineListsFac.GetListsearch(search);
            ConvertJson ConvertJson = new ConvertJson();
            string json = ConvertJson.ToJson(ds);
            return json;
        }
        /// <summary>
        /// 后台线路库(带筛选(推荐产品查询区))
        /// </summary>
        public static string getlinesadsearch2(string linenamesc, string agencysc, string lineidsc, string categorysc, string agencysc2)
        {
            string str = "";
            if (linenamesc != "")
            {
                str += ",\"lineName\":\"" + linenamesc + "\"";
            }
            if (agencysc != "")
            {
                str += ",\"brand\":\"" + agencysc + "\"";
            }
            if (lineidsc != "")
            {
                str += ",\"lineId\":\"" + lineidsc + "\"";
            }
            if (categorysc != "")
            {
                str += ",\"lineCategory\":\"" + categorysc + "\"";
            }
            if (agencysc2 != "")
            {
                str += ",\"agencies\":\"" + agencysc2 + "\"";
            }
            var data = "{\"page\":{\"endRow\":10,\"page\":1,\"records\":0,\"rows\":80,\"search\":false,\"startRow\":1,\"total\":8}" + str + "}";
            var response = HttpUtil.Post(data, urllinesearch, contentType: "application/json");
            //var result3 = JsonConvert.DeserializeObject<LineListModel>(response);
            return response;
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
        public static string getlinecategorys0()
        {
            lineCategoryFac lineCategoryFac = new lineCategoryFac();
            DataSet ds = lineCategoryFac.GetList0();
            ConvertJson ConvertJson = new ConvertJson();
            string json = ConvertJson.ToJson(ds);
            return json;
        }

        /// <summary>
        /// 获取线路类型
        /// </summary>
        public static string getlinecategorys(string status)
        {
            string str = "";
            if (status != "" && status != null)
            {
                str = " status='true' and pattern = 'S1'";
            }
            lineCategoryFac lineCategoryFac = new lineCategoryFac();
            DataSet ds = lineCategoryFac.GetList(str);
            ConvertJson ConvertJson = new ConvertJson();
            string json = ConvertJson.ToJson(ds);
            return json;
        }

        /// <summary>
        /// 获取线路类型
        /// </summary>
        public static string getlinecategorys2(string status, string pattern)
        {
            string str = "";
            //if (status != "" && status != null)
            //{
            //    str += " and b.status='true'";
            //}
            //if (pattern != "" && pattern != null)
            //{
            //    str += " and b.pattern = '" + pattern + "'";
            //}
            lineCategoryFac lineCategoryFac = new lineCategoryFac();
            DataSet ds = lineCategoryFac.GetList2(str);
            ConvertJson ConvertJson = new ConvertJson();
            string json = ConvertJson.ToJson(ds);
            return json;
        }

        /// <summary>
        /// 获取线路类型
        /// </summary>
        public static string getlinecategorys3(string status, string pattern)
        {
            string str = "";
            if (status != "" && status != null)
            {
                str += " and status='true'";
            }
            if (pattern != "" && pattern != null)
            {
                str += " and pattern = '" + pattern + "'";
            }
            lineCategoryFac lineCategoryFac = new lineCategoryFac();
            DataSet ds = lineCategoryFac.GetList3(str);
            ConvertJson ConvertJson = new ConvertJson();
            string json = ConvertJson.ToJson(ds);
            return json;
        }

        /// <summary>
        /// 线路类型添加
        /// </summary>
        public static string addlinecategory(string categoryName, string lineCategory, string imgUrl, string pattern, int order)
        {
            lineCategoryMod model = new lineCategoryMod();
            model.lineCategory = lineCategory;
            model.categoryName = categoryName;
            model.imgUrl = imgUrl;
            model.pattern = pattern;
            model.order = order;
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
        public static string editlinecategory(string categoryName, string lineCategory, string imgUrl, int Id, int order, string pattern)
        {
            lineCategoryMod model = new lineCategoryMod();
            model.lineCategory = lineCategory;
            model.categoryName = categoryName;
            model.imgUrl = imgUrl;
            model.Id = Id;
            model.order = order;
            model.pattern = pattern;
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

        /// <summary>
        /// 获得所有样式
        /// </summary>
        public static string getpatterns()
        {
            lineCategoryFac lineCategoryFac = new lineCategoryFac();
            DataSet ds = lineCategoryFac.GetPatterns();
            ConvertJson ConvertJson = new ConvertJson();
            string json = ConvertJson.ToJson(ds);
            return json;
        }
        /// <summary>
        /// 获得S2所有分类
        /// </summary>
        public static string getpatternss2()
        {
            lineCategoryFac lineCategoryFac = new lineCategoryFac();
            DataSet ds = lineCategoryFac.GetPatternsS2();
            ConvertJson ConvertJson = new ConvertJson();
            string json = ConvertJson.ToJson(ds);
            return json;
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
                str = " status='true' and beginDate <= '" + DateTime.Now + "' and  endDate >= '" + DateTime.Now + "'";
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
        public static string addbannerimg(string alt, string imgUrl, int lineId, int order, string H5Url, string beginDate, string endDate)
        {
            bannerImgMod model = new bannerImgMod();
            model.alt = alt;
            model.imgUrl = imgUrl;
            model.lineId = lineId;
            model.order = order;
            model.H5Url = H5Url;
            model.beginDate = beginDate;
            model.endDate = endDate;
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
        public static string editbannerimg(string alt, string imgUrl, int Id, int lineId, int order, string H5Url, string beginDate, string endDate)
        {
            bannerImgMod model = new bannerImgMod();
            model.alt = alt;
            model.imgUrl = imgUrl;
            model.Id = Id;
            model.lineId = lineId;
            model.order = order;
            model.H5Url = H5Url;
            model.beginDate = beginDate;
            model.endDate = endDate;
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
        public static string addarea2(int aId, string destName, int order, string H5Url)
        {
            destMod model = new destMod();
            model.A_Id = aId;
            model.destName = destName;
            model.order = order;
            model.H5Url = H5Url;
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
        public static string editarea2(int aId, string destName, int order, int Id, string H5Url)
        {
            destMod model = new destMod();
            model.A_Id = aId;
            model.destName = destName;
            model.order = order;
            model.Id = Id;
            model.H5Url = H5Url;
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
        public static string addarea3(string searchName, string H5Url)
        {
            emptySearchMod model = new emptySearchMod();
            model.searchName = searchName;
            model.H5Url = H5Url;
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
        public static string editarea3(string searchName, int Id, string H5Url)
        {
            emptySearchMod model = new emptySearchMod();
            model.searchName = searchName;
            model.Id = Id;
            model.H5Url = H5Url;
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

        /// <summary>
        /// 登录
        /// </summary>
        public static string login(string name, string password)
        {
            string strSql = "";
            strSql = "select count(*) from useradmin where name='" + name + "' and password='" + password + "' ";
            DataSet ds = DbHelperSQL.Query(strSql);
            int i = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            if (i > 0)
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }

        //-----------------------------产品推荐管理 bg
        /// <summary>
        /// 增加产品推荐
        /// </summary>
        public static string addrecommend(string imgUrl, string lineTitle, int order, int lineId, string agency, string lineCategory)
        {
            recommendMod model = new recommendMod();
            model.imgUrl = imgUrl;
            model.lineTitle = lineTitle;
            model.order = order;
            model.lineId = lineId;
            model.travelAgency = agency;
            model.lineCategory = lineCategory;
            recommendFac Fac = new recommendFac();
            if (Fac.Add(model) > 0)
            {
                return "操作成功!";
            }
            else {
                return "操作失败!";
            }
        }

        /// <summary>
        /// 获取
        /// </summary>
        public static string getrecommendlist(string search)
        {
            recommendFac Fac = new recommendFac();
            DataSet ds = Fac.GetList(search);
            ConvertJson ConvertJson = new ConvertJson();
            string json = ConvertJson.ToJson(ds);
            return json;
        }

        /// <summary>
        /// 推荐产品删除
        /// </summary>
        public static string delrecommend(int Id)
        {
            recommendFac Fac = new recommendFac();
            if (Fac.Delete(Id))
                return "操作成功";
            else
                return "操作失败";
        }

        /// <summary>
        /// 线路类型编辑
        /// </summary>
        public static string editrecommend(string lineTitle, string imgUrl, int Id, int order)
        {
            recommendMod model = new recommendMod();
            model.lineTitle = lineTitle;
            model.imgUrl = imgUrl;
            model.Id = Id;
            model.order = order;
            recommendFac Fac = new recommendFac();
            if (Fac.Update(model))
            {
                return "操作成功!";
            }
            else {
                return "操作失败!";
            }
        }


        //-----------------------------产品推荐管理 ed

        /// <summary>
        /// 后台线路cms查询
        /// </summary>
        public static string getlinescmssearch(string json)
        {
            var response = HttpUtil.Post(json, urlcmslinesearch, contentType: "application/json");
            return response;
        }



        /// <summary>
        /// 我的收藏
        /// </summary>
        public static string getCollectList(string Membercode)
        {
            myCollectFac Fac = new myCollectFac();
            DataSet ds = Fac.GetList(Membercode);
            ConvertJson ConvertJson = new ConvertJson();
            string json = ConvertJson.ToJson(ds);
            return json;
        }
        /// <summary>
        /// 添加收藏
        /// </summary>
        public static string addcollect(string title, string description, int price, int? date, string imgurl, int userMID, int lineID)
        {
            myCollectMod model = new myCollectMod();
            model.title = title;
            model.description = description;
            model.price = price;
            model.date = date;
            model.imgurl = imgurl;
            model.userMID = userMID;
            model.lineID = lineID;
            myCollectFac Fac = new myCollectFac();
            if (Fac.Add(model) > 0)
            {
                return "操作成功!";
            }
            else {
                return "操作失败!";
            }
        }
        /// <summary>
        /// 添加收藏
        /// </summary>
        public static string cancelcollect(int lineID, int userMID)
        {
            myCollectFac Fac = new myCollectFac();
            if (Fac.Delete(lineID, userMID))
            {
                return "操作成功!";
            }
            else {
                return "操作失败!";
            }
        }
        /// <summary>
        /// 该线路是否已收藏
        /// </summary>
        public static string getifcollect(int lineID, int userMID)
        {
            myCollectFac Fac = new myCollectFac();
            if (Fac.GetIfCollect(lineID, userMID))
            {
                return "是";
            }
            else {
                return "否";
            }
        }


    }
}
