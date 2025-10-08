using EduCore.Domain.Exceptions;
using EduCore.Domain.ValueObjects;

namespace EduCore.Domain.Entities;

public class Provider : Entity
{
    public Provider(User user, Email email)
    {
        if (user is null || email is null)
            throw new ProviderException("Arguments is null in provider.");
        User = user;
        Email = email;
    }

    public Provider() {}
    public User User { get; set; }
    public Email Email { get; set; }
    public List<Course> Courses { get;private set; } = new();

    public void AddCourse(Course course)
        => Courses.Add(course);
}