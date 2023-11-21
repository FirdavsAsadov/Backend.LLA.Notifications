using Notifications.Infrastructure.Domain.Entiteis;

namespace Notifications.Infrastructure.Application.Common.Identity.Services;

public interface ITokenService
{
    ValueTask<Token> CreateAsync(Guid userId, string value, bool saveChanges = true, CancellationToken cancellationToken = default);
}