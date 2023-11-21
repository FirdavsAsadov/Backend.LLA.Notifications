using Notifications.Infrastructure.Domain.Common.Entities;
using Notifications.Infrastructure.Domain.Enums;

namespace Notifications.Infrastructure.Application.Common.Identity.Models;

public class VerificationCode : IEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public VerificationType Type { get; set; }
    public DateTimeOffset ExpiryTime { get; set; }
    public string? Code { get; set;}
}