using Microsoft.AspNetCore.Mvc;

namespace Demo_Banking.Controllers
{
    public class WebApiClientDemo : Controller
    {
        public class WeatherForecastDtoModel
        {
            public DateOnly Date { get; set; }

            public int TemperatureC { get; set; }

            public int TemperatureF { get; set; }

            public string? Summary { get; set; }
        }


        public async Task<IActionResult> Index()
        {
            const string apiURL = "https://localhost:7250/WeatherForecast";
            using HttpClient client = new ();

            var data = await client.GetFromJsonAsync<IEnumerable<WeatherForecastDtoModel>>(apiURL);

            return View(data);
        }
    }
}
