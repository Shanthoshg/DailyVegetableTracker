
using DailyVegetableTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace DailyVegetableTracker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Vegetable> Vegetables { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vegetable>()
                .HasIndex(v => v.Name)
                .IsUnique();

            modelBuilder.Entity<Purchase>()
                .HasOne(p => p.Vegetable)
                .WithMany()
                .HasForeignKey(p => p.VegetableId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Purchase>()
                .Property(p => p.PricePerUnit)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Purchase>()
                .Property(p => p.Quantity)
                .HasPrecision(12, 3);
        }
    }
}
