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

        public async Task<IEnumerable<CarIndexViewModel>> GetAllCarsAsync()
        {
            return await _context.Cars.ToListAsync();
        }

        public async Task<CarIndexViewModel> GetCarByIdAsync(int id)
        {
            return await _context.Cars.FindAsync(id);
        }

        public async Task<CarIndexViewModel> AddCarAsync(CarDto dto)
        {
            var car = new CarIndexViewModel
            {
                Id = dto.Id,
                Brand = dto.Brand,
                Model = dto.Model,
                Year = dto.Year,
                EnginePower = dto.EnginePower,
                CreatedAt = dto.CreatedAt,
                ModifiedAt = dto.ModifiedAt
            };

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            var carViewModel = new CarIndexViewModel
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                EnginePower = car.EnginePower
            };

            return carViewModel;
        }

        public async Task<CarIndexViewModel> UpdateCarAsync(CarDto dto)
        {
            var car = await _context.Cars.FindAsync(dto.Id);
            if (car == null)
            {
                return null;
            }

            car.Brand = dto.Brand;
            car.Model = dto.Model;
            car.Year = dto.Year;
            car.EnginePower = dto.EnginePower;
            car.ModifiedAt = DateTime.UtcNow;

            _context.Cars.Update(car);
            await _context.SaveChangesAsync();

            var carViewModel = new CarIndexViewModel
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                EnginePower = car.EnginePower
            };

            return carViewModel;
        }

        public async Task<bool> DeleteCarAsync(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return false;
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CarIndexViewModel> DetailsAsync(int id)
        {
            var car = await _context.Cars
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (car == null)
            {
                return null;
            }

            var carViewModel = new CarIndexViewModel
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                EnginePower = car.EnginePower
            };

            return carViewModel;
        }

    }
}