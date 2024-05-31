using Microsoft.AspNetCore.Mvc;

namespace BankingProject.WebMvc.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            return Ok("Manoj Kumar Sharma");
        }
    }
}
