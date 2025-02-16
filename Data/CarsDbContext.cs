using Microsoft.EntityFrameworkCore;
using Cars.Core.Models.Cars;

namespace Cars.Data
{
    public class CarsDbContext : DbContext
    {
        public CarsDbContext(DbContextOptions<CarsDbContext> options) : base(options)
        {
        }

        public DbSet<CarIndexViewModel> Cars { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CarsDb;Trusted_Connection=True;");
            }
        }
    }
}