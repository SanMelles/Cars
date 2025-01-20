using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cars.Core.Models;

namespace Cars.Core.Interfaces
{
    public class ICarService
    {
        public interface ICarServices
        {
            Task<IEnumerable<Car>> GetAllCarsAsync();
            Task<Car> GetCarByIdAsync(int id);
            Task<Car> AddCarAsync(Car car);
            Task<Car> UpdateCarAsync(Car car);
            Task<Car> DeleteCarAsync(int id);
        }   
    }
}
