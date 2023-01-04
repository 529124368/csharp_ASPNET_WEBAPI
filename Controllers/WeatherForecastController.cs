using dotnet.Models;
using dotnet.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnet.Controllers;

[ApiController]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    private readonly StudentService _studentService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger,StudentService studentService)
    {
        _logger = logger;
        _studentService = studentService;
    }

    [HttpGet("test/{id?}")]
    public IEnumerable<WeatherForecast> Get(int id)
    {
        Console.WriteLine(DateTime.Now);
        var re = Enumerable.Range(1, 5).Select(v=>"No:"+v).ToString();
        Console.WriteLine(re);
        _logger.LogInformation("开始了访问");
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpGet("student/{name}")]
    public IActionResult GetInfo(string name)
    {
        return Ok(_studentService.search(name));
    }
    
    [HttpGet("student")]
    public Dictionary<int,Student> GetAll()
    {
        return _studentService.searchALl();
    }
    
    [HttpPost("student")]
    public IActionResult addStudent(Student s)
    {
        _studentService.add(s);
        return Ok(200);
    }
    
    [HttpDelete("student")]
    public IActionResult delStudent(Student s)
    {
        if (_studentService.delete(s))
        {
            Console.WriteLine("删除成功");
        }
        else
        {
            Console.WriteLine("删除失败");
        }
        return Ok(200);
    }


}
