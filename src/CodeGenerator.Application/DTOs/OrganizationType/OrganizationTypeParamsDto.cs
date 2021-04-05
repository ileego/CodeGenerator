using System;
using CodeGenerator.Infra.Common.Paging;

namespace CodeGenerator.Application.DTOs.OrganizationType
{
    /// <summary>
    /// 组织类型 - 查询参数
    /// </summary>
    public class OrganizationTypeParamsDto
    {
        /// <summary>
        /// 分页参数
        /// </summary>
        public PagedModel PageParams { get; set; }
		/// <summary>
		/// 类型名称
		/// </summary>
		public string TypeName { get; set; }
   
    }
}
