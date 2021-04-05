using System;
using CodeGenerator.Infra.Common.Paging;

namespace CodeGenerator.Application.DTOs.ApiResource
{
    /// <summary>
    /// Api资源 - 查询参数
    /// </summary>
    public class ApiResourceParamsDto
    {
        /// <summary>
        /// 分页参数
        /// </summary>
        public PagedModel PageParams { get; set; }
		/// <summary>
		/// ApiKey
		/// </summary>
		public string ApiKey { get; set; }
		/// <summary>
		/// Api名称
		/// </summary>
		public string ApiName { get; set; }
		/// <summary>
		/// 显示名称
		/// </summary>
		public string DisplayName { get; set; }
		/// <summary>
		/// Api文档Url
		/// </summary>
		public string ApiDocumentUrl { get; set; }
		/// <summary>
		/// 描述
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		/// 是否已启用
		/// </summary>
		public bool IsEnabled { get; set; }
   
    }
}
