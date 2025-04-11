using System.Text.Json.Serialization;

namespace weatherapp_nubisoft.Models;

public class WeatherResponseModel
{
    [JsonPropertyName("location")]
    public Location? Location { get; set; }

    [JsonPropertyName("current")]
    public CurrentWeather? Current { get; set; }

    [JsonPropertyName("forecast")]
    public Forecast? Forecast { get; set; }
}

public class Location
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("region")]
    public string Region { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("lat")]
    public double Latitude { get; set; }

    [JsonPropertyName("lon")]
    public double Longitude { get; set; }

    [JsonPropertyName("tz_id")]
    public string TimeZoneId { get; set; }

    [JsonPropertyName("localtime_epoch")]
    public long LocaltimeEpoch { get; set; }

    [JsonPropertyName("localtime")]
    public string Localtime { get; set; }
}

public class CurrentWeather
{
    [JsonPropertyName("last_updated_epoch")]
    public long LastUpdatedEpoch { get; set; }

    [JsonPropertyName("last_updated")]
    public string LastUpdated { get; set; }

    [JsonPropertyName("temp_c")]
    public double TempC { get; set; }

    [JsonPropertyName("temp_f")]
    public double TempF { get; set; }

    [JsonPropertyName("is_day")]
    public int IsDay { get; set; }

    [JsonPropertyName("condition")]
    public WeatherCondition Condition { get; set; }

    [JsonPropertyName("wind_mph")]
    public double WindMph { get; set; }

    [JsonPropertyName("wind_kph")]
    public double WindKph { get; set; }

    [JsonPropertyName("wind_degree")]
    public int WindDegree { get; set; }

    [JsonPropertyName("wind_dir")]
    public string WindDir { get; set; }

    [JsonPropertyName("pressure_mb")]
    public double PressureMb { get; set; }

    [JsonPropertyName("pressure_in")]
    public double PressureIn { get; set; }

    [JsonPropertyName("precip_mm")]
    public double PrecipMm { get; set; }

    [JsonPropertyName("precip_in")]
    public double PrecipIn { get; set; }

    [JsonPropertyName("humidity")]
    public int Humidity { get; set; }

    [JsonPropertyName("cloud")]
    public int Cloud { get; set; }

    [JsonPropertyName("feelslike_c")]
    public double FeelsLikeC { get; set; }

    [JsonPropertyName("feelslike_f")]
    public double FeelsLikeF { get; set; }

    [JsonPropertyName("windchill_c")]
    public double WindChillC { get; set; }

    [JsonPropertyName("windchill_f")]
    public double WindChillF { get; set; }

    [JsonPropertyName("heatindex_c")]
    public double HeatIndexC { get; set; }

    [JsonPropertyName("heatindex_f")]
    public double HeatIndexF { get; set; }

    [JsonPropertyName("dewpoint_c")]
    public double DewPointC { get; set; }

    [JsonPropertyName("dewpoint_f")]
    public double DewPointF { get; set; }

    [JsonPropertyName("vis_km")]
    public double VisibilityKm { get; set; }

    [JsonPropertyName("vis_miles")]
    public double VisibilityMiles { get; set; }

    [JsonPropertyName("uv")]
    public double UV { get; set; }

    [JsonPropertyName("gust_mph")]
    public double GustMph { get; set; }

    [JsonPropertyName("gust_kph")]
    public double GustKph { get; set; }
}

public class WeatherCondition
{
    [JsonPropertyName("text")]
    public string Text { get; set; }

    [JsonPropertyName("icon")]
    public string Icon { get; set; }

    [JsonPropertyName("code")]
    public int Code { get; set; }
}

public class Forecast
{
    [JsonPropertyName("forecastday")]
    public List<ForecastDay> ForecastDay { get; set; }
}

public class ForecastDay
{
    [JsonPropertyName("date")]
    public string Date { get; set; }

    [JsonPropertyName("date_epoch")]
    public long DateEpoch { get; set; }

    [JsonPropertyName("day")]
    public DayForecast Day { get; set; }

    [JsonPropertyName("astro")]
    public Astro Astro { get; set; }

    [JsonPropertyName("hour")]
    public List<HourlyForecast> Hour { get; set; }
}

public class DayForecast
{
    [JsonPropertyName("maxtemp_c")]
    public double MaxTempC { get; set; }

    [JsonPropertyName("mintemp_c")]
    public double MinTempC { get; set; }

    [JsonPropertyName("avgtemp_c")]
    public double AvgTempC { get; set; }

    [JsonPropertyName("maxwind_kph")]
    public double MaxWindKph { get; set; }

    [JsonPropertyName("totalprecip_mm")]
    public double TotalPrecipMm { get; set; }

    [JsonPropertyName("avghumidity")]
    public double AvgHumidity { get; set; }

    [JsonPropertyName("condition")]
    public WeatherCondition Condition { get; set; }

    [JsonPropertyName("uv")]
    public double UV { get; set; }
}

public class Astro
{
    [JsonPropertyName("sunrise")]
    public string Sunrise { get; set; }

    [JsonPropertyName("sunset")]
    public string Sunset { get; set; }

    [JsonPropertyName("moonrise")]
    public string Moonrise { get; set; }

    [JsonPropertyName("moonset")]
    public string Moonset { get; set; }

    [JsonPropertyName("moon_phase")]
    public string MoonPhase { get; set; }

    [JsonPropertyName("moon_illumination")]
    public string MoonIllumination { get; set; }
}

public class HourlyForecast
{
    [JsonPropertyName("time_epoch")]
    public long TimeEpoch { get; set; }

    [JsonPropertyName("time")]
    public string Time { get; set; }

    [JsonPropertyName("temp_c")]
    public double TempC { get; set; }

    [JsonPropertyName("is_day")]
    public int IsDay { get; set; }

    [JsonPropertyName("condition")]
    public WeatherCondition Condition { get; set; }

    [JsonPropertyName("wind_kph")]
    public double WindKph { get; set; }

    [JsonPropertyName("wind_degree")]
    public int WindDegree { get; set; }

    [JsonPropertyName("wind_dir")]
    public string WindDir { get; set; }

    [JsonPropertyName("pressure_mb")]
    public double PressureMb { get; set; }

    [JsonPropertyName("precip_mm")]
    public double PrecipMm { get; set; }

    [JsonPropertyName("humidity")]
    public int Humidity { get; set; }

    [JsonPropertyName("cloud")]
    public int Cloud { get; set; }

    [JsonPropertyName("feelslike_c")]
    public double FeelsLikeC { get; set; }

    [JsonPropertyName("dewpoint_c")]
    public double DewPointC { get; set; }

    [JsonPropertyName("will_it_rain")]
    public int WillItRain { get; set; }

    [JsonPropertyName("chance_of_rain")]
    public int ChanceOfRain { get; set; }

    [JsonPropertyName("vis_km")]
    public double VisibilityKm { get; set; }

    [JsonPropertyName("gust_kph")]
    public double GustKph { get; set; }

    [JsonPropertyName("uv")]
    public double UV { get; set; }
}
