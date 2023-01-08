using dotnet.Controllers;
using Models;
using SqlSugar;

namespace dotnet.Services;

public class StudentService
{
    private readonly ILogger<LszController> _logger;
    private readonly SqlSugarScope _db;
    public StudentService(ILogger<LszController> logger, ISqlSugarClient db)
    {
        this._logger = logger;
        this._db = (SqlSugarScope)db;
    }

    public void add(student s)
    {
        this._db.Insertable(s).ExecuteCommand();

        _logger.LogInformation($"{DateTime.Now}:学生{s.name}信息添加成功");
        
    }
    
    public bool delete(student s)
    {
        try
        {
            if(this._db.Deleteable<student>().Where(v => v.id == s.id).ExecuteCommand() != 0 )
            {
                Console.WriteLine($"学生ID为{s.id}的信息删除了");
            }else
            {
                Console.WriteLine($"学生ID为{s.id}的信息删除失败了");
            }
            
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e.StackTrace);
            return false;
        }
    }
    
    public int update(student s)
    {
       return this._db.Updateable<student>(s).ExecuteCommand();
    }

    public List<student> search(string name)
    {
        return this._db.Queryable<student>().Where(v=>v.name ==name).ToList();
    }
    
    public async Task<List<student>> searchALl()
    {
        return  await this._db.Queryable<student>().ToListAsync();
    }

}