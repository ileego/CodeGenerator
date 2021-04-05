using System;

namespace CodeGenerator.Application.DTOs.EmployeeAuthorize
{
    /// <summary>
    /// 工作人员授权 - 查询结果
    /// </summary>
    public class EmployeeAuthorizeDto
    {
		/// <summary>
		/// 主键
		/// </summary>
		public long Id { get; set; }
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
