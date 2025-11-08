using System.Security.Claims;
using DevTalk.Domain.BackOffice.Entities;
using Microsoft.Extensions.Configuration;

namespace DevTalk.Application.Service.Interfaces;

public interface ITokenService
{
    IEnumerable<Claim> GetClaimsFromUser(User User);
    string GenerateAccessToken(IEnumerable<Claim>claims,IConfiguration configuration);
    string GerenateRefreshToken();

    ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string token, IConfiguration configuration);
}