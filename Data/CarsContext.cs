using Microsoft.EntityFrameworkCore;

namespace Cars.Data
{
    public class CarsContext : DbContext
    {
        public CarsContext(DbContextOptions<CarsContext> options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
    }

    public class Car
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}