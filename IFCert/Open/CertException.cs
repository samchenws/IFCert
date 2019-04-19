using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFCert.Open
{
    #region << 版 本 注 释 >>

    /*----------------------------------------------------------------

    * 项目名称 ：IFCert

    * 项目描述 ：

    * 类 名 称 ：CertException

    * 类 描 述 ：

    * 所在的域 ：SC-201804231815

    * 命名空间 ：IFCert

    * 机器名称 ：SC-201804231815 

    * CLR 版本 ：4.0.30319.42000

    * 作    者 ：SamChen

    * 创建时间 ：2019/04/10 AM 10:16:07

    * 更新时间 ：2019/04/10 AM 10:16:07

    * 版 本 号 ：v1.0.0.0

    *******************************************************************
    * Copyright @ 江苏苏诚金融 2019. All rights reserved.
    *******************************************************************

    //----------------------------------------------------------------*/

    #endregion

    public class CertException : Exception
    {
        private const long serialVersionUID = 8243127099991355146L;
        private int code;

        public CertException(int code, string msg) : base(msg)
        {
            this.code = code;
        }

        public CertException(int code, Exception ex) : base(ex.Message,ex)
        {
            this.code = code;
        }

        public virtual int ErrorCode
        {
            get
            {
                return this.code;
            }
        }

    }
}
