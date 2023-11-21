using Microsoft.EntityFrameworkCore;
using Notifications.Infrastructure.Application.Common.Identity.Services;
using Notifications.Infrastructure.Domain.Entiteis;
using Notifications.Infrastructure.Domain.Enums;
using Notifications.Infrastructure.Persistence.Repositories.Interfaces;

namespace Notifications.Infrastructure.Infrastructure.Common.Identity.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }
    public async ValueTask<Role?> GetByTypeAsync(RoleType roleType, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        return await _roleRepository.Get(asNoTracking: asNoTracking)
            .SingleOrDefaultAsync(role => role.Type == roleType, cancellationToken);
    }
}