using DoctorAppointmentBooking.Application.DTOs;
using DoctorAppointmentBooking.Application.Services;
using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Domain.Interfaces;
using DoctorAppointmentBooking.Infrastructure.Context;
using DoctorAppointmentBooking.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
ConfigureDbContext(builder);
ConfigureIdentity(builder);
ConfigureInjection(builder);
ConfigureJwtSettings(builder);
ConfigureSwagger(builder);

var app = builder.Build();

await ApplyMigrations(app);

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
ConfigureCors(app);
//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
await CreateDefaultRoles(app);
await CreateDefaultAdminUser(app);

app.Run();

static void ConfigureInjection(WebApplicationBuilder webApplicationBuilder)
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
    webApplicationBuilder.Services.AddScoped<IPasswordHasher<IdentityUser>, PasswordHasher<IdentityUser>>();
    webApplicationBuilder.Services.AddScoped<IAuthService, AuthService>();
    webApplicationBuilder.Services.AddScoped<IUserManagerService, UserManagerService>();
    webApplicationBuilder.Services.AddScoped<IEmailService, EmailService>();    
}

static void ConfigureDbContext(WebApplicationBuilder builder1)
{
    builder1.Services.AddDbContext<SqlDbContext>(options =>
    {
        options.UseSqlServer(builder1.Configuration.GetConnectionString("DefaultSqlConnection_dev"),
            sqlServerOptionsAction: sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
            });
    });

    builder1.Services.AddDbContext<AuthDbContext>(options =>
    {
        options.UseSqlServer(builder1.Configuration.GetConnectionString("DefaultAuthSqlConnection"),
            sqlServerOptionsAction: sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
            });
    });

    builder1.Services.AddControllersWithViews();
}

static async Task ApplyMigrations(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<SqlDbContext>();
        await dbContext.Database.MigrateAsync();

        var authDbContext = services.GetRequiredService<AuthDbContext>();
        await authDbContext.Database.MigrateAsync();
    }
}

static void ConfigureCors(WebApplication app)
{
    app.UseCors(builder =>
    {
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
        builder.AllowAnyOrigin();
    });
}

static void ConfigureIdentity(WebApplicationBuilder builder)
{
    builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<AuthDbContext>()
        .AddDefaultTokenProviders();
}

static void ConfigureJwtSettings(WebApplicationBuilder builder)
{
    var emailSettingsSection = builder.Configuration.GetSection("EmailSettings");
    builder.Services.Configure<EmailSettings>(emailSettingsSection);

    var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
    builder.Services.Configure<JwtSettings>(jwtSettingsSection);

    var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
    var key = Encoding.ASCII.GetBytes(jwtSettings.Segredo);
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = jwtSettings.Audiencia,
            ValidIssuer = jwtSettings.Emissor
        };
    });
}

static void ConfigureSwagger(WebApplicationBuilder builder)
{
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "DoctorAppointmentBooking", Version = "v1" });

        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description =
                "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        });
    });
}

static async Task CreateDefaultRoles(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var roles = new[] { "Admin", "Patient", "Doctor" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}

static async Task CreateDefaultAdminUser(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var adminUser = new IdentityUser
        {
            UserName = "admin@admin.com.br",
            Email = "admin@admin.com.br",
            EmailConfirmed = true
        };

        var user = await userManager.FindByEmailAsync(adminUser.Email);
        if (user == null)
        {
            var result = await userManager.CreateAsync(
                adminUser, 
                "@senha01");
            
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}