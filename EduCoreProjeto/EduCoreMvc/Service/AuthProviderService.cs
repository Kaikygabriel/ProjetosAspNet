using System.Text;
using System.Text.Json;
using EduCoreMvc.Models;
using EduCoreMvc.Models.Providers;

namespace EduCoreMvc.Service;

public class AuthProviderService
{
    private const string ApiEndPoint = "Providers";
    private readonly IHttpClientFactory _client;
    private readonly JsonSerializerOptions _options;

    public AuthProviderService(IHttpClientFactory client)
    {
        _client = client;
        _options= new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
    }

    public async Task<bool> RegisterProvider(RegisterProviderDto loginProviderDTo)
    {
        var client = _client.CreateClient("ApiEduCore");
        var userJson = JsonSerializer.Serialize(loginProviderDTo);
        var content = new StringContent(userJson, Encoding.UTF8, "application/json");
        using (var result = await client.PostAsync("Providers/Register",content))
        {
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
    public async Task<TokenModel?> LoginUser(LoginProviderDto loginProviderDTo)
    {
        var client = _client.CreateClient("ApiEduCore");

        var userJson = JsonSerializer.Serialize(loginProviderDTo, _options);
        var content = new StringContent(userJson, Encoding.UTF8, "application/json");

        using var result = await client.PostAsync($"{ApiEndPoint}/Login", content);

        if (!result.IsSuccessStatusCode)
            return null;

        var stream = await result.Content.ReadAsStreamAsync();
        var token = await JsonSerializer.DeserializeAsync<TokenModel>(stream, _options);

        return token;
    }
}