using MediatorX.Core.Abstraction.Interfaces;
using ProductsApi.Application.UseCases.Product.Command.Create;
using ProductsApi.Domain.BackOffice.Interfaces;

namespace ProductsApi.Application.UseCases.Product.Command.Delete;

public class DeleteProductHandler:HandlerBase, IHandler<DeleteProductCommand,bool>
{
    public DeleteProductHandler(IUnitOfWork unitOfWork)
    :base(unitOfWork){ }
    
    public async Task<bool> HandleAsync(DeleteProductCommand request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            UnitOfWork.RepositoryProduct.Delete(request.entity);
            await UnitOfWork.CommitAsync(); 
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}