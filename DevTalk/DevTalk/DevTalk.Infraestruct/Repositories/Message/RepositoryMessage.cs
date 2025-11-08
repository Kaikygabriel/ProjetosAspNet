using DevTalk.Domain.BackOffice.Interfaces.Message;
using DevTalk.Infraestruct.Data.Context;

namespace DevTalk.Infraestruct.Repositories.Message;

public class RepositoryMessage : Repository<Domain.BackOffice.Entities.Message>, IRepositoryMessage
{
    public RepositoryMessage(AppDbContext context) : base(context)
    {
    }
}