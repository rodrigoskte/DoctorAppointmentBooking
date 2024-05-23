using DoctorAppointmentBooking.Application.Services;
using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Domain.Interfaces;
using DoctorAppointmentBooking.Infrastructure.Repository;
using MedicalAppointment.Presentation.BlazorServerApp.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

ConfigurationDBContext(builder);
ConfigurationInterfaces(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

static void ConfigurationInterfaces(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<IBaseRepository<Patient>, BaseRepository<Patient>>();
    builder.Services.AddScoped<IBaseService<Patient>, BaseService<Patient>>();
}

static void ConfigurationDBContext(WebApplicationBuilder builder)
{
    var dbConn = builder.Configuration.GetConnectionString("DefaultConnection");
    // Falta add o Microsoft.EntityFrameworkCore
    // builder.Services.AddDbContext<SqlDbContext>(options => options.UseSqlServer(dbConn));
}