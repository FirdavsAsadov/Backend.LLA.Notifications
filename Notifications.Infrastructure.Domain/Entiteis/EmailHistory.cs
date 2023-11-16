namespace Notifications.Infrastructure.Domain.Entiteis;
using Notifications.Infrastructure.Domain.Enums;
public class EmailHistory : NotificationHistory
{
    public EmailHistory()
    {
        Type = NotificationType.Email;
    }

    public string SendEmailAddress { get; set; } = default!;
    
    public string ReceiverEmailAddress { get; set; } = default!;
    public string Subject { get; set; } = default!;
}