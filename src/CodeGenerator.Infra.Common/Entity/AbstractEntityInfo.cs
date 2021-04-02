using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CodeGenerator.Infra.Common.Entity
{
    public abstract class AbstractEntityInfo : IEntityInfo
    {
        protected virtual IEnumerable<Type> GetEntityTypes(Assembly assembly)
        {
            var efEntities = assembly.GetTypes().Where(m =>
                                                       m.FullName != null
                                                       && typeof(IEntity).IsAssignableFrom(m)
                                                       && !m.IsAbstract).ToArray();

            return efEntities;
        }

        public abstract (Assembly Assembly, IEnumerable<Type> Types) GetEntitiesInfo();
    }
}
