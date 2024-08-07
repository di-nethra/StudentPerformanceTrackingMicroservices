using StudentPerformanceTrackerStudySessionService.Services;
using StudentPerformanceTrackerStudySessionService.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<MongoDBService<StudySession>>(sp =>
    new MongoDBService<StudySession>(builder.Configuration, "StudySessions"));
builder.Services.AddSingleton<StudySessionService>();

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
