using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EduCoreMvc.Models;

namespace EduCoreMvc.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        if (!Request.Cookies.ContainsKey("Token-Auth"))
            ViewBag.Login = false;
        ViewBag.Login = true;
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
