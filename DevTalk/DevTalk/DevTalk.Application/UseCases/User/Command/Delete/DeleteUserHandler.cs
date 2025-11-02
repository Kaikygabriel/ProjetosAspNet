using DevTalk.Domain.BackOffice.Interfaces;
using MediatR;

namespace DevTalk.Application.UseCases.User.Command.Delete;

public class DeleteUserHandler:HandlerBase,IRequestHandler<DeleteUserCommand,bool>  
{
    public DeleteUserHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            UnitOfWork.RepositoryUser.Update(request.User);
            await UnitOfWork.CommitAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}