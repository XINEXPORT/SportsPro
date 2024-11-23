using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsPro.Controllers;
using SportsPro.Data;
using SportsPro.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SportsPro.Tests.Controllers
{
    [TestClass]
    public class TechIncidentControllerTests
    {
        [TestMethod]
        public void Edit_ReturnsRedirect_WhenSessionIsNull()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<Technician>>();
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            mockHttpContextAccessor.Setup(x => x.HttpContext.Session).Returns((ISession)null);

            var controller = new TechIncidentController(mockRepo.Object, null, mockHttpContextAccessor.Object);

            // Act
            var result = controller.Edit(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }
    }
}
