﻿using System;
using System.Collections.Generic;
using System.Text;
using CodeGenerator.Core.Db.Entities;

namespace CodeGenerator.Core.Utils
{
    public class TypeConvert
    {
        public static string Trans(Column col)
        {
            var colType = col.ColumnType.ToLower();
            var dbType = colType.Contains("(")
                ? colType.Substring(0, colType.IndexOf("(", StringComparison.Ordinal))
                : colType;
            switch (dbType)
            {
                case "bit":
                case "bool":
                case "boolean":
                    return "bool";
                //integer
                case "tinyint":
                    return colType.Equals("tinyint(1)") ? "bool" : "byte";
                case "tinyint unsigned":
                    return "sbyte";
                case "smallint":
                    return "short";
                case "smallint unsigned":
                    return "ushort";
                case "int":
                case "mediumint":
                    return "int";
                case "int unsigned":
                case "mediumint unsigned":
                    return "uint";
                case "bigint":
                    return "long";
                case "bigint unsigned":
                    return "ulong";
                //float
                case "float":
                    return "float";
                case "double":
                    return "double";
                case "decimal":
                    return "decimal";
                //date
                case "date":
                case "datetime":
                case "timestamp":
                    return "DateTime";
                //string enum、json当做string处理
                case "json":
                case "enum":
                case "varchar":
                case "tinytext":
                case "mediumtext":
                case "longtext":
                case "text":
                    return "string";
                case "char":
                    return "char[]";
                //blob
                case "binary":
                case "varbinary":
                case "tinyblob":
                case "blob":
                case "mediumblob":
                case "longblob":
                    return "byte[]";
                //geometry
                case "geometry":
                    return "geometry";
                default:
                    throw new Exception("Unknown type");
            }


        }
    }
}
