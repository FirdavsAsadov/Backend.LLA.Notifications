using Notifications.Infrastructure.Application.Common.Identity.Models;

namespace Notifications.Infrastructure.Application.Common.Identity.Services;

public interface IAuthService
{
    ValueTask<bool> RegisterAsync(RegistrationDetails registrationDetails, bool saveChanges = true,
        CancellationToken cancellationToken = default);

    Task<string> LoginAsync(LoginDetails loginDetails, CancellationToken cancellationToken = default);
}