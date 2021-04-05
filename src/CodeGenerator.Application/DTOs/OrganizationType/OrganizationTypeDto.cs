using System;

namespace CodeGenerator.Application.DTOs.OrganizationType
{
    /// <summary>
    /// 组织类型 - 查询结果
    /// </summary>
    public class OrganizationTypeDto
    {
		/// <summary>
		/// 主键
		/// </summary>
		public long Id { get; set; }
		/// <summary>
		/// 类型名称
		/// </summary>
		public string TypeName { get; set; }
	    
    }
}
