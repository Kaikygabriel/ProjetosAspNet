using System.Linq.Expressions;
using Biblioteca.Domain.BackOffice.Entities;
using Biblioteca.Domain.BackOffice.Interfaces;

namespace Biblioteca.TestUnit.Mocks;

public class FakeRepositoryBook : IRepositoryBook
{
    private List<Book> Books = new()
    {
        new Book
        {
            Id = 1,
            Title = "Introdução ao C# e .NET",
            Author = new Author { Name = "José Silva" },
            Price = 99.90m
        },
        new Book
        {Id = 2,
            Title = "Arquitetura Limpa com ASP.NET Core",
            Author = new Author { Name = "Maria Oliveira" },
            Price = 149.50m
        },
        new Book
        {Id = 3,
            Title = "Banco de Dados com Entity Framework",
            Author = new Author { Name = "Carlos Souza" },
            Price = 79.99m
        },
        new Book
        {Id = 4,
            Title = "Testes Automatizados com xUnit",
            Author = new Author { Name = "Fernanda Lima" },
            Price = 59.90m
        }
    };
    public async Task<IEnumerable<Book>> GetAll()
    {
        await Task.Delay(0);
        return Books;
    }

    public async Task<Book?> GetByPredicate(Expression<Func<Book?, bool>> predicate)
    {
        await Task.Delay(0);
        return Books.AsQueryable().FirstOrDefault(predicate);
    }

    public void Create(Book entity)
    {
        Books.Add(entity);
    }

    public void Update(Book entity)
    {
        Books.Remove(entity);
        Books.Add(entity);
    }

    public void Delete(Book entity)
    {
        Books.Remove(entity);
    }
}