using CategoriasMvc.Models;

namespace CategoriasMvc.Services.Interfaces;

public interface IFilmeService
{
    Task<List<FilmesViewModel>> GetFilmes();
    Task<FilmesViewModel> GetFilmeById();
    Task PostFilme(FilmesViewModel filmeVw);
}