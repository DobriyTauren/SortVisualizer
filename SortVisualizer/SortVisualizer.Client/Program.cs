using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SortVisualizer.Client.classes;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<UserDataStorage>();
builder.Services.AddScoped<IndexedDB>();
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
