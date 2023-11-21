using Notifications.Infrastructure.Application.Common.Models.Querying;
using Notifications.Infrastructure.Application.Common.Notifications.Models;
using Notifications.Infrastructure.Domain.Common.Exeptions;
using Notifications.Infrastructure.Domain.Entiteis;

namespace Notifications.Infrastructure.Application.Common.Notifications.Services;

public interface INotificationAggregatorService
{
    ValueTask<FuncResult<bool>> SendAsync(
        NotificationRequest notificationRequest,
        CancellationToken cancellationToken = default
        );

    ValueTask<IList<NotificationTemplate>> GetTemplatesByFilterAsync(
        NotificationTemplateFilter filter,
        CancellationToken cancellationToken = default
    );
}