using Blazored.LocalStorage;
using DoctorAppointmentBooking.Presentation.BlazorWebApp;
using DoctorAppointmentBooking.Presentation.BlazorWebApp.Configuration;
using DoctorAppointmentBooking.Presentation.BlazorWebApp.Provider;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using RestSharp;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMudServices();
CreatePolicies(builder);
ConfigureInjections(builder);
await builder.Build().RunAsync();

void ConfigureInjections(WebAssemblyHostBuilder webAssemblyHostBuilder)
{
    var baseUrlConfig = new BaseUrlConfiguration();
    builder.Configuration.GetSection(BaseUrlConfiguration.CONFIG_NAME).Bind(baseUrlConfig);
    webAssemblyHostBuilder.Services.AddSingleton(baseUrlConfig);
    webAssemblyHostBuilder.Services.AddSingleton<IRestClient>(sp => new RestClient(baseUrlConfig.ApiBase));

    webAssemblyHostBuilder.Services.AddBlazoredLocalStorage();
    
    webAssemblyHostBuilder.Services.AddOptions();
    webAssemblyHostBuilder.Services.AddAuthorizationCore();
    webAssemblyHostBuilder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
}

void CreatePolicies(WebAssemblyHostBuilder builder1)
{
    builder1.Services.AddAuthorizationCore(config =>
    {
        config.AddPolicy("DoctorOnly", policy => policy.RequireRole("doctor"));
        config.AddPolicy("AdminOnly", policy => policy.RequireRole("admin"));
        config.AddPolicy("PatientOnly", policy => policy.RequireRole("patient"));
        config.AddPolicy("DoctorOrAdmin", policy => policy.RequireRole("Doctor", "Admin"));
        config.AddPolicy("PatientOrAdmin", policy => policy.RequireRole("Patient", "Admin"));
        config.AddPolicy("AdminDoctorPatient", policy => policy.RequireRole("Admin", "Doctor", "Patient"));
    });
}