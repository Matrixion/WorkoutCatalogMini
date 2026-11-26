using Microsoft.EntityFrameworkCore;
using WorkoutCatalogMini.Data.Entities;

namespace WorkoutCatalogMini.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Exercise> Exercises => Set<Exercise>();
        public DbSet<MuscleGroup> MuscleGroups => Set<MuscleGroup>();
        public DbSet<Difficulty> Difficulties => Set<Difficulty>();
        public DbSet<ExerciseMuscleGroup> ExerciseMuscleGroups
            => Set<ExerciseMuscleGroup>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ExerciseMuscleGroup>()
                .HasKey(em => new { em.ExerciseId, em.MuscleGroupId });

            modelBuilder.Entity<ExerciseMuscleGroup>()
                .HasOne(em => em.Exercise)
                .WithMany(e => e.MuscleGroups)
                .HasForeignKey(em => em.ExerciseId);

            modelBuilder.Entity<ExerciseMuscleGroup>()
                .HasOne(em => em.MuscleGroup)
                .WithMany(m => m.Exercises)
                .HasForeignKey(em => em.MuscleGroupId);
        }
    }
}