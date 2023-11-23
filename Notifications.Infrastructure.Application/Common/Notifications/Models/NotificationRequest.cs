using Notifications.Infrastructure.Domain.Enums;

namespace Notifications.Infrastructure.Application.Common.Notifications.Models;

public abstract  class NotificationRequest
{
    public Guid? SenderUserId {get; set; }
    public Guid ReceiverUserId {get; set; }
    public NotificationTemplateType TemplateType {get; set; }
    public NotificationType? Type { get; set; } = null;
    public Dictionary<string, ValueTask<string>> Variables { get; set; }
}