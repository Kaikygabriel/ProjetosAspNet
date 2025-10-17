using EduCoreMvc.Models;
using EduCoreMvc.Models.Providers;
using EduCoreMvc.Service;
using Microsoft.AspNetCore.Mvc;

namespace EduCoreMvc.Controllers;


public class ProviderController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly AuthProviderService _serviceProvider;

    public ProviderController(AuthProviderService serviceProvider, IConfiguration configuration)
    {
        _serviceProvider = serviceProvider;
        _configuration = configuration;
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

        Login(new LoginProviderDto()
        {
            Name = loginProviderDTo.Name,
            Password = loginProviderDTo.Password
        });
        
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
        
        if(token is null)
            return View(loginProviderDTo);

        Response.Cookies.Append("Token-Auth",token.token,new CookieOptions()
        {
            Secure = true,
            SameSite = SameSiteMode.Strict,
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(3)
        });
        var claims = TokenService.GetClaimsFromToken(token.token,_configuration);
        Response.Cookies.Append("UserName",claims.Identity!.Name!,new CookieOptions()
        {
            Secure = true,
            SameSite = SameSiteMode.Strict,
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(3)
        });
        
        return RedirectToAction("Index", "Home");
    }
    
    public IActionResult Logout()
    {
        if(!Request.Cookies.ContainsKey("Token-Auth"))
            Response.Cookies.Delete("UserName");    
        if(!Request.Cookies.ContainsKey("UserName"))
            Response.Cookies.Delete("Token-Auth");
        // Remove os cookies
        Response.Cookies.Delete("Token-Auth");
        Response.Cookies.Delete("UserName");
        
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Index()
    {
        if(!Request.Cookies.ContainsKey("Token-Auth") || !Request.Cookies.ContainsKey("UserName"))
            return RedirectToAction("Index", "Home");
        var token = Request.Cookies["Token-Auth"];
        var claims = TokenService.GetClaimsFromToken(token!,_configuration);
        var provider = new ProviderView() { Name = claims.Identity!.Name! };

        return View(provider);
    }
}