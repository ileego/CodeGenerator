using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace CodeGenerator.Infrastructure.Helper.Excel
{
    public class ExcelHelper
    {
        /// <summary>
        /// import file excel file to a IEnumerable of TModel
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="path">excel full path</param>
        /// <returns></returns>
        public static IEnumerable<TModel> ExcelToObject<TModel>(string path) where TModel : class, new()
        {
            var importer = new ExcelInverse();
            return importer.ExcelToObject<TModel>(path);

        }

        public static IEnumerable<TModel> ExcelToObject<TModel>(ExcelImpAttr attr, IFormFile file) where TModel : class, new()
        {
            var importer = new ExcelInverse();
            return importer.ExcelToObject<TModel>(attr, file);

        }

        public static IEnumerable<TModel> ExcelToObject<TModel>(Dictionary<PropertyInfo, string> keyValuePairs, ExcelImpAttr rowModel, IFormFile file) where TModel : class, new()
        {
            var importer = new ExcelInverse();
            return importer.ExcelToObject<TModel>(keyValuePairs, rowModel, file);

        }
        /// <summary>
        /// Export object to excel file
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="data">a IEnumerable of TModel</param>
        /// <param name="path">excel full path</param>
        public static void ObjectToExcel<TModel>(IEnumerable<TModel> data, string path) where TModel : class, new()
        {
            var importer = new ExcelConvert();
            var bytes = importer.ObjectToExcelBytes<TModel>(data);
            var dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir) && !string.IsNullOrEmpty(dir))
            {
                Directory.CreateDirectory(dir);
            }
            File.WriteAllBytes(path, bytes);
        }

        /// <summary>
        /// Export object to excel file data
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ObjectToExcel<TModel>(IEnumerable<TModel> data) where TModel : class, new()
        {
            var importer = new ExcelConvert();
            var bytes = importer.ObjectToExcelBytes<TModel>(data);
            return bytes;
        }

        public static void ObjectToExcel<TModel>(Dictionary<string, string> expField, IEnumerable<TModel> models,
            string path) where TModel : class, new()
        {
            var importer = new ExcelConvert();
            var bytes = importer.ObjectToExcelBytes(expField, models);
            var dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir) && !string.IsNullOrEmpty(dir))
            {
                Directory.CreateDirectory(dir);
            }
            File.WriteAllBytes(path, bytes);
        }
    }
}
