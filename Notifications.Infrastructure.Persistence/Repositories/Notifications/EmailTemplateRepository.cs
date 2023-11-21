using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Notifications.Infrastructure.Domain.Entiteis;
using Notifications.Infrastructure.Persistence.DataBase;
using Notifications.Infrastructure.Persistence.Repositories.Interfaces;

namespace Notifications.Infrastructure.Persistence.Repositories.Notifications;

public class EmailTemplateRepository : EntityRepositoryBase<EmailTemplate, NotificationDbContext>,IEmailTemplateRepository
{
    public EmailTemplateRepository(NotificationDbContext dbContext) : base(dbContext)
    {
    }
    
    public IQueryable<EmailTemplate> Get(Expression<Func<EmailTemplate, bool>>? predicate = default, bool asNoTracking = false)
    {
        return base.Get(predicate, asNoTracking);
    }

    public ValueTask<EmailTemplate> CreateAsync(EmailTemplate emailTemplate, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        return base.CreateAsync(emailTemplate, saveChanges, cancellationToken);
    }

}