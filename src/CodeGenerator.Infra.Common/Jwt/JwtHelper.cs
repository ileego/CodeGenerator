using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CodeGenerator.Infra.Common.Cache;
using CodeGenerator.Infra.Common.Options;
using CodeGenerator.Infra.Common.ValueModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace CodeGenerator.Infra.Common.Jwt
{
    public class JwtHelper : IJwtHelper
    {
        private readonly UserCache _userCache;
        private readonly ICache _cache;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JwtOptions _options;

        public JwtHelper(ICache cache,
            IHttpContextAccessor httpContextAccessor,
            IOptions<JwtOptions> options)
        {
            this._cache = cache;
            this._httpContextAccessor = httpContextAccessor;
            this._options = options.Value;
            this._userCache = new UserCache(this._cache, this._options.CacheKeyPrefix);
        }

        /// <summary>
        /// 创建Token
        /// </summary>
        /// <param name="user"></param>
        /// <param name="tokenType"></param>
        /// <returns></returns>
        public string CreateToken(UserModel user, TokenType tokenType)
        {
            string token;
            switch (tokenType)
            {
                case TokenType.AccessToken:
                    token = CreateAccessToken(user);
                    break;
                case TokenType.RefreshToken:
                    token = CreateRefreshToken(user);
                    break;
                default:
                    throw new Exception("Failed to create token");
            }

            return token;
        }

        /// <summary>
        /// 创建AccessToken
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string CreateAccessToken(UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SymmetricSecurityKey));
            var authTime = DateTime.Now;
            var expires = authTime.AddMinutes(_options.AccessTokenExpire);
            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
            var claims = new[]
            {
                new Claim(ClaimTypes.Sid,user.UserId.ToString()),
                new Claim(ClaimTypes.NameIdentifier,user.Account),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.GivenName,string.IsNullOrEmpty(user.RealName)?"":user.RealName),
            };

            identity.AddClaims(claims);
            _httpContextAccessor.HttpContext.SignInAsync(
                JwtBearerDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));
            var token = new JwtSecurityToken(
                claims: claims,
                issuer: _options.Issuer,
                audience: _options.Audience,
                notBefore: DateTime.Now,
                expires: expires,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            var accessToken = tokenHandler.WriteToken(token);
            return accessToken;
        }

        /// <summary>
        /// 创建RefreshToken
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string CreateRefreshToken(UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SymmetricSecurityKey));
            var authTime = DateTime.Now;
            var expires = authTime.AddMinutes(_options.RefreshTokenExpire);
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Sid, user.UserId.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Account)
            };
            var token = new JwtSecurityToken(
                claims: claims,
                issuer: _options.Issuer,
                audience: _options.Audience,
                notBefore: DateTime.Now,
                expires: expires,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            var refreshToken = tokenHandler.WriteToken(token);
            return refreshToken;
        }

        /// <summary>
        /// 使用RefreshToken创建AccessToken
        /// </summary>
        /// <param name="user"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public string CreateAccessToken(UserModel user, string refreshToken)
        {
            Check.NotNull(user, nameof(user));
            Check.NotNullOrEmpty(refreshToken, parameterName: nameof(refreshToken));
            var token = new JwtSecurityTokenHandler().ReadJwtToken(refreshToken);
            if (token == null)
            {
                throw new Exception("RefreshToken无法解析");
            }

            return CreateAccessToken(user);
        }

        /// <summary>
        /// 缓存AccessToken
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task CacheAccessToken(string userId, string accessToken)
        {
            var key = $"{_options.CacheKeyPrefix}:userId:{userId}";
            await _cache.SetStringAsync(key, accessToken, TimeSpan.FromMinutes(_options.AccessTokenExpire));
        }

        public void SaveToken(UserModel user, string accessToken)
        {
            if (_userCache.Existence(user.UserId.ToString()).Result)
            {
                _ = DeactivateTokenByUserId(user.UserId.ToString());
            }

            _ = _userCache.SetUserCache(user.UserId.ToString(), accessToken, _options.AccessTokenExpire);
            _cache.SetStringAsync(accessToken, JsonConvert.SerializeObject(user), TimeSpan.FromMinutes(_options.AccessTokenExpire));
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
                var existCache = await _cache.KeyExistsAsync(token);
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
            await _cache.KeyDeleteAsync(token);
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
            var obj = _cache.GetStringAsync(token);
            var user = JsonConvert.DeserializeObject<UserModel>(obj.Result);
            return user.UserId.ToString();
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
            await _cache.KeyDeleteAsync(token);
        }
    }

    internal class UserCache
    {
        private readonly string _keyPrefix;
        private readonly ICache _redis;

        public UserCache(ICache redis, string prefix)
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
