using System;

namespace CodeGenerator.Infra.Common.Helper.Excel
{
    [Obsolete(message: "instand of ExcelTitleAttribute")]
    public class ExcelAttribute : ExcelTitleAttribute
    {
        public ExcelAttribute(string name) : base(name) { }
    }
    public class ExcelTitleAttribute : Attribute
    {
        public ExcelTitleAttribute(string title)
        {
            Title = title;
        }

        public int Order { get; set; }
        public string Title { get; set; }
        public class NeedSumAttribute : Attribute
        {

        }

    }
}
