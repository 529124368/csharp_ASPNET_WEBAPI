using dotnet.Models;
using dotnet.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SqlSugar;
using System.Reflection.Metadata.Ecma335;

namespace dotnet.Controllers;

[Route("api")]
[ApiController]
public class LszController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<LszController> _logger;

    private readonly StudentService _studentService;

    private readonly IConfiguration _configuration;

    private readonly SqlSugarScope _db;


    #region 构造函数
    public LszController(
        ILogger<LszController> logger, StudentService studentService, IConfiguration configuration, ISqlSugarClient db)
    {
        this._logger = logger;
        this._studentService = studentService;
        this._configuration = configuration;
        this._db = (SqlSugarScope)db;
    }
    #endregion

    [HttpGet("test/{id?}")]
    public IEnumerable<WeatherForecast> Get(int id)
    {

        Console.WriteLine(this._configuration.GetValue<string>("AllowedHosts"));
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


    [HttpPost("studentAll")]
    public async Task<List<Student>>  getStudentAll()
    {
        List<Student> list = await this._db.Queryable<Student>().ToListAsync();
        return list;
    }


    [HttpPost("check")]
    public bool isConne()
    {
        this._db.DbFirst.IsCreateAttribute().CreateClassFile("C:\\Users\\Administrator\\Desktop\\dotnet\\Models\\", "Models");
        return _db.Ado.IsValidConnection();
    }


}
