using Blazored.SessionStorage;
using KurikulumPascasarjanaFisika;
using KurikulumPascasarjanaFisika.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IKatalogService, KatalogService>();
builder.Services.AddScoped<IRpmkGenerator, RpmkGenerator>();
builder.Services.AddScoped<ICapaianService, CapaianService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddMudServices();


await builder.Build().RunAsync();
