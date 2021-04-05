using System;

namespace CodeGenerator.Application.DTOs.EmployeeAuthorize
{
    /// <summary>
    /// 工作人员授权 - 录入
    /// </summary>
    public class EmployeeAuthorizeInputDto
    {
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
