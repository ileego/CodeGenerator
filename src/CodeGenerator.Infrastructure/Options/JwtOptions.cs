namespace CodeGenerator.Infrastructure.Options
{
    public class JwtOptions
    {
        public string CacheKeyPrefix { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Secret { get; set; }
        public double ExpireMinutes { get; set; }
    }
}
