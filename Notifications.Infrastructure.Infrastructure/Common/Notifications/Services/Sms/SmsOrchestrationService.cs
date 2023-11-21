using AutoMapper;
using Notifications.Infrastructure.Application.Common.Identity.Services;
using Notifications.Infrastructure.Application.Common.Notifications.Models;
using Notifications.Infrastructure.Application.Common.Notifications.Services;
using Notifications.Infrastructure.Domain.Common.Exeptions;
using Notifications.Infrastructure.Domain.Entiteis;
using Notifications.Infrastructure.Domain.Enums;
using Notifications.Infrastructure.Domain.Extensions;

namespace Notifications.Infrastructure.Infrastructure.Common.Notifications.Services;

public class SmsOrchestrationService : ISmsOrchestrationService
{
    private readonly IMapper _mapper;
    private readonly ISmsTemplateService _templateService;
    private readonly ISmsHistoryService _historyService;
    private readonly ISmsRenderingService _renderingService;
    private readonly ISmsSenderService _smsSenderService;
    private readonly IUserService _userService;

    public SmsOrchestrationService(IMapper mapper, ISmsTemplateService templateService, 
        ISmsHistoryService historyService,
        ISmsRenderingService renderingService,
        ISmsSenderService senderService,
        IUserService userService)
    {
        _mapper = mapper;
        _templateService = templateService;
        _historyService = historyService;
        _renderingService = renderingService;
        _smsSenderService = senderService;
        _userService = userService;
    }
    
    public async ValueTask<FuncResult<bool>> SendAsync(SmsNotificationRequest request, CancellationToken cancellationToken = default)
    {
        var sendNotificationRequest = async () =>
        {
            var message = _mapper.Map<SmsMessage>(request);

            var senderUser =
                (await _userService.GetByIdAsync(request.SenderUserId!.Value, cancellationToken: cancellationToken));

            var receiverUser =
                (await _userService.GetByIdAsync(request.ReceiverUserId, cancellationToken: cancellationToken));

            message.SenderPhoneNumber = senderUser.PhoneNumber;
            message.ReceiverPhoneNumber = receiverUser.PhoneNumber;

            message.Template = await _templateService.GetByTypeAsync(request.TemplateType, true, cancellationToken) ??
                               throw new InvalidOperationException(
                                   $"Invalid template for sending {NotificationType.Sms} notification");

            await _renderingService.RenderAsync(message, cancellationToken);

            await _smsSenderService.SendAsync(message, cancellationToken);

            var history = _mapper.Map<SmsHistory>(message);
            
            await _historyService.CreateAsync(history, cancellationToken: cancellationToken);

            return history.IsSuccessful;
        };
        return await sendNotificationRequest.GetValueAsync();
    }
}