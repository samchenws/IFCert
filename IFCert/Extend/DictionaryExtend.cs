using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFCert.Extend
{
    #region << 版 本 注 释 >>

    /*----------------------------------------------------------------

    * 项目名称 ：Eloan365.Common.Extend

    * 项目描述 ：

    * 类 名 称 ：DictionaryExtend

    * 类 描 述 ：

    * 所在的域 ：SC-201804231815

    * 命名空间 ：Eloan365.Common.Extend

    * 机器名称 ：SC-201804231815 

    * CLR 版本 ：4.0.30319.42000

    * 作    者 ：SamChen

    * 创建时间 ：2019/02/28 AM 11:14:29

    * 更新时间 ：2019/02/28 AM 11:14:29

    * 版 本 号 ：v1.0.0.0

    *******************************************************************
    * Copyright @ 江苏苏诚金融 2019. All rights reserved.
    *******************************************************************

    //----------------------------------------------------------------*/

    #endregion

    public static class DictionaryExtend
    {
        public static string ConvertToUrlParam(this Dictionary<string, string> param)
        {
            StringBuilder builder = new StringBuilder();
            if (param != null)
            {
                foreach (KeyValuePair<string, string> key in param)
                {
                    builder.Append($"{key.Key}={key.Value}&");
                }
                if (!string.IsNullOrWhiteSpace(builder.ToString()))
                {
                    builder = builder.Remove(builder.Length - 1, 1);
                }
            }
            return builder.ToString();
        }

    }
}
