using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Notifications.Infrastructure.Application.Common.Identity.Models;
using Notifications.Infrastructure.Application.Common.Identity.Services;
using Notifications.Infrastructure.Domain.Enums;

namespace Notifications.Infrastructure.Infrastructure.Common.Identity.Services;

public class VerificationCodeGeneratorService : IVerificationCodeGeneratorService
{
    private readonly IVerificationCodeService _verificationCodeService;

    public VerificationCodeGeneratorService(IVerificationCodeService verificationCodeService)
    {
        _verificationCodeService = verificationCodeService;
    }
    public ValueTask<string> GenerateCode(VerificationType verificationType, Guid userId)
    {
        throw new NotImplementedException();
    }

    public (VerificationCode Token, bool IsValid) DecodeToken(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentNullException(token);

        var verificationCode = JsonConvert.DeserializeObject<VerificationCode>(token) ??
                               throw new ArgumentNullException("Invalid Verification token", nameof(token));
        
        return (verificationCode, verificationCode.ExpiryTime > DateTimeOffset.UtcNow);
    }
}