using Notifications.Infrastructure.Domain.Common.Entities;

namespace Notifications.Infrastructure.Domain.Entiteis;

public class Token : IEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string TokenValue { get; set; } = string.Empty;
    public bool isRevoked { get; set; }
    public DateTime CreatedTime { get; set; }
}