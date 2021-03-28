using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeGenerator.Core.Db.Repository;
using CodeGenerator.Core.Interfaces;
using CodeGenerator.Core.Utils;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace CodeGenerator.Core.Implements
{
    public class MySqlTableFactory : ITableFactory<Db.Entities.Table, ICollection<Db.Entities.Column>>
    {
        private readonly ITableRepository _tableRepository;
        private readonly IColumnRepository _columnRepository;

        public MySqlTableFactory(
            ITableRepository tableRepository,
            IColumnRepository columnRepository,
            IGenerateContext generateContext)
        {
            this._tableRepository = tableRepository;
            this._columnRepository = columnRepository;
            this.GenerateContext = generateContext;
        }

        public IGenerateContext GenerateContext { get; set; }

        public Table CreateTable(Db.Entities.Table dbTable, ICollection<Db.Entities.Column> dbFields)
        {
            var fields = new List<Field>();
            if (dbFields.All(t => t.TableName != dbTable.TableName))
                throw new Exception("No database fields");
            foreach (var column in dbFields.Where(t => t.TableName == dbTable.TableName).OrderBy(t => t.OrdinalPosition))
            {
                var dataType = TypeConvert.Trans(column);
                long length = 0;
                if (column.CharacterMaximumLength != null || column.NumericPrecision != null)
                    length = dataType.Equals("string") || dataType.Equals("char[]")
                        ? column.CharacterMaximumLength.Value
                        : column.NumericPrecision.Value;
                var isKey = column.ColumnKey.Length > 0;
                var keyType = column.ColumnKey.Equals("PRI") ? KeyTypeEnum.PrimaryKey : KeyTypeEnum.ForeignKey;
                var isNullable = column.IsNullable.Equals("YES");
                fields.Add(new Field(toTable: column.TableName,
                    fieldName: column.ColumnName,
                    dataType: dataType,
                    length: length,
                    precision: column.NumericScale,
                    comment: column.ColumnComment,
                    isNullable: isNullable,
                    isKey: isKey,
                    keyType: isKey ? keyType : default
                    ));
            }
            var table = new Table(dbTable.TableName,
                dbTable.TableComment,
                dbTable.TableType.Contains("VIEW") ? TableTypeEnum.View : TableTypeEnum.Table,
                fields);
            return table;

        }

        public async Task<IGenerateContext> CreateContext()
        {
            var tables = await _tableRepository.Query.ToListAsync();
            var columns = await _columnRepository.Query.ToListAsync();
            foreach (var table in tables)
            {
                var dbFields = columns
                    .Where(t => t.TableName.Equals(table.TableName, StringComparison.OrdinalIgnoreCase)).ToList();
                GenerateContext.AddTable(CreateTable(table, dbFields));
            }

            return GenerateContext;

        }
    }
}
