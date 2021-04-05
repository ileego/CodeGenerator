using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using CodeGenerator.Infra.Common.Entity;

namespace CodeGenerator.Core.Entities
{
    /// <summary>
    /// 应用系统
    /// </summary>
    public class ApplicationSystem : FullAuditEntity
    {
		/// <summary>
		/// 应用编号
		/// </summary>
		public string AppNo { get; set; }
		/// <summary>
		/// 应用名称
		/// </summary>
		public string AppName { get; set; }
		/// <summary>
		/// 显示名称
		/// </summary>
		public string DisplayName { get; set; }
		/// <summary>
		/// 应用地址
		/// </summary>
		public string AppUrl { get; set; }
		/// <summary>
		/// 应用图标Url
		/// </summary>
		public string AppIconUrl { get; set; }
		/// <summary>
		/// 密钥
		/// </summary>
		public string AppSecretKey { get; set; }
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