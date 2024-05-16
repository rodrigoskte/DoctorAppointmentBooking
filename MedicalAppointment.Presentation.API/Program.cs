using DoctorAppointmentBooking.Application.Services;
using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Domain.Interfaces;
using DoctorAppointmentBooking.Infrastructure.Context;
using DoctorAppointmentBooking.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SqlDbContext>();
builder.Services.AddScoped<IBaseRepository<Patient>, BaseRepository<Patient>>();
builder.Services.AddScoped<IBaseService<Patient>, BaseService<Patient>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
