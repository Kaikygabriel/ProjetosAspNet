using System.Text.Json;
using ApiCompras.Filters;
using ApiCompras.Model;
using ApiCompras.Model.DTO;
using ApiCompras.Repository.Interface;
using APICOMPRAS.Pagination;
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
    public async Task<ActionResult> Get()
    {
        var vendas = await unitOfWork.VendaRepository.GetAllAsync();
        var vendasDto = vendas.Adapt<IEnumerable<VendaDto>>();
        return Ok(vendasDto);
    }

        [Authorize]
    [HttpGet("Pagination")]
    public async Task<ActionResult> GetPaginationAsync([FromQuery] ClientePagination pagination)
    {
        if (pagination is null)
            return BadRequest();
        var ListVendas = await unitOfWork.VendaRepository.GetAllPaginationAsync(pagination);
        // if (ListVendas is null)
        //     return NotFound();
        var metadata = new
        {
            ListVendas.HasNext,
            ListVendas.HasPrevius,
            ListVendas.TotalCount,
            ListVendas.TotalPage
        };
        Response.Headers.Append("pagination", JsonSerializer.Serialize(metadata));
        return Ok(ListVendas);
    }
}