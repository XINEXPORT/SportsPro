using Xunit;
using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsPro.Controllers;
using SportsPro.Data;
using SportsPro.Models;
using SportsPro.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace SportsPro.Tests.Controllers
{
    public class TechIncidentControllerTests
    {
        [Fact]
        public void Edit_ReturnsRedirect_WhenSessionIsNull()
        {
            // Arrange
            var mockTechnicianRepo = new Mock<IRepository<Technician>>();
            var mockIncidentRepo = new Mock<IRepository<Incident>>();
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            // Simulate a null session
            mockHttpContextAccessor.Setup(x => x.HttpContext).Returns((HttpContext)null);

            var controller = new TechIncidentController(
                mockTechnicianRepo.Object,
                mockIncidentRepo.Object,
                mockHttpContextAccessor.Object
            );

            // Initialize TempData
            var tempData = new TempDataDictionary(
                Mock.Of<HttpContext>(),
                Mock.Of<ITempDataProvider>()
            );
            controller.TempData = tempData;

            // Act
            var result = controller.Edit(1);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            Assert.Equal("Session is not available. Please select a technician.", controller.TempData["message"]);
        }

        [Fact]
        public void Edit_ReturnsRedirect_WhenTechIDIsNotInSession()
        {
            // Arrange
            var mockTechnicianRepo = new Mock<IRepository<Technician>>();
            var mockIncidentRepo = new Mock<IRepository<Incident>>();
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var mockSession = new Mock<ISession>();

            // Mock TryGetValue to simulate no TechID in session
            mockSession
                .Setup(s => s.TryGetValue("techID", out It.Ref<byte[]>.IsAny))
                .Returns(false); // Simulates "techID" not being in the session

            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(x => x.Session).Returns(mockSession.Object);
            mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(mockHttpContext.Object);

            var controller = new TechIncidentController(
                mockTechnicianRepo.Object,
                mockIncidentRepo.Object,
                mockHttpContextAccessor.Object
            );

            // Initialize TempData
            var tempData = new TempDataDictionary(
                mockHttpContext.Object,
                Mock.Of<ITempDataProvider>()
            );
            controller.TempData = tempData;

            // Act
            var result = controller.Edit(1);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            Assert.Equal("Technician not found. Please select a technician.", controller.TempData["message"]);
        }

        [Fact]
        public void Edit_ReturnsRedirect_WhenTechnicianIsNotFound()
        {
            // Arrange
            var mockTechnicianRepo = new Mock<IRepository<Technician>>();
            var mockIncidentRepo = new Mock<IRepository<Incident>>();
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var mockSession = new Mock<ISession>();

            // Simulate a valid "techID" key in session
            byte[] techIDBytes = BitConverter.GetBytes(1); // Simulate techID = 1
            mockSession
                .Setup(s => s.TryGetValue("techID", out techIDBytes))
                .Returns(true);

            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(x => x.Session).Returns(mockSession.Object);
            mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(mockHttpContext.Object);

            // Simulate a missing technician in the repository
            mockTechnicianRepo.Setup(repo => repo.Get(1)).Returns((Technician)null);

            var controller = new TechIncidentController(
                mockTechnicianRepo.Object,
                mockIncidentRepo.Object,
                mockHttpContextAccessor.Object
            );

            // Initialize TempData
            var tempData = new TempDataDictionary(
                mockHttpContext.Object,
                Mock.Of<ITempDataProvider>()
            );
            controller.TempData = tempData;

            // Act
            var result = controller.Edit(1);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            Assert.Equal("Technician not found. Please select a technician.", controller.TempData["message"]);
        }

        [Fact]
        public void Edit_ReturnsViewResult_WithCorrectModel_WhenTechnicianAndIncidentExist()
        {
            // Arrange
            var mockTechnicianRepo = new Mock<IRepository<Technician>>();
            var mockIncidentRepo = new Mock<IRepository<Incident>>();
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var mockSession = new Mock<ISession>();

            // Simulate valid technician and incident
            var technician = new Technician { TechnicianID = 1, Name = "John Doe" };
            var incident = new Incident { IncidentID = 1, Title = "Test Incident", TechnicianID = 1 };

            // Simulate "techID" in session
            byte[] techIDBytes = BitConverter.GetBytes(1); // Converts integer 1 to byte[]
            mockSession
                .Setup(s => s.TryGetValue("techID", out techIDBytes))
                .Returns(true);

            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(x => x.Session).Returns(mockSession.Object);
            mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(mockHttpContext.Object);

            // Simulate repository behaviors
            mockTechnicianRepo.Setup(repo => repo.Get(1)).Returns(technician);
            mockIncidentRepo.Setup(repo => repo.GetAll()).Returns(new List<Incident> { incident }.AsQueryable());

            var controller = new TechIncidentController(
                mockTechnicianRepo.Object,
                mockIncidentRepo.Object,
                mockHttpContextAccessor.Object
            );

            // Initialize TempData
            var tempData = new TempDataDictionary(
                mockHttpContext.Object,
                Mock.Of<ITempDataProvider>()
            );
            controller.TempData = tempData;

            // Act
            var result = controller.Edit(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<TechIncidentViewModel>(viewResult.Model);

            // Verify model data
            Assert.Equal(technician, model.Technician);
            Assert.Equal(incident, model.Incident);
        }
    }
}
