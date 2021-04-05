using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using CodeGenerator.Infra.Common.Entity;

namespace CodeGenerator.Core.Entities
{
    /// <summary>
    /// 角色授权
    /// </summary>
    public class RoleAuthorize : BaseEntity
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

		/// <summary>
		/// 角色
		/// </summary>
		public virtual ApplicationRole ApplicationRole { get; set; }
		/// <summary>
		/// 应用菜单
		/// </summary>
		public virtual ApplicationMenu ApplicationMenu { get; set; }
     
    }
}