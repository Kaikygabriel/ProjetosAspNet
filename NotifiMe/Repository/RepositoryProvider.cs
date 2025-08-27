using NotifiMe.Data;
using NotifiMe.Models;
using NotifiMe.Repository.Interface;

namespace NotifiMe.Repository;
public class RepositoryProvider(AppDbContext context) : Repository<Provider>(context), IProviderRepository;