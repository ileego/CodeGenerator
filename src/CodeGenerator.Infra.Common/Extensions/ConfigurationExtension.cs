using CodeGenerator.Infra.Common.Options;
using Microsoft.Extensions.Configuration;

namespace CodeGenerator.Infra.Common.Extensions
{
    public static class ConfigurationExtension
    {
        public static IConfigurationSection GetRedisConfigSection(this IConfiguration configuration)
        {
            return configuration.GetSection("RedisConfig");
        }
        public static RedisOptions GetRedisOptions(this IConfiguration configuration)
        {
            return configuration.GetRedisConfigSection().Get<RedisOptions>();
        }

        public static IConfigurationSection GetJwtConfigSection(this IConfiguration configuration)
        {
            return configuration.GetSection("JwtConfig");
        }

        public static JwtOptions GetJwtOptions(this IConfiguration configuration)
        {
            return configuration.GetJwtConfigSection().Get<JwtOptions>();
        }

        public static IConfigurationSection GetCorsConfigSection(this IConfiguration configuration)
        {
            return configuration.GetSection("CorsConfig");
        }

        public static CorsOptions GetCorsOptions(this IConfiguration configuration)
        {
            return configuration.GetCorsConfigSection().Get<CorsOptions>();
        }

        public static IConfigurationSection GetAuthorizationConfigSection(this IConfiguration configuration)
        {
            return configuration.GetSection("AuthorizationConfig");
        }

        public static AuthorizationOptions GetAuthorizationOptions(this IConfiguration configuration)
        {
            return configuration.GetAuthorizationConfigSection().Get<AuthorizationOptions>();
        }
    }
}
