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

    * 类 名 称 ：ErrorCode

    * 类 描 述 ：

    * 所在的域 ：SC-201804231815

    * 命名空间 ：IFCert

    * 机器名称 ：SC-201804231815 

    * CLR 版本 ：4.0.30319.42000

    * 作    者 ：SamChen

    * 创建时间 ：2019/04/10 AM 10:22:15

    * 更新时间 ：2019/04/10 AM 10:22:15

    * 版 本 号 ：v1.0.0.0

    *******************************************************************
    * Copyright @ 江苏苏诚金融 2019. All rights reserved.
    *******************************************************************

    //----------------------------------------------------------------*/

    #endregion

    public class ErrorCode
    {
        private const long serialVersionUID = -1679458253208555786L;
        public const int PARAMETER_EMPTY = 1001;
        public const int PARAMETER_INVALID = 1002;
        public const int MAKE_HASH_ERROR = 1003;
        public const int FILE_ERROR = 1004;
        public const int BATCHNUM_ERROR = 1005;
        public const int EDIT_ERROR = 1006;
        public const int APIKEY_ERROR = 1007;
        public const int NETWORK_ERROR = 1100;
        public const int IDCARD_ERROR = 1108;
        public const int PHONE_ERROR = 1109;
        public const int OMPANYIDCARD_ERROR = 1110;
    }
}
