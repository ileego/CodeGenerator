using System;
using System.Collections.Generic;
using System.Text;
using CodeGenerator.Core.Interfaces;

namespace CodeGenerator.Core.Implements
{
    /// <summary>
    /// 上下文
    /// </summary>
    public class GenerateContext : IGenerateContext
    {
        /// <summary>
        /// 主命名空间
        /// </summary>
        public string Namespace { get; set; }
        /// <summary>
        /// 表集合
        /// </summary>
        public ICollection<ITable> Tables { get; }
        /// <summary>
        /// 添加表
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public ICollection<ITable> AddTable(ITable table)
        {
            this.Tables.Add(table);
            return this.Tables;
        }

        public GenerateContext()
        {
            Tables = new List<ITable>();

        }

        public void Test()
        {
            var usingBuilder = new StringBuilder();
            var mapBuilder = new StringBuilder();
            foreach (var table in Tables)
            {
                usingBuilder.Append(Namespace + ".Dto." + table.TableName + ";\r\n");
                mapBuilder.Append("/*" + table.Comment + "*/\r\n");
                mapBuilder.Append("CreateMap<" + table.TableName + "," + table.TableName + "Input>();");
                mapBuilder.Append("CreateMap<" + table.TableName + "Input," + table.TableName + ">();");
                mapBuilder.Append("CreateMap<" + table.TableName + "," + table.TableName + "QueryResult>();");
                mapBuilder.Append("CreateMap<" + table.TableName + "QueryResult," + table.TableName + ">();");
            }

            usingBuilder.ToString();
            mapBuilder.ToString();
        }
    }
}
