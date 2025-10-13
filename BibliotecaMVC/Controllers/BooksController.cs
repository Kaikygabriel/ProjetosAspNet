using BibliotecaMVC.Services;
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
        bool isLoggedIn = Request.Cookies.ContainsKey("x-acess-token");
        ViewBag.IsLoggedIn = isLoggedIn;
        if (isLoggedIn)
        {
            var name = TokenService.GetNameFromToken(Request.Cookies["x-acess-token"]!.ToString());
            ViewBag.UserName = name;
        }
        return View(await service.GetAllAsync());
    }
     
}