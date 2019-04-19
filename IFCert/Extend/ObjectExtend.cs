using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFCert.Extend
{
    #region << 版 本 注 释 >>

    /*----------------------------------------------------------------

    * 项目名称 ：IFCert.Extend

    * 项目描述 ：

    * 类 名 称 ：ObjectExtend

    * 类 描 述 ：

    * 所在的域 ：SC-201804231815

    * 命名空间 ：IFCert.Extend

    * 机器名称 ：SC-201804231815 

    * CLR 版本 ：4.0.30319.42000

    * 作    者 ：SamChen

    * 创建时间 ：2019/04/12 PM 5:15:25

    * 更新时间 ：2019/04/12 PM 5:15:25

    * 版 本 号 ：v1.0.0.0

    *******************************************************************
    * Copyright @ 江苏苏诚金融 2019. All rights reserved.
    *******************************************************************

    //----------------------------------------------------------------*/

    #endregion

    public static  class ObjectExtend
    {
        public static Dictionary<string, object> ToDictionary<T>(this T obj,bool allowNull = false, bool camelCase = true)
            where T:class,new()
        {
            Dictionary<string, object> res = new Dictionary<string, object>();
            var type = obj.GetType();
            foreach (var c in type.GetProperties())
            {
                var name = c.Name;
                var val = c.GetValue(obj, null);
                if (camelCase == true)
                {
                    name = c.Name.Substring(0, 1).ToLower() + c.Name.Substring(1);
                }
                if (!allowNull)
                {
                    if (val != null)
                        res.Add(name, val);
                }
                else
                {
                    res.Add(name, val);
                }
            }
            return res;
        }

        public static string toString(this List<Dictionary<String, Object>> obj)
        {
            return foo(obj);
        }
        public static string foo(List<Dictionary<String, Object>> obj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("[");
            foreach (var item in obj)
            {
                builder.Append(fooDic(item));
            }
            builder = builder.Remove(builder.Length - 2, 2);
            builder.Append("]");
            return builder.ToString();
        }

        public static string fooDic(Dictionary<String, Object> dic)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("{");
            foreach (var kv in dic)
            {

                var pdic = kv.Value as List<Dictionary<String, object>>;
                var val = kv.Value.ToString();
                if (pdic != null)
                {
                    val = foo(pdic);
                }
                builder.Append(kv.Key + "=" + val + ", ");
            }
            builder = builder.Remove(builder.Length - 2, 2);
            builder.Append("}, ");
            return builder.ToString();
        }

    }
}
