using Notifications.Infrastructure.Domain.Entiteis;
using System.Linq.Expressions;

namespace Notifications.Infrastructure.Persistence.Repositories.Interfaces
{
    public interface ISmsTemplateRepository
    {
        IQueryable<SmsTemplate> Get(
            Expression<Func<SmsTemplate, bool>>? predicate = default,
            bool asNoTracking = false);

        ValueTask<SmsTemplate> CreateAsync(
            SmsTemplate template,
            bool saveChanges = true, 
            CancellationToken cancellationToken = default);
    }
}
