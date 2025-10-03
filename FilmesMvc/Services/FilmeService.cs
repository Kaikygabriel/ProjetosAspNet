using System.Text;
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
    private FilmesViewModel filmeByid;
    public FilmeService(IHttpClientFactory clientFactory)
    {
        optionsJson = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
        this.clientFactory =clientFactory;
    }

    public async Task<List<FilmesViewModel>> GetFilmes()
    {
        var client = clientFactory.CreateClient("FilmesApi");
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

    public async Task<FilmesViewModel> GetFilmeById(int id)
    {
        var cliente = clientFactory.CreateClient("FilmesApi");
        using (var response = await cliente.GetAsync(ApiEndPoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                filmeByid = JsonSerializer.Deserialize<FilmesViewModel>(apiResponse)!;
            }
            else
                return null;
        }
        return filmeByid;
    }

    public async Task<FilmesViewModel> PostFilme(FilmesViewModel filmeVw)
    {
        var cliente = clientFactory.CreateClient("FilmesApi");
        var filmeJson = JsonSerializer.Serialize(filmeVw);
        StringContent content = new StringContent(filmeJson, Encoding.UTF8, "Application/Json");
        using (var response = await cliente.PostAsync(ApiEndPoint,content))
        {
            if (!response.IsSuccessStatusCode)
                return null;
        }

        return filmeVw;
    }
}