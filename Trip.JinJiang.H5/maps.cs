using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Trip.JinJiang.H5
{
    class maps
    {
        /// <summary>
        /// map对应的旅行社名称,经需求确定是取值固定写死.
        /// </summary>
        public static string mapAgencies(string response)
        {
            response = Regex.Replace(response, "SHT", "上海华亭海外旅游有限公司", RegexOptions.IgnoreCase);
            response = Regex.Replace(response, "STD", "旅游事业部", RegexOptions.IgnoreCase);
            response = Regex.Replace(response, "SWD", "上海锦江国际旅游股份有限公司网点分公司", RegexOptions.IgnoreCase);
            response = Regex.Replace(response, "STS", "上海旅行社有限公司", RegexOptions.IgnoreCase);
            response = Regex.Replace(response, "SHZ", "浙江锦旅国旅旅行社有限公司", RegexOptions.IgnoreCase);
            response = Regex.Replace(response, "CTH", "上海中旅国际旅行社有限公司", RegexOptions.IgnoreCase);
            response = Regex.Replace(response, "CYR", "上海中旅杨休国际旅行社有限公司", RegexOptions.IgnoreCase);
            response = Regex.Replace(response, "SIT", "信息中心", RegexOptions.IgnoreCase);
            response = Regex.Replace(response, "SHA", "上海国旅国际旅行社有限公司", RegexOptions.IgnoreCase);
            response = Regex.Replace(response, "BJT", "北京锦江国际旅行社有限公司", RegexOptions.IgnoreCase);
            response = Regex.Replace(response, "SJT", "上海锦江旅游有限公司", RegexOptions.IgnoreCase);

            return response;
        }
    }
}
