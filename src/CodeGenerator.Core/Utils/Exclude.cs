using System;
using System.Collections.Generic;
using System.Linq;
using CodeGenerator.Core.Interfaces;
using CodeGenerator.Infra.Common.Entity;
using CodeGenerator.Infra.Common.Extensions;
using JetBrains.Annotations;

namespace CodeGenerator.Core.Utils
{
    public static class Exclude
    {
        /// <summary>
        /// 默认排除的Field的PropertyName列表
        /// </summary>
        /// <returns></returns>
        public static ICollection<string> DefaultExcludeFields()
        {
            var propertyInfos = typeof(FullAuditEntity).GetProperties();
            return propertyInfos.Select(t => t.Name).ToList();
        }

        /// <summary>
        /// 在ICollection<ITable> 排除在excludeTableNames名单中的与ITable的TableName同名的项，并返回一个新的ICollection<ITable>。
        /// </summary>
        /// <param name="tables"></param>
        /// <param name="excludeTableNames"></param>
        /// <returns></returns>
        public static ICollection<ITable> ExcludeTables([NotNull] this ICollection<ITable> tables,
            [NotNull] ICollection<string> excludeTableNames)
        {
            var newTables = tables.Exclude(t =>
                excludeTableNames.Any(e => e.Equals(t.TableName, StringComparison.OrdinalIgnoreCase)));
            return newTables;
        }

        /// <summary>
        /// 在ICollection<ITable> 排除在excludeClassNames名单中的与ITable的ClassName同名的项，并返回一个新的ICollection<ITable>。
        /// </summary>
        /// <param name="tables"></param>
        /// <param name="excludeClassNames"></param>
        /// <returns></returns>
        public static ICollection<ITable> ExcludeTablesByClassNames([NotNull] this ICollection<ITable> tables,
            [NotNull] ICollection<string> excludeClassNames)
        {
            var newTables = tables.Exclude(t =>
                excludeClassNames.Any(e => e.Equals(t.ClassName, StringComparison.OrdinalIgnoreCase)));
            return newTables;
        }

        /// <summary>
        /// 在ICollection<IField> 排除在excludeFieldNames名单中的与IField的FieldName同名的项，并返回一个新的ICollection<IField>。
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="excludeFieldNames"></param>
        /// <returns></returns>
        public static ICollection<IField> ExcludeFields([NotNull] this ICollection<IField> fields,
            [NotNull] ICollection<string> excludeFieldNames)
        {
            var newFields = fields.Exclude(t =>
                excludeFieldNames.Any(e => e.Equals(t.FieldName, StringComparison.OrdinalIgnoreCase)));
            return newFields;
        }

        /// <summary>
        /// 在ICollection<IField> 排除在excludePropertyNames名单中的与IField的PropertyName同名的项，并返回一个新的ICollection<IField>。
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="excludePropertyNames"></param>
        /// <returns></returns>
        public static ICollection<IField> ExcludeFieldsByPropertyNames([NotNull] this ICollection<IField> fields,
            [NotNull] ICollection<string> excludePropertyNames)
        {
            var newFields = fields.Exclude(t =>
                excludePropertyNames.Any(e => e.Equals(t.PropertyName, StringComparison.OrdinalIgnoreCase)));
            return newFields;
        }
    }
}
