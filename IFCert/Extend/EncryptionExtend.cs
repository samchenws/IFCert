using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IFCert.Extend
{
    #region << 版 本 注 释 >>

    /*----------------------------------------------------------------

    * 项目名称 ：IFCert.Extend

    * 项目描述 ：

    * 类 名 称 ：EncryptionExtend

    * 类 描 述 ：

    * 所在的域 ：SC-201804231815

    * 命名空间 ：IFCert.Extend

    * 机器名称 ：SC-201804231815 

    * CLR 版本 ：4.0.30319.42000

    * 作    者 ：SamChen

    * 创建时间 ：2019/04/10 PM 12:04:12

    * 更新时间 ：2019/04/10 PM 12:04:12

    * 版 本 号 ：v1.0.0.0

    *******************************************************************
    * Copyright @ 江苏苏诚金融 2019. All rights reserved.
    *******************************************************************

    //----------------------------------------------------------------*/

    #endregion

    public static class EncryptionExtend
    {
        public static string ToSHA256(this string str, string encoding = "UTF-8")
        {
            byte[] clearBytes = Encoding.GetEncoding(encoding).GetBytes(str);
            SHA256 sha256 = new SHA256Managed();
            sha256.ComputeHash(clearBytes);
            byte[] hashedBytes = sha256.Hash;
            sha256.Clear();
            string output = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

            return output;
        }
        public static string ToBase64String(this string source, string encoding = "UTF-8")
        {
            byte[] bytes = Encoding.GetEncoding(encoding).GetBytes(source);
            return Convert.ToBase64String(bytes);
        }
        public static string ToMD5(this string myString, string encoding = "UTF-8")
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.GetEncoding(encoding).GetBytes(myString);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x");
            }

            return byte2String;
        }
    }
}
