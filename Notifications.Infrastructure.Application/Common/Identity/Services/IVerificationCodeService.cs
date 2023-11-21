using System.Linq.Expressions;
using Notifications.Infrastructure.Application.Common.Identity.Models;

namespace Notifications.Infrastructure.Application.Common.Identity.Services;

public interface IVerificationCodeService
{
    ValueTask<string> Generate(Guid userId);
    ValueTask<VerificationCode> CreateAsync(VerificationCode code);
    ValueTask<VerificationCode> UpdateAsync(VerificationCode code);
    IQueryable<VerificationCode> Get(Expression<Func<VerificationCode, bool>> predicate);
}