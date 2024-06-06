using DoctorAppointmentBooking.Application.Services;
using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Domain.Interfaces;
using DoctorAppointmentBooking.Infrastructure.Context;
using DoctorAppointmentBooking.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
ConfigureDbContext(builder);
ConfigureInjection(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

ConfigureCors(app);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureInjection(WebApplicationBuilder webApplicationBuilder)
{
    webApplicationBuilder.Services.AddScoped<IBaseRepository<Patient>, BaseRepository<Patient>>();
    webApplicationBuilder.Services.AddScoped<IBaseService<Patient>, BaseService<Patient>>();
    webApplicationBuilder.Services.AddScoped<IBaseRepository<Specialty>, BaseRepository<Specialty>>();
    webApplicationBuilder.Services.AddScoped<IBaseService<Specialty>, BaseService<Specialty>>();
    webApplicationBuilder.Services.AddScoped<IBaseRepository<Doctor>, BaseRepository<Doctor>>();
    webApplicationBuilder.Services.AddScoped<IBaseService<Doctor>, BaseService<Doctor>>();
    webApplicationBuilder.Services.AddScoped<IBaseRepository<DoctorSpecialty>, BaseRepository<DoctorSpecialty>>();
    webApplicationBuilder.Services.AddScoped<IBaseService<DoctorSpecialty>, BaseService<DoctorSpecialty>>();
    webApplicationBuilder.Services.AddScoped<IBaseRepository<Schedule>, BaseRepository<Schedule>>();
    webApplicationBuilder.Services.AddScoped<IBaseService<Schedule>, BaseService<Schedule>>();
    webApplicationBuilder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
    webApplicationBuilder.Services.AddScoped<IDoctorService, DoctorService>();
    webApplicationBuilder.Services.AddScoped<ISpecialtyRepository, SpecialtyRepository>();
    webApplicationBuilder.Services.AddScoped<ISpecialtyService, SpecialtyService>();
    webApplicationBuilder.Services.AddScoped<IDoctorSpecialtyRepository, DoctorSpecialtyRepository>();
    webApplicationBuilder.Services.AddScoped<IDoctorSpecialtyService, DoctorSpecialtyService>();
    webApplicationBuilder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
    webApplicationBuilder.Services.AddScoped<IScheduleService, ScheduleService>();
    webApplicationBuilder.Services.AddScoped<IPatientRepository, PatientRepository>();
    webApplicationBuilder.Services.AddScoped<IPatientService, PatientService>();
}

void ConfigureDbContext(WebApplicationBuilder builder1)
{
    builder1.Services.AddDbContext<SqlDbContext>(options =>
    {
        options.UseSqlServer(builder1.Configuration.GetConnectionString("DefaultSqlConnection_dev"));
    });
}

void ConfigureCors(WebApplication app)
{
    app.UseCors(builder =>
    {
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
        builder.AllowAnyOrigin();
    });
}

