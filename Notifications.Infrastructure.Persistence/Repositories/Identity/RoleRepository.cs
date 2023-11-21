using System.Linq.Expressions;
using Notifications.Infrastructure.Domain.Entiteis;
using Notifications.Infrastructure.Persistence.DataBase;
using Notifications.Infrastructure.Persistence.Repositories.Interfaces;

namespace Notifications.Infrastructure.Persistence.Repositories.Identity;

public class RoleRepository : EntityRepositoryBase<Role, NotificationDbContext>, IRoleRepository
{
    public RoleRepository(NotificationDbContext dbContext) : base(dbContext)
    {
    }

    public IQueryable<Role> Get(Expression<Func<Role, bool>> predicate = default, bool asNoTracking = false)
    {
        return base.Get(predicate, asNoTracking);
    }
}