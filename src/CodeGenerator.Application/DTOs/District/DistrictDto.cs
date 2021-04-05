using System;

namespace CodeGenerator.Application.DTOs.District
{
    /// <summary>
    /// 行政区 - 查询结果
    /// </summary>
    public class DistrictDto
    {
		/// <summary>
		/// 主键
		/// </summary>
		public long Id { get; set; }
		/// <summary>
		/// 代码
		/// </summary>
		public string Code { get; set; }
		/// <summary>
		/// 名称
		/// </summary>
		public string Name { get; set; }
	    
    }
}
