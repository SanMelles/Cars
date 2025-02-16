using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cars.Core.Models.Cars;
using Cars.Core.Dto;

namespace Cars.Core.ServiceInterface
{
    public interface ICarServices
    {
        Task<IEnumerable<CarIndexViewModel>> GetAllCarsAsync();
        Task<CarIndexViewModel> AddCarAsync(CarDto car);
        Task<CarIndexViewModel> UpdateCarAsync(CarDto car);
        Task<CarIndexViewModel> DetailsAsync(int id);
        Task<bool> DeleteCarAsync(int id);
    }   
}
