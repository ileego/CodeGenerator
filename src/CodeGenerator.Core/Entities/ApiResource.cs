using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using CodeGenerator.Infra.Common.Entity;

namespace CodeGenerator.Core.Entities
{
    /// <summary>
    /// Api资源
    /// </summary>
    public class ApiResource : FullAuditEntity
    {
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