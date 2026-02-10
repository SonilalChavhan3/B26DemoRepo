using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WebApplication.Controllers;

namespace NUnitTestProject
{

    [TestFixture]
    public class HomeControllerTests
    {
        private Mock<ILogger<HomeController>> _mockLogger = null!;

        [SetUp]
        public void SetUp()
        {
            _mockLogger = new Mock<ILogger<HomeController>>();
        }

        [Test]
        public void BhartiPawar_Returns_ViewResult()
        {
            // Arrange
            var controller = new HomeController(_mockLogger.Object);

            // Act
            var result = controller.BhartiPawar();

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        [Test]
        public void BhartiPawar_Returns_DefaultViewName()
        {
            // Arrange
            var controller = new HomeController(_mockLogger.Object);

            // Act
            var result = controller.BhartiPawar() as ViewResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(string.IsNullOrEmpty(result!.ViewName), Is.True, "Expected the action to return the default view (null or empty ViewName).");
        }

        [Test]
        public void BhartiPawar_Works_When_Logger_Is_Null()
        {
            // Arrange
            var controller = new HomeController(null);

            // Act
            var result = controller.BhartiPawar();

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        [Test]
        public void BhartiPawar_DoesNot_Modify_ModelState()
        {
            // Arrange
            var controller = new HomeController(_mockLogger.Object);

            // Act
            var result = controller.BhartiPawar();

            // Assert
            Assert.That(controller.ModelState.IsValid, Is.True);
        }
    }
}
