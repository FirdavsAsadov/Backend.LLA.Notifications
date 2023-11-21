using Notifications.Infrastructure.Application.Common.Identity.Services;
using Notifications.Infrastructure.Domain.Entiteis;
using Notifications.Infrastructure.Persistence.Repositories.Interfaces;

namespace Notifications.Infrastructure.Infrastructure.Common.Identity.Services;

public class TokenService : ITokenService
{
    private readonly ITokenRepository _tokenRepository;

    public TokenService(ITokenRepository tokenRepository)
    {
        _tokenRepository = tokenRepository;
    }

    public ValueTask<Token> CreateAsync(Token token, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        return _tokenRepository.CreateAsync(token, saveChanges, cancellationToken);
    }
    public async ValueTask<Token> CreateAsync(Guid userId, string value, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        var accessToken = new Token
        {
            UserId = userId,
            TokenValue = value,
            CreatedTime = DateTime.UtcNow
        };

        await _tokenRepository.CreateAsync(accessToken, saveChanges, cancellationToken);

        return accessToken;
    }
}