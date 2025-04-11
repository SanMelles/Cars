using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cars.Core.Models.Cars;
using Cars.Core.ServiceInterface;
using Cars.Data;
using Cars.Models;
using Microsoft.IdentityModel.Tokens;
using Cars.Core.Dto;

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
                    Year = x.Year,
                    EnginePower = x.EnginePower,
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
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            var result = await _carService.Create(dto);

            if (result == null)
            {
                TempData["ErrorMessage"] = "Failed to create the car.";
                return View("CreateUpdate", vm); // Jääb lehele, kui salvestamine ei õnnestu
            }

            TempData["SuccessMessage"] = "Car added successfully!";
            return RedirectToAction(nameof(Index)); // Viib Cars lehele tagasi
        }



        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var car = await _carService.Details(id);

            if (car == null)
            {
                return View("Error");
            }

            var vm = new CarDto
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
        public async Task<IActionResult> Update(int id)
        {
            var car = await _carService.Details(id);

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
                ModifiedAt = DateTime.Now,
            };

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CarIndexViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var dto = new CarDto
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
                return View(vm);
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var car = await _carService.Details(id);

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
        public async Task<IActionResult> DeleteConfirmation(int id)
        {
            bool isDeleted = await _carService.Delete(id);

            if (!isDeleted)
            {
                TempData["ErrorMessage"] = "Failed to delete the car.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
