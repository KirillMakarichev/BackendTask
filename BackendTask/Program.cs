using BackendTask.DataBase;
using BackendTask.Middlewares;
using BackendTask.Providers;
using BackendTask.Providers.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Postgres");

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContextPool<TreeContext>(opt => opt.UseNpgsql(connectionString));

builder.Services.AddTransient<ITreeProvider, TreeProvider>();
builder.Services.AddTransient<IExceptionsProvider, ExceptionsProvider>();
var app = builder.Build();

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<TreeContext>();
dbContext.Database.Migrate();

app.UseMiddleware<LoggingMiddleware>();
app.MapControllers();

app.Run();