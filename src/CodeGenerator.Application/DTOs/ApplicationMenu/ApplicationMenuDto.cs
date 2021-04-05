using System;

namespace CodeGenerator.Application.DTOs.ApplicationMenu
{
    /// <summary>
    /// 应用菜单 - 查询结果
    /// </summary>
    public class ApplicationMenuDto
    {
		/// <summary>
		/// 主键
		/// </summary>
		public long Id { get; set; }
		/// <summary>
		/// 父级应用菜单主键
		/// </summary>
		public long? ApplicationMenuId { get; set; }
		/// <summary>
		/// 所属应用
		/// </summary>
		public long ApplicationSystemId { get; set; }
		/// <summary>
		/// 菜单代码
		/// </summary>
		public string MenuCode { get; set; }
		/// <summary>
		/// 菜单名称
		/// </summary>
		public string MenuName { get; set; }
		/// <summary>
		/// 菜单Url
		/// </summary>
		public string MenuUrl { get; set; }
		/// <summary>
		/// 菜单图标
		/// </summary>
		public string MenuIcon { get; set; }
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
		/// 当前层级
		/// </summary>
		public short CurrentLevel { get; set; }
		/// <summary>
		/// 创建人
		/// </summary>
		public long Creator { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreationTime { get; set; }
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
