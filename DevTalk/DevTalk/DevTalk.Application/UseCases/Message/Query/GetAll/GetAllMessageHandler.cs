using Dapper;
using DevTalk.Domain.BackOffice.Interfaces;
using MediatR;
using Microsoft.Data.SqlClient;

namespace DevTalk.Application.UseCases.Message.Query.GetAll;

public class GetAllMessageHandler :
    HandlerBase,
    IRequestHandler<GetAllMessageQuery,IEnumerable<Domain.BackOffice.Entities.Message>>
{
    public GetAllMessageHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<IEnumerable<Domain.BackOffice.Entities.Message>> Handle(GetAllMessageQuery request, CancellationToken cancellationToken)
    {
        var connection = new SqlConnection( "Server=localhost;Database=DevTalk;Trusted_Connection=True;TrustServerCertificate=True;");
        return await connection.QueryAsync<Domain.BackOffice.Entities.Message>
            ("SELECT * FROM [Messages]");
    }
}