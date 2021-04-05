using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using CodeGenerator.Infra.Common.Entity;

namespace CodeGenerator.Core.Entities
{
    /// <summary>
    /// 应用菜单
    /// </summary>
    public class ApplicationMenu : FullAuditEntity
    {
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
		/// 应用菜单的子节点
		/// </summary>
		public virtual ICollection<ApplicationMenu> Children { get; set; }
		/// <summary>
		/// 所属应用
		/// </summary>
		public virtual ApplicationSystem ApplicationSystem { get; set; }
     
    }
}