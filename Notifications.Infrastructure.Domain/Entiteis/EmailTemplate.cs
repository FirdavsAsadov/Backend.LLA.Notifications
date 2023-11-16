using Notifications.Infrastructure.Domain.Enums;
using Type = Notifications.Infrastructure.Domain.Enums.NotificationType;
namespace Notifications.Infrastructure.Domain.Entiteis;

public class EmailTemplate : NotificationTemplate
{
    public EmailTemplate()
    {
        Type = Type.Email;
    }

    public string Subject { get; set; } = default!;
}