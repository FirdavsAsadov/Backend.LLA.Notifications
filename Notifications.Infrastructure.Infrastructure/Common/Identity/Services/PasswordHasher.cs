using Notifications.Infrastructure.Application.Common.Identity.Services;
using BC = BCrypt.Net.BCrypt;

namespace Notifications.Infrastructure.Infrastructure.Common.Identity.Services;

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        return BC.HashPassword(password);
    }

    public bool ValidatePassword(string password, string hashedPassword)
    {
        return BC.Verify(password, hashedPassword);
    }
}