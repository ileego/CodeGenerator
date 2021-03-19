using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodeGenerator.Infra.Common.Helper.Excel
{
    public class ExcelConvert
    {
        public byte[] ObjectToExcelBytes<TModel>(IEnumerable<TModel> data, ICollection<string> hideCol = null)
        {
            var workbook = new XSSFWorkbook();
            var sheet = workbook.CreateSheet();
            var attrDict = ExcelUtil.GetExportAttrDict<TModel>(hideCol);
            var attrArray = attrDict.OrderBy(c => c.Value.Order).ToArray();

            var headerRow = sheet.CreateRow(0);
            headerRow.CreateCell(0).SetCellValue("序号");
            for (var i = 0; i < attrArray.Length; i++)
            {
                headerRow.CreateCell(i + 1).SetCellValue(attrArray[i].Value.Title);
            }
            var rowNumber = 1;
            foreach (var item in data)
            {
                var row = sheet.CreateRow(rowNumber++);
                row.CreateCell(0).SetCellValue(rowNumber - 1);
                for (var i = 0; i < attrArray.Length; i++)
                {
                    if ((attrArray[i].Key.GetValue(item, null) ?? "") is DateTime)
                    {
                        if (attrArray[i].Key.Name.ToLower().Contains("time"))
                        {
                            row.CreateCell(i + 1).SetCellValue(Convert.ToDateTime(attrArray[i].Key.GetValue(item, (object[])null) ?? "")
                                .ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                        else
                        {
                            row.CreateCell(i + 1).SetCellValue(Convert.ToDateTime(attrArray[i].Key.GetValue(item, (object[])null) ?? "")
                                .ToString("yyyy-MM-dd"));
                        }

                        continue;
                    }

                    row.CreateCell(i + 1).SetCellValue((attrArray[i].Key.GetValue(item, (object[])null) ?? "").ToString());
                }
            }
            for (var i = 0; i < attrArray.Length; i++)
            {
                sheet.AutoSizeColumn(i);
            }

            using (var output = new MemoryStream())
            {
                workbook.Write(output);
                var bytes = output.ToArray();
                return bytes;
            }
        }
        public byte[] ObjectToExcelBytes<TModel>(Dictionary<string, string> dict, IEnumerable<TModel> models)
        {
            var workbook = new XSSFWorkbook();
            var sheet = workbook.CreateSheet();
            var attrArray = dict.ToArray();

            var headerRow = sheet.CreateRow(0);

            for (var i = 0; i < attrArray.Length; i++)
            {
                headerRow.CreateCell(i).SetCellValue(attrArray[i].Value);
            }
            var rowNumber = 1;
            foreach (var model in models)
            {
                var row = sheet.CreateRow(rowNumber++);

                for (var i = 0; i < attrArray.Length; i++)
                {
                    var propertyInfo = typeof(TModel).GetProperty(attrArray[i].Key);
                    if (propertyInfo == null) continue;
                    var pValue = propertyInfo.GetValue(model);
                    if (pValue == null) continue;
                    row.CreateCell(i).SetCellValue(pValue.ToString());
                }
            }
            for (var i = 0; i < attrArray.Length; i++)
            {
                sheet.AutoSizeColumn(i);
            }

            using (var output = new MemoryStream())
            {
                workbook.Write(output);
                var bytes = output.ToArray();
                return bytes;
            }
        }
    }
}
