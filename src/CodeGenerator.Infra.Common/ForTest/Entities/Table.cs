using System;
using System.Collections.Generic;
using System.Text;
using CodeGenerator.Infra.Common.BaseEntities;

namespace CodeGenerator.Infra.Common.ForTest.Entities
{
    public class Table : NoKeyEntity
    {
        public string TableName { get; set; }
        public string TableType { get; set; }
        public string TableComment { get; set; }
    }
}
