using System;
using CodeGenerator.Infra.Common.Paging;

namespace CodeGenerator.Application.DTOs.ClientsAuthorize
{
    /// <summary>
    /// 客户端授权 - 查询参数
    /// </summary>
    public class ClientsAuthorizeParamsDto
    {
        /// <summary>
        /// 分页参数
        /// </summary>
        public PagedModel PageParams { get; set; }
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
