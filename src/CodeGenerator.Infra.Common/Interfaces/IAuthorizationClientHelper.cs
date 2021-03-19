using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeGenerator.Infra.Common.ValueModel;

namespace CodeGenerator.Infra.Common.Interfaces
{
    public interface IAuthorizationClientHelper
    {
        /// <summary>
        /// 从授权服务获取用户信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<AuthorizeResult<UserModel>> GetUser(string token);
        /// <summary>
        /// 从授权服务获取本软件功能，IsSelected为True表示已授权
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<AuthorizeResult<ICollection<FunctionsQueryWithSelectedResult>>> GetFunctions(string token);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="changePasswordModel"></param>
        /// <returns></returns>
        Task<AuthorizeResult<bool>> ChangePassword(ChangePasswordModel changePasswordModel);
    }

    /// <summary>
    /// 授权Token基类
    /// </summary>
    public class RequestAuthorizeInfoModel
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 软件Id
        /// </summary>
        public string SoftwareId { get; set; }
    }

    /// <summary>
    /// 返回结果
    /// </summary>
    public class AuthorizeResult<T>
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Succeed { get; set; }
        /// <summary>
        /// 错误码
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 数据内容
        /// </summary>
        public T Content { get; set; }

    }

    /// <summary>
    /// 修改密码
    /// </summary>
    public class ChangePasswordModel : RequestAuthorizeInfoModel
    {
        /// <summary>
        /// 原密码
        /// </summary>
        public string OldPassword { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        public string NewPassword { get; set; }
        /// <summary>
        /// 新密码确认
        /// </summary>
        public string NewPasswordConfirm { get; set; }
    }

    /// <summary>
    /// 软件系统功能
    /// </summary>
    public class FunctionsQueryWithSelectedResult
    {
        /// <summary>        
        /// 主键
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>        
        /// 软件系统主键
        /// </summary>
        public Guid SoftwareSystemsId { get; set; }
        /// <summary>        
        /// 父级主键
        /// </summary>
        public Guid ParentId { get; set; }
        /// <summary>        
        /// 功能代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>        
        /// 功能名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>        
        /// 功能路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 功能图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>        
        /// 功能描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>        
        /// 操作按钮:设置按钮权限,Json结构,ButtonName:true
        /// </summary>
        public string OperatingButtons { get; set; }
        /// <summary>        
        /// 是否已启用
        /// </summary>
        public bool IsEnabled { get; set; }
        /// <summary>
        /// 是否选中授权
        /// </summary>
        public bool IsSelected { get; set; }
        /// <summary>
        /// 下级功能列表
        /// </summary>
        public ICollection<FunctionsQueryWithSelectedResult> Children { get; set; }

    }
}
