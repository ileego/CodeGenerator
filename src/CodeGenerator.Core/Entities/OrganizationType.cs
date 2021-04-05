using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using CodeGenerator.Infra.Common.Entity;

namespace CodeGenerator.Core.Entities
{
    /// <summary>
    /// 组织类型
    /// </summary>
    public class OrganizationType : BaseEntity
    {
		/// <summary>
		/// 类型名称
		/// </summary>
		public string TypeName { get; set; }

     
    }
}