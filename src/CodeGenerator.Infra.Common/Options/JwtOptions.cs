namespace CodeGenerator.Infra.Common.Options
{
    /// <summary>
    /// Jwt 配置项
    /// </summary>
    public class JwtOptions
    {
        /// <summary>
        /// 缓存键前缀
        /// </summary>
        public string CacheKeyPrefix { get; set; }

        /// <summary>
        /// 颁发者
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Audience
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 加密Key
        /// </summary>
        public string SymmetricSecurityKey { get; set; }

        /// <summary>
        /// 时钟歪斜(分钟)
        /// </summary>
        public int ClockSkew { get; set; }

        /// <summary>
        /// AccessToken 过期时间（分钟）
        /// </summary>
        public int AccessTokenExpire { get; set; }

        /// <summary>
        ///  RefreshToken过期时间（分钟）
        /// </summary>
        public int RefreshTokenExpire { get; set; }
    }
}
