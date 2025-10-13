using MediatorX.Core.Abstraction.Interfaces;

namespace LojaApi.Application.UseCases.Category.Commmands.Update;

public class UpdateCategoryRequest : IRequest<bool>
{
    public UpdateCategoryRequest()
    {
        
    }
    public UpdateCategoryRequest(Domain.BackOffice.Entitys.Category category)
    {
        Category = category;
    }

    public Domain.BackOffice.Entitys.Category Category { get; }
}