using System.Linq.Expressions;
using Filmes.Domain.Entities;
using Filmes.Domain.Interfaces;

namespace FilmesApiTestXUnit.Mocks;

public class FakeRepositoryFilmes : IRepositoryFilme
{
    List<Filme> filmes = new List<Filme>
    {
        new Filme { Id = 1, Titulo = "A Origem", Autor = "Christopher Nolan", Categoria = "Ficção Científica" },
        new Filme { Id = 2, Titulo = "O Poderoso Chefão", Autor = "Francis Ford Coppola", Categoria = "Drama" },
        new Filme { Id = 3, Titulo = "Interestelar", Autor = "Christopher Nolan", Categoria = "Aventura" },
        new Filme { Id = 4, Titulo = "Parasita", Autor = "Bong Joon-ho", Categoria = "Suspense" },
        new Filme { Id = 5, Titulo = "Vingadores: Ultimato", Autor = "Anthony e Joe Russo", Categoria = "Ação" }
    };
    public async Task<IEnumerable<Filme>> GetAll(CancellationToken cancellationToken)
    {
        await Task.Delay(0);
        return filmes;
    }

    public async Task<Filme?> GetByPredicate(Expression<Func<Filme, bool>> predicate, CancellationToken cancellationToken)
    {
        await Task.Delay(0);
        return filmes.AsQueryable().FirstOrDefault(predicate);
    }

    public void Create(Filme entity)
    {
        filmes.Add(entity);
    }

    public void Update(Filme entity)
    {
        filmes.Add(entity);
    }

    public void Delete(Filme entity)
    {
        filmes.Remove(entity);
    }
}