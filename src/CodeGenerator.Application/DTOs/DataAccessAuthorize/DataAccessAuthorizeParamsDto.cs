using System;
using CodeGenerator.Infra.Common.Paging;

namespace CodeGenerator.Application.DTOs.DataAccessAuthorize
{
    /// <summary>
    /// 数据访问授权 - 查询参数
    /// </summary>
    public class DataAccessAuthorizeParamsDto
    {
        /// <summary>
        /// 分页参数
        /// </summary>
        public PagedModel PageParams { get; set; }
		/// <summary>
		/// 角色主键
		/// </summary>
		public long ApplicationRoleId { get; set; }
		/// <summary>
		/// 行政区主键
		/// </summary>
		public long DistrictId { get; set; }
   
    }
}
