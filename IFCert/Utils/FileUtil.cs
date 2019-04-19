using IFCert.Open;
using System;
using System.Collections.Generic;
using System.Configuration;
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

    * 类 名 称 ：FileUtil

    * 类 描 述 ：

    * 所在的域 ：SC-201804231815

    * 命名空间 ：IFCert.Utils

    * 机器名称 ：SC-201804231815 

    * CLR 版本 ：4.0.30319.42000

    * 作    者 ：SamChen

    * 创建时间 ：2019/04/10 PM 1:00:36

    * 更新时间 ：2019/04/10 PM 1:00:36

    * 版 本 号 ：v1.0.0.0

    *******************************************************************
    * Copyright @ 江苏苏诚金融 2019. All rights reserved.
    *******************************************************************

    //----------------------------------------------------------------*/

    #endregion

    class FileUtil
    {
        private static FileUtil instance;
        private static readonly string SEQ_PATH = "properties.config";
        private const string SEQ_ARG1 = "date";
        private static readonly object lockObj = new object();
        private static readonly Configuration config ;
        static FileUtil() {
          
             config = System.Configuration.ConfigurationManager.OpenExeConfiguration(SEQ_PATH);
        }
        public static FileUtil Instance
        {
            get
            {
                lock (lockObj)
                {
                    if (instance == null)
                    {
                        instance = new FileUtil();
                    }
                    return instance;
                }
            }
        }

        public virtual string getPropertes(string key)
        {
             return  config.AppSettings.Settings[key]?.Value;
        }
        public virtual void initProertes(long seq)
        {
          
            try
            {
                if (config.AppSettings.Settings["date"] == null)
                {
                    config.AppSettings.Settings.Add("date", DateTime.Now.ToString("yyyyMMdd"));
                }
                else
                {
                    config.AppSettings.Settings["date"].Value = DateTime.Now.ToString("yyyyMMdd");
                }
                if (config.AppSettings.Settings["seq"] == null)
                {
                    config.AppSettings.Settings.Add("seq", seq.ToString());
                }
                else
                {
                    config.AppSettings.Settings["seq"].Value = seq.ToString();
                }
                config.Save(ConfigurationSaveMode.Modified);
                System.Configuration.ConfigurationManager.RefreshSection("appSettings");
            }
            catch (IOException e)
            {
                throw new CertException(1006, e);
            }
        }


        public virtual void updateProertes(long seq)
        {
 
            try
            {
                if (config.AppSettings.Settings["date"] == null)
                {
                    config.AppSettings.Settings.Add("date", DateTime.Now.ToString("yyyyMMdd"));
                }
                else {
                    config.AppSettings.Settings["date"].Value = DateTime.Now.ToString("yyyyMMdd");
                }
                if (config.AppSettings.Settings["seq"] == null)
                {
                    config.AppSettings.Settings.Add("seq", seq.ToString());
                }
                else {
                    config.AppSettings.Settings["seq"].Value = seq.ToString();
                }
                config.Save(ConfigurationSaveMode.Modified);
                System.Configuration.ConfigurationManager.RefreshSection("appSettings");
            }
            catch (IOException e)
            {
                throw new CertException(1006, e);
            }
        }

    }

}


