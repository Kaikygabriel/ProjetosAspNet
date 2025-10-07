using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Data;
using ToDoApi.Entities;
using ToDoApi.Repository.Interface;

namespace ToDoApi.Repository;

public class RepositoryTodo :IRepositoryToDo
{
    private readonly AppDbContext context;

    public RepositoryTodo(AppDbContext context)
    {
        this.context = context;
    }

    public  async Task<IEnumerable<ToDo>> GetAll()
    {
        return await context.Todos.AsNoTracking().ToListAsync();
    }

    public async Task<ToDo?> GetById(int id)
    {
        return await context.Todos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public void Create(ToDo entity)
    {
        if (entity is null)
            throw new Exception();
        context.Todos.Add(entity);
    }

    public void Update(ToDo entity)
    {
        if (entity is null)
            throw new Exception();
        context.Todos.Update(entity);
    }

    public void Delete(ToDo entity)
    {
        if (entity is null)
            throw new Exception();
        context.Todos.Remove(entity);
    }
}