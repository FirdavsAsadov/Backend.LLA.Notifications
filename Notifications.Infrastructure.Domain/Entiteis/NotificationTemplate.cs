using Notifications.Infrastructure.Domain.Common.Entities;
using Notifications.Infrastructure.Domain.Enums;

namespace Notifications.Infrastructure.Domain.Entiteis
{
    public class NotificationTemplate : IEntity
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = default!;
        public NotificationType Type { get; set; }
    }
}
