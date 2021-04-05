using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using CodeGenerator.Infra.Common.Entity;

namespace CodeGenerator.Core.Entities
{
    /// <summary>
    /// 数据访问授权
    /// </summary>
    public class DataAccessAuthorize : BaseEntity
    {
		/// <summary>
		/// 角色主键
		/// </summary>
		public long ApplicationRoleId { get; set; }
		/// <summary>
		/// 行政区主键
		/// </summary>
		public long DistrictId { get; set; }

		/// <summary>
		/// 角色
		/// </summary>
		public virtual ApplicationRole ApplicationRole { get; set; }
		/// <summary>
		/// 行政区
		/// </summary>
		public virtual District District { get; set; }
     
    }
}