using MediatorX.Core.Abstraction.Interfaces;
using ProductsApi.Domain.BackOffice.Interfaces;

namespace ProductsApi.Application.UseCases.User.Command.Delete;

public class DeleteUserHandler : HandlerBase,IHandler<DeleteUserCommand,bool>
{
    public DeleteUserHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<bool> HandleAsync(DeleteUserCommand request, CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            UnitOfWork.RepositoryUser.Delete(request.User);
            await UnitOfWork.CommitAsync(); 
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}