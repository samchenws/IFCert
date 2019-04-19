using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFCert.Utils
{
    #region << 版 本 注 释 >>

    /*----------------------------------------------------------------

    * 项目名称 ：IFCert.Utils

    * 项目描述 ：

    * 类 名 称 ：PhoneInfo

    * 类 描 述 ：

    * 所在的域 ：SC-201804231815

    * 命名空间 ：IFCert.Utils

    * 机器名称 ：SC-201804231815 

    * CLR 版本 ：4.0.30319.42000

    * 作    者 ：SamChen

    * 创建时间 ：2019/04/10 AM 11:44:27

    * 更新时间 ：2019/04/10 AM 11:44:27

    * 版 本 号 ：v1.0.0.0

    *******************************************************************
    * Copyright @ 江苏苏诚金融 2019. All rights reserved.
    *******************************************************************

    //----------------------------------------------------------------*/

    #endregion

    public class PhoneInfo
    {
        public static Dictionary<string, string> map = new Dictionary<string, string>();
        static PhoneInfo()
        {
            try
            {
                var txt = File.ReadAllText("phone_area.txt");
                var rows = txt.Split(';');
                foreach (string row in rows)
                {
                    var kv = row.Split(',');
                    if (kv.Length > 1)
                        map.Add(kv[0], kv[1]);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public static string getAscription(string phonenum)
        {
            if (phonenum.Length != 11)
            {
                throw new Exception("手机号码错误!");
            }
            var city = phonenum.Substring(0, 7);
            if (!map.TryGetValue(city, out city))
            {
                city = phonenum.Substring(0, 7);
            }
            return city;
        }
    }
}
