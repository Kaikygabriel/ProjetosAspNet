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
    public async Task<ActionResult> Register(UserRegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        var authentication = await _authentication.AuthenticationRegisterAsync(model);
        if(!authentication)
            return RedirectToAction("Error", "Home");
        
        return RedirectToAction("Index", "Home"); 
    }
    [HttpGet("Login")]
    public ActionResult Login()
    {
        return View();
    }
    [HttpPost("Login")]
    public async Task<ActionResult> Login(UserLoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);
        
        
        
        
        TokenViewModel? token = await _authentication.AuthenticationLoginAsync(model);
        if(token is null)
            return RedirectToAction("Error", "Home");

        Response.Cookies.Append("x-acess-token",token.Token!,new CookieOptions()
        {
            Secure = true,
            HttpOnly = true,
            SameSite = SameSiteMode.Strict
        });
        return Redirect("/");
    }
}