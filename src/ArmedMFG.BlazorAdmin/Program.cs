﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using ArmedMFG.BlazorAdmin;
using ArmedMFG.BlazorAdmin.Services;
using Blazored.LocalStorage;
using ArmedMFG.BlazorShared;
using ArmedMFG.BlazorShared.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Syncfusion.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#admin");
builder.RootComponents.Add<HeadOutlet>("head::after");

var configSection = builder.Configuration.GetRequiredSection(BaseUrlConfiguration.CONFIG_NAME);
builder.Services.Configure<BaseUrlConfiguration>(configSection);

builder.Services.AddScoped(_ => new HttpClient() { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<ToastService>();
builder.Services.AddScoped<HttpService>();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped(sp => (CustomAuthStateProvider)sp.GetRequiredService<AuthenticationStateProvider>());

builder.Services.AddSyncfusionBlazor();

builder.Services.AddBlazorServices();

builder.Logging.AddConfiguration(builder.Configuration.GetRequiredSection("Logging"));

await ClearLocalStorageCache(builder.Services);

await builder.Build().RunAsync();

static async Task ClearLocalStorageCache(IServiceCollection services)
{
    var sp = services.BuildServiceProvider();
    var localStorageService = sp.GetRequiredService<ILocalStorageService>();

    await localStorageService.RemoveItemAsync(nameof(CatalogBrand));
    await localStorageService.RemoveItemAsync(nameof(CatalogType));

    await localStorageService.RemoveItemAsync(nameof(ProductCategory));
    await localStorageService.RemoveItemAsync(nameof(MaterialCategory));
}
