using MediatorX.Core.Abstraction.Interfaces;

namespace LojaApi.Application.UseCases.Category.Commmands.Create;

public class CreateCategoryRequest : IRequest<bool>
{
    public CreateCategoryRequest()
    {
        
    }
    public CreateCategoryRequest(Domain.BackOffice.Entitys.Category category)
    {
        Category = category;
    }

    public Domain.BackOffice.Entitys.Category Category { get; }
}