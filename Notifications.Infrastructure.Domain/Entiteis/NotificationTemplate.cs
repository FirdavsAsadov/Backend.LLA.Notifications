using Notifications.Infrastructure.Domain.Common.Entities;
using Notifications.Infrastructure.Domain.Enums;

namespace Notifications.Infrastructure.Domain.Entiteis
{
    public abstract class NotificationTemplate : IEntity
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = default!;
        public NotificationType Type { get; set; }

        public IList<NotificationHistory> Histories { get; set; } = new List<NotificationHistory>();
    }
}
