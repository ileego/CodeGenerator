using System.Linq;
using CodeGenerator.Core.Interfaces;
using CodeGenerator.Infra.Common.Entity;

namespace CodeGenerator.Core.Utils
{
    public static class BaseClass
    {
        public static string FindBaseClass(this ITable table)
        {
            var baseClassName = nameof(NoKeyEntity);
            var basePropertyInfos = typeof(BaseEntity).GetProperties().ToArray();
            if (basePropertyInfos.Any(t => table.Fields.Any(f => f.PropertyName == t.Name)))
                baseClassName = nameof(BaseEntity);

            var creationPropertyInfos = typeof(CreationAuditEntity).GetProperties().Where(t => t.DeclaringType.Name == nameof(CreationAuditEntity)).ToArray();
            if (creationPropertyInfos.Any(t => table.Fields.Any(f => f.PropertyName == t.Name)))
                baseClassName = nameof(CreationAuditEntity);

            var modifyPropertyInfos = typeof(ModifyAuditEntity).GetProperties().Where(t => t.DeclaringType.Name == nameof(ModifyAuditEntity)).ToArray();
            if (modifyPropertyInfos.Any(t => table.Fields.Any(f => f.PropertyName == t.Name)))
                baseClassName = nameof(ModifyAuditEntity);

            var deletionPropertyInfos = typeof(DeletionAuditEntity).GetProperties().Where(t => t.DeclaringType.Name == nameof(DeletionAuditEntity)).ToArray();
            if (deletionPropertyInfos.Any(t => table.Fields.Any(f => f.PropertyName == t.Name)))
                baseClassName = nameof(DeletionAuditEntity);

            var fullPropertyInfos = typeof(FullAuditEntity).GetProperties().Where(t => t.DeclaringType.Name == nameof(FullAuditEntity)).ToArray();
            if (fullPropertyInfos.Any(t => table.Fields.Any(f => f.PropertyName == t.Name))
                && creationPropertyInfos.Any(t => table.Fields.Any(f => f.PropertyName == t.Name))
                && modifyPropertyInfos.Any(t => table.Fields.Any(f => f.PropertyName == t.Name)))
                baseClassName = nameof(FullAuditEntity);
            return baseClassName;
        }

    }
}
