using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Notifications.Infrastructure.Application.Common.Identity.Services;
using Notifications.Infrastructure.Domain.Entiteis;
using Notifications.Infrastructure.Domain.Enums;
using Notifications.Infrastructure.Persistence.Repositories.Interfaces;

namespace Notifications.Infrastructure.Infrastructure.Common.Identity.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public ValueTask<User> CreateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return _userRepository.CreateAsync(user, saveChanges, cancellationToken);
    }
    

    public async ValueTask<User?> GetSystemUserAsync(bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        return await _userRepository.Get(user => user.RoleType == RoleType.System, asNoTracking)
            .Include(user => user.UserSettings)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public ValueTask<User?> GetByIdAsync(Guid userId, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        return _userRepository.GetByIdAsync(userId, asNoTracking, cancellationToken);
    }


    public ValueTask<User> UpdateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return _userRepository.UpdateAsync(user, saveChanges, cancellationToken);
    }

    public IQueryable<User> Get(Expression<Func<User, bool>> predicate)
    {
        return _userRepository.Get(predicate);
    }

    public async ValueTask<User> GetUserByEmailAsync(string emailAddress, bool asNoTracking = false,
        CancellationToken cancellationToken = default)
    {
        return await _userRepository.Get(asNoTracking: asNoTracking)
            .Include(user => user.Role)
            .SingleOrDefaultAsync(user => user.EmailAddress == emailAddress, cancellationToken: cancellationToken);
    }
}