using System.Linq.Expressions;
using Microsoft.Extensions.Options;
using Notifications.Infrastructure.Application.Common.Identity.Models;
using Notifications.Infrastructure.Application.Common.Identity.Services;
using Notifications.Infrastructure.Application.Common.Identity.Settings;
using Notifications.Infrastructure.Persistence.DataBase;

namespace Notifications.Infrastructure.Infrastructure.Common.Identity.Services;

public class VerificationCodeService : IVerificationCodeService
{
    private readonly NotificationDbContext _dbContext;
    private readonly VerificationCodeSettings _verificationCodeSettings;

    public VerificationCodeService(NotificationDbContext dbContext, IOptions<VerificationCodeSettings> verificationCodeSettings)
    {
        _dbContext = dbContext;
        _verificationCodeSettings = verificationCodeSettings.Value;
    }
    public async ValueTask<string> Generate(Guid userId)
    {
        var verification = new VerificationCode
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            ExpiryTime =
                DateTime.UtcNow.AddMinutes(
                    _verificationCodeSettings.IdentityVerificationCodeExpirationDurationInMinutes),
            Code = new Random().Next(1000, 99999).ToString()
        };

        var createdCode = await CreateAsync(verification);

        return new(createdCode.Code);
    }

    public async ValueTask<VerificationCode> CreateAsync(VerificationCode code)
    {
        _dbContext._VerificationCodes.AddAsync(code);
        await _dbContext.SaveChangesAsync();

        return code;
    }

    public IQueryable<VerificationCode> Get(Expression<Func<VerificationCode, bool>> predicate)
        => _dbContext._VerificationCodes.Where(predicate.Compile()).AsQueryable();
    
    public async ValueTask<VerificationCode> UpdateAsync(VerificationCode code)
    {
        var updatingCode = _dbContext._VerificationCodes.FirstOrDefault(code => code.Code == code.Code);
        if (updatingCode is null)
            throw new InvalidOperationException("This Code Already exsisting!!");

        var newCode = new VerificationCode
        {
            Id = code.Id,
            UserId = code.UserId,
            Code = new Random().Next(1000, 99999).ToString(),
            ExpiryTime =
                DateTime.UtcNow.AddMinutes(
                    _verificationCodeSettings.IdentityVerificationCodeExpirationDurationInMinutes)
        };

        await _dbContext.AddAsync(newCode);
        await _dbContext.SaveChangesAsync();

        return newCode;
    }
}