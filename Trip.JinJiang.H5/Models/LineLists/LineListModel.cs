﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Trip.JinJiang.H5
{

    public class LineListModel
    {
        public Page page { get; set; }
        public Line[] lines { get; set; }
        public DepartureMonth[] departureMonths { get; set; }
        public Brand[] brands { get; set; }
        public Agency[] agencies { get; set; }
        public PriceScope[] priceScopes { get; set; }
        public Day[] days { get; set; }
        public Spot[] spots { get; set; }
        public Promotion[] promotions { get; set; }
        public ClusterType[] clusterTypes { get; set; }
        public LineCategory[] lineCategorys { get; set; }
        public City[] citys { get; set; }
        public PriceScopeAndLine[] priceScopeAndLine { get; set; }
        public object errorMessage { get; set; }
    }

}
