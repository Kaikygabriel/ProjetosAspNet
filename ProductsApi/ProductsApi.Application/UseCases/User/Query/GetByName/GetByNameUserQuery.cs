using MediatorX.Core.Abstraction.Interfaces;

namespace ProductsApi.Application.UseCases.User.Query.GetByName;

public record GetByNameUserQuery(string Name) : IRequest<Domain.BackOffice.Entitys.User>;