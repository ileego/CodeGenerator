using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using CodeGenerator.Infra.Common.Entity;

namespace CodeGenerator.Core.Entities
{
    /// <summary>
    /// 工作人员授权
    /// </summary>
    public class EmployeeAuthorize : BaseEntity
    {
		/// <summary>
		/// 角色主键
		/// </summary>
		public long ApplicationRoleId { get; set; }
		/// <summary>
		/// 工作人员主键
		/// </summary>
		public long EmployeeId { get; set; }

		/// <summary>
		/// 角色
		/// </summary>
		public virtual ApplicationRole ApplicationRole { get; set; }
		/// <summary>
		/// 工作人员
		/// </summary>
		public virtual Employee Employee { get; set; }
     
    }
}