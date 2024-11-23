using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using SportsPro.Controllers;
using SportsPro.Data;
using SportsPro.Models;
using System.Collections.Generic;
using System.Linq;

namespace SportsPro.Tests.Controllers
{
    public class ValidationControllerTests
    {
        [Fact]
        public void CheckProductCode_ReturnsError_WhenCodeIsEmpty()
        {
            // Arrange
            var controller = new ValidationController();

            // Act
            var result = controller.CheckProductCode("", Mock.Of<IRepository<Product>>()) as JsonResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Code cannot be empty.", result?.Value);
        }

        [Fact]
        public void CheckProductCode_ReturnsError_WhenCodeExists()
        {
            // Arrange
            var mockProductRepo = new Mock<IRepository<Product>>();
            mockProductRepo.Setup(repo => repo.GetAll()).Returns(new List<Product>
            {
                new Product { ProductCode = "ABC123", Name = "Test Product" }
            }.AsQueryable());

            var controller = new ValidationController();

            // Act
            var result = controller.CheckProductCode("ABC123", mockProductRepo.Object) as JsonResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("The code 'ABC123' already exists.", result?.Value);
        }

        [Fact]
        public void CheckProductCode_ReturnsTrue_WhenCodeDoesNotExist()
        {
            // Arrange
            var mockProductRepo = new Mock<IRepository<Product>>();
            mockProductRepo.Setup(repo => repo.GetAll()).Returns(new List<Product>
            {
                new Product { ProductCode = "ABC123", Name = "Test Product" }
            }.AsQueryable());

            var controller = new ValidationController();

            // Act
            var result = controller.CheckProductCode("XYZ789", mockProductRepo.Object) as JsonResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(true, result?.Value);
        }
    }
}
