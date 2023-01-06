using dotnet.Services;
using Microsoft.Extensions.Configuration;
using SqlSugarHelper;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddSqlsugarSetup(builder.Configuration);
    //
    builder.Services.AddSingleton<StudentService>();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

{
    app.UseExceptionHandler("/error");
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}

