using Microsoft.AspNetCore.Mvc;

namespace Demo_Banking.Controllers
{
    public class DemoController : Controller
    {
        private readonly ILogger<DemoController> _logger;

        public DemoController(ILogger<DemoController> logger)
        {
            _logger = logger;
        }

        // GET https://localhost:xxxx/Demo/Index
        public string Index()
        {
            return "hello world";
        }

        // GET https://localhost:xxxx/Demo/Add?a=10&b=20&name=Manoj%20Kumar%20Sharma HTTP/1.1
        [HttpGet]
        public string Add(int a, int b, string name)
        {
            // String Interpolation
            return $"Hi {name.ToUpper()}, the sum of {a} and {b} = {a + b}";
        }

        [HttpGet]
        public ActionResult Greet()
        {
            // return new OkResult();
            return new OkObjectResult("hello world");
        }

        [HttpGet]
        public IActionResult GreetAsync()
        {
            return new OkObjectResult("Hello world asynchronously");
        }

        [HttpGet]
        public IActionResult GenerateView()
        {
            _logger.LogInformation("Generating content for view");
            // return View();                   // /Views/Demo/GenerateView.cshtml
            return View(viewName: "DemoView");  // /Views/Demo/DemoView.cshtml
        }

        [HttpGet]
        public IActionResult GenerateInfo()
        {
            var employeeViewModel = new
            {
                Id = 10,
                Name = "First Employee"
            };
            return View(employeeViewModel);
            // return View(viewName: "GenerateInfo", model: employeeModel);
        }

        [HttpGet]
        public IActionResult GenerateEmployees()
        {
            Demo_Banking.ViewModels.EmployeeViewModel employeeViewModel
                = new ViewModels.EmployeeViewModel()
                {
                    Id = 20,
                    Name = "Second Employee"
                };

            // return View(employeeViewModel);
            return View("EditEmployee", employeeViewModel);
        }


        [HttpPost]
        public IActionResult GenerateEmployees(ViewModels.EmployeeViewModel inputModel)
        {
            return View("EditEmployee", inputModel);
        }
    }
}
