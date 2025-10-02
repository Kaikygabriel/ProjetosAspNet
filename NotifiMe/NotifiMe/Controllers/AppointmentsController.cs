using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using NotifiMe.Extesion;
using NotifiMe.Models;
using NotifiMe.Models.DTO;
using NotifiMe.Repository.Interface;

namespace NotifiMe.Controllers;

[ApiController]
[Route("[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public AppointmentsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    } 

    [Authorize]
    [HttpGet]
    public async Task<ActionResult> GetAllByWork([FromQuery] string work)
    {
        var providers = await _unitOfWork.ProviderRepository.GetAllAsync();
        IEnumerable<Provider>? providerFromQuery = providers.Where(x => x.Work == work);
        return Ok(providerFromQuery!.Adapt<IEnumerable<ProviderDTO>>());
    }

    [Authorize("UserOnly")]
    [HttpPost("create")]
    public async Task<ActionResult> Create([FromBody] RequestCreateAppointmentDTO model)
    {
        var acessUserName = model.userName ?? throw new NullReferenceException(nameof(model.userName));
        var acessProviderName = model.ProviderName ?? throw new NullReferenceException(nameof(model.ProviderName));

        var user = await _unitOfWork.UserRepository.GetByPredicateAsync(x => x.Name == acessUserName);
        var provider = await _unitOfWork.ProviderRepository.GetByPredicateAsync(x => x.Name == acessProviderName);

        if (user is null || provider is null || !user.CheckPassword(model.UserPassword)|| 
            !provider.CheckValidateDate(model.DateAppointment) || !user.CheckValidateDate(model.DateAppointment))
            return NotFound("the components is invalid");

        var newAppointment = new Appointment
        {
            DateAppointment = model.DateAppointment,
            IdUser = user.Id,
            IdProvider = provider.Id,
            IsCanceled = false,
            DateFromCreated = DateTime.Now
        };
        
        user.Appointments.Add(newAppointment);
        provider.Appointments.Add(newAppointment);

        _unitOfWork.AppointmentRepository.Create(newAppointment);
        _unitOfWork.UserRepository.Update(user);
        _unitOfWork.ProviderRepository.Update(provider);
        await _unitOfWork.CommitAsync();

        return Ok(new
        {
            nameUser = user.Name,
            dateAppointment = newAppointment.DateAppointment,
            nameProvider = provider.Name
        });
    }
}