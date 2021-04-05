using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using CodeGenerator.Infra.Common.Entity;

namespace CodeGenerator.Core.Entities
{
    /// <summary>
    /// 客户端
    /// </summary>
    public class Client : FullAuditEntity
    {
		/// <summary>
		/// 客户端编号
		/// </summary>
		public string ClientNo { get; set; }
		/// <summary>
		/// 客户端名称
		/// </summary>
		public string ClientName { get; set; }
		/// <summary>
		/// 客户端密钥
		/// </summary>
		public string ClientSecretKey { get; set; }
		/// <summary>
		/// Token有效期（分钟）
		/// </summary>
		public int TokenLifetime { get; set; }
		/// <summary>
		/// 续期Token有效期（分钟）
		/// </summary>
		public int? RefreshTokenLifetime { get; set; }
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