using ApiConsultasMedicas.Data;
using ApiConsultasMedicas.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiConsultasMedicas.Controllers;

[ApiController]
[Route("[controller]")]
public class ConsultasControllers:ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetAsync([FromServices]ApiConsultaContext context)
    {
        return Ok(await context.Consultas.AsNoTracking().ToListAsync());
    }

    [HttpPost]
    public ActionResult Post(Consulta consulta,[FromServices] ApiConsultaContext context)
    {
        context.Consultas.Add(consulta);
        context.SaveChanges();
        return Created();
    }
    
}