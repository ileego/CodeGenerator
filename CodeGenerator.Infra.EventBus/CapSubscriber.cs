using System;
using System.Linq.Expressions;
using DotNetCore.CAP;

namespace CodeGenerator.Infra.EventBus
{
    public abstract class CapSubscriber : IEventSubscriber, ICapSubscribe
    {
        protected Expression<Func<TEntity, object>>[] UpdatingProps<TEntity>(params Expression<Func<TEntity, object>>[] expressions)
        {
            return expressions;
        }
    }
}
