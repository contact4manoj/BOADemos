using Xunit.Abstractions;

namespace BankingProject.xUnitTests;

// Add the following NuGet Packages:
//      Microsoft.EntityFrameworkCore.InMemory      (same version as EF Core in WebApp)
//      Moq                                         (latest version)
//      FluentAssertions                            (latest version)
// Also add Project Reference to the WebApp

public partial class CategoriesApiTests
{

    private readonly ITestOutputHelper _testOutputHelper;

    public CategoriesApiTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

}
