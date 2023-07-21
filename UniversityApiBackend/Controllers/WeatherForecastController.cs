using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace UniversityApiBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };


        //Esta es la instancia privada que se inicializa en el constructor, tendria que estar en todos los controllers
        private readonly ILogger<WeatherForecastController> _logger;
        //Aqui esta inicializado el logger y le pasa por parametro el controller
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador , User")]
        public IEnumerable<WeatherForecast> Get()
        {
            //de esta forma especifico que me de el nombre del controlador que tuvo el error y la funcion y tipo de error log
            _logger.LogTrace($"{nameof(WeatherForecastController)}-{nameof(Get)} - Trace Level Log"); 
            _logger.LogDebug($"{nameof(WeatherForecastController)}-{nameof(Get)} - Debug Level Log");
            _logger.LogInformation($"{nameof(WeatherForecastController)}-{nameof(Get)} - Information Level Log");
            _logger.LogWarning($"{nameof(WeatherForecastController)}-{nameof(Get)} - Warning Level Log");
            _logger.LogError($"{nameof(WeatherForecastController)}-{nameof(Get)} - Error Level Log");
            _logger.LogCritical($"{nameof(WeatherForecastController)}-{nameof(Get)} - Critical Level Log");




            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}