using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;

namespace DevTalk.Application.UseCases.Message.Command.Create;

public class CreateMessageHandler : IRequestHandler<CreateMessageCommand,bool>
{
    public async Task<bool> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {

        try
        {
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
        
    }
}