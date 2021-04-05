using System;

namespace CodeGenerator.Application.DTOs.ClientsAuthorize
{
    /// <summary>
    /// 客户端授权 - 查询结果
    /// </summary>
    public class ClientsAuthorizeDto
    {
		/// <summary>
		/// 主键
		/// </summary>
		public long Id { get; set; }
		/// <summary>
		/// Api资源主键
		/// </summary>
		public long ApiResourceId { get; set; }
		/// <summary>
		/// 客户端主键
		/// </summary>
		public long ClientId { get; set; }
	    
    }
}
