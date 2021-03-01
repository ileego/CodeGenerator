using CodeGenerator.Infrastructure.ValueModel;
using System.Threading.Tasks;

namespace CodeGenerator.Infrastructure
{
    public interface IJwtHelper
    {
        string CreateToken(UserModel user);
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
}
