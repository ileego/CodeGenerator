using System;

namespace CodeGenerator.Application.DTOs.Organization
{
    /// <summary>
    /// 组织 - 查询结果
    /// </summary>
    public class OrganizationDto
    {
		/// <summary>
		/// 主键
		/// </summary>
		public long Id { get; set; }
		/// <summary>
		/// 行政区主键
		/// </summary>
		public long DistrictiD { get; set; }
		/// <summary>
		/// 上级组织主键
		/// </summary>
		public long? OrganizationId { get; set; }
		/// <summary>
		/// 组织类型主键
		/// </summary>
		public long OrganizationTypeId { get; set; }
		/// <summary>
		/// 统一社会信用代码 
		/// </summary>
		public string UnifiedSocialCreditCode { get; set; }
		/// <summary>
		/// 名称
		/// </summary>
		public string OrganizationName { get; set; }
		/// <summary>
		/// 法人
		/// </summary>
		public string LegalPerson { get; set; }
		/// <summary>
		/// 注册地址
		/// </summary>
		public string RegisteredAddress { get; set; }
		/// <summary>
		/// 联系人
		/// </summary>
		public string Contact { get; set; }
		/// <summary>
		/// 联系电话
		/// </summary>
		public string ContactNumber { get; set; }
		/// <summary>
		/// 联系地址
		/// </summary>
		public string ContactAddress { get; set; }
		/// <summary>
		/// 定序位置
		/// </summary>
		public short OrdinalPosition { get; set; }
		/// <summary>
		/// 状态:正常、停用
		/// </summary>
		public string Status { get; set; }
		/// <summary>
		/// 备注
		/// </summary>
		public string Remarks { get; set; }
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
