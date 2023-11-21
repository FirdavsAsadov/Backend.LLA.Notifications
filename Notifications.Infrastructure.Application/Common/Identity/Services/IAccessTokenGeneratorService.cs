using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Notifications.Infrastructure.Domain.Entiteis;

namespace Notifications.Infrastructure.Application.Common.Identity.Services;

public interface IAccessTokenGeneratorService
{
    string GetToken(User user);
    JwtSecurityToken GetJwtToken(User user);
    List<Claim> GetClaims(User user);
}