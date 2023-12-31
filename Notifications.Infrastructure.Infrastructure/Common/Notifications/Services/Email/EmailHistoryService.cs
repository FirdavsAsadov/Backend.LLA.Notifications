using Microsoft.EntityFrameworkCore;
using Notifications.Infrastructure.Application.Common.Models.Querying;
using Notifications.Infrastructure.Application.Common.Notifications.Services;
using Notifications.Infrastructure.Application.Common.Querying.Extensions;
using Notifications.Infrastructure.Domain.Entiteis;
using Notifications.Infrastructure.Persistence.Repositories.Interfaces;

namespace Notifications.Infrastructure.Infrastructure.Common.Notifications.Services;

public class EmailHistoryService : IEmailHistoryService
{
    private readonly IEmailHistoryRepository _emailHistoryRepository;

    public EmailHistoryService(IEmailHistoryRepository emailHistoryRepository)
    {
        _emailHistoryRepository = emailHistoryRepository;
    }

    public async ValueTask<IList<EmailHistory>> GetByFilterAsync(FilterPagination paginationOptions,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    ) => 
        await _emailHistoryRepository.Get().ApplyPagination(paginationOptions).ToListAsync(cancellationToken);

    public async ValueTask<EmailHistory> CreateAsync(EmailHistory emailHistory, bool saveChanges = true,
        CancellationToken cancellationToken = default
    ) => await _emailHistoryRepository.CreateAsync(emailHistory, saveChanges, cancellationToken);
}