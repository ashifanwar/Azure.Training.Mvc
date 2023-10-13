using Microsoft.EntityFrameworkCore;
using Azure.Training.Mvc.WebApi.Models;

namespace Azure.Training.Mvc.WebApi.Data
{
    public class AzureTrainingMvcDataContext : DbContext
    {
        public AzureTrainingMvcDataContext (DbContextOptions<AzureTrainingMvcDataContext> options)
            : base(options)
        {
        }

        public DbSet<Login> Login { get; set; }

        public DbSet<Employee> Employee { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(c =>
            {
                c.HasKey(c => new { c.EmployeeId });
                c.Property(e => e.EmployeeId).UseIdentityColumn();
            });
        }
    }
}
