using Notifications.Infrastructure.Domain.Entiteis;

namespace Notifications.Infrastructure.Application.Common.Identity.Services;

public interface IAccountService
{
    ValueTask<User> CreateUserAsync(User user);
    
}