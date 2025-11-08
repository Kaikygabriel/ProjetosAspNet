using Dapper;
using DevTalk.Domain.BackOffice.Interfaces;
using MediatR;
using Microsoft.Data.SqlClient;

namespace DevTalk.Application.UseCases.User.Query.GetById;

public class GetByNameHandler : HandlerBase,IRequestHandler<GetByNameUserQuery,Domain.BackOffice.Entities.User>
{
    public GetByNameHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Domain.BackOffice.Entities.User?> Handle(GetByNameUserQuery request, CancellationToken cancellationToken)
    {
        return await UnitOfWork.RepositoryUser.GetByPredicate(x => x.Name == request.Name);
    }
}