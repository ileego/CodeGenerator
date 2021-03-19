using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace CodeGenerator.Infra.Common.Helper.Excel
{
    internal class ExcelUtil
    {
        public static Dictionary<PropertyInfo, ExcelTitleAttribute> GetExportAttrDict<T>(ICollection<string> hideCol = null)
        {
            var dict = new Dictionary<PropertyInfo, ExcelTitleAttribute>();
            foreach (var propertyInfo in typeof(T).GetProperties())
            {
                if (hideCol != null && (hideCol.Count != 0 && hideCol.Contains(propertyInfo.Name)))
                {
                    continue;
                }
                var attr =
                    propertyInfo.GetCustomAttributes(true)
                        .FirstOrDefault(c => c is ExcelTitleAttribute || c is DisplayAttribute);
                if (attr == null) continue;
                var attr1 = attr;
                if (attr is DisplayAttribute)
                {
                    var display = attr as DisplayAttribute;
                    //attr1 = new ExcelTitleAttribute(display.Name) { Order = 1000 };
                    attr1 = new ExcelTitleAttribute(display.Name) { Order = display.Order };
                }
                dict.Add(propertyInfo, attr1 as ExcelTitleAttribute);
            }
            return dict;
        }
    }
}