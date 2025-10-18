using MediatorX.Core.Abstraction.Interfaces;

namespace ProductsApi.Application.UseCases.Product.Command.Create;

public record CreateProductCommand(string Name,string Category,decimal Price): IRequest<bool>;