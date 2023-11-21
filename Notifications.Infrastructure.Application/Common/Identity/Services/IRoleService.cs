using Notifications.Infrastructure.Domain.Entiteis;
using Notifications.Infrastructure.Domain.Enums;

namespace Notifications.Infrastructure.Application.Common.Identity.Services;

public interface IRoleService
{
    ValueTask<Role?> GetByTypeAsync(RoleType roleType, bool asNoTracking = false, CancellationToken cancellationToken = default);
}