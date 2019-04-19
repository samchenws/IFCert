using IFCert.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFCert.Utils
{
    #region << 版 本 注 释 >>

    /*----------------------------------------------------------------

    * 项目名称 ：IFCert.Utils

    * 项目描述 ：

    * 类 名 称 ：IdCardUtil

    * 类 描 述 ：

    * 所在的域 ：SC-201804231815

    * 命名空间 ：IFCert.Utils

    * 机器名称 ：SC-201804231815 

    * CLR 版本 ：4.0.30319.42000

    * 作    者 ：SamChen

    * 创建时间 ：2019/04/10 AM 10:52:18

    * 更新时间 ：2019/04/10 AM 10:52:18

    * 版 本 号 ：v1.0.0.0

    *******************************************************************
    * Copyright @ 江苏苏诚金融 2019. All rights reserved.
    *******************************************************************

    //----------------------------------------------------------------*/

    #endregion

    public class IdCardUtil
    {
        public const int CHINA_ID_MIN_Length = 15;
        public const int CHINA_ID_MAX_Length = 18;
        public static readonly string[] cityCode = new string[] { "11", "12", "13", "14", "15", "21", "22", "23", "31", "32", "33", "34", "35", "36", "37", "41", "42", "43", "44", "45", "46", "50", "51", "52", "53", "54", "61", "62", "63", "64", "65", "71", "81", "82", "91" };
        public static readonly int[] power = new int[] { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
        public static readonly string[] verifyCode = new string[] { "1", "0", "X", "9", "8", "7", "6", "5", "4", "3", "2" };
        public const int MIN = 1930;
        public static IDictionary<string, string> cityCodes = new Dictionary<string, string>();
        public static IDictionary<string, int> twFirstCode = new Dictionary<string, int>();
        public static IDictionary<string, int> hkFirstCode = new Dictionary<string, int>();

        static IdCardUtil()
        {
            cityCodes.Add("12", "天津");
            cityCodes.Add("13", "河北");
            cityCodes.Add("14", "山西");
            cityCodes.Add("15", "内蒙古");
            cityCodes.Add("21", "辽宁");
            cityCodes.Add("22", "吉林");
            cityCodes.Add("23", "黑龙江");
            cityCodes.Add("31", "上海");
            cityCodes.Add("32", "江苏");
            cityCodes.Add("33", "浙江");
            cityCodes.Add("34", "安徽");
            cityCodes.Add("35", "福建");
            cityCodes.Add("36", "江西");
            cityCodes.Add("37", "山东");
            cityCodes.Add("41", "河南");
            cityCodes.Add("42", "湖北");
            cityCodes.Add("43", "湖南");
            cityCodes.Add("44", "广东");
            cityCodes.Add("45", "广西");
            cityCodes.Add("46", "海南");
            cityCodes.Add("50", "重庆");
            cityCodes.Add("51", "四川");
            cityCodes.Add("52", "贵州");
            cityCodes.Add("53", "云南");
            cityCodes.Add("54", "西藏");
            cityCodes.Add("61", "陕西");
            cityCodes.Add("62", "甘肃");
            cityCodes.Add("63", "青海");
            cityCodes.Add("64", "宁夏");
            cityCodes.Add("65", "新疆");
            cityCodes.Add("71", "台湾");
            cityCodes.Add("81", "香港");
            cityCodes.Add("82", "澳门");
            cityCodes.Add("91", "国外");
            twFirstCode.Add("A", 10);
            twFirstCode.Add("B", 11);
            twFirstCode.Add("C", 12);
            twFirstCode.Add("D", 13);
            twFirstCode.Add("E", 14);
            twFirstCode.Add("F", 15);
            twFirstCode.Add("G", 16);
            twFirstCode.Add("H", 17);
            twFirstCode.Add("J", 18);
            twFirstCode.Add("K", 19);
            twFirstCode.Add("L", 20);
            twFirstCode.Add("M", 21);
            twFirstCode.Add("N", 22);
            twFirstCode.Add("P", 23);
            twFirstCode.Add("Q", 24);
            twFirstCode.Add("R", 25);
            twFirstCode.Add("S", 26);
            twFirstCode.Add("T", 27);
            twFirstCode.Add("U", 28);
            twFirstCode.Add("V", 29);
            twFirstCode.Add("X", 30);
            twFirstCode.Add("Y", 31);
            twFirstCode.Add("W", 32);
            twFirstCode.Add("Z", 33);
            twFirstCode.Add("I", 34);
            twFirstCode.Add("O", 35);
            hkFirstCode.Add("A", 1);
            hkFirstCode.Add("B", 2);
            hkFirstCode.Add("C", 3);
            hkFirstCode.Add("R", 18);
            hkFirstCode.Add("U", 21);
            hkFirstCode.Add("Z", 26);
            hkFirstCode.Add("X", 24);
            hkFirstCode.Add("W", 23);
            hkFirstCode.Add("O", 15);
            hkFirstCode.Add("N", 14);
        }
        public static string conver15CardTo18(string idCard)
        {
            string idCard18 = "";
            if (idCard.Length != 15)
            {
                return null;
            }
            if (isNum(idCard))
            {
                string birthday = idCard.Substring(6, 8).Insert(6, "-").Insert(4, "-");
                DateTime? birthDate = null;
                try
                {
                    birthDate =DateTime.Parse(birthday);//----
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                }
         
                string sYear = birthDate.Value.Year.ToString();
                idCard18 = idCard.Substring(0, 6) + sYear + idCard.Substring(8);

                char[] cArr = idCard18.ToCharArray();
                if (cArr != null)
                {
                    int[] iCard = converCharToInt(cArr);
                    int iSum17 = getPowerSum(iCard);

                    string sVal = getCheckCode18(iSum17);
                    if (sVal.Length > 0)
                    {
                        idCard18 = idCard18 + sVal;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else
            {
                return null;
            }
            return idCard18;
        }
        public static bool validateCard(string idCard)
        {
            string card = idCard.Trim();
            if (validateIdCard18(card))
            {
                return true;
            }
            if (validateIdCard15(card))
            {
                return true;
            }
            string[] cardval = validateIdCard10(card);
            if ((cardval != null) && (cardval[2].Equals("true")))
            {
                return true;
            }

            return false;
        }
        public static bool validateIdCard18(string idCard)
        {
            bool bTrue = false;
            if (idCard.Length == 18)
            {
                string code17 = idCard.Substring(0, 17);

                string code18 = idCard.Substring(17, 1);
                if (isNum(code17))
                {
                    char[] cArr = code17.ToCharArray();
                    if (cArr != null)
                    {
                        int[] iCard = converCharToInt(cArr);
                        int iSum17 = getPowerSum(iCard);

                        string val = getCheckCode18(iSum17);
                        if ((val.Length > 0) && (val.Equals(code18, StringComparison.OrdinalIgnoreCase)))
                        {
                            bTrue = true;
                        }
                    }
                }
            }

            return bTrue;
        }
        public static bool validateIdCard15(string idCard)
        {
            if (idCard.Length != 15)
            {
                return false;
            }
            if (isNum(idCard))
            {
                string proCode = idCard.Substring(0, 2);
                if (cityCodes[proCode] == null)
                {
                    return false;
                }
                string birthCode = idCard.Substring(6, 8).Insert(6, "-").Insert(4, "-");
                DateTime? birthDate = null;
                try
                {
                    birthDate = DateTime.Parse(birthCode);//--
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                }
 
                if (!valiDate(birthDate.Value.Year, Convert.ToInt32(birthCode.Substring(2, 2)), Convert.ToInt32(birthCode.Substring(4, 2))))
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }


        public static string[] validateIdCard10(string idCard)
        {
            string[] info = new string[3];
            string card = idCard.ReplaceAll("[\\(|\\)]", "");
            if ((card.Length != 8) && (card.Length != 9) && (idCard.Length != 10))
            {
                return null;
            }
            if (idCard.Matches("^[a-zA-Z][0-9]{9}$"))
            {
                info[0] = "台湾";
                Console.WriteLine("11111");
                string char2 = idCard.Substring(1, 1);
                if (char2.Equals("1"))
                {
                    info[1] = "M";
                    Console.WriteLine("MMMMMMM");
                }
                else if (char2.Equals("2"))
                {
                    info[1] = "F";
                    Console.WriteLine("FFFFFFF");
                }
                else
                {
                    info[1] = "N";
                    info[2] = "false";
                    Console.WriteLine("NNNN");
                    return info;
                }
                info[2] = (validateTWCard(idCard) ? "true" : "false");
            }
            else if (idCard.Matches("^[1|5|7][0-9]{6}\\(?[0-9A-Z]\\)?$"))
            {
                info[0] = "澳门";
                info[1] = "N";
            }
            else if (idCard.Matches("^[A-Z]{1,2}[0-9]{6}\\(?[0-9A]\\)?$"))
            {
                info[0] = "香港";
                info[1] = "N";
                info[2] = (validateHKCard(idCard) ? "true" : "false");
            }
            else
            {
                return null;
            }
            return info;
        }
        public static bool validateTWCard(string idCard)
        {
            string start = idCard.Substring(0, 1);
            string mid = idCard.Substring(1, 8);
            string end = idCard.Substring(9, 1);
            int? iStart = (int?)twFirstCode[start];
            int? sum = Convert.ToInt32(iStart.Value / 10 + iStart.Value % 10 * 9);
            char[] chars = mid.ToCharArray();
            int? iflag = Convert.ToInt32(8);
            char[] arrayOfChar1;
            int j = (arrayOfChar1 = chars).Length;
            for (int i = 0; i < j; i++)
            {
                char c = arrayOfChar1[i];
                sum = Convert.ToInt32(sum.Value + Convert.ToInt32(c) * iflag.Value);
                iflag = Convert.ToInt32(iflag.Value - 1);
            }
            return (sum.Value % 10 == 0 ? 0 : 10 - sum.Value % 10) == Convert.ToInt32(end);
        }

        public static bool validateHKCard(string idCard)
        {
            string card = idCard.ReplaceAll("[\\(|\\)]", "");
            int? sum = Convert.ToInt32(0);
            if (card.Length == 9)
            {
                sum = Convert.ToInt32((Convert.ToInt32(card.Substring(0, 1).ToUpper().ToCharArray()[0]) - 55) * 9 + (Convert.ToInt32(card.Substring(1, 1).ToUpper().ToCharArray()[0]) - 55) * 8);
                card = card.Substring(1, 8);
            }
            else
            {
                sum = Convert.ToInt32(522 + (Convert.ToInt32(card.Substring(0, 1).ToUpper().ToCharArray()[0]) - 55) * 8);
            }
            string mid = card.Substring(1, 6);
            string end = card.Substring(7, 1);
            char[] chars = mid.ToCharArray();
            int? iflag = Convert.ToInt32(7);
            char[] arrayOfChar1;
            int j = (arrayOfChar1 = chars).Length;
            for (int i = 0; i < j; i++)
            {
                char c = arrayOfChar1[i];
                sum = Convert.ToInt32(sum.Value + Convert.ToInt32(c) * iflag.Value);
                iflag = Convert.ToInt32(iflag.Value - 1);
            }
            if (end.ToUpper().Equals("A"))
            {
                sum = Convert.ToInt32(sum.Value + 10);
            }
            else
            {
                sum = Convert.ToInt32(sum.Value + Convert.ToInt32(end));
            }
            return sum.Value % 11 == 0;
        }
        public static int[] converCharToInt(char[] ca)
        {
            int len = ca.Length;
            int[] iArr = new int[len];
            try
            {
                for (int i = 0; i < len; i++)
                {
                    iArr[i] = int.Parse(ca[i].ToString());
                }
            }
            catch (System.FormatException e)
            {
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
            return iArr;
        }
        public static int getPowerSum(int[] iArr)
        {
            int iSum = 0;
            if (power.Length == iArr.Length)
            {
                for (int i = 0; i < iArr.Length; i++)
                {
                    for (int j = 0; j < power.Length; j++)
                    {
                        if (i == j)
                        {
                            iSum += iArr[i] * power[j];
                        }
                    }
                }
            }
            return iSum;
        }
        public static string getCheckCode18(int iSum)
        {
            string sCode = "";
            switch (iSum % 11)
            {
                case 10:
                    sCode = "2";
                    break;
                case 9:
                    sCode = "3";
                    break;
                case 8:
                    sCode = "4";
                    break;
                case 7:
                    sCode = "5";
                    break;
                case 6:
                    sCode = "6";
                    break;
                case 5:
                    sCode = "7";
                    break;
                case 4:
                    sCode = "8";
                    break;
                case 3:
                    sCode = "9";
                    break;
                case 2:
                    sCode = "x";
                    break;
                case 1:
                    sCode = "0";
                    break;
                case 0:
                    sCode = "1";
                    break;
            }

            return sCode;
        }
        public static int getAgeByIdCard(string idCard)
        {
            int iAge = 0;
            if (idCard.Length == 15)
            {
                idCard = conver15CardTo18(idCard);
            }
            string year = idCard.Substring(6, 4);
            DateTime cal = new DateTime();
            int iCurrYear = cal.Year;
            iAge = iCurrYear - Convert.ToInt32(year);
            return iAge;
        }

        public static string getBirthByIdCard(string idCard)
        {
            int? len = Convert.ToInt32(idCard.Length);
            if (len.Value < 15)
            {
                return null;
            }
            if (len.Value == 15)
            {
                idCard = conver15CardTo18(idCard);
            }
            return idCard.Substring(6, 8).Insert(6, "-").Insert(4, "-");
        }

        public static short? getYearByIdCard(string idCard)
        {
            int? len = Convert.ToInt32(idCard.Length);
            if (len.Value < 15)
            {
                return null;
            }
            if (len.Value == 15)
            {
                idCard = conver15CardTo18(idCard);
            }
            return Convert.ToInt16(idCard.Substring(6, 4));
        }

        public static short? getMonthByIdCard(string idCard)
        {
            int? len = Convert.ToInt32(idCard.Length);
            if (len.Value < 15)
            {
                return null;
            }
            if (len.Value == 15)
            {
                idCard = conver15CardTo18(idCard);
            }
            return Convert.ToInt16(idCard.Substring(10, 2));
        }

        public static short? getDateByIdCard(string idCard)
        {
            int? len = Convert.ToInt32(idCard.Length);
            if (len.Value < 15)
            {
                return null;
            }
            if (len.Value == 15)
            {
                idCard = conver15CardTo18(idCard);
            }
            return Convert.ToInt16(idCard.Substring(12, 2));
        }

        public static string getGenderByIdCard(string idCard)
        {
            string sGender = "N";
            if (idCard.Length == 15)
            {
                idCard = conver15CardTo18(idCard);
            }
            string sCardNum = idCard.Substring(16, 1);
            if (int.Parse(sCardNum) % 2 != 0)
            {
                sGender = "M";
            }
            else
            {
                sGender = "F";
            }
            return sGender;
        }

        public static string getProvinceByIdCard(string idCard)
        {
            int len = idCard.Length;
            string sProvince = null;
            string sProvinNum = "";
            if ((len == 15) || (len == 18))
            {
                sProvinNum = idCard.Substring(0, 2);
            }
            sProvince = (string)cityCodes[sProvinNum];
            return sProvince;
        }

        public static bool isNum(string val)
        {
            return (string.ReferenceEquals(val, null)) || ("".Equals(val)) ? false : val.Matches("^[0-9]*$");
        }

        public static bool valiDate(int iYear, int iMonth, int iDate)
        {
            DateTime cal = new DateTime();
            int year = cal.Year;

            if ((iYear < 1930) || (iYear >= year))
            {
                return false;
            }
            if ((iMonth < 1) || (iMonth > 12))
            {
                return false;
            }
            int datePerMonth;
            switch (iMonth)
            {
                case 4:
                case 6:
                case 9:
                case 11:
                    datePerMonth = 30;
                    break;
                case 2:
                    bool dm = ((iYear % 4 == 0) && (iYear % 100 != 0)) || ((iYear % 400 == 0) && (iYear > 1930) && (iYear < year));
                    datePerMonth = dm ? 29 : 28;
                    break;
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                default:
                    datePerMonth = 31;
                    break;
            }
            return (iDate >= 1) && (iDate <= datePerMonth);
        }

    }
}
