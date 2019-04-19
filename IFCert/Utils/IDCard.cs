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

    * 项目名称 ：IFCert

    * 项目描述 ：

    * 类 名 称 ：IDCard

    * 类 描 述 ：

    * 所在的域 ：SC-201804231815

    * 命名空间 ：IFCert

    * 机器名称 ：SC-201804231815 

    * CLR 版本 ：4.0.30319.42000

    * 作    者 ：SamChen

    * 创建时间 ：2019/04/10 AM 10:24:11

    * 更新时间 ：2019/04/10 AM 10:24:11

    * 版 本 号 ：v1.0.0.0

    *******************************************************************
    * Copyright @ 江苏苏诚金融 2019. All rights reserved.
    *******************************************************************

    //----------------------------------------------------------------*/

    #endregion

    public class IDCard
    {
        private readonly string cardNumber;
        private bool? cacheValidateResult = null;
        private DateTime? cacheBirthDate = null;
        private const string BIRTH_DATE_FORMAT = "yyyyMMdd";
        private static readonly DateTime MINIMAL_BIRTH_DATE = new DateTime(1900, 1, 1);// 身份证的最小出生日期,1900年1月1日
        private const int NEW_CARD_NUMBER_LENGTH = 18;
        private const int OLD_CARD_NUMBER_LENGTH = 15;
        private static readonly char[] VERIFY_CODE = new char[] { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };
        private static readonly int[] VERIFY_CODE_WEIGHT = new int[] { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
        public virtual bool validate()
        {
            if (this.cacheValidateResult == null)
            {
                bool result = true;

                result = (result) && (this.cardNumber != null);

                result = (result) && (18 == this.cardNumber.Length);
                for (int i = 0; (result) && (i < 17); i++)
                {
                    char ch = this.cardNumber.CharAt(i);
                    result = (result) && (ch >= '0') && (ch <= '9');
                }
                result = (result) && (calculateVerifyCode(this.cardNumber) == this.cardNumber.CharAt(17));
                try
                {
                    DateTime birthDate = BirthDate;
                    result = (result) && (birthDate != null);
                    result = (result) && (birthDate < DateTime.Now);
                    result = (result) && (birthDate > MINIMAL_BIRTH_DATE);

                    string birthdayPart = BirthDayPart;
                    string realBirthdayPart = birthDate.ToString("yyyy-MM-dd");
                    result = (result) && (birthdayPart.Equals(realBirthdayPart));
                }
                catch (Exception)
                {
                    result = false;
                }
                this.cacheValidateResult = Convert.ToBoolean(result);
            }
            return this.cacheValidateResult.Value;
        }
        public IDCard(string cardNumber)
        {
            if (!string.ReferenceEquals(cardNumber, null))
            {
                cardNumber = cardNumber.Trim();
                if (15 == cardNumber.Length)
                {
                    cardNumber = contertToNewCardNumber(cardNumber);
                }
            }
            this.cardNumber = cardNumber;
        }
        public virtual string CardNumber
        {
            get
            {
                return this.cardNumber;
            }
        }
        public virtual string AddressCode
        {
            get
            {
                checkIfValid();
                return this.cardNumber.Substring(0, 6);
            }
        }
        public virtual string Ascription
        {
            get
            {
                checkIfValid();
                string city = this.cardNumber.Substring(0, 6);
                city = (string)IdcardInfo.cityCodeMap[city];
                if (string.ReferenceEquals(city, null))
                {
                    city = this.cardNumber.Substring(0, 7);
                }
                return city;
            }
        }

        public virtual string Sex
        {
            get
            {
                checkIfValid();
                string sex = this.cardNumber.Substring(16, 1);
                sex = int.Parse(sex) % 2 == 0 ? "0" : "1";
                return sex;
            }
        }


        public virtual int Age
        {
            get
            {
                checkIfValid();
                string birthdays = cardNumber.Substring(6, 8).Insert(6, "-").Insert(4, "-");

                int year = 0;
                try
                {
                    DateTime birthdate = DateTime.Parse(birthdays);
                    year = birthdate.Year;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                }

                int years = DateTime.Now.Year;
                int age = years - year;
                return age;
            }
        }

        public virtual DateTime BirthDate
        {
            get
            {
                if (this.cacheBirthDate == null)
                {
                    try
                    {
                        this.cacheBirthDate = DateTime.Parse(BirthDayPart);
                    }
                    catch (Exception)
                    {
                        throw new Exception("身份证的出生日期无效");
                    }
                }
                return cacheBirthDate.Value;
            }
        }
 
        public virtual bool Male
        {
            get
            {
                return 1 == GenderCode;
            }
        }

        public virtual bool Femal
        {
            get
            {
                return !Male;
            }
        }

        private int GenderCode
        {
            get
            {
                checkIfValid();
                char genderCode = this.cardNumber.CharAt(16);
                return genderCode - '0' & 0x1;
            }
        }

        private string BirthDayPart
        {
            get
            {
                return cardNumber.Substring(6, 8).Insert(6, "-").Insert(4, "-");
             
            }
        }
 
        private void checkIfValid()
        {
            if (!validate())
            {
                throw new Exception("身份证号码不正确!");
            }
        }
        private static char calculateVerifyCode(string cardNumber)
        {
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                char ch = cardNumber.CharAt(i);
                sum += (ch - '0') * VERIFY_CODE_WEIGHT[i];
            }
            return VERIFY_CODE[(sum % 11)];
        }

        private static string contertToNewCardNumber(string oldCardNumber)
        {
            StringBuilder buf = new StringBuilder(18);
            buf.Append(oldCardNumber.Substring(0, 6));
            buf.Append("19");
            buf.Append(oldCardNumber.Substring(6));
            buf.Append(calculateVerifyCode(buf.ToString()));
            return buf.ToString();
        }
     
    }
}
