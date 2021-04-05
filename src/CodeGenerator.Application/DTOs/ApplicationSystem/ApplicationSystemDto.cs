using System;

namespace CodeGenerator.Application.DTOs.ApplicationSystem
{
    /// <summary>
    /// 应用系统 - 查询结果
    /// </summary>
    public class ApplicationSystemDto
    {
		/// <summary>
		/// 主键
		/// </summary>
		public long Id { get; set; }
		/// <summary>
		/// 应用编号
		/// </summary>
		public string AppNo { get; set; }
		/// <summary>
		/// 应用名称
		/// </summary>
		public string AppName { get; set; }
		/// <summary>
		/// 显示名称
		/// </summary>
		public string DisplayName { get; set; }
		/// <summary>
		/// 应用地址
		/// </summary>
		public string AppUrl { get; set; }
		/// <summary>
		/// 应用图标Url
		/// </summary>
		public string AppIconUrl { get; set; }
		/// <summary>
		/// 密钥
		/// </summary>
		public string AppSecretKey { get; set; }
		/// <summary>
		/// 描述
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		/// 是否已启用
		/// </summary>
		public bool IsEnabled { get; set; }
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
