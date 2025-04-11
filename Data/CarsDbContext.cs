using Microsoft.EntityFrameworkCore;
using Cars.Core.Models.Cars;

namespace Cars.Data
{
    public class CarsDbContext : DbContext
    {
        public CarsDbContext(DbContextOptions<CarsDbContext> options) : base(options)
        {
            
        }
        public DbSet<Car> Cars { get; set; }
    }
}