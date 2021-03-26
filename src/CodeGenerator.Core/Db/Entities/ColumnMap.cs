using System;
using System.Collections.Generic;
using System.Text;
using CodeGenerator.Infra.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeGenerator.Core.Db.Entities
{
    public class ColumnMap : IEntityTypeConfiguration<Column>
    {
        public void Configure(EntityTypeBuilder<Column> builder)
        {
            builder.ToView("view_columns");
            builder.Property(t => t.TableName).HasColumnName("TABLE_NAME");
            builder.Property(t => t.ColumnName).HasColumnName("COLUMN_NAME");
            builder.Property(t => t.DataType).HasColumnName("DATA_TYPE");
            builder.Property(t => t.ColumnType).HasColumnName("COLUMN_TYPE");
            builder.Property(t => t.IsNullable).HasColumnName("IS_NULLABLE");
            builder.Property(t => t.CharacterMaximumLength).HasColumnName("CHARACTER_MAXIMUM_LENGTH");
            builder.Property(t => t.NumericPrecision).HasColumnName("NUMERIC_PRECISION");
            builder.Property(t => t.NumericScale).HasColumnName("NUMERIC_SCALE");
            builder.Property(t => t.ColumnDefault).HasColumnName("COLUMN_DEFAULT");
            builder.Property(t => t.ColumnKey).HasColumnName("COLUMN_KEY");
            builder.Property(t => t.ColumnComment).HasColumnName("COLUMN_COMMENT");
            builder.Property(t => t.OrdinalPosition).HasColumnName("ORDINAL_POSITION");
        }
    }
}
