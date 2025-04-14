using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using weatherapp_nubisoft.Data;

namespace weatherapp_nubisoft.Controllers;

[Authorize]
public class FavouriteCityCRUDController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;

    public FavouriteCityCRUDController(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
        _httpClient = new()
        {
            BaseAddress = new Uri("http://api.weatherapi.com/v1/")
        };
    }
    
    // Returns a list of the current user's favorite cities
    [HttpGet("registered/get-favourite-cities")]
    public async Task<IActionResult> GetFavouriteCities()
    {
        var userId = User.FindFirst("jti")?.Value;
     
        if (userId is null)
            return Unauthorized("Problem with authorizing, try again.");
        
        var favCities = await _context.FavouriteCities
            .Where(fc => fc.AppUserId == userId)
            .ToListAsync();

        return Ok(JsonSerializer.Serialize(new
        {
            UserId = userId,
            FavouriteCities = favCities.Select(fc => fc.CityName)
        }));
    }

    // Adds a new favorite city to the current user
    [HttpPost("registered/add-favourite-city")]
    public async Task<IActionResult> AddFavouriteCity([FromBody] Dictionary<string, string> body)
    {
        if (!body.TryGetValue("cityName", out var cityName) || string.IsNullOrWhiteSpace(cityName))
            return BadRequest("City name is required.");

        var cityResponse = _httpClient
            .GetAsync($"current.json?key={_configuration.GetValue<string>("WeatherApiKey")}&q={cityName}");
        
        if ( cityResponse.Result.StatusCode != System.Net.HttpStatusCode.OK)
            return BadRequest("City doesn't exist");

        var userId = User.FindFirst("jti")?.Value;
        if (userId is null)
            return Unauthorized("Problem with authorizing, try again.");
        
        _context.FavouriteCities.Add(new()
        {
            AppUserId = userId,
            CityName = cityName
        });

        await _context.SaveChangesAsync();
        
        return Ok("Favourite city added");
    }

    // Removes a favorite city by name from the current user
    [HttpDelete("registered/delete-favourite-city")]
    public async Task<IActionResult> DeleteFavouriteCity([FromBody] Dictionary<string, string> body)
    {
        if (!body.TryGetValue("cityName", out var cityName) || string.IsNullOrWhiteSpace(cityName))
            return BadRequest("City name is required.");
        
        var userId = User.FindFirst("jti")?.Value;
        if (userId is null)
            return Unauthorized("Problem with authorizing, try again.");
        
        var cityToRemove = _context.FavouriteCities
            .FirstOrDefault(fc => fc.AppUserId == userId && fc.CityName == cityName);
        
        if (cityToRemove is null)
            return BadRequest("City doesn't exist in your favorites");
        
        _context.FavouriteCities.Remove(cityToRemove);
        
        await _context.SaveChangesAsync();
        
        return Ok("Favourite city deleted");
    }
}