using Notifications.Infrastructure.Domain.Common.Entities;
using Notifications.Infrastructure.Domain.Enums;

namespace Notifications.Infrastructure.Domain.Entiteis;

public class User : IEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string EmailAddress { get; set; } = default!;
    public string Password { get; set; } = default!;
    public Guid RoleId { get; set; }
    public Role Role { get; set; }
    public  RoleType RoleType { get; set; }

    public UserSettings UserSettings { get; set; } = default!;
 }