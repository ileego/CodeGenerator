using System.Linq;
using CodeGenerator.Core.Interfaces;
using CodeGenerator.Infra.Common.BaseEntities;

namespace CodeGenerator.Core.Utils
{
    public class FindBaseClass
    {
        public static string Find(ITable table)
        {
            var baseClassName = nameof(NoKeyEntity);
            var propertyInfos = typeof(Entity).GetProperties().ToArray();
            if (propertyInfos.Any(t => table.Fields.Any(f => f.PropertyName == t.Name)))
                baseClassName = nameof(Entity);
            propertyInfos = typeof(CreationAuditEntity).GetProperties().Where(t => t.DeclaringType.Name == nameof(CreationAuditEntity)).ToArray();
            if (propertyInfos.Any(t => table.Fields.Any(f => f.PropertyName == t.Name)))
                baseClassName = nameof(CreationAuditEntity);
            propertyInfos = typeof(ModifyAuditEntity).GetProperties().Where(t => t.DeclaringType.Name == nameof(ModifyAuditEntity)).ToArray();
            if (propertyInfos.Any(t => table.Fields.Any(f => f.PropertyName == t.Name)))
                baseClassName = nameof(ModifyAuditEntity);
            propertyInfos = typeof(FullAuditEntity).GetProperties().Where(t => t.DeclaringType.Name == nameof(FullAuditEntity)).ToArray();
            if (propertyInfos.Any(t => table.Fields.Any(f => f.PropertyName == t.Name)))
                baseClassName = nameof(FullAuditEntity);
            return baseClassName;
        }

    }
}
