using EduCoreMvc.Models;
using EduCoreMvc.Models.Providers;
using EduCoreMvc.Service;
using Microsoft.AspNetCore.Mvc;

namespace EduCoreMvc.Controllers;


public class ProviderController : Controller
{

    private readonly AuthProviderService _serviceProvider;

    public ProviderController(AuthProviderService serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    [HttpGet]
    public async Task<IActionResult> Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterProviderDto loginProviderDTo)
    {
        if (Request.Cookies.ContainsKey("Token-Auth"))
            return RedirectToAction("Index", "Home");
        
        if (!ModelState.IsValid || loginProviderDTo is null)
            return View(loginProviderDTo);
        
        var result = await _serviceProvider.RegisterProvider(loginProviderDTo);
        if (!result)
            return RedirectToAction("Error", "Home");

        Login(new LoginProviderDto() { Name = loginProviderDTo.Name, Password = loginProviderDTo.Password });
        return RedirectToAction("Index", "Home");
    }
    
    
    [HttpGet]
    public async Task<IActionResult> Login()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginProviderDto loginProviderDTo)
    {
        if (Request.Cookies.ContainsKey("Token-Auth"))
            return RedirectToAction("Index", "Home");
        
        if (!ModelState.IsValid || loginProviderDTo is null)
            return View(loginProviderDTo);
        
        var token = await _serviceProvider.LoginUser(loginProviderDTo);
        Response.Cookies.Append("Token-Auth",token.Token);
        
        return RedirectToAction("Index", "Home");
    }
}