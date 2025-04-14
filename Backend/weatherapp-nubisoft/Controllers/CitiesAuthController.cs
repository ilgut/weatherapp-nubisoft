using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using weatherapp_nubisoft.Data;
using weatherapp_nubisoft.Models;

namespace weatherapp_nubisoft.Controllers;

[Authorize]
public class CitiesAuthController : Controller
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;

    public CitiesAuthController(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
        _httpClient = new()
        {
            BaseAddress = new Uri("http://api.weatherapi.com/v1/")
        };
    }

    // Gets current weather data for a single city provided in query
    [HttpGet("registered/current")]
    public async Task<IActionResult> GetCurrentWeatherForCity([FromQuery] string city)
    {
        var userId = User.FindFirst("jti")?.Value;
        
        if (userId is null)
            return Unauthorized("Problem with authorizing, try again.");
        
        var weatherRequestResponse = _httpClient.GetAsync(
            $"current.json?key={_configuration.GetValue<string>("WeatherApiKey")}&q={city}");
        
        var weatherJson = await weatherRequestResponse.Result.Content.ReadAsStringAsync();
            
        if (weatherRequestResponse.Result.StatusCode != System.Net.HttpStatusCode.OK)
            return BadRequest("Could not retrieve weather data");

        var weatherDeserialized = JsonSerializer.Deserialize<WeatherResponseModel>(weatherJson);
        
        CurrentWeatherResponseModel weatherResponse = new (weatherDeserialized);
        
        return Ok(JsonSerializer.SerializeToNode(new [] { weatherResponse }));
    }
    
    // Gets forecast data for a single city provided in query
    // Default behavior : forecast for 1 day
    [HttpGet("registered/forecast")]
    public async Task<IActionResult> GetForecastWeatherForCity([FromQuery] string city, [FromQuery] int days = 1)
    {
        var userId = User.FindFirst("jti")?.Value;
        
        if (userId is null)
            return Unauthorized("Problem with authorizing, try again.");
        
        var weatherRequestResponse = _httpClient.GetAsync(
            $"forecast.json?key={_configuration.GetValue<string>("WeatherApiKey")}&q={city}&days={days}");
        
        var weatherJson = await weatherRequestResponse.Result.Content.ReadAsStringAsync();
            
        if (weatherRequestResponse.Result.StatusCode != System.Net.HttpStatusCode.OK)
            return BadRequest("Could not retrieve weather data");

        var weatherDeserialized = JsonSerializer.Deserialize<WeatherResponseModel>(weatherJson);
        
        ForecastWeatherResponseModel weatherResponse = new (weatherDeserialized, days);
        
        return Ok(JsonSerializer.SerializeToNode(new [] { weatherResponse }));
    }
    
    // Gets current weather for all favorite cities of the logged-in user
    [HttpGet("registered/weather-favourite-cities-current")]
    public async Task<IActionResult> GetWeatherFavouriteCitiesCurrent()
    {
        var userId = User.FindFirst("jti")?.Value;
        
        if (userId is null)
            return Unauthorized("Problem with authorizing, try again.");
        
        var favoriteCities = await _context.FavouriteCities
            .Where(fc => fc.AppUserId == userId)
            .Select(fc => fc.CityName)
            .ToListAsync();

        var weatherBroadcast = new List<CurrentWeatherResponseModel>();
        
        foreach (var city in favoriteCities)
        {
            var weatherResponse = _httpClient.GetAsync(
                $"current.json?key={_configuration.GetValue<string>("WeatherApiKey")}&q={city}");
            
            var weatherJson = await weatherResponse.Result.Content.ReadAsStringAsync();
            
            if (weatherResponse.Result.StatusCode != System.Net.HttpStatusCode.OK)
                return BadRequest("Could not retrieve weather data");
            
            var weatherDeserialized = JsonSerializer.Deserialize<WeatherResponseModel>(weatherJson);
            
            weatherBroadcast.Add(new CurrentWeatherResponseModel(weatherDeserialized));
        }
        
        return Ok(JsonSerializer.SerializeToNode(weatherBroadcast));
    }

    // Gets forecast for all favorite cities of the logged-in user
    // default behavior : forecast for 1 day
    [HttpGet("registered/weather-favourite-cities-forecast")]
    public async Task<IActionResult> GetWeatherFavouriteCitiesForecast([FromQuery] int days = 1)
    {
        var userId = User.FindFirst("jti")?.Value;
        
        if (userId is null)
            return Unauthorized("Problem with authorizing, try again.");
        
        var favoriteCities = await _context.FavouriteCities
            .Where(fc => fc.AppUserId == userId)
            .Select(fc => fc.CityName)
            .ToListAsync();

        var weatherBroadcast = new List<ForecastWeatherResponseModel>();
        
        foreach (var city in favoriteCities)
        {
            var weatherResponse = _httpClient.GetAsync(
                $"forecast.json?key={_configuration.GetValue<string>("WeatherApiKey")}&q={city}&days={days}");
            
            var weatherJson = await weatherResponse.Result.Content.ReadAsStringAsync();
            
            if (weatherResponse.Result.StatusCode != System.Net.HttpStatusCode.OK)
                return BadRequest("Could not retrieve weather data");
            
            var weatherDeserialized = JsonSerializer.Deserialize<WeatherResponseModel>(weatherJson);
            
            weatherBroadcast.Add(new ForecastWeatherResponseModel(weatherDeserialized, days));
        }
        
        return Ok(JsonSerializer.SerializeToNode(weatherBroadcast));
    }
    
    
}