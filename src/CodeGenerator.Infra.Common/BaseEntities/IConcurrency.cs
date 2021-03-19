namespace CodeGenerator.Infra.Common.BaseEntities
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConcurrency
    {
        /// <summary>
        /// 并发控制列
        /// </summary>
        public byte[] RowVersion { get; set; }
    }
}
