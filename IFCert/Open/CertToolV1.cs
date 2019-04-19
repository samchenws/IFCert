using DevOne.Security.Cryptography.BCrypt;
using IFCert.Extend;
using IFCert.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Text;

namespace IFCert.Open
{
    #region << 版 本 注 释 >>

    /*----------------------------------------------------------------

    * 项目名称 ：IFCert

    * 项目描述 ：

    * 类 名 称 ：CertToolV1

    * 类 描 述 ：

    * 所在的域 ：SC-201804231815

    * 命名空间 ：IFCert

    * 机器名称 ：SC-201804231815 

    * CLR 版本 ：4.0.30319.42000

    * 作    者 ：SamChen

    * 创建时间 ：2019/04/10 AM 10:18:26

    * 更新时间 ：2019/04/10 AM 10:18:26

    * 版 本 号 ：v1.0.0.0

    *******************************************************************
    * Copyright @ 江苏苏诚金融 2019. All rights reserved.
    *******************************************************************

    //----------------------------------------------------------------*/

    #endregion

    public class CertToolV1
    {
        private static String CONTENT_CHARSET = "UTF-8";

        private static readonly long? MAX_VALUE = Convert.ToInt64(long.MaxValue);
        public virtual string getCompanyAscription(string cood)
        {
            string city = null;
            try
            {
                city = IdcardInfo.getCompanyAscription(cood);
            }
            catch (Exception e)
            {
                throw new CertException(1110, e);
            }
            return city;
        }
        public virtual string getIdcardAscription(string idcard)
        {
            string city = null;
            try
            {
                IDCard idCard = new IDCard(idcard);
                city = idCard.Ascription;
            }
            catch (Exception e)
            {
                throw new CertException(1108, e);
            }
            return city;
        }
 
        public virtual string getSex(string idcard)
        {
            string sex = null;
            try
            {
                IDCard idCard = new IDCard(idcard);
                sex = idCard.Sex;
            }
            catch (Exception e)
            {
                throw new CertException(1108, e);
            }
            return sex;
        }
 
        public virtual string getAge(string idcard)
        {
            string age = null;
            try
            {
                IDCard idCard = new IDCard(idcard);
                age = idCard.Age.ToString();
            }
            catch (Exception e)
            {
                throw new CertException(1108, e);
            }
            return age;
        }

        public virtual string getPhoneAscription(string phonenum)
        {
            string city = null;
            try
            {
                city = PhoneInfo.getAscription(phonenum);
            }
            catch (Exception e)
            {
                throw new CertException(1109, e);
            }
            return city;
        }
        /// <summary>
        /// 获取身份证hash
        /// </summary>
        /// <param name="idCard"></param>
        /// <returns></returns>
        public virtual string idCardHash(string idCard)
        {
            string hash = null;
            try
            {
                string idCard_des = AESCrypt.AesEncrypt(idCard);

                hash = idCard_des.ToSHA256();
            }
            catch (Exception e)
            {
                throw new CertException(1003, e);
            }
            return hash;
        }
        public virtual string nameHash(string name)
        {
            string hash = null;
            try
            {
                string idCard_des = AESCrypt.AES_Encrypt(name);

                hash = idCard_des.ToSHA256();
            }
            catch (Exception e)
            {
                throw new CertException(1003, e);
            }
            return hash;
        }
        public virtual JObject phoneHash(string phone)
        {
            string salt = BCryptHelper.GenerateSalt();
            string pHash = (phone + salt).ToSHA256();
  
            string phoneBase64 = null;
            try
            {  
                phoneBase64 = pHash.ToBase64String();
            }
            catch (Exception e)
            {
                throw new CertException(1003, e);
            }
            var s = BCryptHelper.GenerateSalt();
            string phoneHash = BCryptHelper.HashPassword(phoneBase64, s);

            StringBuilder strB = new StringBuilder();
            strB.Append("{\"phone\":\"");
            strB.Append(phoneHash);
            strB.Append("\",\"salt\":\"");
            strB.Append(salt);
            strB.Append("\"}");
            JObject jo = JObject.Parse(strB.ToString());

            return jo;
        }

        public virtual string batchNumber(string sourceCode, string tradeDate, string num, string seqId)
        {
            StringBuilder batch_num = new StringBuilder();
            batch_num.Append(sourceCode);
            batch_num.Append("_");
            if (string.IsNullOrEmpty(tradeDate))
            {
                tradeDate = DateTime.Now.ToString("yyyyMMdd");
            }
            batch_num.Append(tradeDate + num);
            batch_num.Append("_");
            if (string.IsNullOrEmpty(seqId))
            {
                throw new CertException(1005, "流水号生成失败!");
            }
            batch_num.Append(seqId);

            return batch_num.ToString();
        }

        public virtual string getApiKey(string apiKey, string source_code, string versionStr, long? currentTime, string nonce)
        {
            double version_double = Convert.ToDouble(versionStr);
            int version = (int)(version_double * 100.0D);
            string versionHex = "0x" + version.ToString("x");
            StringBuilder s = new StringBuilder();
            s.Append(source_code);
            s.Append(versionHex);
            s.Append(apiKey);
            s.Append(currentTime);
            s.Append(nonce);
            string key = s.ToString().ToSHA256();
            return key;
        }

        public virtual string checkCode(string msgs)
        {
            return msgs.ToMD5();
        }


        private long? seqence()
        {
            lock (this)
            {
                long? seq = null;
                string seq_date = null;
                try
                {
                    string f_seq = FileUtil.Instance.getPropertes("seq");
                    seq_date = FileUtil.Instance.getPropertes("date");
                    if (string.IsNullOrEmpty(f_seq))
                    {
                        FileUtil.Instance.initProertes(1L);
                        return Convert.ToInt64(1L);
                    }
                    seq = Convert.ToInt64(f_seq);
                    if (string.IsNullOrEmpty(seq_date))
                    {
                        seq_date = DateTime.Now.ToString("yyyyMMdd");
                    }
                }
                catch (CertException e)
                {
                    throw new CertException(1005, e);
                }
                if (seq.Value >= MAX_VALUE.Value)
                {
                    return Convert.ToInt64(-1L);
                }
                if ((!string.ReferenceEquals(seq_date, null)) && (seq_date.Equals(DateTime.Now.ToString("yyyyMMdd"))))
                {
                    seq = Convert.ToInt64(seq.Value + 1L);
                    FileUtil.Instance.updateProertes(seq.Value);
                }
                return seq;
            }
        }

        public virtual void editStartNumber(long? startSq)
        {
            if (startSq == null)
            {
                startSq = Convert.ToInt64(1L);
            }
            FileUtil.Instance.initProertes(startSq.Value);
        }

    }
}
