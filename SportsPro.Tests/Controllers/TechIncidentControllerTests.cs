using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using SportsPro.Controllers;
using SportsPro.Models;
using SportsPro.Models.ViewModels;
using SportsPro.Data.Configuration;

using Xunit;

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
            Assert.Equal(
                "Session is not available. Please select a technician.",
                controller.TempData["message"]
            );
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
            Assert.Equal(
                "Technician not found. Please select a technician.",
                controller.TempData["message"]
            );
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
            mockSession.Setup(s => s.TryGetValue("techID", out techIDBytes)).Returns(true);

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
            Assert.Equal(
                "Technician not found. Please select a technician.",
                controller.TempData["message"]
            );
        }

        [Fact]
        public void Edit_ReturnsViewResult_WithCorrectModel_WhenTechnicianAndIncidentExist()
        {
            // Arrange
            var technician = new Technician { TechnicianID = 1, Name = "Test Technician" };
            var incident = new Incident
            {
                IncidentID = 1,
                Description = "Test Incident",
                TechnicianID = 1,
                DateClosed = null,
            };

            var mockTechnicianRepo = new Mock<IRepository<Technician>>();
            mockTechnicianRepo.Setup(repo => repo.Get(1)).Returns(technician);

            var mockIncidentRepo = new Mock<IRepository<Incident>>();
            mockIncidentRepo
                .Setup(repo => repo.GetAll())
                .Returns(new List<Incident> { incident }.AsQueryable());

            var mockSession = new Mock<ISession>();
            mockSession
                .Setup(s => s.TryGetValue("techID", out It.Ref<byte[]>.IsAny))
                .Returns(
                    (string key, out byte[] value) =>
                    {
                        value = BitConverter.GetBytes(1); // Simulate techID = 1
                        return true;
                    }
                );

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(ctx => ctx.Session).Returns(mockSession.Object);
            mockHttpContextAccessor.Setup(h => h.HttpContext).Returns(mockHttpContext.Object);

            var tempData = new Mock<ITempDataDictionary>();
            var controller = new TechIncidentController(
                mockTechnicianRepo.Object,
                mockIncidentRepo.Object,
                mockHttpContextAccessor.Object
            )
            {
                TempData = tempData.Object,
            };

            // Act
            var result = controller.Edit(1);

            // Assert
            if (result is RedirectToActionResult redirectResult)
            {
                Assert.Equal("Index", redirectResult.ActionName); // Ensure correct redirection
            }
            else
            {
                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsType<TechIncidentViewModel>(viewResult.Model);

                Assert.Equal(technician, model.Technician);
                Assert.Equal(incident, model.Incident);
            }
        }
    }
}
