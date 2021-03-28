namespace CodeGenerator.Core.Interfaces
{
    /// <summary>
    /// 键类型枚举
    /// </summary>
    public enum KeyTypeEnum
    {
        /// <summary>
        /// 主键
        /// </summary>
        PrimaryKey = 0,
        /// <summary>
        /// 外键
        /// </summary>
        ForeignKey = 1
    }

    /// <summary>
    /// 键接口
    /// </summary>
    public interface IKey
    {
        /// <summary>
        /// 键类型
        /// </summary>
        public KeyTypeEnum KeyType { get; set; }
        /// <summary>
        /// 键名称
        /// </summary>
        public string KeyName { get; set; }
    }
}
