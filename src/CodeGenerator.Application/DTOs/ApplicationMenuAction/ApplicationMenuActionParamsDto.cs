using System;
using CodeGenerator.Infra.Common.Paging;

namespace CodeGenerator.Application.DTOs.ApplicationMenuAction
{
    /// <summary>
    /// 应用菜单功能关联 - 查询参数
    /// </summary>
    public class ApplicationMenuActionParamsDto
    {
        /// <summary>
        /// 分页参数
        /// </summary>
        public PagedModel PageParams { get; set; }
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
