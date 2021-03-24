using System;
using System.Collections.Generic;
using System.Text;
using CodeGenerator.Infra.Common.ForTest.Entities;
using CodeGenerator.Infra.Common.Interfaces;

namespace CodeGenerator.Infra.Common.ForTest.Repository
{
    public interface ITableRepository : IQueryRepository<Table>
    {
    }
}
