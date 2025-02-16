using Cars.Core.ServiceInterface;
using Microsoft.EntityFrameworkCore;
using Cars.Data;
using Cars.Core.Models.Cars;

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

        public async Task<CarIndexViewModel> AddCarAsync(CarIndexViewModel car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
            return car;
        }

        public async Task<CarIndexViewModel> UpdateCarAsync(CarIndexViewModel car)
        {
            _context.Entry(car).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return car;
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
    }
}