using MediatR;

namespace DevTalk.Application.UseCases.User.Query.GetById;

public record GetByNameUserQuery(String Name) : IRequest<Domain.BackOffice.Entities.User>;