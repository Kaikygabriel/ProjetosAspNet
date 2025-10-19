using MediatorX.Core.Abstraction.Interfaces;
using ProductsApi.Domain.BackOffice.Interfaces;

namespace ProductsApi.Application.UseCases.User.Query.GetByName;

public class GetByNameUserHandler : HandlerBase,
    IHandler<GetByNameUserQuery,Domain.BackOffice.Entitys.User>
{
    public GetByNameUserHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Domain.BackOffice.Entitys.User?> HandleAsync(GetByNameUserQuery request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var user = await UnitOfWork.RepositoryUser.
                GetByPredicate(x => x.Name == request.Name);
            return user;
        }
        catch (Exception e)
        {
            return null;
        }
    }
}