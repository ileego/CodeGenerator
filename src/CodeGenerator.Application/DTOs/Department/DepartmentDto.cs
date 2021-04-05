using System;

namespace CodeGenerator.Application.DTOs.Department
{
    /// <summary>
    /// 部门 - 查询结果
    /// </summary>
    public class DepartmentDto
    {
		/// <summary>
		/// 主键
		/// </summary>
		public long Id { get; set; }
		/// <summary>
		/// 上级部门主键
		/// </summary>
		public long? DepartmentId { get; set; }
		/// <summary>
		/// 所属组织主键
		/// </summary>
		public long OrganizationId { get; set; }
		/// <summary>
		/// 部门名称
		/// </summary>
		public string DepartmentName { get; set; }
		/// <summary>
		/// 部门主管
		/// </summary>
		public string DepartmentHead { get; set; }
		/// <summary>
		/// 定序位置
		/// </summary>
		public short? OrdinalPosition { get; set; }
		/// <summary>
		/// 是否已启用
		/// </summary>
		public bool IsEnabled { get; set; }
		/// <summary>
		/// 当前层级
		/// </summary>
		public short CurrentLevel { get; set; }
		/// <summary>
		/// 创建人
		/// </summary>
		public long Creator { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreationTime { get; set; }
		/// <summary>
		/// 最后修改人
		/// </summary>
		public long? LastModifier { get; set; }
		/// <summary>
		/// 最后修改时间
		/// </summary>
		public DateTime? LastModificationTime { get; set; }
		/// <summary>
		/// 是否已删除
		/// </summary>
		public bool IsDeleted { get; set; }
		/// <summary>
		/// 删除人
		/// </summary>
		public long? Deleter { get; set; }
		/// <summary>
		/// 删除时间
		/// </summary>
		public DateTime? DeletionTime { get; set; }
	    
    }
}
