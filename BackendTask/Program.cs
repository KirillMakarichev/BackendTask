using BackendTask.Controllers;
using BackendTask.DataBase;
using BackendTask.Providers;
using BackendTask.Providers.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Postgres");

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContextPool<TreeContext>(opt => opt.UseNpgsql(connectionString));
builder.Services.AddTransient<ITreeProvider, TreeProvider>();

var app = builder.Build();

app.MapControllers();

app.Run();