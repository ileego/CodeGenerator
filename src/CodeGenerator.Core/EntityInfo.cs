using System;
using System.Collections.Generic;
using System.Reflection;
using CodeGenerator.Infra.Common.Entity;

namespace CodeGenerator.Core
{
    public class EntityInfo : AbstractEntityInfo
    {
        public override (Assembly Assembly, IEnumerable<Type> Types) GetEntitiesInfo()
        {
            var assembly = this.GetType().Assembly;
            var entityTypes = base.GetEntityTypes(assembly);

            return (assembly, entityTypes);
        }
    }
}
