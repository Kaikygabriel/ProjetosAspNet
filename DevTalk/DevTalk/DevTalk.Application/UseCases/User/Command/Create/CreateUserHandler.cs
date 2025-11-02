using DevTalk.Domain.BackOffice.Interfaces;
using MediatR;

namespace DevTalk.Application.UseCases.User.Command.Create;

public class CreateUserHandler : HandlerBase,IRequestHandler<CreateUserCommand,bool>
{
    public CreateUserHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
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