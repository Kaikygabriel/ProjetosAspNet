using MediatR;

namespace DevTalk.Application.UseCases.User.Command.Update;

public record UpdateUserCommand(Domain.BackOffice.Entities.User User):IRequest<bool>;