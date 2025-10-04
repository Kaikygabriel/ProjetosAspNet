using BibliotecaMVC.Models;

namespace BibliotecaMVC.Services.Interfaces;

public interface IServiceClientHttpBook
{
    Task<IEnumerable<BookViewModel>> GetAllAsync();
}