using Notifications.Infrastructure.Domain.Entiteis;
using System.Linq.Expressions;

namespace Notifications.Infrastructure.Persistence.Repositories.Interfaces
{
    public interface IEmailHistoryRepository
    {
        IQueryable<EmailHistory> Get(
            Expression<Func<EmailHistory, bool>>? predicate = default,
            bool asNoTracking = false
            );

        ValueTask<EmailHistory> CreateAsync( EmailHistory emailHistory ,
            bool saveChanges = true,
            CancellationToken cancellationToken = default);
    }
}
