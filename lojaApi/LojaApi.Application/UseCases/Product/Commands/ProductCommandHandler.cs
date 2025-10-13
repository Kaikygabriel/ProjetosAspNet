using LojaApi.Application.UseCases.Product.Commands.Create;
using LojaApi.Application.UseCases.Product.Commands.Delete;
using LojaApi.Application.UseCases.Product.Commands.Update;
using LojaApi.Domain.BackOffice.Interfaces;
using MediatorX.Core.Abstraction.Interfaces;

namespace LojaApi.Application.UseCases.Product.Commands;

public class ProductCommandHandler(IUnitOfWork unitOf):
    IHandler<CreateProductRequest, bool>,
    IHandler<UpdateProductRequest,bool>,
    IHandler<DeleteProductRequest , bool>
{
    public async Task<bool> HandleAsync(CreateProductRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            unitOf.RepositoryProduct.Create(request.Product);
            await unitOf.CommitAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<bool> HandleAsync(UpdateProductRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            unitOf.RepositoryProduct.Update(request.Product);
            await unitOf.CommitAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<bool> HandleAsync(DeleteProductRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            unitOf.RepositoryProduct.Delete(request.Product);
            await unitOf.CommitAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}