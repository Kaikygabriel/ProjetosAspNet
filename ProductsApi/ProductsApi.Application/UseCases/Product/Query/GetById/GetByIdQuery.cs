using MediatorX.Core.Abstraction.Interfaces;

namespace ProductsApi.Application.UseCases.Product.Query.GetById;

public record GetByIdQuery(int Id): IRequest<Domain.BackOffice.Entities.Product>;