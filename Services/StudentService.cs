using dotnet.Controllers;
using Models;
using SqlSugar;

namespace dotnet.Services;

public class StudentService
{
    private static Dictionary<int, student> box=new Dictionary<int, student>();
    private readonly ILogger<LszController> _logger;
    private readonly SqlSugarScope _db;
    public StudentService(ILogger<LszController> logger, ISqlSugarClient db)
    {
        this._logger = logger;
        this._db = (SqlSugarScope)db;
    }

    public void add(student s)
    {
        Console.WriteLine(this._db.Insertable<student>(s).ExecuteCommand());

        if (checkData(s))
        {
            box.Add(box.Count+1,s);
            _logger.LogInformation($"{DateTime.Now}:学生{s.name}信息添加成功");
        }
        else
        {
            _logger.LogInformation($"{DateTime.Now}:学生{s.name}信息添加不成功");
        }
        
    }
    
    public bool delete(student s)
    {
        try
        {
            var res = box.FirstOrDefault(v => v.Value.name == s.name);
            Console.WriteLine($"key是{res.Key},值得名字是{res.Value.name}");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e.StackTrace);
            return false;
        }
    }
    
    public void update(student s)
    {
       
    }
    public student search(string name)
    {
        var s = box.FirstOrDefault(v => v.Value.name == name);
        return s.Value;
    }
    
    public Dictionary<int, student> searchALl()
    {
        return box;
    }

    #region 数据检测
    private bool checkData(student s)
    {
        var res = box.FirstOrDefault(v => v.Value.name == s.name);
        if (res.Value is null)
        {
            return true;
        }
        return false;
    }
    #endregion
}