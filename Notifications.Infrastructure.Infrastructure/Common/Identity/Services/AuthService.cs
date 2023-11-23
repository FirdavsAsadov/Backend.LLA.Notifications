using System.Security.Authentication;
using Notifications.Infrastructure.Application.Common.Identity.Models;
using Notifications.Infrastructure.Application.Common.Identity.Services;
using Notifications.Infrastructure.Application.Common.Notifications.Models;
using Notifications.Infrastructure.Application.Common.Notifications.Services;
using Notifications.Infrastructure.Domain.Entiteis;
using Notifications.Infrastructure.Domain.Enums;

namespace Notifications.Infrastructure.Infrastructure.Common.Identity.Services;

public class AuthService : IAuthService
{
    private readonly IAccessTokenGeneratorService _accessTokenGeneratorService;
    private readonly IRoleService _roleService;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IAccountService _accountService;
    private readonly IUserService _userService;
    private readonly INotificationAggregatorService _notificationAggregatorService;
    public AuthService(IAccessTokenGeneratorService accessTokenGeneratorService,
        IRoleService roleService, 
        ITokenService tokenService, 
        IPasswordHasher passwordHasher,
        IAccountService accountService,
        IUserService userService,
        INotificationAggregatorService notificationAggregatorService)
    {
        _accessTokenGeneratorService = accessTokenGeneratorService;
        _roleService = roleService;
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;
        _accountService = accountService;
        _userService = userService;
        _notificationAggregatorService = notificationAggregatorService;
    }
    public async ValueTask<bool> RegisterAsync(RegistrationDetails registrationDetails, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        var defaultRole = await _roleService.GetByTypeAsync(RoleType.Guest, true, cancellationToken) ??
                          throw new InvalidOperationException("Role with this type is not");

        var newUser = new User
        {
            Id = Guid.NewGuid(),
            FirstName = registrationDetails.FirstName,
            LastName = registrationDetails.LastName,
            EmailAddress = registrationDetails.Email,
            Password = _passwordHasher.HashPassword(registrationDetails.Password),
            PhoneNumber = registrationDetails.PhoneNumber,
            RoleId = defaultRole.Id
        };

        await _accountService.CreateUserAsync(newUser);
        var smsWelcomeMessage = "Sistemaga xush keilbsiz";

        var notification = new EmailNotificationRequest()
        {
            SenderUserId = Guid.Empty,
            ReceiverUserId = newUser.Id,
            TemplateType = NotificationTemplateType.EmailVerificationNotification,
            Type = NotificationType.Sms,
            Variables = new Dictionary<string, ValueTask<string>>()
            {
                { "VerificationCode", new(smsWelcomeMessage) }
            }
        };
        await _notificationAggregatorService.SendAsync(notification);

        return true;
    }

    public async Task<string> LoginAsync(LoginDetails loginDetails, CancellationToken cancellationToken = default)
    {
        var founderUser = await _userService.GetUserByEmailAsync(loginDetails.Email);
        if (founderUser == null)
            throw new AuthenticationException("Login details are invalid!");

        var smsWelcomeMessage = "Sistemaga xush keilbsiz";

        var notification = new EmailNotificationRequest()
        {
            ReceiverUserId = Guid.Empty,
            TemplateType = NotificationTemplateType.EmailVerificationNotification,
            Type = NotificationType.Sms,
            Variables = new Dictionary<string, ValueTask<string>>()
            {
                { "VerificationCode", new(smsWelcomeMessage) }
            }
        };

        await _notificationAggregatorService.SendAsync(notification);
        var accessToken = _accessTokenGeneratorService.GetToken(founderUser);
        await _tokenService.CreateAsync(founderUser.Id, accessToken, cancellationToken: cancellationToken);

        return accessToken;
    }
}