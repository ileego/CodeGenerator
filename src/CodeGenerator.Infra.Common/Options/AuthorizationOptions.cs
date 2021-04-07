namespace CodeGenerator.Infra.Common.Options
{
    /// <summary>
    /// 授权客户端配置
    /// </summary>
    public class AuthorizationOptions
    {
        /// <summary>
        /// 应用编号
        /// </summary>
        public string AppNo { get; set; }
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
