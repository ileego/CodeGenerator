using Novacode;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace CodeGenerator.Infra.Common.Helper
{
    public class WordHelper
    {
        /// <summary>
        /// 导出word
        /// </summary>
        /// <param name="tempFilePath">模板路径</param>
        /// <param name="outDir">保存路径</param>
        /// <param name="fileName">文件名</param>
        /// <param name="data">数据</param>
        public static void Export(string tempFilePath, string outDir, string fileName, Dictionary<string, object> data)
        {
            var filePath = Path.Combine(outDir, fileName);
            if (!System.IO.Directory.Exists(outDir))
            {
                System.IO.Directory.CreateDirectory(outDir);
            }
            System.IO.File.Copy(tempFilePath, filePath, true);
            //新建一个Word文档，加载Load的方法和Create使用一样。
            using (DocX document = DocX.Load(filePath))
            {

                ReplaceDoc(document, data);//普通文本替换
                ReplaceTable(document, data);//表格处理
                ReplaceList(document, data);//文本列表处理

                document.Save();//保存
            }
        }

        private static void ReplaceDoc(DocX doc, Dictionary<string, object> data)
        {
            foreach (var item in doc.Paragraphs)
            {
                ReplaceParagraph(item, data);
            }
        }

        private static void ReplaceList(DocX doc, Dictionary<string, object> data)
        {
            //一定要在 普通文本替换和表格处理之后
            foreach (var p in doc.Paragraphs)
            {
                var li = GetListInfo(p, data);
                if (li.IsList)
                {
                    var pc = li.PTemp;
                    for (int i = 0; i < li.Data.Count; i++)
                    {
                        var pt = pc.InsertParagraphAfterSelf(p);
                        if (li.IsDict)
                        {
                            pc = ReplaceParagraph(pt, (Dictionary<string, object>)li.Data[i]);
                        }
                        else
                        {
                            pc = ReplaceParagraph(pt, li.Data[i]);
                        }
                    }

                    //删除模板行
                    li.PTemp.Remove(false);
                }
                else
                {
                    //do nonthing
                }
            }
        }

        private static void ReplaceTable(DocX doc, Dictionary<string, object> data)
        {
            var tbs = doc.Tables;
            foreach (var table in tbs)
            {
                //需要先判断表格是列表还是表单
                var ti = GetTableInfo(table, data);
                if (ti.IsList)
                {
                    for (int i = 0; i < ti.Data.Count; i++)
                    {
                        var rt = table.InsertRow(ti.RowTemp);
                        rt.Height = ti.RowTemp.Height;
                        rt.MinHeight = ti.RowTemp.MinHeight;
                        if (ti.IsDict)
                        {
                            ReplaceRow(rt, (Dictionary<string, object>)ti.Data[i]);
                        }
                        else
                        {
                            ReplaceRow(rt, ti.Data[i]);
                        }
                    }

                    //删除模板行
                    ti.RowTemp.Remove();
                }
                else
                {
                    //do nonthing
                }
            }
        }

        private static void ReplaceRow(Row row, Dictionary<string, object> data)
        {
            foreach (var cell in row.Cells)
            {
                foreach (var item in cell.Paragraphs)
                {
                    ReplaceParagraph(item, data);
                }
            }
        }

        private static void ReplaceRow(Row row, object data)
        {
            foreach (var cell in row.Cells)
            {
                foreach (var item in cell.Paragraphs)
                {
                    ReplaceParagraph(item, data);
                }
            }
        }

        private static Paragraph ReplaceParagraph(Paragraph p, Dictionary<string, object> data)
        {
            Paragraph pr = p;
            var ms = GetMatches(p.Text);
            var ks = new List<string>();
            var rs = new List<string>();
            foreach (Match m in ms)
            {
                if (m.Groups.Count > 1)
                {
                    string text = m.Groups[1].Value;
                    if (text.Contains("."))
                    {
                        var ts = text.Split(".");
                        text = ts[ts.Length - 1];
                    }
                    ks.Add(text);
                    rs.Add(m.Value);
                }
            }
            bool isCt = data.Any(op => ks.Any(o => o.Contains(op.Key)));
            bool isReplace = false;

            if (isCt)
            {
                if (ks.Count > 1)
                {
                    for (int i = 0; i < ks.Count; i++)
                    {
                        if (data.ContainsKey(ks[i]))
                        {
                            p.ReplaceText(rs[i], data[ks[i]]?.ToString());
                        }
                    }
                }
                else if (ks.Count == 1)
                {
                    string text = ks[0];
                    if (data.ContainsKey(text))
                    {
                        var ct = data[text]?.ToString();
                        var cts = ResolveText(ct);
                        var pc = p;
                        foreach (var item2 in cts)
                        {
                            if (string.IsNullOrWhiteSpace(item2)) continue;
                            var pt = pc.InsertParagraphAfterSelf(p);
                            pt.ReplaceText(rs[0], item2);
                            pc = pt;
                            pr = pc;
                        }
                        isReplace = true;
                    }
                }
                if (isReplace)
                {
                    //删除原来段落
                    p.Remove(false);
                }
            }

            return pr;
        }

        private static Paragraph ReplaceParagraph(Paragraph p, object data)
        {
            var ms = GetMatches(p.Text);
            var isReplace = false;
            Paragraph pr = p;
            foreach (Match item1 in ms)
            {
                if (item1.Groups.Count > 1)
                {
                    string text = item1.Groups[1].Value;

                    var ct = data?.ToString();
                    var cts = ResolveText(ct);
                    var pc = p;
                    foreach (var item2 in cts)
                    {
                        var pt = pc.InsertParagraphAfterSelf(p);
                        pt.ReplaceText(item1.Value, item2);
                        pc = pt;
                        pr = pc;
                    }
                    isReplace = true;
                }
            }
            if (isReplace)
            {
                //删除原来段落
                p.Remove(false);
            }
            return pr;
        }

        private static IList<string> ResolveText(string text)
        {
            if (string.IsNullOrEmpty(text)) return new List<string>();
            text = text.Replace("\r\n", "\n").Replace("\r", "\n");
            return text.Split('\n');
        }

        private static MatchCollection GetMatches(string text)
        {
            if (string.IsNullOrEmpty(text)) text = "";
            Regex regex = new Regex("[#|\\$]([a-zA-Z0-9_.]+?)[#|\\$]", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            return regex.Matches(text);
        }

        /// <summary>
        /// 只获取列表匹配项
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static MatchCollection GetListMatches(string text)
        {
            if (string.IsNullOrEmpty(text)) text = "";
            Regex regex = new Regex("\\$([a-zA-Z0-9_.]+?)\\$", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            return regex.Matches(text);
        }

        //纯辅助方法
        private class TableInfo
        {
            public bool IsList { get; set; }
            public bool IsDict { get; set; }
            public Row RowTemp { get; set; }
            public IList<object> Data { get; set; }
        }

        //判断表格是列表还是表单
        private static TableInfo GetTableInfo(Table table, Dictionary<string, object> data)
        {
            TableInfo result = new TableInfo();
            var r0 = table.Rows[table.Rows.Count - 1];
            var c0 = r0.Cells[r0.Cells.Count - 1];
            var ct = c0.Paragraphs[0].Text;
            var ms = GetListMatches(ct);
            foreach (Match item in ms)
            {
                if (item.Groups.Count > 1)
                {
                    string text = item.Groups[1].Value;
                    if (text.Contains("."))
                    {
                        result.IsDict = true;
                        text = text.Split('.')[0];
                    }

                    if (data.ContainsKey(text) && (data[text] is IList))//判断是否是列表
                    {
                        result.RowTemp = r0;
                        result.IsList = true;
                        result.Data = new List<object>();
                        foreach (var item1 in (data[text] as IList))
                        {
                            result.Data.Add(item1);
                        }
                        break;
                    }
                }
            }
            return result;
        }

        private class ListInfo
        {
            public bool IsList { get; set; }
            public bool IsDict { get; set; }
            public Paragraph PTemp { get; set; }
            public IList<object> Data { get; set; }
        }

        private static ListInfo GetListInfo(Paragraph p, Dictionary<string, object> data)
        {
            ListInfo result = new ListInfo();
            var ms = GetListMatches(p.Text);
            foreach (Match item in ms)
            {
                if (item.Groups.Count > 1)
                {
                    string text = item.Groups[1].Value;
                    if (text.Contains("."))
                    {
                        result.IsDict = true;
                        text = text.Split('.')[0];
                    }
                    if (data.ContainsKey(text) && (data[text] is IList))//判断是否是列表
                    {
                        result.PTemp = p;
                        result.IsList = true;
                        result.Data = new List<object>();
                        foreach (var item1 in (data[text] as IList))
                        {
                            result.Data.Add(item1);
                        }
                        break;
                    }
                }
            }
            return result;
        }
    }
}
