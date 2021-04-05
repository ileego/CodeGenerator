using System;

namespace CodeGenerator.Application.DTOs.ApplicationRole
{
    /// <summary>
    /// 角色 - 录入
    /// </summary>
    public class ApplicationRoleInputDto
    {
		/// <summary>
		/// 所属应用主键
		/// </summary>
		public long ApplicationSystemId { get; set; }
		/// <summary>
		/// 角色代码
		/// </summary>
		public string RoleCode { get; set; }
		/// <summary>
		/// 角色名称
		/// </summary>
		public string RoleName { get; set; }
		/// <summary>
		/// 描述
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		/// 是否已启用
		/// </summary>
		public bool IsEnabled { get; set; }
        
    }
}
