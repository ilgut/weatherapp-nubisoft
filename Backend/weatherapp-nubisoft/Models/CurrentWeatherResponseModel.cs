namespace weatherapp_nubisoft.Models;

public class CurrentWeatherResponseModel
{
    public string City { get; set; }
    public string Country { get; set; }
    public string Time { get; set; }
    public double Temperature { get; set; }
    public double TemperatureFeelsLike { get; set; }
    public string IconUrl { get; set; }

    public CurrentWeatherResponseModel(WeatherResponseModel? weatherResponse)
    {
        if (weatherResponse is null)
        {
            throw new ArgumentNullException(nameof(weatherResponse));
        }
        City = weatherResponse.Location.Name;
        Country = weatherResponse.Location.Country;
        Time = weatherResponse.Location.Localtime;
        Temperature = weatherResponse.Current.TempC;
        TemperatureFeelsLike = weatherResponse.Current.FeelsLikeC;
        IconUrl = weatherResponse.Current.Condition.Icon;
    }
}