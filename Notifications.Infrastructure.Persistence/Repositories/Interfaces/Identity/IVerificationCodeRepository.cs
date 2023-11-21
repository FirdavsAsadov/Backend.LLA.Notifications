using System.Linq.Expressions;
using Notifications.Infrastructure.Application.Common.Identity.Models;

namespace Notifications.Infrastructure.Persistence.Repositories.Interfaces;

public interface IVerificationCodeRepository
{
    IQueryable<VerificationCode> Get(
        Expression<Func<VerificationCode, bool>>?
            predicate = default,
        bool asNoTracking = false);

    ValueTask<VerificationCode> CreateAsync(
        VerificationCode verificationCode,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
        );
}