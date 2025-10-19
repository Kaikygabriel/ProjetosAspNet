using MediatorX.Core.Abstraction.Interfaces;

namespace ProductsApi.Application.UseCases.User.Command.Delete;

public record DeleteUserCommand(Domain.BackOffice.Entitys.User User): IRequest<bool>;