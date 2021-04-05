using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using CodeGenerator.Infra.Common.Entity;

namespace CodeGenerator.Core.Entities
{
    /// <summary>
    /// 部门
    /// </summary>
    public class Department : FullAuditEntity
    {
		/// <summary>
		/// 上级部门主键
		/// </summary>
		public long? DepartmentId { get; set; }
		/// <summary>
		/// 所属组织主键
		/// </summary>
		public long OrganizationId { get; set; }
		/// <summary>
		/// 部门名称
		/// </summary>
		public string DepartmentName { get; set; }
		/// <summary>
		/// 部门主管
		/// </summary>
		public string DepartmentHead { get; set; }
		/// <summary>
		/// 定序位置
		/// </summary>
		public short? OrdinalPosition { get; set; }
		/// <summary>
		/// 是否已启用
		/// </summary>
		public bool IsEnabled { get; set; }
		/// <summary>
		/// 当前层级
		/// </summary>
		public short CurrentLevel { get; set; }

		/// <summary>
		/// 部门的子节点
		/// </summary>
		public virtual ICollection<Department> Children { get; set; }
		/// <summary>
		/// 所属组织
		/// </summary>
		public virtual Organization Organization { get; set; }
     
    }
}