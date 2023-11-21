using System.Linq.Expressions;
using Notifications.Infrastructure.Domain.Entiteis;

namespace Notifications.Infrastructure.Persistence.Repositories.Interfaces;

public interface ITokenRepository
{
    IQueryable<Token> Get(
        Expression<Func<Token, bool>>?
            predicate = default,
        bool asNoTracking = false);

    ValueTask<Token> CreateAsync(
        Token token,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
    );
}