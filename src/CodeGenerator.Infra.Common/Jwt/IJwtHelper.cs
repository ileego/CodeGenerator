using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CodeGenerator.Infra.Common.ValueModel;

namespace CodeGenerator.Infra.Common.Jwt
{
    public interface IJwtHelper
    {
        /// <summary>
        /// 创建Token
        /// </summary>
        /// <param name="user"></param>
        /// <param name="tokenType"></param>
        /// <returns></returns>
        string CreateToken(UserModel user, TokenType tokenType);

        /// <summary>
        /// 创建AccessToken
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        string CreateAccessToken(UserModel user);

        /// <summary>
        /// 创建RefreshToken
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        string CreateRefreshToken(UserModel user);

        /// <summary>
        /// 使用RefreshToken创建AccessToken
        /// </summary>
        /// <param name="user"></param>
        /// <param name="refreshTokenTxt"></param>
        /// <returns></returns>
        string CreateAccessToken(UserModel user, string refreshTokenTxt);

        /// <summary>
        /// 缓存AccessToken
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        Task CacheAccessToken(string userId, string accessToken);

        /// <summary>
        /// 获取缓存中的AccessToken
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<string> GetCacheAccessToken(string userId);

        /// <summary>
        /// AccessToken是否在缓存中
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> AccessTokenIsCached(string userId);

        /// <summary>
        /// 移除缓存中的AccessToken
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task RemoveCacheAccessToken(string userId);

        /// <summary>
        /// 从Token中解析出Claims
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        IEnumerable<Claim> GetClaimsFromToken(string token);

    }

    public enum TokenType
    {
        AccessToken = 1,
        RefreshToken = 2
    }

}
