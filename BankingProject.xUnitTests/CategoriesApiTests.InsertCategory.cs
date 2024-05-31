using FluentAssertions;
using BankingProject.WebMvc.Controllers;
using BankingProject.WebMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace BankingProject.xUnitTests;


/// <remarks>
///     Bad insertion data scenarios:
///     - Name is NULL
///     - Name is EMPTY STRING
///     - Name contains lesser number of characters than what is required
///     - Name contains more characters than what is allowed
///     - NULL Category object
///     
///     LIMITATIONS OF WORKING WITH InMemory Database
///     - Relationships between Tables are not supported.
///     - EF Core DataAnnotation Validations are not supported.
/// </remarks>

public partial class CategoriesApiTests
{
    [Fact]
    public void InsertCategory_OkResult()
    {
        // ARRANGE
        var dbName = nameof(CategoriesApiTests.InsertCategory_OkResult);
        var logger = Mock.Of<ILogger<CategoriesApiController>>();
        var dbContext = DbContextMocker.GetApplicationDbContext(dbName);
        var apiController = new CategoriesApiController(dbContext, logger);
        Category categoryToAdd = new Category
        {
            CategoryId = 5,           // because inmemory db, identity insert is not supported. provide value.
            CategoryName = "New Category"             // IF = null, then: INVALID!  CategoryName is REQUIRED
        };

        // ACT
        IActionResult actionResultPost = apiController.PostCategory(categoryToAdd).Result;

        // ASSERT - check if the IActionResult is Ok
        Assert.IsType<OkObjectResult>(actionResultPost);

        // ASSERT - check if the Status Code is (HTTP 200) "Ok", (HTTP 201 "Created")
        int expectedStatusCode = (int)System.Net.HttpStatusCode.OK;
        var actualStatusCode = (actionResultPost as OkObjectResult).StatusCode.Value;
        Assert.Equal<int>(expectedStatusCode, actualStatusCode);

        // Extract the result from the IActionResult object.
        var postResult = actionResultPost.Should().BeOfType<OkObjectResult>().Subject;

        // ASSERT - if the result is a CreatedAtActionResult
        Assert.IsType<CreatedAtActionResult>(postResult.Value);

        // Extract the inserted Category object
        Category actualCategory = (postResult.Value as CreatedAtActionResult).Value
                                  .Should().BeAssignableTo<Category>().Subject;

        // ASSERT - if the inserted Category object is NOT NULL
        Assert.NotNull(actualCategory);

        // Check if the inserted object returned from the API matches with the object sent to the API for insertion.
        Assert.Equal(categoryToAdd.CategoryId, actualCategory.CategoryId);
        Assert.Equal(categoryToAdd.CategoryName, actualCategory.CategoryName);

        _testOutputHelper.WriteLine("Category Inserted successfully!");
    }
}
