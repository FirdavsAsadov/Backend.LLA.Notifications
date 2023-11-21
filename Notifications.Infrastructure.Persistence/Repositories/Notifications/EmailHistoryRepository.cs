using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Notifications.Infrastructure.Domain.Entiteis;
using Notifications.Infrastructure.Persistence.DataBase;
using Notifications.Infrastructure.Persistence.Repositories.Interfaces;

namespace Notifications.Infrastructure.Persistence.Repositories.Notifications;

public class EmailHistoryRepository : EntityRepositoryBase<EmailHistory, NotificationDbContext>,IEmailHistoryRepository
{
    public EmailHistoryRepository(NotificationDbContext dbContext) : base(dbContext)
    {
    }
    
    public IQueryable<EmailHistory> Get(Expression<Func<EmailHistory, bool>>? predicate = default, bool asNoTracking = false)
    {
        return base.Get(predicate, asNoTracking);
    }

    public ValueTask<EmailHistory> CreateAsync(EmailHistory emailHistory, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        return base.CreateAsync(emailHistory, saveChanges, cancellationToken);
    }

}