using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeGenerator.Core.Db.Entities;

namespace CodeGenerator.Core.Utils
{
    public class TypeConvert
    {
        public static string Trans(Column col)
        {
            var colType = col.ColumnType.ToLower();
            var isNull = col.IsNullable.Equals("yes", StringComparison.OrdinalIgnoreCase);
            var dbType = colType.Contains("(")
                ? colType.Substring(0, colType.IndexOf("(", StringComparison.Ordinal))
                : colType;
            var type = "";
            switch (dbType)
            {
                case "bit":
                case "bool":
                case "boolean":
                    type = "bool";
                    break;
                //integer
                case "tinyint":
                    type = colType.Equals("tinyint(1)") ? "bool" : "byte";
                    break;
                case "tinyint unsigned":
                    type = "sbyte";
                    break;
                case "smallint":
                    type = "short";
                    break;
                case "smallint unsigned":
                    type = "ushort";
                    break;
                case "int":
                case "mediumint":
                    type = "int";
                    break;
                case "int unsigned":
                case "mediumint unsigned":
                    type = "uint";
                    break;
                case "bigint":
                    type = "long";
                    break;
                case "bigint unsigned":
                    type = "ulong";
                    break;
                //float
                case "float":
                    type = "float";
                    break;
                case "double":
                    type = "double";
                    break;
                case "decimal":
                    type = "decimal";
                    break;
                //date
                case "date":
                case "datetime":
                case "timestamp":
                    type = "DateTime";
                    break;
                //string enum、json当做string处理
                case "json":
                case "enum":
                case "varchar":
                case "tinytext":
                case "mediumtext":
                case "longtext":
                case "text":
                    type = "string";
                    break;
                case "char":
                    type = "char[]";
                    break;
                //blob
                case "binary":
                case "varbinary":
                case "tinyblob":
                case "blob":
                case "mediumblob":
                case "longblob":
                    type = "byte[]";
                    break;
                //geometry
                case "geometry":
                    type = "MySqlGeometry";
                    break;
                default:
                    throw new Exception("Unknown type");
            }

            if (isNull)
            {
                var types = new[] { "sbyte", "byte", "short", "ushort", "int",
                    "uint", "long", "ulong", "char", "float", "double", "bool", "decimal","DateTime","MySqlGeometry"};
                if (types.Contains(type))
                {
                    type += "?";
                }
            }

            return type;
        }
    }
}
