using ClinicToCloudCodingChallenge.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicToCloudCodingChallenge.Database
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }

    }
}
