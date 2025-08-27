using NotifiMe.Data;
using NotifiMe.Models;
using NotifiMe.Repository.Interface;

namespace NotifiMe.Repository;

public class RepositoryUser(AppDbContext context): Repository<User>(context),IUserRepository;