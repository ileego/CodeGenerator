using CodeGenerator.Infra.Common.BaseEntities;

namespace CodeGenerator.Core.Db.Entities
{
    public class Table : NoKeyEntity
    {
        public string TableName { get; set; }
        public string TableType { get; set; }
        public string TableComment { get; set; }
    }
}
