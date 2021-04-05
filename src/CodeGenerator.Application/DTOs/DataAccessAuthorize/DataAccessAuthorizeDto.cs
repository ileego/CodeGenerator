using System;

namespace CodeGenerator.Application.DTOs.DataAccessAuthorize
{
    /// <summary>
    /// 数据访问授权 - 查询结果
    /// </summary>
    public class DataAccessAuthorizeDto
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
		/// 行政区主键
		/// </summary>
		public long DistrictId { get; set; }
	    
    }
}
