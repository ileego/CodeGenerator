
namespace CodeGenerator.Infrastructure.Helper.Excel
{
    /// <summary>
    /// 导入表格参数
    /// </summary>
    public class ExcelImpAttr
    {
        /// <summary>
        /// 要导入的Sheet名称
        /// </summary>
        public string SheetName { get; set; }
        /// <summary>
        /// 标题行号
        /// </summary>
        public int TitleIndex { get; set; }
        /// <summary>
        /// 读取的起始行号
        /// </summary>
        public int RowNumber { get; set; }
        /// <summary>
        /// 读取的总行数
        /// </summary>
        public int RowSize { get; set; }
    }
}