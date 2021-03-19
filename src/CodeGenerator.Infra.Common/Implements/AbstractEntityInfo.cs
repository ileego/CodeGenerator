using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CodeGenerator.Infra.Common.BaseEntities;

namespace CodeGenerator.Infra.Common.Implements
{
    public abstract class AbstractEntityInfo
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
