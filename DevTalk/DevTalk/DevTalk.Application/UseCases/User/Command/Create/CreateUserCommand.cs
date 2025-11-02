using MediatR;

namespace DevTalk.Application.UseCases.User.Command.Create;

public record CreateUserCommand(Domain.BackOffice.Entities.User User):  IRequest<bool>;