using System;

namespace CodeGenerator.Application.DTOs.ApplicationMenuAction
{
    /// <summary>
    /// 应用菜单功能关联 - 录入
    /// </summary>
    public class ApplicationMenuActionInputDto
    {
		/// <summary>
		/// 应用菜单主键
		/// </summary>
		public long ApplicationMenuId { get; set; }
		/// <summary>
		/// 应用功能主键
		/// </summary>
		public long ApplicationActionId { get; set; }
        
    }
}
