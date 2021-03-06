﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Trip.JinJiang.H5
{

    public class Line
    {
        public int lineId { get; set; }
        public string lineName { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string destinationInfo { get; set; }
        public string spotInfo { get; set; }
        public string lineSubjectInfo { get; set; }
        public string[] destination { get; set; }
        public string[] spot { get; set; }
        public string[] lineSubject { get; set; }
        public int? manualRecommend { get; set; }
        public int imageNo { get; set; }
        public string brand { get; set; }
        public string agency { get; set; }
        public int days { get; set; }
        public int isPromotion { get; set; }
        public int[] departureMonth { get; set; }
        public int[] priceScope { get; set; }
        public EsGroup[] esGroup { get; set; }
        public double? point { get; set; }
        public string[] imageUrls { get; set; }
        public string lineCategory { get; set; }
        public int? minPrice { get; set; }
        public Tag[] tags { get; set; }
        public string productCode { get; set; }
        public string[] priceScopeAndLine { get; set; }
        public string lineDistrict { get; set; }
        public int? originalPrice { get; set; }
        public string priceExplain { get; set; }
        public string recommend { get; set; }
        //V1.0.9新增
        public string businessCategory { get; set; }
    }

}
