using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trip.JinJiang.H5.Model
{
    public class OrderdetailMod
    {
        public Orders[] orders { get; set; }
        public int adultNum { get; set; }
        public string departrueDate { get; set; }
        public string createTime { get; set; }

        public class Orders
        {
            public string code { get; set; }
            public string orderStatus { get; set; }
            public string payStatus { get; set; }
            public string orderStatusName { get; set; }
            public string payStatusName { get; set; }
            public string channel { get; set; }
            public string channelName { get; set; }
            public string channelCode { get; set; }
            public string channelCodeName { get; set; }
            public string lineName { get; set; }
            public int amount { get; set; }
            public bool canOnlinePay { get; set; }
            public bool canCancel { get; set; }
            public int adultNum { get; set; }
            public string departrueDate { get; set; }
            public string createTime { get; set; }
        }
    }
}
