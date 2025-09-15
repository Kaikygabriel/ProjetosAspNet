using Catalogo.Domain.Entities;
using Catalogo.Domain.Interfaces;
using Catalogo.Infratructure.Context;

namespace Catalogo.Infratructure.Repositorys;

public class RepositoryProduto(AppDbContext context) : Repository<Produto>(context), IRepositoryProduto;