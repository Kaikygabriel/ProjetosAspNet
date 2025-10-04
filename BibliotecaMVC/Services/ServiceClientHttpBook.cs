using System.Text.Json;
using BibliotecaMVC.Models;
using BibliotecaMVC.Services.Interfaces;

namespace BibliotecaMVC.Services;

public class ServiceClientHttpBook : IServiceClientHttpBook
{
    private readonly IHttpClientFactory _clientFactory;
    private JsonSerializerOptions _json;

    public ServiceClientHttpBook(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _json= new JsonSerializerOptions{PropertyNameCaseInsensitive  = true};
    }

    public async Task<IEnumerable<BookViewModel>> GetAllAsync()
    {
        List<BookViewModel> books = new();
        var client = _clientFactory.CreateClient("BookClient");
        using (var resposta = await client.GetAsync("/Books"))
        {
            if (resposta.IsSuccessStatusCode)
            {
                var apiResponse = await resposta.Content.ReadAsStreamAsync(); 
                books = JsonSerializer.Deserialize<List<BookViewModel>>(apiResponse,_json)!;
            }
            else
            {
                return null;
            }
        }
        return books;
    }
}