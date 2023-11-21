using Notifications.Infrastructure.Domain.Common.Entities;
using Notifications.Infrastructure.Domain.Enums;

namespace Notifications.Infrastructure.Domain.Entiteis;

public class Role : IEntity
{
    public Guid Id { get; set; }
    public RoleType Type { get; set; }  
    public bool IsDisabled { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime ModifiedTime { get; set; }
}