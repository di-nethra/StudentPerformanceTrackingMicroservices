using StudentPerformanceTrackerBreakService.Services;
using StudentPerformanceTrackerBreakService.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<MongoDBService<Break>>(sp =>
    new MongoDBService<Break>(builder.Configuration, "Breaks"));
builder.Services.AddSingleton<BreakService>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
