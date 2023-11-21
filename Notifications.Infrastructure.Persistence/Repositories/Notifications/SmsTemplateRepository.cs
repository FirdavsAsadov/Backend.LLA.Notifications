using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Notifications.Infrastructure.Domain.Entiteis;
using Notifications.Infrastructure.Persistence.DataBase;
using Notifications.Infrastructure.Persistence.Repositories.Interfaces;

namespace Notifications.Infrastructure.Persistence.Repositories.Notifications;

public class SmsTemplateRepository : EntityRepositoryBase<SmsTemplate, NotificationDbContext>,ISmsTemplateRepository
{
    public SmsTemplateRepository(NotificationDbContext dbContext) : base(dbContext)
    {
    }
    
    public IQueryable<SmsTemplate> Get(Expression<Func<SmsTemplate, bool>>? predicate = default, bool asNoTracking = false)
    {
        return base.Get(predicate, asNoTracking);
    }

    public ValueTask<SmsTemplate> CreateAsync(SmsTemplate template, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return base.CreateAsync(template, saveChanges, cancellationToken);
    }

}