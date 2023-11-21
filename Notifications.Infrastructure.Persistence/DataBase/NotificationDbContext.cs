using Notifications.Infrastructure.Application.Common.Identity.Models;
using Notifications.Infrastructure.Domain.Entiteis;

namespace Notifications.Infrastructure.Persistence.DataBase;
using Microsoft.EntityFrameworkCore;

public class NotificationDbContext : DbContext
{
    public DbSet<SmsTemplate> _SmsTemplates => Set<SmsTemplate>();
    public DbSet<EmailTemplate> _EmailTemplates => Set<EmailTemplate>();
    public DbSet<EmailHistory> _EmailHistory => Set<EmailHistory>();
    public DbSet<SmsHistory> _SmsHistory => Set<SmsHistory>();
    public DbSet<User> _User => Set<User>();
    public DbSet<UserSettings> _UserSettings => Set<UserSettings>();
    public DbSet<Role> _Role => Set<Role>();
    public DbSet<Token> _Token => Set<Token>();
    public DbSet<VerificationCode> _VerificationCodes => Set<VerificationCode>();
    public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotificationDbContext).Assembly);
    }
}