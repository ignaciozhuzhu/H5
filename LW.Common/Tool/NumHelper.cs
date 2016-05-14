using LW.Common.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LW.Common.Tool
{
    public class NumHelper
    {
        /// <summary>
        /// 获取自动生成的单号
        /// </summary>
        /// <param name="VoucherNo">tbl_SysVoucherNumbering表中的Voucher字段数据,别给错值了</param>
        /// <returns></returns>
        public static string GetNum(string VoucherNo)
        {
            string strSql = "DECLARE @return_value nvarchar(500);EXEC [dbo].[proc_sysGetVoucherCode] '" + VoucherNo + "',@return_value output;";
            object obj = DataSource.GetSingle(strSql);
            if (obj == null) return string.Empty;
            else return obj.ToString();
        }
        /// <summary>
        /// 获取自动生成的单号
        /// </summary>
        /// <param name="VoucherNo"></param>
        /// <param name="strConn"></param>
        /// <returns></returns>
        public static string GetNum(string VoucherNo, string strConn)
        {
            if (string.IsNullOrEmpty(strConn))
            {
                throw new Exception("conn参数不允许为空！");
            }
            string strSql = "DECLARE @return_value nvarchar(500);EXEC [dbo].[proc_sysGetVoucherCode] '" + VoucherNo + "',@return_value output;";
            object obj = DataSource.GetSingle(strSql, strConn);
            if (obj == null) return string.Empty;
            else return obj.ToString();
        }

        /// <summary>
        /// 获取自动生成的单号,可以加到事务中去,在后面需要调用到生成的单号可以用:@return_value这个参数来获取
        /// </summary>
        /// <param name="VoucherNo">tbl_SysVoucherNumbering表中的Voucher字段数据,别给错值了</param>
        /// <returns></returns>
        public static string GetNumSqlInTrans(string VoucherNo)
        {
            return "DECLARE @return_value nvarchar(500);EXEC [dbo].[proc_sysGetVoucherCode] '" + VoucherNo + "',@return_value output;";
        }
    }
}
