using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IFCert.Extend
{
    #region << 版 本 注 释 >>

    /*----------------------------------------------------------------

    * 项目名称 ：IFCert.Extend

    * 项目描述 ：

    * 类 名 称 ：StringExtend

    * 类 描 述 ：

    * 所在的域 ：SC-201804231815

    * 命名空间 ：IFCert.Extend

    * 机器名称 ：SC-201804231815 

    * CLR 版本 ：4.0.30319.42000

    * 作    者 ：SamChen

    * 创建时间 ：2019/04/10 AM 10:32:00

    * 更新时间 ：2019/04/10 AM 10:32:00

    * 版 本 号 ：v1.0.0.0

    *******************************************************************
    * Copyright @ 江苏苏诚金融 2019. All rights reserved.
    *******************************************************************

    //----------------------------------------------------------------*/

    #endregion

    public static class StringExtend
    {
        public static Char CharAt(this string s, int index)
        {
            if ((index >= s.Length) || (index < 0))
                return  new Char();
            return s.Substring(index, 1)[0];
        }
        public static bool Matches(this string str, string pattern)
        {
            return Regex.IsMatch(str, pattern);
        }
        public static string ReplaceAll(this string str, string pattern,string replacement) {
          return  Regex.Replace(str.Trim(), pattern, replacement);
        }
        public static string LastMask(this string str,char _char='*',int length=6) {
            return str.Substring(0, str.Length - length).PadRight(str.Length, _char);
        }
    }
}
