using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace DevTalk.Application.Service.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim>claims,IConfiguration configuration);
}