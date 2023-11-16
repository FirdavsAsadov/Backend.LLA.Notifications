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
    public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotificationDbContext).Assembly);
    }
}