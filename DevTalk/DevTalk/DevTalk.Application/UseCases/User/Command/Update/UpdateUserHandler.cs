using DevTalk.Domain.BackOffice.Interfaces;
using MediatR;

namespace DevTalk.Application.UseCases.User.Command.Update;

public class UpdateUserHandler:HandlerBase,IRequestHandler<UpdateUserCommand,bool>  
{
    public UpdateUserHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
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