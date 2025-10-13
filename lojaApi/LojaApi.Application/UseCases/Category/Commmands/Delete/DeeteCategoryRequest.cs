using MediatorX.Core.Abstraction.Interfaces;

namespace LojaApi.Application.UseCases.Category.Commmands.Delete;

public class DeleteCategoryRequest : IRequest<bool>
{
    public DeleteCategoryRequest()
    {
        
    }
    public DeleteCategoryRequest(Domain.BackOffice.Entitys.Category category)
    {
        Category = category;
    }

    public Domain.BackOffice.Entitys.Category Category { get; }
}