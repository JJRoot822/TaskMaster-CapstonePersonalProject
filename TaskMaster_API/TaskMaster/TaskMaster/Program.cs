using Microsoft.EntityFrameworkCore;
using TaskMaster.Data.Context;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("TaskMasterDB");

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<TaskMasterDbContext>(options =>
    options.UseSqlite(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
