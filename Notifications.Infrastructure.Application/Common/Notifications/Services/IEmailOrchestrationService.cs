using Notifications.Infrastructure.Application.Common.Notifications.Models;
using Notifications.Infrastructure.Domain.Common.Exeptions;

namespace Notifications.Infrastructure.Application.Common.Notifications.Services;

public interface IEmailOrchestrationService
{
    ValueTask<FuncResult<bool>> SendAsync(
        EmailNotificationRequest request,
        CancellationToken cancellationToken = default
    );
}