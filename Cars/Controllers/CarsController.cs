using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cars.Core.Models.Cars;
using Cars.Core.ServiceInterface;
using Cars.Data;
using Cars.Models;
using Microsoft.IdentityModel.Tokens;

namespace Cars.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarsDbContext _context;
        private readonly ICarServices _carService;

        public CarsController(CarsDbContext context, ICarServices carService)
        {
            _context = context;
            _carService = carService;
        }

        public IActionResult Index()
        {
            var result = _context.Cars
                .Select(x => new CarIndexViewModel
                {
                    Id = x.Id,
                    Brand = x.Brand,
                    Model = x.Model,
                    Year = x.Year
                });

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CarCreateUpdateViewModel result = new();
            return View("CreateUpdate", result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarCreateUpdateViewModel vm)
        {
            var dto = new CarDto()
            {
                Id = vm.Id,
                Brand = vm.Brand,
                Model = vm.Model,
                Year = vm.Year,
                EnginePower = vm.EnginePower,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt
            };

            var result = await _carService.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var car = await _carService.DetailAsync(id);

            if (car == null)
            {
                return View("Error");
            }

            var vm = new CarDetailsViewModel
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                EnginePower = car.EnginePower,
                CreatedAt = car.CreatedAt,
                ModifiedAt = car.ModifiedAt
            };

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var car = await _carService.DetailAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            var vm = new CarCreateUpdateViewModel
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                EnginePower = car.EnginePower,
                CreatedAt = car.CreatedAt,
                ModifiedAt = car.ModifiedAt
            };

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CarCreateUpdateViewModel vm)
        {
            var dto = new CarDto()
            {
                Id = vm.Id,
                Brand = vm.Brand,
                Model = vm.Model,
                Year = vm.Year,
                EnginePower = vm.EnginePower,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt
            };

            var result = await _carService.Update(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var car = await _carService.DetailAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            var vm = new CarDeleteViewModel
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                EnginePower = car.EnginePower,
                CreatedAt = car.CreatedAt,
                ModifiedAt = car.ModifiedAt
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var car = await _carService.Delete(id);

            if (car == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
