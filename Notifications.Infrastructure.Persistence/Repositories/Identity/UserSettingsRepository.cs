using Microsoft.EntityFrameworkCore;
using Notifications.Infrastructure.Domain.Entiteis;
using Notifications.Infrastructure.Persistence.DataBase;
using Notifications.Infrastructure.Persistence.Repositories.Interfaces;

namespace Notifications.Infrastructure.Persistence.Repositories.Identity;

public class UserSettingsRepository : EntityRepositoryBase<UserSettings, NotificationDbContext>,IUserSettingRepository
{
    public UserSettingsRepository(NotificationDbContext dbContext) : base(dbContext)
    {
    }

    public ValueTask<UserSettings?> GetByIdAsync(Guid userId, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(userId, asNoTracking, cancellationToken);
    }
}