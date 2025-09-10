using ApiCursos.Repository.Interfaces;
using ApiCursosTestXUnit.Mocks;

namespace ApiCursosTestXUnit.CursoControllerTest;

public class CursoUnitTest
{
    public IUnitOfWork repository;

    public CursoUnitTest()
    {
        repository = new FakeUnitOfWork();
    }
}