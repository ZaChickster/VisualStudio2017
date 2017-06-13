using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VisualStudio2017.Backend.Domain;

namespace VisualStudio2017.ReactRedux.Controllers
{
    [Route("api")]
    public class WeatherController : Controller
    {
		private static readonly string[] Summaries = {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("WeatherForecasts")]
        public IEnumerable<WeatherForecast> WeatherForecasts(int startDateIndex)
        {
	        Random rng = new Random();
	        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
	        {
		        DateFormatted = DateTime.Now.AddDays(index + startDateIndex).ToString("d"),
		        TemperatureC = rng.Next(-20, 55),
		        Summary = Summaries[rng.Next(Summaries.Length)]
	        });
        }
	}
}
