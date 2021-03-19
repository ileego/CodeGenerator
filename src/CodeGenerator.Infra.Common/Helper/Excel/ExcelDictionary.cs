using System;
using System.Collections.Generic;
using System.Reflection;

namespace CodeGenerator.Infra.Common.Helper.Excel
{
    public static class ExcelDictionary
    {
        /// <summary>
        /// U => T属性映射键值对
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TBase"></typeparam>
        /// <param name="tSource"></param>
        /// <returns></returns>
        public static Dictionary<PropertyInfo, string> GetPropertyDictionary<TSource, TBase>(TSource tSource) where TSource : class, new()
        {
            var tsProperties = tSource.GetType().GetProperties();
            var keyValuePairs = new Dictionary<PropertyInfo, string>();
            try
            {
                foreach (var tsProperty in tsProperties)
                {
                    var value = tsProperty.GetValue(tSource, null);
                    var uProperty = typeof(TBase).GetProperty(tsProperty.Name);
                    if (uProperty == null || value == null) continue;
                    var str = value.ToString().Replace("\r", "").Trim();
                    str = str.Replace("\n", "");
                    keyValuePairs.Add(uProperty, str);
                }
                return keyValuePairs;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static Dictionary<string, string> GetPropertyDictionary(List<ExportExcelTitle> titles)
        {
            var kvs = new Dictionary<string, string>();

            foreach (var exportExcelTitle in titles)
            {
                if (exportExcelTitle.PropertyName == null) continue;
                var propName = exportExcelTitle.PropertyName;
                var title = exportExcelTitle.ExportTitle;
                kvs.Add(propName, title);
            }
            return kvs;
        }
    }
}