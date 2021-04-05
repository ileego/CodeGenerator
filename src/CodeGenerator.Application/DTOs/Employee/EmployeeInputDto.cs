using System;

namespace CodeGenerator.Application.DTOs.Employee
{
    /// <summary>
    /// 工作人员 - 录入
    /// </summary>
    public class EmployeeInputDto
    {
		/// <summary>
		/// 所属部门主键
		/// </summary>
		public long DepartmentId { get; set; }
		/// <summary>
		/// 账号
		/// </summary>
		public string Account { get; set; }
		/// <summary>
		/// 密码
		/// </summary>
		public string Password { get; set; }
		/// <summary>
		/// 姓名
		/// </summary>
		public string EmployeeName { get; set; }
		/// <summary>
		/// 性别
		/// </summary>
		public string Gender { get; set; }
		/// <summary>
		/// 民族
		/// </summary>
		public string Nation { get; set; }
		/// <summary>
		/// 出生日期
		/// </summary>
		public string BirthDate { get; set; }
		/// <summary>
		/// 证件类型
		/// </summary>
		public string CertificateType { get; set; }
		/// <summary>
		/// 证件号码
		/// </summary>
		public string CertificateNo { get; set; }
		/// <summary>
		/// 证件地址
		/// </summary>
		public string CertificateAddress { get; set; }
		/// <summary>
		/// 手机号
		/// </summary>
		public string MobileNo { get; set; }
		/// <summary>
		/// 联系电话
		/// </summary>
		public string ContactNumber { get; set; }
		/// <summary>
		/// 微信
		/// </summary>
		public string Weixin { get; set; }
		/// <summary>
		/// 电邮
		/// </summary>
		public string Email { get; set; }
		/// <summary>
		/// 通讯地址
		/// </summary>
		public string PostalAddress { get; set; }
		/// <summary>
		/// 紧急联系人
		/// </summary>
		public string EmergencyContact { get; set; }
		/// <summary>
		/// 紧急联系人电话
		/// </summary>
		public string EmergencyContactNumber { get; set; }
		/// <summary>
		/// 入职日期
		/// </summary>
		public DateTime? JoiningDate { get; set; }
		/// <summary>
		/// 工作岗位
		/// </summary>
		public string JobPosition { get; set; }
		/// <summary>
		/// 工作职务
		/// </summary>
		public string JobTitle { get; set; }
		/// <summary>
		/// 附加信息1
		/// </summary>
		public string AdditionalInformation1 { get; set; }
		/// <summary>
		/// 附加信息2
		/// </summary>
		public string AdditionalInformation2 { get; set; }
		/// <summary>
		/// 附加信息3
		/// </summary>
		public string AdditionalInformation3 { get; set; }
		/// <summary>
		/// 附加信息4
		/// </summary>
		public string AdditionalInformation4 { get; set; }
		/// <summary>
		/// 附加信息5
		/// </summary>
		public string AdditionalInformation5 { get; set; }
		/// <summary>
		/// 附加信息6
		/// </summary>
		public string AdditionalInformation6 { get; set; }
		/// <summary>
		/// 状态:正常、停用
		/// </summary>
		public string Status { get; set; }
        
    }
}
