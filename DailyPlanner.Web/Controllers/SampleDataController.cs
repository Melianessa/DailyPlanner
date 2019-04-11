using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace DailyPlanner.Web.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private readonly ILogger _logger;
        public SampleDataController(ILogger<SampleDataController> logger)
        {
            _logger = logger;
        }
        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            try
            {
                for (int i = 0; i < 6; i++)
                {
                    var a = new int[2];
                    a[i] = i;
                    Console.Write(a[i]);
                }
                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                });
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Error in WeatherForecasts method: {e.Message}");
            }
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5);
                }
            }
        }
    }
}
