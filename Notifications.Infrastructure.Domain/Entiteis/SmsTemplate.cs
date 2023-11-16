namespace Notifications.Infrastructure.Domain.Entiteis;
using Type = Notifications.Infrastructure.Domain.Enums.NotificationType;
public class SmsTemplate : NotificationTemplate
{
    public SmsTemplate()
    {
        Type = Type.Sms;
    }
}