using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
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

            modelBuilder.Entity<Animal>()
                .HasIndex(p => p.Tag);

            modelBuilder.Entity<Group>()
                .HasIndex(p => p.Name);

            base.OnModelCreating(modelBuilder);
        }
    }
}
