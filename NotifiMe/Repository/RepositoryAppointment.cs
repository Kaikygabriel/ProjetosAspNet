using NotifiMe.Data;
using NotifiMe.Models;
using NotifiMe.Repository.Interface;

namespace NotifiMe.Repository;

public class RepositoryAppointment(AppDbContext context): Repository<Appointment>(context),IAppointmentRepository;