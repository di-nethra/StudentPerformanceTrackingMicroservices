using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudentPerformanceMonitoringApp.Data;
using StudentPerformanceTracker.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();


builder.Services.AddScoped<StudySessionService>();  
builder.Services.AddScoped<BreakService>();  
builder.Services.AddScoped<PredictionService>();  

builder.Services.AddHttpClient<StudySessionServiceHttpClient>(client =>
{
    client.BaseAddress = new Uri("https://studysessionmicroservice-gchqd8ghbnb6brfx.southeastasia-01.azurewebsites.net/");  
});

builder.Services.AddHttpClient<BreakServiceHttpClient>(client =>
{
    client.BaseAddress = new Uri("https://sessionbreaksmicroservice-adeuhqgzbkgyd0bt.southeastasia-01.azurewebsites.net/"); 
});

builder.Services.AddHttpClient<PredictionServiceHttpClient>(client =>
{
    client.BaseAddress = new Uri("https://predictionmicroservice-fqf8crf0aucsddfg.southeastasia-01.azurewebsites.net/");
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
