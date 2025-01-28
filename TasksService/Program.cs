using TasksService.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TaskDbConnection")));

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
