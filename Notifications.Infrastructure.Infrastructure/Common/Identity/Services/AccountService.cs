using Notifications.Infrastructure.Application.Common.Identity.Services;
using Notifications.Infrastructure.Application.Common.Notifications.Models;
using Notifications.Infrastructure.Application.Common.Notifications.Services;
using Notifications.Infrastructure.Domain.Entiteis;
using Notifications.Infrastructure.Domain.Enums;
using Notifications.Infrastructure.Persistence.DataBase;

namespace Notifications.Infrastructure.Infrastructure.Common.Identity.Services;

public class AccountService : IAccountService
{
    private readonly IVerificationCodeGeneratorService _verificationCodeGeneratorService;
    private readonly INotificationAggregatorService _notificationAggregatorService;
    private readonly IUserService _userService;
    private readonly NotificationDbContext _notificationDbContext;

    public AccountService(IVerificationCodeGeneratorService verificationCodeGeneratorService, 
        INotificationAggregatorService notificationAggregatorService,
        IUserService userService, 
        NotificationDbContext notificationDbContext)
    {
        _verificationCodeGeneratorService = verificationCodeGeneratorService;
        _notificationAggregatorService = notificationAggregatorService;
        _userService = userService;
        _notificationDbContext = notificationDbContext;
    }

    public async ValueTask<User> CreateUserAsync(User user)
    {
        await _userService.CreateAsync(user);

        var emailVerification = _verificationCodeGeneratorService.GenerateCode(
            VerificationType.EmailAddressVerification, user
                .Id = Guid.Empty);

        var notification = new NotificationRequest()
        {
            ReceiverUserId = Guid.Empty,
            TemplateType = NotificationTemplateType.EmailVerificationNotification,
            Type = NotificationType.Email,
            Variables = new Dictionary<string, ValueTask<string>>()
            {
                { "VerificationCode", emailVerification }
            }
        };
        var result = await _notificationAggregatorService.SendAsync(notification);

        return user;
    }

}