namespace weatherapp_nubisoft.Models;

public class ForecastWeatherResponseModel
{
    public string City { get; set; }
    public string Country { get; set; }
    public List<ForecastDayWeatherResponseModel> Forecast { get; set; }

    public ForecastWeatherResponseModel(WeatherResponseModel weather, int days)
    {
        if (weather is null)
        {
            throw new ArgumentNullException(nameof(weather));
        }

        if (weather.Forecast is null)
        {
            throw new ArgumentNullException(nameof(weather.Forecast));
        }

        City = weather.Location.Name;
        Country = weather.Location.Country;
        Forecast = new List<ForecastDayWeatherResponseModel>();

        for (var i = 0; i < days; i++)
        { 
            ForecastDayWeatherResponseModel forecastDay = new()
            {
                Date = weather.Forecast.ForecastDay[i].Date,
                MinTemperature = weather.Forecast.ForecastDay[i].Day.MinTempC,
                MaxTemperature = weather.Forecast.ForecastDay[i].Day.MaxTempC,
                Condition = weather.Forecast.ForecastDay[i].Day.Condition.Text,
                Icon = weather.Forecast.ForecastDay[i].Day.Condition.Icon
            };
        
            Forecast.Add(forecastDay);
        }
    }
}

public class ForecastDayWeatherResponseModel
{
    public string Date { get; set; }
    public double MinTemperature { get; set; }
    public double MaxTemperature { get; set; }
    public string Condition { get; set; }
    public string Icon { get; set; }
}