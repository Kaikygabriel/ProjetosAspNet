using DevTalk.Application.Pagination;
using MediatR;

namespace DevTalk.Application.UseCases.Message.Query.GetAll;

public record GetAllMessageQuery (QueryStringParameters Parameters) 
    : IRequest<IEnumerable<Domain.BackOffice.Entities.Message>>;