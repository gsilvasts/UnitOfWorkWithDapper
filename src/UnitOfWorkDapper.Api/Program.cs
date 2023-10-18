using UnitOfWorkDapper.Application.Interfaces;
using UnitOfWorkDapper.Application.Service;
using UnitOfWorkDapper.Core.Interfaces.Repositories;
using UnitOfWorkDapper.Core.Interfaces.UnitOfWork;
using UnitOfWorkDapper.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = Environment.GetEnvironmentVariable("TASK_CONNECTIONSTRING") ?? string.Empty;
var dataBase = Environment.GetEnvironmentVariable("TASK_DATABASE") ?? string.Empty;
var user = Environment.GetEnvironmentVariable("TASK_USER") ?? string.Empty;
var password = Environment.GetEnvironmentVariable("TASK_PASSWORD") ?? string.Empty;

connectionString = connectionString
    .Replace("user", user)
    .Replace("password", password)
    .Replace("dataBase", dataBase);

builder.Services.AddScoped(x=> new DapperDbSession(connectionString));

builder.Services.AddTransient<ITaskService, TaskService>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<ITaskRepository, TaskRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
