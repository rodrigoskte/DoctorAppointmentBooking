using DoctorAppointmentBooking.Infrastructure.Context;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MedicalAppointment.Presentation.BlazorWebApp;
using MedicalAppointment.Presentation.BlazorWebApp.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// var configSection = builder.Configuration.GetRequiredSection(BaseUrlConfiguration.CONFIG_NAME);
// builder.Services.Configure<BaseUrlConfiguration>(configSection);

var config = new BaseUrlConfiguration();
builder.Configuration.GetSection(BaseUrlConfiguration.CONFIG_NAME).Bind(config);
builder.Services.AddSingleton(config);

builder.Services.AddDbContext<SqlDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSqlConnection_dev"));
});

await builder.Build().RunAsync();