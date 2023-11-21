using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Notifications.Infrastructure.Domain.Entiteis;
using Notifications.Infrastructure.Persistence.DataBase;
using Notifications.Infrastructure.Persistence.Repositories.Interfaces;

namespace Notifications.Infrastructure.Persistence.Repositories.Notifications;

public class SmsHistoryRepository : EntityRepositoryBase<SmsHistory, NotificationDbContext>,ISmsHistoryRepository
{
    public SmsHistoryRepository(NotificationDbContext dbContext) : base(dbContext)
    {
    }
    
    public IQueryable<SmsHistory> Get(Expression<Func<SmsHistory, bool>>? predicate = default, bool asNoTracking = false)
    {
        return base.Get(predicate, asNoTracking);
    }

    public ValueTask<SmsHistory> CreateAsync(SmsHistory smsHistory, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return base.CreateAsync(smsHistory, saveChanges, cancellationToken);
    }

}