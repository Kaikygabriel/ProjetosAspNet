using MediatorX.Core.Abstraction.Interfaces;
using ProductsApi.Domain.BackOffice.Interfaces;

namespace ProductsApi.Application.UseCases.User.Command.Create;

public class CreateUserHandler:HandlerBase,IHandler<CreateUserCommand,bool>
{
    public CreateUserHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<bool> HandleAsync(CreateUserCommand request, CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            UnitOfWork.RepositoryUser.Create(request.User);
            await UnitOfWork.CommitAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}