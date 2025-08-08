using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ScheduleMeetingSystem.Api.Middleware;
using ScheduleMeetingSystem.Application;
using ScheduleMeetingSystem.Infrastructure;
using ScheduleMeetingSystem.Infrastructure.DbContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddDbContext<ScheduleMeetingSystemDbContext>(options =>
{
    options.UseInMemoryDatabase("ScheduleMeetingSystemDb");
});

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
