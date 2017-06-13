using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VisualStudio2017.Domain;
using VisualStudio2017.Domain.DataAccess;

namespace VisualStudio2017.Angular2.Controllers
{
	[Route("api")]
    public class SampleDataController : Controller
    {
		private readonly IAppDataAccess _dataAccess;

		public SampleDataController(IAppDataAccess da)
		{
			_dataAccess = da;
		}

		private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("WeatherForecasts")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            Random rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }
    }
}
