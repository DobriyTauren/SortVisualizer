using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SortVisualizer.Client.classes;
using SortVisualizer.Client.Pages;
using SortVisualizer.Components;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddDbContext<AlgorithmsContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("SortVisualizer")));

builder.Services.AddLogging(builder =>
{
    builder.AddConsole(); 
});

builder.Services.AddMvc(setupAction: options => options.EnableEndpointRouting = false);

builder.Services.AddScoped<GlobalData>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseMvcWithDefaultRoute();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(SortVisualizer.Client._Imports).Assembly);

app.Run();
