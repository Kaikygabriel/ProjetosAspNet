using System.Text.Json;
using CategoriasMvc.Models;
using CategoriasMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CategoriasMvc.Services;

public class FilmeService : IFilmeService
{
    private const string ApiEndPoint = "/Filmes";
    private readonly IHttpClientFactory clientFactory;
    private readonly JsonSerializerOptions optionsJson;

    private List<FilmesViewModel> filmesViewModels = new();
    
    public FilmeService(IHttpClientFactory clientFactory)
    {
        optionsJson = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
        this.clientFactory =clientFactory;
    }

    public async Task<List<FilmesViewModel>> GetFilmes()
    {
        var client = clientFactory.CreateClient("Filmes");
        using (var response = await client.GetAsync(ApiEndPoint))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                filmesViewModels = JsonSerializer.Deserialize<List<FilmesViewModel>>
                    (apiResponse, optionsJson)!;
            }
            else
            {
                return null;
            }
        }
        return filmesViewModels;
    }

    public Task<FilmesViewModel> GetFilmeById()
    {
        throw new NotImplementedException();
    }

    public Task PostFilme(FilmesViewModel filmeVw)
    {
        throw new NotImplementedException();
    }
}