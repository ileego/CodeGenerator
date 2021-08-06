using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CodeGenerator.Infra.Common.Cache;
using CodeGenerator.Infra.Common.Extensions.String;
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

        /// <summary>
        /// 获取缓存中的AccessToken
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<string> GetCacheAccessToken(string userId)
        {
            var key = $"{_options.CacheKeyPrefix}:userId:{userId}";
            var accessToken = await _cache.GetStringAsync(key);
            return accessToken;
        }

        /// <summary>
        /// AccessToken是否在缓存中
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> AccessTokenIsCached(string userId)
        {
            var key = $"{_options.CacheKeyPrefix}:userId:{userId}";
            return await _cache.KeyExistsAsync(key);
        }

        /// <summary>
        /// 移除缓存中的AccessToken
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task RemoveCacheAccessToken(string userId)
        {
            var key = $"{_options.CacheKeyPrefix}:userId:{userId}";
            await _cache.KeyDeleteAsync(key);
        }

        /// <summary>
        /// 从Token中解析出Claims
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IEnumerable<Claim> GetClaimsFromToken(string token)
        {
            if (token.IsNullOrEmpty())
            {
                throw new ArgumentNullException();
            }
            var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            if (jwtSecurityToken == null)
            {
                throw new ArgumentException("token is wrong");
            }

            return jwtSecurityToken.Claims;
        }
    }
}
