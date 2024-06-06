using DoctorAppointmentBooking.Application.Services;
using DoctorAppointmentBooking.Domain.Interfaces;
using MedicalAppointment.Presentation.BlazorWebApp;
using MedicalAppointment.Presentation.BlazorWebApp.Configuration;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RestSharp;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

ConfigureInjections(builder);

await builder.Build().RunAsync();

void ConfigureInjections(WebAssemblyHostBuilder webAssemblyHostBuilder)
{
    // Configuração do BaseUrlConfiguration
    var baseUrlConfig = new BaseUrlConfiguration();
    builder.Configuration.GetSection(BaseUrlConfiguration.CONFIG_NAME).Bind(baseUrlConfig);

    webAssemblyHostBuilder.Services.AddSingleton(baseUrlConfig);

    // Configuração do RestClient
    webAssemblyHostBuilder.Services.AddSingleton<IRestClient>(sp => new RestClient(baseUrlConfig.ApiBase));
    webAssemblyHostBuilder.Services.AddScoped<ISpecialtyService, SpecialtyService>();
}