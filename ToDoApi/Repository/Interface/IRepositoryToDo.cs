using ToDoApi.Entities;

namespace ToDoApi.Repository.Interface;

public interface IRepositoryToDo
{
    Task<IEnumerable<ToDo>> GetAll();
    Task<ToDo> GetById(int id);
    void Create(ToDo entity);
    void Update(ToDo entity);
    void Delete(ToDo entity);
}