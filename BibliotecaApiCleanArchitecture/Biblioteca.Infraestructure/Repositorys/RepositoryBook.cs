using Biblioteca.Domain.BackOffice.Entities;
using Biblioteca.Domain.BackOffice.Interfaces;
using Biblioteca.Infraestructure.Context;

namespace Biblioteca.Infraestructure.Repositorys;

public class RepositoryBook(AppDbContext context) : Repository<Book>(context),IRepositoryBook;