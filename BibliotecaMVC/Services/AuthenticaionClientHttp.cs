using System.Text;
using System.Text.Json;
using BibliotecaMVC.Models;
using BibliotecaMVC.Services.Interfaces;

namespace BibliotecaMVC.Services;

public class AuthenticaionClientHttp : IAuthenticationClientHttp
{
    private readonly IHttpClientFactory _clientFactory;
    private JsonSerializerOptions _jsonSerializerOptions;

    public AuthenticaionClientHttp(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<bool> AuthenticationAsync(UserLoginViewModel model)
    {
        var client = _clientFactory.CreateClient("BookClient");
        var modelJson = JsonSerializer.Serialize(model);
        var stringContent = new StringContent(modelJson, Encoding.UTF8, "Application/Json");
        using (var response = await client.PostAsync("/Auth/Register",stringContent))
        {
            if (!response.IsSuccessStatusCode)
                return false;
        }

        return true;
    }
}