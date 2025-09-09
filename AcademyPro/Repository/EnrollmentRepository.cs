using AcademyPro.Data;
using AcademyPro.Models;
using AcademyPro.Repository.Interfaces;

namespace AcademyPro.Repository;


public class EnrollmentRepository(AppDbContext context) :  Repository<Enrollment>(context) , IEnrollmentRepository;