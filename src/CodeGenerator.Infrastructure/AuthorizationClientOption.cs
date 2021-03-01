using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator.Infrastructure
{
    /// <summary>
    /// 授权客户端配置
    /// </summary>
    public class AuthorizationClientOption
    {
        /// <summary>
        /// 软件Id
        /// </summary>
        public string SoftwareId { get; set; }
        /// <summary>
        /// 授权服务地址
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 密钥
        /// </summary>
        public string Secret { get; set; }
    }
}
