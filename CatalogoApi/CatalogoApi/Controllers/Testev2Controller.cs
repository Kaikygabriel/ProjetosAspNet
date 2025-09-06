using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoApi.Controllers;

[ApiController]
[Route("api/teste")]
[ApiVersion("2.0")]
public class Testev2Controller  : ControllerBase
{
    [HttpGet]
    public ActionResult GetVersion()
    {
        return Ok("Version 2");
    }
}