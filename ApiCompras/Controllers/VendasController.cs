using ApiCompras.Filters;
using ApiCompras.Model;
using ApiCompras.Model.DTO;
using ApiCompras.Repository.Interface;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCompras.Controllers;

[ApiController]
[Route("[controller]")]
public class VendasController : ControllerBase
{
    private readonly IUnitOfWork unitOfWork;
    public VendasController(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get()
    {
        var vendas = await unitOfWork.VendaRepository.GetAllAsync();
        var vendasDto = vendas.Adapt<IEnumerable<VendaDto>>();
        return Ok(vendasDto);
    }
}