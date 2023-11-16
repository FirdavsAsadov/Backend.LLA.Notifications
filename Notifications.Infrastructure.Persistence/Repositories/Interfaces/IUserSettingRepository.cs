using Notifications.Infrastructure.Domain.Entiteis;

namespace Notifications.Infrastructure.Persistence.Repositories.Interfaces
{
    public interface IUserSettingRepository
    {
        ValueTask<UserSettings?> GetByIdAsync(
            Guid userId,
            bool asNoTracking = false,
            CancellationToken cancellationToken = default
            );
    }
}
