using CategoriasMvc.Models;

namespace CategoriasMvc.Services.Interfaces;

public interface IFilmeService
{
    Task<List<FilmesViewModel>> GetFilmes();
    Task<FilmesViewModel> GetFilmeById(int id);
    Task<FilmesViewModel> PostFilme(FilmesViewModel filmeVw);
}