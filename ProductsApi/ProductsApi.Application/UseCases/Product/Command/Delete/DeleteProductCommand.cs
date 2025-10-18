using MediatorX.Core.Abstraction.Interfaces;

namespace ProductsApi.Application.UseCases.Product.Command.Delete;

public record DeleteProductCommand(Domain.BackOffice.Entitys.Product entity) : IRequest<bool>;