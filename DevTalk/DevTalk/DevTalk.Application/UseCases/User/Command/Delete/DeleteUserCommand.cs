using MediatR;

namespace DevTalk.Application.UseCases.User.Command.Delete;

public record DeleteUserCommand(Domain.BackOffice.Entities.User User):IRequest<bool>;