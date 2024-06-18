using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;

namespace DemoJsonSerilizer_ConvertSerialization.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        [HttpGet("JsonSerializer")]
        public IActionResult Get()
        {
            
            var data =  Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            };

            //Serialize
            string serializedData = System.Text.Json
                .JsonSerializer
                .Serialize(data, options:options);

            //Deserialize
            var dataList = System.Text.Json
                .JsonSerializer
                .Deserialize<IEnumerable<WeatherForecast>>(serializedData, options: options);

            return Ok(dataList);
        }

        [HttpGet("JsonConvert")]
        public IActionResult Get2()
        {
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            };
            var data = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            //Serialize
            string serializedData = JsonConvert
                .SerializeObject(data);

            //Deserialize
            var dataList = JsonConvert
                .DeserializeObject<IEnumerable<WeatherForecast>>(serializedData);

            return Ok(dataList);
        }
    }
}
