using Microsoft.EntityFrameworkCore;
using Notifications.Infrastructure.Persistence.DataBase;

namespace Notifications.Infrastructure.Api.Configurations;

public static partial class HostConfiguration
{
    private static WebApplicationBuilder AddNotificationInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<NotificationDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("NotificationsDatabaseConnection")));
        return builder;
    }
}