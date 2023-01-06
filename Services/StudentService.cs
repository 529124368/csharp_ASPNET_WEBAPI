using dotnet.Controllers;
using dotnet.Models;
namespace dotnet.Services;

public class StudentService
{
    private static Dictionary<int,Student> box=new Dictionary<int, Student>();
    private readonly ILogger<LszController> _logger;
    public StudentService(ILogger<LszController> logger)
    {
        _logger = logger;
        Console.WriteLine("StudentService创建了");
    }

    public void add(Student s)
    {
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
    
    public bool delete(Student s)
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
    
    public void update(Student s)
    {
       
    }
    public Student search(string name)
    {
        var s = box.FirstOrDefault(v => v.Value.name == name);
        return s.Value;
    }
    
    public Dictionary<int,Student> searchALl()
    {
        return box;
    }

    #region 数据检测
    private bool checkData(Student s)
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