using Biblioteca.Application.DTOS;
using Biblioteca.Application.Interfaces;
using Biblioteca.Domain.BackOffice.Entities;
using Biblioteca.Domain.BackOffice.Interfaces;
using Biblioteca.Infraestructure.Repositorys;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public BooksController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    public async Task<ActionResult> GetAllAsync()
    {
        return Ok(await _unitOfWork.RepositoryBook.GetAll());
    }
    [HttpGet("{id:int:min(1)}")]
    public async Task<ActionResult>GetByidAsync([FromRoute]int id)
    {
        var book = await _unitOfWork.RepositoryBook.GetByPredicate(x => x.Id == id);
        if (book is null)
            return NotFound();
        return Ok(book);
    }

    [HttpPost]
    public async Task<ActionResult> PostAsync([FromBody] BookCreateDTO model)
    {
        if (model is null)
            return BadRequest();
        var bookExist = await _unitOfWork.RepositoryBook.GetByPredicate(x => x.Title == model.Title);
        if (bookExist is not null)
            return NotFound();
        var bookCreate = model.Adapt<Book>();
        _unitOfWork.RepositoryBook.Create(bookCreate);
        await _unitOfWork.CommitAsync();

        return Created();
    }
}