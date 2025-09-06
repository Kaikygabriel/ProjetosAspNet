using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoApi.Controllers;

[ApiController]
[Route("api/teste")]
[ApiVersion("1.0")]
public class Testev1Controller  : ControllerBase
{
    [HttpGet]
    public ActionResult GetVersion()
    {
        return Ok("Version 1");
    }
}