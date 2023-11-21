namespace Notifications.Infrastructure.Application.Common.Identity.Services;

public interface IPasswordHasher
{
    string HashPassword(string password);
    bool ValidatePassword(string password, string hashedPassword);
}