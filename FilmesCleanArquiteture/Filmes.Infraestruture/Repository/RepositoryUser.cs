using Filmes.Domain.Entities;
using Filmes.Domain.Interfaces;
using Filmes.Infraestruture.Data;

namespace Filmes.Infraestruture.Repository;

public class RepositoryUser(AppDbContext context) : Repository<User>(context),IRepositoryUser 
{
        public override void Create(User entity)
        { 
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(entity.PasswordHash);
            entity.PasswordHash = hashPassword;
            base.Create(entity);
        }
}