using System.Linq.Expressions;
using Notifications.Infrastructure.Domain.Entiteis;

namespace Notifications.Infrastructure.Application.Common.Identity.Services;

public interface IUserService
{

    ValueTask<User?> GetSystemUserAsync(bool asNoTracking = false, CancellationToken cancellationToken = default);

    ValueTask<User?> GetByIdAsync(
        Guid userId,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    );

    ValueTask<User> CreateAsync(
        User user,
        bool saveChanges = true,
        CancellationToken cancellationToken = default);

    ValueTask<User> UpdateAsync(
        User user,
        bool saveChanges = true,
        CancellationToken cancellationToken = default);
    
    IQueryable<User> Get(Expression<Func<User, bool>> predicate);
    
    ValueTask<User> GetUserByEmailAsync(string emailAddress, bool asNoTracking = false, CancellationToken cancellationToken = default);

}