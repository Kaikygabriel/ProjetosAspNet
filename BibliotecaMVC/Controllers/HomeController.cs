using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BibliotecaMVC.Models;
using BibliotecaMVC.Services;

namespace BibliotecaMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        bool isLoggedIn = Request.Cookies.ContainsKey("x-acess-token");
        ViewBag.IsLoggedIn = isLoggedIn;
        if (isLoggedIn)
        {
            var name = TokenService.GetNameFromToken(Request.Cookies["x-acess-token"]!.ToString());
            ViewBag.UserName = name;

        }
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
