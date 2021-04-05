using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using CodeGenerator.Infra.Common.Entity;

namespace CodeGenerator.Core.Entities
{
    /// <summary>
    /// 角色
    /// </summary>
    public class ApplicationRole : FullAuditEntity
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

		/// <summary>
		/// 所属应用
		/// </summary>
		public virtual ApplicationSystem ApplicationSystem { get; set; }
     
    }
}