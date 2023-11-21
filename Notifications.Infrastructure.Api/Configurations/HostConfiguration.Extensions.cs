using System.Reflection;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Notifications.Infrastructure.Application.Common.Identity.Services;
using Notifications.Infrastructure.Application.Common.Identity.Settings;
using Notifications.Infrastructure.Application.Common.Notifications.Brokers;
using Notifications.Infrastructure.Application.Common.Notifications.Services;
using Notifications.Infrastructure.Infrastructure.Common.Identity.Services;
using Notifications.Infrastructure.Infrastructure.Common.Notifications.Brokers;
using Notifications.Infrastructure.Infrastructure.Common.Notifications.Services;
using Notifications.Infrastructure.Infrastructure.Common.Notifications.Services.Aggregation;
using Notifications.Infrastructure.Infrastructure.Common.Settings;
using Notifications.Infrastructure.Persistence.DataBase;
using Notifications.Infrastructure.Persistence.Repositories.Identity;
using Notifications.Infrastructure.Persistence.Repositories.Interfaces;
using Notifications.Infrastructure.Persistence.Repositories.Notifications;

namespace Notifications.Infrastructure.Api.Configurations;

public static partial class HostConfiguration
{
    private static readonly ICollection<Assembly> Assemblies;

    static HostConfiguration()
    {
        Assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load).ToList();
        Assemblies.Add(Assembly.GetExecutingAssembly());
    }

    private static WebApplicationBuilder AddValidators(this WebApplicationBuilder builder)
    {
        builder.Services.AddValidatorsFromAssemblies(Assemblies);
        
        return builder;
    }
    private static WebApplicationBuilder AddMappers(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(Assemblies);
        
        return builder;
    }

    private static WebApplicationBuilder AddNotificationInfrastructureNotifications(this WebApplicationBuilder builder)
    {
        builder.Services
            .Configure<TemplateRendiringSettings>(builder.Configuration.GetSection(nameof(TemplateRendiringSettings)))
            .Configure<SmtpEmailSenderSettings>(builder.Configuration.GetSection(nameof(SmtpEmailSenderSettings)))
            .Configure<TwilioSmsSenderSettings>(builder.Configuration.GetSection(nameof(TwilioSmsSenderSettings)))
            .Configure<NotificationSettings>(builder.Configuration.GetSection(nameof(NotificationSettings)));

        builder.Services
            .AddScoped<IEmailTemplateRepository, EmailTemplateRepository>()
            .AddScoped<ISmsTemplateRepository, SmsTemplateRepository>()
            .AddScoped<IEmailHistoryRepository, EmailHistoryRepository>()
            .AddScoped<ISmsHistoryRepository, SmsHistoryRepository>();

        builder.Services
            .AddScoped<ISmsSenderBroker, TwilioSmsSenderBroker>()
            .AddScoped<IEmailSenderBroker, SmtpEmailSenderBroker>();

        builder.Services
            .AddScoped<ISmsTemplateService, SmsTemplateService>()
            .AddScoped<IEmailTemplateService, EmailTemplateService>()
            .AddScoped<IEmailHistoryService, EmailHistoryService>()
            .AddScoped<ISmsHistoryService, SmsHistoryService>();
            
        builder.Services
            .AddScoped<IEmailSenderService, EmailSenderService>()
            .AddScoped<ISmsSenderService, SmsSenderService>()
            .AddScoped<IEmailRenderingService, EmailRendiringService>()
            .AddScoped<ISmsRenderingService, SmsRenderingService>();

        builder.Services.AddScoped<ISmsOrchestrationService, SmsOrchestrationService>()
            .AddScoped<IEmailOrchestrationService, EmailOrchestrationService>();

        builder.Services.AddScoped<INotificationAggregatorService, NotificationAggregationService>();

        return builder;
    }

    private static WebApplicationBuilder AddNotificationInfrastructureIdentity(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<VerificationCodeSettings>(builder.Configuration.GetSection(nameof(VerificationCodeSettings)))
            .Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)))
            .Configure<VerificationTokenSettings>(builder.Configuration.GetSection(nameof(VerificationTokenSettings)));

        builder.Services.AddDataProtection();

        builder.Services
            .AddTransient<IAccessTokenGeneratorService, AccessTokenGeneratorService>()
            .AddTransient<IPasswordHasher, PasswordHasher>()
            .AddTransient<IVerificationCodeGeneratorService, VerificationCodeGeneratorService>();

        builder.Services
            .AddScoped<IUserService, UserService>()
            .AddScoped<ITokenService, TokenService>()
            .AddScoped<IVerificationCodeService, VerificationCodeService>()
            .AddScoped<IRoleService, RoleService>();

        builder.Services
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<IAccountService, AccountService>();

        builder.Services
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IRoleRepository, RoleRepository>()
            .AddScoped<ITokenRepository, TokenRepository>()
            .AddScoped<IVerificationCodeRepository, VerificationCodeRepository>()
            .AddScoped<IUserSettingRepository, UserSettingsRepository>();

        builder.Services.AddDbContext<NotificationDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("NotificationsDatabaseConnection")));
        
        var jwtSettings = new JwtSettings();
        builder.Configuration.GetSection(nameof(JwtSettings)).Bind(jwtSettings);
        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = jwtSettings.ValidateIssuer,
                    ValidIssuer = jwtSettings.ValidIssuer,
                    ValidAudience = jwtSettings.ValidAudience,
                    ValidateAudience = jwtSettings.ValidateAudience,
                    ValidateLifetime = jwtSettings.ValidateLifetime,
                    ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                };
            });
        return builder;
    }

    private static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        

        return builder;
    }
    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddControllers();
        return builder;
    }

    private static WebApplication UseExposers(this WebApplication app)
    {
        app.MapControllers();
        
        return app;
    }

    private static WebApplication UseDevTools(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        
        return app;
    }
}