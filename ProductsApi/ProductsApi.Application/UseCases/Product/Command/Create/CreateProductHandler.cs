using MediatorX.Core.Abstraction.Interfaces;
using ProductsApi.Domain.BackOffice.Interfaces;
using ProductsApi.Domain.BackOffice.ObjectValue;

namespace ProductsApi.Application.UseCases.Product.Command.Create;

public class CreateProductHandler:HandlerBase,IHandler<CreateProductCommand,bool>
{

    public CreateProductHandler(IUnitOfWork unitOfWork) 
    : base(unitOfWork){ }

    public async Task<bool> HandleAsync(CreateProductCommand request,
                                  CancellationToken cancellationToken = default)
    {
        try
        {
            var product = new Domain.BackOffice.Entitys.Product
                (request.Price, new Category(request.Category), request.Name);
            UnitOfWork.RepositoryProduct.Create(product);
            await UnitOfWork.CommitAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
       
    }
}