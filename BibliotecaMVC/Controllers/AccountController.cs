using BibliotecaMVC.Models;
using BibliotecaMVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaMVC.Controllers;

public class AccountController : Controller
{
    private readonly IAuthenticationClientHttp _authentication;

    public AccountController(IAuthenticationClientHttp authentication)
    {
        _authentication = authentication;
    }

    [HttpGet("Register")]
    public ActionResult Register()
    {
        return View();
    }
    [HttpPost("Register")]
    public async Task<ActionResult> Register(UserLoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        var authentication = await _authentication.AuthenticationAsync(model);
        if(!authentication)
            return RedirectToAction("Error", "Home");
        
        return RedirectToAction("Index", "Home"); 
    }
}