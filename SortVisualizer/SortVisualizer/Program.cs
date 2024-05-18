using Blazored.LocalStorage;
using Microsoft.EntityFrameworkCore;
using SortVisualizer.Client.classes;
using SortVisualizer.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddDbContext<AlgorithmsContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("SortVisualizer")));

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddLogging(builder =>
{
    builder.ClearProviders();
    builder.AddConsole();

    builder.SetMinimumLevel(LogLevel.Information);

    // Откл логирование команд базы данных
    builder.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
});

builder.Services.AddMvc(setupAction: options => options.EnableEndpointRouting = false);

builder.Services.AddScoped<UserDataStorage>();
builder.Services.AddScoped<IndexedDB>();

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
app.UseDeveloperExceptionPage();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(SortVisualizer.Client._Imports).Assembly);

app.Run();