using Catalogo.Domain.Entities;
using Catalogo.Domain.Interfaces;
using Catalogo.Infratructure.Context;

namespace Catalogo.Infratructure.Repositorys;

public class RepositoryCategoria(AppDbContext context) : Repository<Categoria>(context),IRepositoryCategoria;