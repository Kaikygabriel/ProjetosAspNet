using Filmes.Domain.Entities;
using Filmes.Domain.Interfaces;
using Filmes.Infraestruture.Data;

namespace Filmes.Infraestruture.Repository;

public class RepositoryFilme(AppDbContext context) : Repository<Filme>(context),IRepositoryFilme
{
    
}