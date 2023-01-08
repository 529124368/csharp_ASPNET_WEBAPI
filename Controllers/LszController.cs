using dotnet.Services;
using Microsoft.AspNetCore.Mvc;
using Models;
using SqlSugar;

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

    [HttpGet("student/{name}")]
    public IActionResult GetInfo(string name)
    {
        return Ok(_studentService.search(name));
    }
    
    [HttpGet("student")]
    public async Task <List<student>> GetAll()
    {
        return await _studentService.searchALl();
    }
    
    [HttpPost("student")]
    public IActionResult addStudent(student s)
    {
        _studentService.add(s);
        return Ok(200);
    }
    
    [HttpDelete("student")]
    public IActionResult delStudent(student s)
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


    [HttpPut("student")]
    public IActionResult updateStduent(student s)
    {
        return Ok(_studentService.update(s));
    }

    [HttpPost("check")]
    public bool isConne()
    {
        //数据库实体批量生成class
        this._db.DbFirst.IsCreateAttribute().CreateClassFile("C:\\Users\\Administrator\\Desktop\\dotnet\\Models\\", "Models");
        return _db.Ado.IsValidConnection();
    }


}
