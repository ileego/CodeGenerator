using Microsoft.AspNetCore.Http;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CodeGenerator.Infra.Common.Helper.Excel
{
    public class ExcelInverse
    {

        public IEnumerable<TModel> ExcelToObject<TModel>(ExcelImpAttr attr, IFormFile file) where TModel : class, new()
        {
            var result = GetDataRows(attr.SheetName, file);
            var dict = ExcelUtil.GetExportAttrDict<TModel>();
            var dictColumns = new Dictionary<int, KeyValuePair<PropertyInfo, ExcelTitleAttribute>>();

            var rows = result;

            //移动到标题行
            while (attr.TitleIndex > 1)
            {
                rows.MoveNext();
                attr.TitleIndex--;
            }

            var titleRow = (IRow)rows.Current;

            if (titleRow != null)
                foreach (var cell in titleRow.Cells)
                {
                    var prop = dict.FirstOrDefault(c => cell.StringCellValue.Trim() == c.Value.Title);
                    if (prop.Key != null && !dictColumns.ContainsKey(cell.ColumnIndex))
                    {
                        dictColumns.Add(cell.ColumnIndex, prop);
                    }
                }
            var models = new List<TModel>();
            while (rows.MoveNext())
            {
                var row = (IRow)rows.Current;
                if (row != null)
                {
                    var firstCell = row.GetCell(0);
                    if (firstCell == null || firstCell.CellType == CellType.Blank ||
                        string.IsNullOrWhiteSpace(firstCell.ToString()))
                        continue;
                }

                var model = new TModel();

                foreach (var pair in dictColumns)
                {
                    var propType = pair.Value.Key.PropertyType;
                    if (propType == typeof(DateTime?) ||
                        propType == typeof(DateTime))
                    {
                        pair.Value.Key.SetValue(model, GetCellDateTime(row, pair.Key), null);
                    }
                    else
                    {
                        var cellVal = GetCellValue(row, pair.Key);
                        if (string.IsNullOrEmpty(cellVal)) continue;
                        if (propType.Name.Equals("Decimal") && cellVal.Equals("#N/A"))
                        {
                            pair.Value.Key.SetValue(model, default(decimal), null);
                        }
                        else
                        {
                            var ss = Convert.ChangeType(cellVal, propType);
                            pair.Value.Key.SetValue(model, ss, null);
                        }

                    }
                }
                models.Add(model);
                //yield return model;
            }

            return models;
        }

        public IEnumerable<TModel> ExcelToObject<TModel>(string path, int? type = null) where TModel : class, new()
        {
            var result = GetDataRows(path);
            var dict = ExcelUtil.GetExportAttrDict<TModel>();
            var dictColumns = new Dictionary<int, KeyValuePair<PropertyInfo, ExcelTitleAttribute>>();

            var rows = result;

            var titleRow = (IRow)rows.Current;
            if (titleRow != null)
                foreach (var cell in titleRow.Cells)
                {
                    var prop = dict.FirstOrDefault(c => cell.StringCellValue.Trim() == c.Value.Title);
                    if (prop.Key != null && !dictColumns.ContainsKey(cell.ColumnIndex))
                    {
                        dictColumns.Add(cell.ColumnIndex, prop);
                    }
                }
            var models = new List<TModel>();
            while (rows.MoveNext())
            {
                var row = (IRow)rows.Current;
                if (row != null)
                {
                    var firstCell = row.GetCell(0);
                    if (firstCell == null || firstCell.CellType == CellType.Blank ||
                        string.IsNullOrWhiteSpace(firstCell.ToString()))
                        continue;
                }

                var model = new TModel();

                foreach (var pair in dictColumns)
                {
                    var propType = pair.Value.Key.PropertyType;
                    if (propType == typeof(DateTime?) ||
                        propType == typeof(DateTime))
                    {
                        pair.Value.Key.SetValue(model, GetCellDateTime(row, pair.Key), null);
                    }
                    else
                    {
                        var cellVal = GetCellValue(row, pair.Key);
                        if (string.IsNullOrEmpty(cellVal)) continue;
                        var ss = Convert.ChangeType(cellVal, propType);
                        pair.Value.Key.SetValue(model, ss, null);
                    }
                }
                models.Add(model);
                //yield return model;
            }

            return models;
        }
        public IEnumerable<TModel> ExcelToObject<TModel>(Dictionary<PropertyInfo, string> keyValuePairs, ExcelImpAttr rowModel, IFormFile file) where TModel : class, new()
        {
            var rows = GetDataRows(rowModel.SheetName, file);
            var titleNumber = rowModel.TitleIndex - 1;
            var rowNumber = rowModel.RowNumber - 1;
            var rowSize = rowModel.RowSize + rowNumber;
            var iRows = new List<IRow> { (IRow)rows.Current };
            while (rows.MoveNext())
            {
                iRows.Add((IRow)rows.Current);
            }
            var dictColumns = new Dictionary<int, KeyValuePair<PropertyInfo, string>>();

            var titleRow = iRows[titleNumber];
            if (titleRow != null)
                foreach (var cell in titleRow.Cells)
                {
                    var cellValue = cell.StringCellValue.Replace("\r", "").Trim();
                    cellValue = cellValue.Replace("\n", "");
                    var prop = keyValuePairs.FirstOrDefault(c => c.Value.ToString() == cellValue);
                    if (prop.Key != null && !dictColumns.ContainsKey(cell.ColumnIndex))
                    {
                        dictColumns.Add(cell.ColumnIndex, prop);
                    }
                }
            var models = new List<TModel>();
            for (var i = rowNumber; i < rowSize; i++)
            {
                var row = iRows[i];
                if (row != null)
                {
                    var firstCell = row.GetCell(0);
                    if (firstCell == null || firstCell.CellType == CellType.Blank ||
                        string.IsNullOrWhiteSpace(firstCell.ToString()))
                        continue;
                }
                var model = new TModel();
                foreach (var pair in dictColumns)
                {
                    var propType = pair.Value.Key.PropertyType;
                    if (propType == typeof(DateTime?) ||
                        propType == typeof(DateTime))
                    {
                        pair.Value.Key.SetValue(model, GetCellDateTime(row, pair.Key), null);
                    }
                    else
                    {
                        var cellValue = GetCellValue(row, pair.Key);
                        if (string.IsNullOrEmpty(cellValue)) continue;
                        var val = Convert.ChangeType(cellValue, propType);
                        pair.Value.Key.SetValue(model, val, null);
                    }
                }
                models.Add(model);
            }
            return models;
        }
        public IEnumerable<TModel> ExcelToObject<TModel>(Dictionary<PropertyInfo, string> keyValuePairs, int rowNumber, string path) where TModel : class, new()
        {
            var result = GetDataRows(path);
            //var dict = keyPairs;
            var dictColumns = new Dictionary<int, KeyValuePair<PropertyInfo, string>>();

            var rows = result;

            var titleRow = (IRow)rows.Current;
            if (titleRow != null)
                foreach (var cell in titleRow.Cells)
                {
                    var prop = keyValuePairs.FirstOrDefault(c => cell.StringCellValue.Trim() == c.Value.ToString());
                    if (prop.Key != null && !dictColumns.ContainsKey(cell.ColumnIndex))
                    {
                        dictColumns.Add(cell.ColumnIndex, prop);
                    }
                }
            var models = new List<TModel>();
            var x = 0;
            while (rows.MoveNext() && x < rowNumber)
            {

                var row = (IRow)rows.Current;
                if (row != null)
                {
                    var firstCell = row.GetCell(0);
                    if (firstCell == null || firstCell.CellType == CellType.Blank ||
                        string.IsNullOrWhiteSpace(firstCell.ToString()))
                        continue;
                }

                var model = new TModel();

                foreach (var pair in dictColumns)
                {
                    var propType = pair.Value.Key.PropertyType;
                    if (propType == typeof(DateTime?) ||
                        propType == typeof(DateTime))
                    {
                        pair.Value.Key.SetValue(model, GetCellDateTime(row, pair.Key), null);
                    }
                    else
                    {
                        var temp = GetCellValue(row, pair.Key);
                        switch (temp)
                        {
                            case "TRUE":
                                {
                                    var valtemp = Convert.ChangeType(true, propType);
                                    pair.Value.Key.SetValue(model, valtemp, null);
                                    continue;
                                }
                            case "FALSE":
                                {
                                    var valtemp = Convert.ChangeType(false, propType);
                                    pair.Value.Key.SetValue(model, valtemp, null);
                                    continue;
                                }
                        }
                        var val = Convert.ChangeType(GetCellValue(row, pair.Key), propType);
                        pair.Value.Key.SetValue(model, val, null);
                    }
                }
                models.Add(model);
                x++;
            }
            return models;
        }
        public List<ImportExcelTitle> ExcelTitles(string sheetName, int titleRowIndex, IFormFile file)
        {
            var rows = GetDataRows(sheetName, file);
            rows.Reset();
            var titleList = new List<ImportExcelTitle>();
            for (var i = 0; i < titleRowIndex; i++)
            {
                rows.MoveNext();
            }
            var titleRow = (IRow)rows.Current;
            if (titleRow != null)
                titleList.AddRange(titleRow.Cells.Select(cell =>
                    new ImportExcelTitle() { Title = cell.StringCellValue.Trim() }));
            return titleList;
        }
        public List<ImportExcelTitle> ExcelTitles(string path)
        {
            var rows = GetDataRows(path);
            var titleList = new List<ImportExcelTitle>();
            rows.Reset();
            var titleRow = (IRow)rows.Current;
            if (titleRow != null)
                titleList.AddRange(titleRow.Cells.Select(cell =>
                    new ImportExcelTitle { Title = cell.StringCellValue.Trim() }));
            return titleList;
        }
        static string GetCellValue(IRow row, int index)
        {
            var result = string.Empty;
            try
            {
                switch (row.GetCell(index).CellType)
                {
                    case CellType.Numeric:
                        result = row.GetCell(index).NumericCellValue.ToString(CultureInfo.InvariantCulture);
                        break;
                    case CellType.String:
                        result = row.GetCell(index).StringCellValue;
                        break;
                    case CellType.Blank:
                        result = string.Empty;
                        break;
                    case CellType.Formula:
                        result = row.GetCell(index).ToString();
                        if (!result.Contains("+") &&
                            !result.Contains("-") &&
                            !result.Contains("*") &&
                            !result.Contains("/") &&
                            !result.Contains("%") &&
                            !result.Contains("^") &&
                            !result.Contains("("))
                        {
                            var r = result.Replace("\"", "").Replace("'", "");
                            result = r;
                            break;
                        }
                        result = row.GetCell(index).NumericCellValue.ToString(CultureInfo.InvariantCulture);
                        break;
                    #region

                    //case CellType.Formula:
                    //    result = row.GetCell(index).CellFormula;
                    //    break;
                    //case CellType.Boolean:
                    //    result = row.GetCell(index).NumericCellValue.ToString();
                    //    break;
                    //case CellType.Error:
                    //    result = row.GetCell(index).NumericCellValue.ToString();
                    //    break;
                    //case CellType.Unknown:
                    //    result = row.GetCell(index).NumericCellValue.ToString();
                    //    break;

                    #endregion
                    default:
                        result = row.GetCell(index).ToString();
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return (result ?? "").Trim();
        }
        static IEnumerator GetDataRows(string path)
        {
            var extension = path.Substring(path.LastIndexOf('.') + 1);
            if (extension.ToLower().Equals("xlsx"))
            {
                if (string.IsNullOrWhiteSpace(path))
                    return null;
                XSSFWorkbook xssfworkbook;
                try
                {
                    using (var file = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        xssfworkbook = new XSSFWorkbook(file);
                    }
                }
                catch (Exception e)
                {
                    return null;
                }
                var sheet = xssfworkbook.GetSheetAt(0);
                var rows = sheet.GetRowEnumerator();
                rows.MoveNext();
                return rows;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(path))
                    return null;
                HSSFWorkbook hssfworkbook;
                try
                {
                    using (var file = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        hssfworkbook = new HSSFWorkbook(file);
                    }
                }
                catch (Exception e)
                {
                    return null;
                }
                var sheet = hssfworkbook.GetSheetAt(0);
                var rows = sheet.GetRowEnumerator();
                rows.MoveNext();
                return rows;
            }
        }
        static IEnumerator GetDataRows(string sheetName, IFormFile file)
        {
            var extension = file.FileName.Substring(file.FileName.LastIndexOf('.') + 1);
            var openFile = file.OpenReadStream();
            if (extension.ToLower().Equals("xlsx"))
            {
                var xssfworkbook = new XSSFWorkbook(openFile);
                var sheet = xssfworkbook.GetSheet(sheetName);
                var rows = sheet.GetRowEnumerator();
                rows.MoveNext();
                return rows;
            }
            else
            {
                var hssfworkbook = new HSSFWorkbook(openFile);
                var sheet = hssfworkbook.GetSheet(sheetName);
                var rows = sheet.GetRowEnumerator();
                rows.MoveNext();
                return rows;
            }
        }
        DateTime? GetCellDateTime(IRow row, int index)
        {
            DateTime? result = null;
            try
            {
                switch (row.GetCell(index).CellType)
                {
                    case CellType.Numeric:
                        try
                        {
                            result = row.GetCell(index).DateCellValue;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    case CellType.String:
                        var str = row.GetCell(index).StringCellValue;
                        if (str.EndsWith("年"))
                        {
                            DateTime dt;
                            if (DateTime.TryParse((str + "-01-01").Replace("年", ""), out dt))
                            {
                                result = dt;
                            }
                        }
                        else if (str.EndsWith("月"))
                        {
                            DateTime dt;
                            if (DateTime.TryParse((str + "-01").Replace("年", "").Replace("月", ""), out dt))
                            {
                                result = dt;
                            }
                        }
                        else if (!str.Contains("年") && !str.Contains("月") && !str.Contains("日"))
                        {

                            DateTime dt;
                            if (DateTime.TryParse(str, out dt))
                            {
                                result = dt;
                            }
                            else if (DateTime.TryParse((str + "-01-01").Replace("年", "").Replace("月", ""), out dt))
                            {
                                result = dt;
                            }
                            else
                            {
                                result = null;
                            }

                        }
                        else
                        {
                            DateTime dt;
                            if (DateTime.TryParse(str.Replace("年", "").Replace("月", ""), out dt))
                            {
                                result = dt;
                            }
                        }
                        break;
                    case CellType.Blank:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return result;
        }
    }
}
