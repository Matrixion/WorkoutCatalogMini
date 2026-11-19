using Microsoft.EntityFrameworkCore;
using WorkoutCatalogMini.Data.Entities;

namespace WorkoutCatalogMini.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Exercise> Exercises => Set<Exercise>();
    }
}
