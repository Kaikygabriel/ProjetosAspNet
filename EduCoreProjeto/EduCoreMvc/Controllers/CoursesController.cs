using EduCoreMvc.Models;
using EduCoreMvc.Models.Course;
using EduCoreMvc.Service;
using Microsoft.AspNetCore.Mvc;

namespace EduCoreMvc.Controllers;

[Route("[controller]")]
public class CoursesController : Controller
{

    private readonly CourseService _courseService;

    public CoursesController(CourseService courseService)
    {
        _courseService = courseService;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        if (Request.Cookies["Token-Auth"] is null)
            return RedirectToAction("Index", "Home");
        var token = Request.Cookies["Token-Auth"];
        var courses = await _courseService.GetCourses(token!);
        if(courses is null)
            return RedirectToAction("Index", "Home");
        return View(courses);
    }

    [HttpGet("Create")]
    public IActionResult Create()
    {
        if (Request.Cookies["Token-Auth"] is null)
            return RedirectToAction("Index", "Home");
        return View();
    }
    [HttpPost("Create")]
    public async Task<IActionResult> Create(CourseCreate model)
    {
        if (Request.Cookies["Token-Auth"] is null)
            return RedirectToAction("Index", "Home");
        var token = Request.Cookies["Token-Auth"]; 
        var result = await _courseService.Create(model,token!);
        if (!result)
            return View();
        
        return RedirectToAction("Index", "Courses");
    }
}