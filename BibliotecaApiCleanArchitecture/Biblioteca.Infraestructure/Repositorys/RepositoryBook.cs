using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Infraestructure.Context;

namespace Biblioteca.Infraestructure.Repositoroys;

public class RepositoryBook(AppDbContext context) : Repository<Book>(context),IRepositoryBook;