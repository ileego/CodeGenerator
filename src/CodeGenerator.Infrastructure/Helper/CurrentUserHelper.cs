using CodeGenerator.Infrastructure;
using CodeGenerator.Infrastructure.ValueModel;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CodeGenerator.Infrastructure.Helper
{
    /// <summary>
    /// 当前登录用户工具
    /// </summary>
    public class CurrentUserHelper
    {
        readonly IRedis _redis;
        readonly IJwtHelper _jwt;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="redis"></param>
        /// <param name="jwtHelper"></param>
        public CurrentUserHelper(
            IRedis redis,
            IJwtHelper jwtHelper)
        {
            _redis = redis;
            _jwt = jwtHelper;
        }

        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        public async Task<UserModel> GetUser()
        {
            string token = _jwt.GetCurrentUserToken();
            var obj = await _redis.GetStringAsync(token);
            var user = JsonConvert.DeserializeObject<UserModel>(obj);
            return user;
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
