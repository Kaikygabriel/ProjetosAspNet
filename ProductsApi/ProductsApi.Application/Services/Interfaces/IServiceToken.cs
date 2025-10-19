using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace ProductsApi.Application.Services.Interfaces;

public interface IServiceToken
{
    string GerenateAcessToken(IEnumerable<Claim> claims, IConfiguration configuration);
}