using CodeGenerator.Infra.Common;
using CodeGenerator.Infra.Common.ValueModel;
using Newtonsoft.Json;
using System.Threading.Tasks;
using CodeGenerator.Infra.Common.Cache;
using CodeGenerator.Infra.Common.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace CodeGenerator.Infra.Common.Helper
{
    /// <summary>
    /// 当前登录用户工具
    /// </summary>
    public class CurrentUserHelper
    {
        private readonly ICache _redis;
        private readonly IJwtHelper _jwt;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthenticationSchemeProvider _schemeProvider;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="redis"></param>
        /// <param name="jwtHelper"></param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="schemeProvider"></param>
        public CurrentUserHelper(
            ICache redis,
            IJwtHelper jwtHelper,
            IHttpContextAccessor httpContextAccessor,
            IAuthenticationSchemeProvider schemeProvider)
        {
            _redis = redis;
            _jwt = jwtHelper;
            _httpContextAccessor = httpContextAccessor;
            _schemeProvider = schemeProvider;
        }

        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        public async Task<UserModel> GetUser()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var defaultAuthenticate = await _schemeProvider.GetDefaultAuthenticateSchemeAsync();
            var result = await httpContext.AuthenticateAsync(defaultAuthenticate.Name);
            if (result.Succeeded)
            {
                //获取 Token
                var token = result.Ticket.Properties.Items[".Token.access_token"];
                var obj = await _redis.GetStringAsync(token);
                var user = JsonConvert.DeserializeObject<UserModel>(obj);
                return user;
            }
            return null;


        }

        /// <summary>
        /// 使用Token从缓存中获取登录用户
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<UserModel> GetUser(string token)
        {
            var obj = await _redis.GetStringAsync(token);
            var user = JsonConvert.DeserializeObject<UserModel>(obj);
            return user;
        }
    }
}
