using LadyLuxe.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LadyLuxe.Data
{
    public class LadyLuxeDbContext : DbContext
    {
        public LadyLuxeDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet <Category> Category { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sub_Category> Sub_Categories { get; set; }
       //  public DbSet<Category> Category { get; set; }//ndo nimeona

    }
}
