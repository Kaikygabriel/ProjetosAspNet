using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using ProductsApi.Domain.BackOffice.Entitys;

namespace ProductsApi.Application.Services.Interfaces;

public interface IServiceToken
{
    string GenerateAccessToken(IEnumerable<Claim> claims, IConfiguration configuration);
    IEnumerable<Claim> GetClaimsFromUser(User user);
}