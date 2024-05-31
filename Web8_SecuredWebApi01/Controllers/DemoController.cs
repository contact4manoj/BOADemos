using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web8_SecuredWebApi01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class DemoController : ControllerBase
    {
        [HttpGet("FirstApi")]
        public IActionResult FirstApi()
        {
            return Ok("hello world");
        }

        [AllowAnonymous]
        [HttpPost("LoginPost")]
        public IActionResult PostLogin(string username, string password)
        {
            return Ok();
        }

        // /api/Demo/LoginGet?username=demo&password=Password@1234
        [AllowAnonymous]
        [HttpGet("LoginGet")]
        public IActionResult GetLogin(string username, string password)
        {
            return Ok();
        }

        [Authorize]
        [HttpGet("SecondApiWithAuthentication")]
        public IActionResult SecondApiWithAuthentication()
        {
            return Ok("another world");
        }
    }
}
