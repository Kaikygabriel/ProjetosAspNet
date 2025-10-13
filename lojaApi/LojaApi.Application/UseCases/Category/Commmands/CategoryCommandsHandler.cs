using LojaApi.Application.UseCases.Category.Commmands.Create;
using LojaApi.Application.UseCases.Category.Commmands.Delete;
using LojaApi.Application.UseCases.Category.Commmands.Update;
using LojaApi.Domain.BackOffice.Interfaces;
using MediatorX.Core.Abstraction.Interfaces;

namespace LojaApi.Application.UseCases.Category.Commmands;

public class CategoryCommandsHandler(IUnitOfWork unitOf) :
    IHandler<CreateCategoryRequest , bool>,
    IHandler<UpdateCategoryRequest , bool>,
    IHandler<DeleteCategoryRequest , bool>
{
    public async Task<bool> HandleAsync(CreateCategoryRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            unitOf.RepositoryCategory.Create(request.Category);
            await unitOf.CommitAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<bool> HandleAsync(UpdateCategoryRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            unitOf.RepositoryCategory.Update(request.Category);
            await unitOf.CommitAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<bool> HandleAsync(DeleteCategoryRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            unitOf.RepositoryCategory.Delete(request.Category);
            await unitOf.CommitAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}