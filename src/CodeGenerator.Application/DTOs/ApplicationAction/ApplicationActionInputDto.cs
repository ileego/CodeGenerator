using System;

namespace CodeGenerator.Application.DTOs.ApplicationAction
{
    /// <summary>
    /// 应用功能 - 录入
    /// </summary>
    public class ApplicationActionInputDto
    {
		/// <summary>
		/// 功能组标识
		/// </summary>
		public string GroupTag { get; set; }
		/// <summary>
		/// 功能组名称
		/// </summary>
		public string GroupName { get; set; }
		/// <summary>
		/// 功能标识
		/// </summary>
		public string ActionTag { get; set; }
		/// <summary>
		/// 功能名称
		/// </summary>
		public string ActionName { get; set; }
		/// <summary>
		/// 描述
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		/// 定序位置
		/// </summary>
		public short OrdinalPosition { get; set; }
		/// <summary>
		/// 是否已启用
		/// </summary>
		public bool IsEnabled { get; set; }
        
    }
}
