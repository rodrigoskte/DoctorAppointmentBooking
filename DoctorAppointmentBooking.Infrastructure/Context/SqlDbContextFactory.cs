using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DoctorAppointmentBooking.Infrastructure.Context
{
    public class SqlDbContextFactory : IDesignTimeDbContextFactory<SqlDbContext>
    {
        public SqlDbContext CreateDbContext(string[] args)
        {
            // Caminho para a pasta do projeto API onde está o appsettings.json
            var basePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../MedicalAppointment.Presentation.API"));

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<SqlDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultSqlConnection_dev");

            optionsBuilder.UseSqlServer(connectionString);

            return new SqlDbContext(optionsBuilder.Options);
        }
    }
}
