using MediatorX.Core.Abstraction.Interfaces;

namespace ProductsApi.Application.UseCases.Product.Command.Delete;

public record DeleteProductCommand(Domain.BackOffice.Entities.Product entity) : IRequest<bool>;