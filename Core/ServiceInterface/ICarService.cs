using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cars.Core.Models.Cars;
using Cars.Core.Dto;

namespace Cars.Core.ServiceInterface
{
    public interface ICarServices
    {
        Task<Car> Create(CarDto car);
        Task<Car> Update(CarDto car);
        Task<Car> Details(int id);
        Task<bool> Delete(int id);
    }   
}
