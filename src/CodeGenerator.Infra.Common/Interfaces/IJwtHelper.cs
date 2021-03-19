using System.Threading.Tasks;
using CodeGenerator.Infra.Common.ValueModel;

namespace CodeGenerator.Infra.Common.Interfaces
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
        /// 判断当前 Token 是否有效
        /// </summary>
        /// <returns></returns>
        Task<bool> IsCurrentActiveTokenAsync();
        /// <summary>
        /// 当前用户 Token
        /// </summary>
        /// <returns></returns>
        string GetCurrentUserToken();
        /// <summary>
        /// 注销用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task DeactivateTokenByUserId(string userId);
        /// <summary>
        /// 停止用户的当前登录
        /// </summary>
        /// <returns></returns>
        Task DeactivateTokenCurrentAsync();
    }

    public enum TokenType
    {
        AccessToken = 1,
        RefreshToken = 2
    }

}
