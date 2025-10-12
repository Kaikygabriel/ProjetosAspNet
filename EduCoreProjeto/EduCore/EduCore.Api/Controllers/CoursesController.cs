using EduCore.Application.Course.Commands.Create;
using EduCore.Application.Course.Query.All;
using EduCore.Application.Course.Query.GetByTitle;
using EduCore.Application.DTOS.Course;
using EduCore.Application.Interfaces;
using EduCore.Domain.Entities;
using Mapster;
using MediatorX.Core.Abstraction.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduCore.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CoursesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CoursesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [Authorize]
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        return Ok(await _mediator.SendAsync(new QueryAllCourses()));
    }
    
    [Authorize]
    [HttpGet("{tile:alpha:min(2)}")]
    public async Task<ActionResult> GetById(string title)
    {
        Course? course = await _mediator.SendAsync(new QueryByTitleCourse(title));
        if (course is null)
            return NotFound();
        return Ok(course);
    }
    
    [Authorize("ProviderOnly")]
    [HttpPost]
    public async Task<ActionResult> PostAsync([FromServices]ICourseServiceCache _cacheCourse,
                                                CreateCourseDTO courseDto)
    {
        var provider = await _cacheCourse.Unit.RepositoryProvider.
            GetByPredicateAsync(x => x.Id == courseDto.ProviderId);
        if (provider is null)
            return NotFound();
        Course course = courseDto.Adapt<Course>();
        var result = await _mediator.SendAsync(new CreateCourseCommand(course));
        if (!result)
            return BadRequest();
        return Created();
    }

}