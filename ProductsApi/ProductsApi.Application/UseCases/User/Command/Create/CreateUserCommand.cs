using MediatorX.Core.Abstraction.Interfaces;

namespace ProductsApi.Application.UseCases.User.Command.Create;

public record CreateUserCommand(Domain.BackOffice.Entitys.User User) : IRequest<bool>;