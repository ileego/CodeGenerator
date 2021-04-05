using System;
using CodeGenerator.Infra.Common.Paging;

namespace CodeGenerator.Application.DTOs.EmployeeAuthorize
{
    /// <summary>
    /// 工作人员授权 - 查询参数
    /// </summary>
    public class EmployeeAuthorizeParamsDto
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
		/// 工作人员主键
		/// </summary>
		public long EmployeeId { get; set; }
   
    }
}
