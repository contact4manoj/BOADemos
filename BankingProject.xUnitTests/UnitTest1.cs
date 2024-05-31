using Microsoft.AspNetCore.Mvc;

namespace BankingProject.xUnitTests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        // ARRANGE
        int a = 10;
        int b = 20;
        int actualResult;
        int expectedResult = 30;

        // ACT
        actualResult = a + b;

        // ASSERT
        Assert.Equal<int>(expected: expectedResult, actual: actualResult);

        Assert.IsType<int>(actualResult);
    }


    [Fact]
    public void CheckIf_Demo_IndexWorks()
    {
        // ARRANGE
        var controller = new BankingProject.WebMvc.Controllers.DemoController();
        var expectedResult = "Manoj Kumar Sharma";

        // ACT
        var actionResult = controller.Index();

        // ASSERT
        Assert.NotNull(actionResult);

        Assert.IsType<OkObjectResult>(actionResult);

        var result = actionResult as OkObjectResult;
        Assert.NotNull(result);
        Assert.Equal((int)System.Net.HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(expectedResult, result.Value);
    }
}