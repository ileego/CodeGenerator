using System;
using System.Collections.Generic;
using System.Reflection;

namespace CodeGenerator.Infra.Common.Entity
{
    public interface IEntityInfo
    {
        (Assembly Assembly, IEnumerable<Type> Types) GetEntitiesInfo();
    }
}
