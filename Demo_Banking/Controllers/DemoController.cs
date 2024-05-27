using Microsoft.AspNetCore.Mvc;

namespace Demo_Banking.Controllers
{
    public class DemoController : Controller
    {
        public DemoController()
        {

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
    }
}
