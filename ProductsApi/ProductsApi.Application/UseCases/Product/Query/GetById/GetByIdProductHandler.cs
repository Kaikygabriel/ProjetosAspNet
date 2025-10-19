using MediatorX.Core.Abstraction.Interfaces;
using ProductsApi.Domain.BackOffice.Interfaces;

namespace ProductsApi.Application.UseCases.Product.Query.GetById;

public class GetByIdProductHandler :HandlerBase,  IHandler<GetByIdQuery,Domain.BackOffice.Entitys.Product>
{
    public GetByIdProductHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    public async Task<Domain.BackOffice.Entitys.Product?> HandleAsync
        (GetByIdQuery request, CancellationToken cancellationToken = default)
    {
        try
        {
            return await UnitOfWork.RepositoryProduct.GetByPredicate(x => x.Id == request.Id);
        }
        catch (Exception e)
        {
            return null;
        }
    }
}