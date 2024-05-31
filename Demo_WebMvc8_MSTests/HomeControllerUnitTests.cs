using Demo_WebMvc8.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Demo_WebMvc8_MSTests;

[TestClass]
public class HomeControllerUnitTests
{
    [TestMethod]
    public void Testing_Index()
    {
        // Arrange
        var logger = Mock.Of<ILogger<HomeController>>();
        var controller = new HomeController(logger);
        var desiredViewResultName = "Index";

        // Act
        ViewResult? result = controller.Index() as ViewResult;

        // Assert
        Assert.IsNotNull(result);

        Assert.AreEqual(expected: desiredViewResultName, actual: result.ViewName);
    }
}