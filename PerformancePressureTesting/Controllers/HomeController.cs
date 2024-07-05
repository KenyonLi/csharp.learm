using Microsoft.AspNetCore.Mvc;
using PerformancePressureTesting.ResourceMoniter;

namespace PerformancePressureTesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetWeatherForecast")]
        [ResourceMoniters]
        public IEnumerable<WeatherForecast> Get()
        {
            var rt = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
           .ToArray();
            Thread.Sleep(1000);//休息一秒
            return rt;
        }
    }
}
