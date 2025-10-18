using MediatorX.Core.Abstraction.Interfaces;

namespace ProductsApi.Application.UseCases.Product.Query.GetAll;

public record GetAllProductsQuery:IRequest<IEnumerable<Domain.BackOffice.Entitys.Product>>;