using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using weatherapp_nubisoft.Models;

namespace weatherapp_nubisoft.Controllers;

public class CitiesNoAuthController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public CitiesNoAuthController(IConfiguration configuration)
    {
        _configuration = configuration;
        _httpClient = new()
        {
            BaseAddress = new Uri("http://api.weatherapi.com/v1/")
        };
    }
    
    [AllowAnonymous]
    [HttpGet("realtime-weather")]
    public async Task<IActionResult> RealTimeWeather()
    {
        var responseGliwice = _httpClient.GetAsync(
                $"current.json?key={_configuration.GetValue<string>("WeatherApiKey")}&q=Gliwice");
        
        var responseHamburg = _httpClient.GetAsync(
                $"current.json?key={_configuration.GetValue<string>("WeatherApiKey")}&q=Hamburg");
        
        var weatherGliwice = JsonSerializer.Deserialize<WeatherResponseModel>(await responseGliwice.Result.Content.ReadAsStringAsync());
        
        var weatherHamburg =  JsonSerializer.Deserialize<WeatherResponseModel>(await responseHamburg.Result.Content.ReadAsStringAsync());

        if (weatherHamburg is null || weatherGliwice is null)
        {
            return NotFound();
        }
        
        CurrentWeatherResponseModel resultGliwice = new (weatherGliwice);
        CurrentWeatherResponseModel resultHamburg = new (weatherHamburg);
        
        return Ok(JsonSerializer.SerializeToNode(new [] { resultGliwice, resultHamburg }));
    }

    // default behavior : forecast for 1 day
    [AllowAnonymous]
    [HttpGet("forecast-weather")]
    public async Task<IActionResult> ForecastWeather([FromQuery] int days = 1)
    {
        // trial version allows only for max 3 days forecast
        if (days < 1 || days > 3)
        {
            return BadRequest("Days must be between 1 and 3");
        }

        var responseGliwice = _httpClient.GetAsync(
                $"forecast.json?key={_configuration.GetValue<string>("WeatherApiKey")}&q=Gliwice&days={days}");
        
        var responseHamburg = _httpClient.GetAsync(
                $"forecast.json?key={_configuration.GetValue<string>("WeatherApiKey")}&q=Hamburg&days={days}");
        
        var weatherGliwice = JsonSerializer.Deserialize<WeatherResponseModel>(await responseGliwice.Result.Content.ReadAsStringAsync());
        var weatherHamburg =  JsonSerializer.Deserialize<WeatherResponseModel>(await responseHamburg.Result.Content.ReadAsStringAsync());

        if (weatherHamburg is null || weatherGliwice is null)
        {
            return NotFound();
        }
        
        ForecastWeatherResponseModel forecastGliwice = new (weatherGliwice, days);
        ForecastWeatherResponseModel forecastHamburg = new (weatherHamburg, days);

        return Ok(JsonSerializer.Serialize(new [] { forecastGliwice, forecastHamburg }));
    }
}