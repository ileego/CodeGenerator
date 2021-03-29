﻿using CodeGenerator.Core.Db.Entities;
using CodeGenerator.Infra.Common.Interfaces;

namespace CodeGenerator.Core.Db.Repository
{
    public interface IColumnRepository : IQueryRepository<Column>
    {
    }
}