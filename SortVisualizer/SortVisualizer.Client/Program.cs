using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using SortVisualizer.Client.classes;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<UserDataStorage>();


await builder.Build().RunAsync();
