using AutoMapper;
using Notifications.Infrastructure.Application.Common.Notifications.Models;

namespace Notifications.Infrastructure.Infrastructure.Common.Mappers;

public class NotificationMessageMapper : Profile
{
    public NotificationMessageMapper()
    {
        CreateMap<EmailNotificationRequest, EmailMessage>();
        CreateMap<SmsNotificationRequest, SmsMessage>();
    }
}