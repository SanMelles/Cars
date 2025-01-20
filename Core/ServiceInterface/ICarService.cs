using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cars.Core.Models;

namespace Cars.Core.ServiceInterface
{
    public interface ICarServices
    {
        Task<IEnumerable<Car>> GetAllCarsAsync();
        Task<Car> GetCarByIdAsync(int id);
        Task<Car> AddCarAsync(Car car);
        Task<Car> UpdateCarAsync(Car car);
        Task<bool> DeleteCarAsync(int id);
    }   
}
