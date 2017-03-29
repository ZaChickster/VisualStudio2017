using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VisualStudio2017.Domain;
using VisualStudio2017.Domain.DataAccess;
using Microsoft.AspNetCore.Http;

namespace VisualStudio2017.Angular2.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
		private readonly IAppDataAccess _dataAccess;

		public SampleDataController(IAppDataAccess da)
		{
			_dataAccess = da;
		}

		private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }
    }
}
