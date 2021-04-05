using System;

namespace CodeGenerator.Application.DTOs.RoleAuthorize
{
    /// <summary>
    /// 角色授权 - 录入
    /// </summary>
    public class RoleAuthorizeInputDto
    {
		/// <summary>
		/// 角色主键
		/// </summary>
		public long ApplicationRoleId { get; set; }
		/// <summary>
		/// 应用菜单主键
		/// </summary>
		public long ApplicationMenuId { get; set; }
		/// <summary>
		/// 可用功能：以json数组保存
		/// </summary>
		public string AvailableActions { get; set; }
        
    }
}
