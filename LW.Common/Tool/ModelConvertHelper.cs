/*// 获得查询结果
 * 
//DataTable dt = DataSource.ExecuteDataTable(...);
//// 把DataTable转换为IList<UserInfo>
//IList<UserInfo> users = ModelConvertHelper<UserInfo>.ConvertToModel(dt);
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;



namespace LW.Common.Tool
{
    /// <summary>
    /// 实体转换辅助类
    /// </summary>
    public class ModelConvertHelper<T> where T : new()
    {
        public static IList<T> ConvertToModel(DataTable dt)
        {
            // 定义集合
            IList<T> ts = new List<T>();

            // 获得此模型的类型
            Type type = typeof(T);

            string tempName = "";

            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();

                // 获得此模型的公共属性
                PropertyInfo[] propertys = t.GetType().GetProperties();

                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;

                    // 检查DataTable是否包含此列
                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter
                        if (!pi.CanWrite) continue;

                        object value = dr[tempName];
                        if (value != DBNull.Value){
                         //   try {
                            if (value.GetType() != typeof(Guid))
                            {
                                pi.SetValue(t, value, null);
                            }
                            else {
                                pi.SetValue(t, value.ToString(), null);
                            
                            }
                            //}
                            //catch 
                            //{
                            // //   pi.SetValue(t, value.ToString(), null);
                            //}
                        }
                    }
                }
                ts.Add(t);
            }
            return ts;
        }
    }
}
