using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web8_SecuredWebApi01.Models;

namespace Web8_SecuredWebApi01.Controllers;

[Authorize]                                             // Authorization Filter
[Route("api/[controller]")]
[ApiController]
public class SecuredPeopleController : ControllerBase
{
    private ILogger<SecuredPeopleController> _logger;

    public SecuredPeopleController(ILogger<SecuredPeopleController> logger)
    {
        _logger = logger;
    }


    // define the Route Path as endpoints not registered in Main()
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]           // on Authentication error
    [ProducesResponseType(StatusCodes.Status403Forbidden)]              // on Authorization error
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Person>))]
    [HttpGet(Name = "GetSecuredPeople")]
    public IActionResult Get()
    {
        Person[] persons = new[]
        {
            new Person { Name = "Employee 1", Age = 20 },
            new Person { Name = "Employee 2", Age = 27, Gender = Gender.Male },
            new Person { Name = "Employee 3", Age = 32, Gender = Gender.Female },
            new Person { Name = "Employee 4", Age = 46, Gender = Gender.Male },
            new Person { Name = "Employee 5", Age = 18, Gender = Gender.Female }
        };

        _logger.LogInformation("extracting all employees");

        return Ok(persons);
    }


    [AllowAnonymous]                        // Authentication Filter
    [HttpPost]
    public IActionResult Post(Person person)
    {
        _logger.LogInformation("Inserted new person!");
        return Ok();
    }


    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Manager")]            // Authorization Filter
    public IActionResult Delete(int id)
    {
        return Ok();
    }
}
