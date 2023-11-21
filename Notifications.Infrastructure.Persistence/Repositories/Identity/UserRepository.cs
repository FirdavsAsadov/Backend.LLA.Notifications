using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Notifications.Infrastructure.Domain.Entiteis;
using Notifications.Infrastructure.Persistence.DataBase;
using Notifications.Infrastructure.Persistence.Repositories.Interfaces;

namespace Notifications.Infrastructure.Persistence.Repositories.Identity;

public class UserRepository : EntityRepositoryBase<User, NotificationDbContext>,IUserRepository
{
    public UserRepository(NotificationDbContext dbContext) : base(dbContext)
    {
    }

    public IQueryable<User> Get(Expression<Func<User, bool>>? predicate = default, bool asNoTracking = false)
    {
        return base.Get(predicate, asNoTracking);
    }
    
    public ValueTask<User?> GetByIdAsync(Guid userId, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(userId, asNoTracking, cancellationToken);
    }

    public ValueTask<User> CreateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return base.CreateAsync(user, saveChanges, cancellationToken);
    }

    public ValueTask<User> UpdateAsync(User user, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        return base.UpdateAsnync(user, saveChanges, cancellationToken);
    }
}