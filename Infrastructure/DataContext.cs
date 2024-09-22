
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<AnimalGroups> AnimalGroups { get; set; }
        public DbSet<Group> Groups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>()
                .HasMany(e => e.AnimalGroups)
                .WithOne(e => e.Animal)
                .HasForeignKey(e => e.AnimalId)
                .IsRequired(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
