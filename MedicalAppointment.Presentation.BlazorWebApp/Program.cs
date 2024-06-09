using Blazored.LocalStorage;
using DoctorAppointmentBooking.Application.Services;
using DoctorAppointmentBooking.Domain.Interfaces;
using MedicalAppointment.Presentation.BlazorWebApp;
using MedicalAppointment.Presentation.BlazorWebApp.Configuration;
using MedicalAppointment.Presentation.BlazorWebApp.Provider;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RestSharp;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAuthorizationCore(config =>
{
    config.AddPolicy("DoctorOnly", policy => policy.RequireRole("doctor"));
    config.AddPolicy("AdminOnly", policy => policy.RequireRole("admin"));
    config.AddPolicy("PatientOnly", policy => policy.RequireRole("patient"));
});

ConfigureInjections(builder);

await builder.Build().RunAsync();

void ConfigureInjections(WebAssemblyHostBuilder webAssemblyHostBuilder)
{
    var baseUrlConfig = new BaseUrlConfiguration();
    builder.Configuration.GetSection(BaseUrlConfiguration.CONFIG_NAME).Bind(baseUrlConfig);
    webAssemblyHostBuilder.Services.AddSingleton(baseUrlConfig);
    webAssemblyHostBuilder.Services.AddSingleton<IRestClient>(sp => new RestClient(baseUrlConfig.ApiBase));
    webAssemblyHostBuilder.Services.AddScoped<ISpecialtyService, SpecialtyService>();

    webAssemblyHostBuilder.Services.AddBlazoredLocalStorage();
    
    webAssemblyHostBuilder.Services.AddOptions();
    webAssemblyHostBuilder.Services.AddAuthorizationCore();
    webAssemblyHostBuilder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
}