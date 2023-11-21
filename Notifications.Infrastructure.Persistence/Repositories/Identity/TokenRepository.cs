using System.Linq.Expressions;
using Notifications.Infrastructure.Domain.Entiteis;
using Notifications.Infrastructure.Persistence.DataBase;
using Notifications.Infrastructure.Persistence.Repositories.Interfaces;

namespace Notifications.Infrastructure.Persistence.Repositories.Identity;

public class TokenRepository : EntityRepositoryBase<Token, NotificationDbContext>,ITokenRepository
{
    public TokenRepository(NotificationDbContext dbContext) : base(dbContext)
    {
        
    }
    public IQueryable<Token> Get(Expression<Func<Token, bool>>? predicate = default, bool asNoTracking = false)
    {
        return base.Get(predicate, asNoTracking);
    }

    public ValueTask<Token> CreateAsync(Token token, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return base.CreateAsync(token, saveChanges, cancellationToken);
    }
}