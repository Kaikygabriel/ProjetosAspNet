using MediatorX.Core.Abstraction.Interfaces;
using ProductsApi.Domain.BackOffice.Interfaces;

namespace ProductsApi.Application.UseCases.Product.Query.GetAll;

public class GetAllProductHandler : 
    HandlerBase,
    IHandler<GetAllProductsQuery,IEnumerable<Domain.BackOffice.Entities.Product>>
{
    public GetAllProductHandler(IUnitOfWork unitOfWork) :
            base(unitOfWork) { }
    
    public async Task<IEnumerable<Domain.BackOffice.Entities.Product>?> HandleAsync
        (GetAllProductsQuery request, CancellationToken cancellationToken = default)
        => await UnitOfWork.RepositoryProduct.GetAll(request.Parameters);
    
}