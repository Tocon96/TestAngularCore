using Microsoft.EntityFrameworkCore;
using TestAngular.API.Models;

namespace TestAngular.API.Data
{
    public class TestAngularDbContext : DbContext   
    {
        public TestAngularDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
