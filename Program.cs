using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SortVisualizer.Client;
using SortVisualizer.Client.Classes;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<UserDataStorage>();
builder.Services.AddScoped<IndexedDB>();
builder.Services.AddBlazoredLocalStorage();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


await builder.Build().RunAsync();
