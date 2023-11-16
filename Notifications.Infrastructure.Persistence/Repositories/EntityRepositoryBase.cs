using Notifications.Infrastructure.Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace Notifications.Infrastructure.Persistence.Repositories
{
    public abstract class EntityRepositoryBase<TEntity, TContext> where TEntity : class, IEntity where TContext : DbContext 
    {
    }
}
