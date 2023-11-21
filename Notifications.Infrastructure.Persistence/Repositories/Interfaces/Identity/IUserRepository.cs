using Notifications.Infrastructure.Domain.Entiteis;
using System.Linq.Expressions;

namespace Notifications.Infrastructure.Persistence.Repositories.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<User> Get(Expression<Func<User, bool>>? predicate = default, bool asNoTracking = false);

        ValueTask<User?> GetByIdAsync(Guid userId, bool asNoTracking = false, CancellationToken cancellationToken = default);

        ValueTask<User> CreateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default);

        ValueTask<User> UpdateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default);
    }
}
