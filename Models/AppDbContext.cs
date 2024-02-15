using Microsoft.EntityFrameworkCore;

namespace Todo.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Datasource (sqlite is in the root folder)
            optionsBuilder.UseSqlite("Data Source=db.sqlite");
        }
    }
}
