using System.Linq.Expressions;
using EduCore.Domain.Entities;
using EduCore.Domain.Interfaces;
using EduCore.Domain.ValueObjects;

namespace EduCore.Test.Mocks;

public class MockStudentRepository : IRepositoryStudent 
{
    private readonly List<Student> _students = new();

    public MockStudentRepository()
    {
        _students.Add(new Student(
            new User { Name = "Gabriel", 
                PasswordHash= BCrypt.Net.BCrypt.HashPassword("senhaSegura2") },
            new Email { Adress = "gabriel@example.com" }
        ));
            
        _students.Add(new Student(
            new User { Name = "Maria", 
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("senhaSegura") },
            new Email { Adress= "maria@example.com" }
        ));
    }

    public async Task<IEnumerable<Student>> GetAllAsync()
    {
        await Task.Delay(0);
        return _students;
    }

    public async Task<Student?> GetByPredicateAsync(Expression<Func<Student, bool>> predicate)
    {
        await Task.Delay(0);
        return _students.AsQueryable().FirstOrDefault(predicate);
    }

    public void Create(Student entity)
    {
        _students.Add(entity);
    }

    public void Update(Student entity)
    {
        _students.Add(entity);
    }

    public void Delete(Student entity)
    {
        _students.Remove(entity);
    }
}