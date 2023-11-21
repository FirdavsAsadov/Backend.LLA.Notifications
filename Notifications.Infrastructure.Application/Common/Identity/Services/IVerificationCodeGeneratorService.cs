using Notifications.Infrastructure.Application.Common.Identity.Models;
using Notifications.Infrastructure.Domain.Enums;

namespace Notifications.Infrastructure.Application.Common.Identity.Services;

public interface IVerificationCodeGeneratorService
{
    ValueTask<string> GenerateCode(VerificationType verificationType, Guid userId);
    (VerificationCode Token, bool IsValid) DecodeToken(string token);
}