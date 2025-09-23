using Catalogo.Domain.Entities;
using Catalogo.Domain.Interfaces;
using Catalogo.Infratructure.Context;

namespace Catalogo.Infratructure.Repositorys;

public class RepositoryUser(AppDbContext context)  : Repository<User>(context),IRepositoryUser;