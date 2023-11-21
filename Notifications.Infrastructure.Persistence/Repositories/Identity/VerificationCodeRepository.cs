using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Notifications.Infrastructure.Application.Common.Identity.Models;
using Notifications.Infrastructure.Persistence.DataBase;
using Notifications.Infrastructure.Persistence.Repositories.Interfaces;

namespace Notifications.Infrastructure.Persistence.Repositories.Identity;

public class VerificationCodeRepository : EntityRepositoryBase<VerificationCode, NotificationDbContext>, IVerificationCodeRepository
{
    public VerificationCodeRepository(NotificationDbContext dbContext) : base(dbContext)
    {
    }

    public ValueTask<VerificationCode> CreateAsync(VerificationCode verificationCode, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        return base.CreateAsync(verificationCode, saveChanges, cancellationToken);
    }

    public IQueryable<VerificationCode> Get(
        Expression<Func<VerificationCode, bool>>?
            predicate = default,
        bool asNoTracking = false)
    {
        return base.Get(predicate, asNoTracking);
    }
}