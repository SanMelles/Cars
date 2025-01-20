using Microsoft.EntityFrameworkCore;
using Cars.Core.Models;

namespace Cars.Data
{
    public class CarsContext : DbContext
    {
        public CarsContext(DbContextOptions<CarsContext> options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>().ToTable("Car");

            modelBuilder.Entity<Car>().Property(c => c.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Car>().Property(c => c.Make).IsRequired();
            modelBuilder.Entity<Car>().Property(c => c.Model).IsRequired();
            modelBuilder.Entity<Car>().Property(c => c.Year).IsRequired();
            modelBuilder.Entity<Car>().Property(c => c.CreatedAt).IsRequired();
            modelBuilder.Entity<Car>().Property(c => c.ModifiedAt).IsRequired();
        }
    }
}