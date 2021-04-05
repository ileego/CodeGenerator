using System;

namespace CodeGenerator.Application.DTOs.ApplicationAction
{
    /// <summary>
    /// 应用功能 - 查询结果
    /// </summary>
    public class ApplicationActionDto
    {
		/// <summary>
		/// 主键
		/// </summary>
		public long Id { get; set; }
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
		/// <summary>
		/// 创建人
		/// </summary>
		public long Creator { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime? CreationTime { get; set; }
		/// <summary>
		/// 最后修改人
		/// </summary>
		public long? LastModifier { get; set; }
		/// <summary>
		/// 最后修改时间
		/// </summary>
		public DateTime? LastModificationTime { get; set; }
		/// <summary>
		/// 是否已删除
		/// </summary>
		public bool IsDeleted { get; set; }
		/// <summary>
		/// 删除人
		/// </summary>
		public long? Deleter { get; set; }
		/// <summary>
		/// 删除时间
		/// </summary>
		public DateTime? DeletionTime { get; set; }
	    
    }
}
