using CodeGenerator.Infrastructure;
using CodeGenerator.Infrastructure.ValueModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CodeGenerator.Infrastructure.Options;

namespace CodeGenerator.Infrastructure.Impl
{
    public class JwtHelper : IJwtHelper
    {
        private readonly UserCache _userCache;
        private readonly IRedis _redis;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JwtOptions _options;

        public JwtHelper(IRedis redis,
            IHttpContextAccessor httpContextAccessor,
            IOptions<JwtOptions> options)
        {
            this._redis = redis;
            this._httpContextAccessor = httpContextAccessor;
            this._options = options.Value;
            this._userCache = new UserCache(this._redis, this._options.CacheKeyPrefix);
        }

        public string CreateToken(UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret));
            var authTime = DateTime.Now;
            var expireAt = authTime.AddMinutes(_options.ExpireMinutes);
            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Sid,user.UserId),
                new Claim(ClaimTypes.Name,user.Account),
                new Claim(ClaimTypes.GivenName,user.UserName),
                new Claim(ClaimTypes.Surname,string.IsNullOrEmpty(user.RealName)?"":user.RealName),
                new Claim(ClaimTypes.Role,string.IsNullOrEmpty(user.RoleName)?"":user.RoleName),
                new Claim(ClaimTypes.UserData,string.IsNullOrEmpty(user.PositionName)?"":user.PositionName),
                new Claim(ClaimTypes.Expiration,expireAt.ToString(CultureInfo.InvariantCulture)),
            };

            identity.AddClaims(claims);
            _httpContextAccessor.HttpContext.SignInAsync(
                JwtBearerDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _options.Issuer,
                Audience = _options.Audience,
                Expires = expireAt,
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenStr = tokenHandler.WriteToken(token);
            if (_userCache.Existence(user.UserId).Result)
            {
                _ = DeactivateTokenByUserId(user.UserId);
            }

            _ = _userCache.SetUserCache(user.UserId, tokenStr, _options.ExpireMinutes);
            _redis.SetStringAsync(tokenStr, JsonConvert.SerializeObject(user), TimeSpan.FromMinutes(_options.ExpireMinutes));
            return tokenStr;
        }

        /// <summary>
        /// 判断当前 Token 是否有效
        /// </summary>
        /// <returns></returns>
        public async Task<bool> IsCurrentActiveTokenAsync()
            => await IsActiveAsync(GetCurrentUserToken());

        /// <summary>
        /// 判断 Token 是否有效
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns></returns>
        public async Task<bool> IsActiveAsync(string token)
        {
            try
            {
                var existCache = await _redis.KeyExistsAsync(token);
                return existCache;

            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// 获取 HTTP 请求的 Token 值
        /// </summary>
        /// <returns></returns>
        public string GetCurrentUserToken()
        {
            //http header
            var authorizationHeader = _httpContextAccessor
                .HttpContext.Request.Headers["authorization"];

            //token
            return authorizationHeader == StringValues.Empty
                ? string.Empty
                : authorizationHeader.Single().Split(" ").Last();// bearer token

        }

        /// <summary>
        /// 注销用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task DeactivateTokenByUserId(string userId)
        {
            var token = GetExistenceToken(userId);
            if (token == null) return;
            await _userCache.Delete(userId);
            await _redis.KeyDeleteAsync(token);
        }

        /// <summary>
        /// 停止用户的当前登录
        /// </summary>
        /// <returns></returns>
        public async Task DeactivateTokenCurrentAsync()
        {
            var token = GetCurrentUserToken();
            if (token == null) return;
            await DeactivateTokenByToken(token);
        }

        /// <summary>
        /// 判断是否存在当前 Token
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private string GetExistenceToken(string userId)
            => _userCache.GetUserCache(userId).Result;

        /// <summary>
        /// 判断是否存在当前 UserId
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private string GetExistenceUserId(string token)
        {
            var obj = _redis.GetStringAsync(token);
            var user = JsonConvert.DeserializeObject<UserModel>(obj.Result);
            return user.UserId;
        }

        /// <summary>
        /// 停止用户的当前登录 Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task DeactivateTokenByToken(string token)
        {
            var userId = GetExistenceUserId(token);
            await _userCache.Delete(userId);
            await _redis.KeyDeleteAsync(token);
        }
    }

    internal class UserCache
    {
        private readonly string _keyPrefix;
        private readonly IRedis _redis;

        public UserCache(IRedis redis, string prefix)
        {
            _redis = redis;
            _keyPrefix = $"{prefix}_userKey_";
        }

        /// <summary>
        /// 用户缓存是否存在
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> Existence(string userId)
        {
            var key = $"{_keyPrefix}{userId}";
            return await _redis.KeyExistsAsync(key);
        }

        public async Task SetUserCache(string userId, string token, double expireMinutes)
        {
            var key = $"{_keyPrefix}{userId}";
            var expiry = TimeSpan.FromMinutes(expireMinutes);
            await _redis.SetStringAsync(key, token, expiry);
        }

        public async Task<string> GetUserCache(string userId)
        {
            var key = $"{_keyPrefix}{userId}";
            var cache = await _redis.GetStringAsync(key);
            return cache;
        }

        public async Task Delete(string userId)
        {
            var key = $"{_keyPrefix}{userId}";
            await _redis.KeyDeleteAsync(key);
        }
    }

}
