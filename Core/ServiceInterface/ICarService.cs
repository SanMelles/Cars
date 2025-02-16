using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cars.Core.Models.Cars;

namespace Cars.Core.ServiceInterface
{
    public interface ICarServices
    {
        Task<IEnumerable<CarIndexViewModel>> GetAllCarsAsync();
        Task<CarIndexViewModel> GetCarByIdAsync(int id);
        Task<CarIndexViewModel> AddCarAsync(CarIndexViewModel car);
        Task<CarIndexViewModel> UpdateCarAsync(CarIndexViewModel car);
        Task<bool> DeleteCarAsync(int id);
    }   
}
