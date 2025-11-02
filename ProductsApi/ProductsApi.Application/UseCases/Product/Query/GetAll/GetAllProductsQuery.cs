using MediatorX.Core.Abstraction.Interfaces;
using ProductsApi.Domain.BackOffice.ObjectValue;

namespace ProductsApi.Application.UseCases.Product.Query.GetAll;

public record GetAllProductsQuery(QueryStringParameters Parameters):
    IRequest<IEnumerable<Domain.BackOffice.Entities.Product>>;