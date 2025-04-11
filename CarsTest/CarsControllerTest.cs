using Xunit;
using Cars.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Cars.Core.ServiceInterface;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Cars.Core.Dto;
using System.Collections.Generic;
using Cars.Core.Models.Cars;
using Cars.Data;
using Microsoft.EntityFrameworkCore;

namespace Cars.Tests
{
    public class CarControllerTests
    {
        private readonly Mock<ICarServices> _mockCarService;
        private readonly CarsController _controller;

        public CarControllerTests()
        {
            _mockCarService = new Mock<ICarServices>();
            _controller = new CarsController(null, _mockCarService.Object);
            _controller.TempData = new Mock<ITempDataDictionary>().Object;
        }

        [Fact]
        public void Create_ShouldReturnView_WhenCalled()
        {
            // Arrange
            // Act
            var result = _controller.Create();
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<CarCreateUpdateViewModel>(viewResult.Model);
        }

        [Fact]
        public async Task Create_ShouldRedirectToIndex_WhenCarIsCreated()
        {
            // Arrange
            var carViewModel = new CarCreateUpdateViewModel
            {
                Id = 1,
                Brand = "Toyota",
                Model = "Corolla",
                Year = 2020,
                EnginePower = 150
            };

            _mockCarService.Setup(s => s.Create(It.IsAny<CarDto>())).ReturnsAsync(new Car());

            // Act
            var result = await _controller.Create(carViewModel);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public async Task Create_ShouldReturnView_WhenCarCreationFails()
        {
            // Arrange
            var carViewModel = new CarCreateUpdateViewModel
            {
                Id = 1,
                Brand = "Toyota",
                Model = "Corolla",
                Year = 2020,
                EnginePower = 150
            };
            _mockCarService.Setup(s => s.Create(It.IsAny<CarDto>())).ReturnsAsync((Car)null);
            // Act
            var result = await _controller.Create(carViewModel);
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("CreateUpdate", viewResult.ViewName);
        }

        [Fact]
        public async Task DeleteConfirmation_ShouldRedirectToIndex_WhenCarIsDeleted()
        {
            // Arrange
            int carId = 1;
            _mockCarService.Setup(s => s.Delete(carId)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteConfirmation(carId);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);

            // Kontrollime, et teenuse `Delete` meetodit kutsuti välja
            _mockCarService.Verify(s => s.Delete(carId), Times.Once);
        }
    }
}
