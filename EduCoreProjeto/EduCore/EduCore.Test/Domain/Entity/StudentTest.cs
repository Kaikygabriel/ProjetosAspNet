using EduCore.Domain.Entities;
using EduCore.Domain.Exceptions;

namespace EduCore.Test.Domain.Entity;

public class StudentTest
{
    [Fact]
    public void CreateStudentWithParametersNull_Return_StudentException()
    {
        Assert.Throws<StudentException>(() =>
        {
            new Student(null, null);
        });
    }
}