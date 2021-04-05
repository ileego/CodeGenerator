using System;

namespace CodeGenerator.Application.DTOs.DataAccessAuthorize
{
    /// <summary>
    /// 数据访问授权 - 录入
    /// </summary>
    public class DataAccessAuthorizeInputDto
    {
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
