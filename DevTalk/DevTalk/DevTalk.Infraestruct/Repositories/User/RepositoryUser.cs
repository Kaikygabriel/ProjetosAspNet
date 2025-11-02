using DevTalk.Domain.BackOffice.Interfaces;
using DevTalk.Domain.BackOffice.Interfaces.User;
using DevTalk.Infraestruct.Data.Context;

namespace DevTalk.Infraestruct.Repositories.User;

public class RepositoryUser:Repository<Domain.BackOffice.Entities.User>,IRepositoryUser
{
    public RepositoryUser(AppDbContext context) : base(context)
    {
    }
}