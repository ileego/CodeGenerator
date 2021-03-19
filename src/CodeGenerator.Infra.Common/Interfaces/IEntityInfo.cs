using System;
using System.Collections.Generic;
using System.Reflection;

namespace CodeGenerator.Infra.Common.Interfaces
{
    public interface IEntityInfo
    {
        (Assembly Assembly, IEnumerable<Type> Types) GetEntitiesInfo();
    }
}
