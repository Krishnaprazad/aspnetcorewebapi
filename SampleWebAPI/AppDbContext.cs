using Microsoft.EntityFrameworkCore;

namespace SampleWebAPI
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
    }
}