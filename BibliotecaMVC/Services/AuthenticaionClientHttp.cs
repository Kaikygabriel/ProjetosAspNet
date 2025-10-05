using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
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

    public async Task<bool> AuthenticationRegisterAsync(UserRegisterViewModel model)
    {
        var client = _clientFactory.CreateClient("Authentication");
        var modelJson = JsonSerializer.Serialize(model);
        var stringContent = new StringContent(modelJson, Encoding.UTF8, "Application/Json");
        using (var response = await client.PostAsync("/Auth/Register",stringContent))
        {
            if (!response.IsSuccessStatusCode)
                return false;
        }

        return true;
    }
    
    public async Task<TokenViewModel?> AuthenticationLoginAsync(UserLoginViewModel model)
    {
        TokenViewModel tokenView = new();
        var client = _clientFactory.CreateClient("Authentication");
        var modelJson = JsonSerializer.Serialize(model);
        var stringContent = new StringContent(modelJson, Encoding.UTF8, "Application/Json");
        using (var response = await client.PostAsync("/Auth/Login",stringContent))
        {
            if (response.IsSuccessStatusCode)
            {
                var content =await response.Content.ReadAsStreamAsync();
                var token = JsonSerializer.Deserialize<TokenViewModel>(content,_jsonSerializerOptions);
                tokenView = token;
            }
            else
            {
                return null;
            }
        }

        return tokenView;
    }
}