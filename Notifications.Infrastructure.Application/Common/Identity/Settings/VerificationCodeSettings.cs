namespace Notifications.Infrastructure.Application.Common.Identity.Settings;

public class VerificationCodeSettings
{
    public string IdentityVerificationCodePurpose { get; set; } = default!;
    
    public int IdentityVerificationCodeExpirationDurationInMinutes { get; set; }
    
    public string VerificationServiceType { get; set; } = default!;
}