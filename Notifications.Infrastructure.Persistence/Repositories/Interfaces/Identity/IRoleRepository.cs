using System.Linq.Expressions;
using Notifications.Infrastructure.Domain.Entiteis;

namespace Notifications.Infrastructure.Persistence.Repositories.Interfaces;

public interface IRoleRepository
{
    IQueryable<Role> Get(Expression<Func<Role, bool>>? predicate  = default, bool asNoTracking = false);
}