namespace CodeGenerator.Core.Interfaces
{
    /// <summary>
    /// 字段
    /// </summary>
    public interface IField
    {
        /// <summary>
        /// 属于哪个表
        /// </summary>
        string ToTable { get; set; }
        /// <summary>
        /// 字段名
        /// </summary>
        string FieldName { get; set; }
        /// <summary>
        /// 数据库数据类型
        /// </summary>
        string DataType { get; set; }
        /// <summary>
        /// 字段长度
        /// </summary>
        int Length { get; set; }
        /// <summary>
        /// 精度
        /// </summary>
        int? Precision { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        string Comment { get; set; }
        /// <summary>
        /// 是否键
        /// </summary>
        bool IsKey { get; set; }
        /// <summary>
        /// 键类型
        /// </summary>
        KeyTypeEnum? KeyType { get; set; }
    }
}
