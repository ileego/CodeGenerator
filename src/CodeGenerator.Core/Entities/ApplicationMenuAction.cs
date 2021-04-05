using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using CodeGenerator.Infra.Common.Entity;

namespace CodeGenerator.Core.Entities
{
    /// <summary>
    /// 应用菜单功能关联
    /// </summary>
    public class ApplicationMenuAction : BaseEntity
    {
		/// <summary>
		/// 应用菜单主键
		/// </summary>
		public long ApplicationMenuId { get; set; }
		/// <summary>
		/// 应用功能主键
		/// </summary>
		public long ApplicationActionId { get; set; }

		/// <summary>
		/// 应用菜单
		/// </summary>
		public virtual ApplicationMenu ApplicationMenu { get; set; }
		/// <summary>
		/// 应用功能
		/// </summary>
		public virtual ApplicationAction ApplicationAction { get; set; }
     
    }
}