using BibliotecaMVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaMVC.Controllers;

public class BooksController : Controller
{
    private readonly IServiceClientHttpBook service;

    public BooksController(IServiceClientHttpBook service)
    {
        this.service = service;
    }

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        return View(await service.GetAllAsync());
    }
     
}