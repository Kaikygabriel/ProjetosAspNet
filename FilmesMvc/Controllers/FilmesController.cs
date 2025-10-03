using CategoriasMvc.Models;
using CategoriasMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CategoriasMvc.Controllers;

[Route("[controller]")]
public class FilmesController : Controller
{
    private readonly IFilmeService _filmeService;

    public FilmesController(IFilmeService filmeService)
    {
        _filmeService = filmeService;
    }

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var filmes = await _filmeService.GetFilmes();
        if (filmes is null)
            return View("Error");
        return View(filmes);
    }

    [HttpGet("Create")]
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost("Create")]
    public async Task<ActionResult> Create(FilmesViewModel model)
    {
        if(!ModelState.IsValid)
        {
            ViewBag.Erro = "Error in create movie.";
            return View(model);
        }
        var request = await _filmeService.PostFilme(model);
        return RedirectToAction(nameof(Index));
    }
}