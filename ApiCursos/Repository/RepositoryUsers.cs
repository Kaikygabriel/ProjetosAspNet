using ApiCursos.Model;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace ApiCursos.Repository;


public class RepositoryUsers
{
    public List<LoginModel> users = new List<LoginModel>()
    {
        new LoginModel(){Name = "kaiky",Password = "123"}
    };
    public LoginModel? Get(string name)
    {
        return users.FirstOrDefault(x => x.Name == name); 
    }
}