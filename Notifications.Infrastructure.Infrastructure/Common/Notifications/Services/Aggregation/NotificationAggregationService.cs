using AutoMapper;
using Microsoft.Extensions.Options;
using Notifications.Infrastructure.Application.Common.Identity.Services;
using Notifications.Infrastructure.Application.Common.Models.Querying;
using Notifications.Infrastructure.Application.Common.Notifications.Models;
using Notifications.Infrastructure.Application.Common.Notifications.Services;
using Notifications.Infrastructure.Domain.Common.Exeptions;
using Notifications.Infrastructure.Domain.Entiteis;
using Notifications.Infrastructure.Domain.Enums;
using Notifications.Infrastructure.Domain.Extensions;
using Notifications.Infrastructure.Infrastructure.Common.Settings;

namespace Notifications.Infrastructure.Infrastructure.Common.Notifications.Services.Aggregation;

public class NotificationAggregationService : INotificationAggregatorService
{
    private readonly IMapper _mapper;
    private readonly NotificationSettings _notificationSettings;
    private readonly ISmsOrchestrationService _smsOrchestrationService;
    private readonly IEmailOrchestrationService _emailOrchestrationService;
    private readonly IUserService _userService;
    private readonly ISmsTemplateService _smsTemplateService;
    private readonly IEmailTemplateService _emailTemplateService;

    public NotificationAggregationService(IMapper mapper,
        IOptions<NotificationSettings> notificationSettings,
        ISmsTemplateService smsTemplateService,
        IEmailTemplateService emailTemplateService,
        ISmsOrchestrationService smsOrchestrationService,
        IEmailOrchestrationService emailOrchestrationService,
        IUserService userService
        )
    {
        _mapper = mapper;
        _notificationSettings = notificationSettings.Value;
        _smsTemplateService = smsTemplateService;
        _smsOrchestrationService = smsOrchestrationService;
        _emailOrchestrationService = emailOrchestrationService;
        _userService = userService;
        _emailTemplateService = emailTemplateService;
    }
    
    public async ValueTask<FuncResult<bool>> SendAsync(NotificationRequest notificationRequest, CancellationToken cancellationToken = default)
    {
        var sendNotificationTask = async () =>
        {
            var senderUser = notificationRequest.SenderUserId.HasValue
                ? await _userService.GetByIdAsync(notificationRequest.SenderUserId.Value,
                    cancellationToken: cancellationToken)
                : await _userService.GetSystemUserAsync(true, cancellationToken);

            notificationRequest.SenderUserId = senderUser!.Id;

            var receiverUser = await _userService.GetByIdAsync(notificationRequest.ReceiverUserId,
                cancellationToken: cancellationToken);

            if (!notificationRequest.Type.HasValue && receiverUser!.UserSettings.PreferredNotificationType.HasValue)
                notificationRequest.Type = receiverUser!.UserSettings.PreferredNotificationType.Value;

            if (!notificationRequest.Type.HasValue)
                notificationRequest.Type = _notificationSettings.DefaultNotificationType;

            var sendNotificationTask = notificationRequest.Type switch
            {
                NotificationType.Sms => _smsOrchestrationService.SendAsync(
                    _mapper.Map<SmsNotificationRequest>(notificationRequest),
                    cancellationToken),
                NotificationType.Email => _emailOrchestrationService.SendAsync(
                    _mapper.Map<EmailNotificationRequest>(notificationRequest),
                    cancellationToken),
                
              _ => throw new NotImplementedException("This type of notification is not supported yet.")
            };
            var sendNotificationResult = await sendNotificationTask;
            return sendNotificationResult.Data;
        };

        return await sendNotificationTask.GetValueAsync();
    }

    public async ValueTask<IList<NotificationTemplate>> GetTemplatesByFilterAsync(NotificationTemplateFilter filter, CancellationToken cancellationToken = default)
    {
        var templates = new List<NotificationTemplate>();
        
        if(filter.TemplateType.Contains(NotificationType.Sms))
            templates.AddRange(
                await _smsTemplateService.GetByFilterAsync(filter, cancellationToken: cancellationToken));
        
        if(filter.TemplateType.Contains(NotificationType.Email))
            templates.AddRange(await _emailTemplateService.GetByFilterAsync(filter, cancellationToken: cancellationToken));

        return templates;
    }
}