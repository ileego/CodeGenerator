using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using CodeGenerator.Infra.Common.Entity;

namespace CodeGenerator.Core.Entities
{
    /// <summary>
    /// 客户端授权
    /// </summary>
    public class ClientsAuthorize : BaseEntity
    {
		/// <summary>
		/// Api资源主键
		/// </summary>
		public long ApiResourceId { get; set; }
		/// <summary>
		/// 客户端主键
		/// </summary>
		public long ClientId { get; set; }

		/// <summary>
		/// Api资源
		/// </summary>
		public virtual ApiResource ApiResource { get; set; }
		/// <summary>
		/// 客户端
		/// </summary>
		public virtual Client Client { get; set; }
     
    }
}