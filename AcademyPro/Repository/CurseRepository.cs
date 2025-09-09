using AcademyPro.Data;
using AcademyPro.Models;
using AcademyPro.Repository.Interfaces;

namespace AcademyPro.Repository;

public class CurseRepository(AppDbContext context) : Repository<Curse>(context) , ICurseRepository;