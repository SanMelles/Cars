using Cars.Core.ServiceInterface;
using Microsoft.EntityFrameworkCore;
using Cars.Data;
using Cars.Core.Models.Cars;
using Cars.Core.Dto;


namespace Cars.ApplicationService.Services
{
    public class CarServices : ICarServices
    {
        private readonly CarsDbContext _context;

        public CarServices(CarsDbContext context)
        {
            _context = context;
        }

        public async Task<Car> Create(CarDto dto)
        {
            Car car = new Car
            {
                Id = dto.Id,
                Brand = dto.Brand,
                Model = dto.Model,
                Year = dto.Year,
                EnginePower = dto.EnginePower,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return car;
        }


        public async Task<Car> Update(CarDto dto)
        {
            Car domain = new();

            domain.Id = dto.Id;
            domain.Brand = dto.Brand;
            domain.Model = dto.Model;
            domain.Year = dto.Year;
            domain.EnginePower = dto.EnginePower;
            domain.CreatedAt = dto.CreatedAt;
            domain.ModifiedAt = dto.ModifiedAt;

            _context.Cars.Update(domain);
            await _context.SaveChangesAsync();

            return domain;
        }


        public async Task<bool> Delete(int id)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(x => x.Id == id);

            if (car == null)
            {
                return false;
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<Car> Details(int id)
        {
            var car = await _context.Cars
                                    .FirstOrDefaultAsync(x => x.Id == id);

            if (car == null)
            {
                return null;
            }

            return car;
        }


    }
}