using System;

namespace CodeGenerator.Application.DTOs.Client
{
    /// <summary>
    /// 客户端 - 查询结果
    /// </summary>
    public class ClientDto
    {
		/// <summary>
		/// 主键
		/// </summary>
		public long Id { get; set; }
		/// <summary>
		/// 客户端编号
		/// </summary>
		public string ClientNo { get; set; }
		/// <summary>
		/// 客户端名称
		/// </summary>
		public string ClientName { get; set; }
		/// <summary>
		/// 客户端密钥
		/// </summary>
		public string ClientSecretKey { get; set; }
		/// <summary>
		/// Token有效期（分钟）
		/// </summary>
		public int TokenLifetime { get; set; }
		/// <summary>
		/// 续期Token有效期（分钟）
		/// </summary>
		public int? RefreshTokenLifetime { get; set; }
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
