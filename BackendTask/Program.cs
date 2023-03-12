using BackendTask.Controllers;
using BackendTask.DataBase;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Postgres");

builder.Services.AddControllers();
builder.Services.AddDbContextPool<TreeContext>(opt => opt.UseNpgsql(connectionString));

builder.Services.AddHostedService<TestAdd>();

var app = builder.Build();

app.MapControllers();

app.Run();