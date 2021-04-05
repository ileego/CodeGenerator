using System;

namespace CodeGenerator.Application.DTOs.ClientsAuthorize
{
    /// <summary>
    /// 客户端授权 - 录入
    /// </summary>
    public class ClientsAuthorizeInputDto
    {
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
