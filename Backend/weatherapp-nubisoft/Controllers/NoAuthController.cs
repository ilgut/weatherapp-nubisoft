using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using weatherapp_nubisoft.Models;

namespace weatherapp_nubisoft.Controllers;

public class NoAuthController : Controller
{
    private static HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public NoAuthController(IConfiguration configuration)
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
        var weatherGliwice = JsonSerializer.Deserialize<WeatherResponseModel>
            (await _httpClient.GetAsync("current.json?key=" 
                                       + _configuration.GetValue<string>("WeatherApiKey") + "&q=Gliwice")
                .Result.Content.ReadAsStringAsync());
        
        var weatherHamburg =  JsonSerializer.Deserialize<WeatherResponseModel>
        (await _httpClient.GetAsync("current.json?key=" 
                                   + _configuration.GetValue<string>("WeatherApiKey") + "&q=Hamburg")
            .Result.Content.ReadAsStringAsync());

        CurrentWeatherResponseModel resultGliwice = new (weatherGliwice);
        CurrentWeatherResponseModel resultHamburg = new (weatherHamburg);
        
        return Ok(JsonSerializer.SerializeToNode(new [] { resultGliwice, resultHamburg }));
    }
}