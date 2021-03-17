using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CodeGenerator.Infrastructure
{
    public interface IEntityInfo
    {
        (Assembly Assembly, IEnumerable<Type> Types) GetEntitiesInfo();
    }
}
