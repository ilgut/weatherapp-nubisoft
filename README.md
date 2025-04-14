# weatherapp-nubisoft

WeatherApp is a demonstration web API built with ASP.NET Core and Entity Framework Core. The application provides current and forecast weather data using WeatherAPI and supports authenticated user profiles with JWT-based authentication.

## Features

- JWT-based user authentication
- User registration and login
- Search current weather for any city
- Get forecast data (up to 3 days)
- Save favorite cities to your user profile
- Retrieve weather data for all saved cities
- Entity Framework Core with SQLite database
- Weather data provided by WeatherAPI

## How to Run the Application

### 1. Clone the repository

```
git clone https://github.com/your-username/weatherapp-nubisoft.git
cd weatherapp-nubisoft
```
### 2. (IMPORTANT) Configure WeatherAPI Key
Edit appsettings.json and provide an environment variable with your API key from https://www.weatherapi.com. You'll need to paste the API key in "WeatherApiKey" field.

### 3. Open .sln file and run the app
The application will start and be accessible at http://localhost:5134

### 4. Create an account using http://localhost:5134/register endpoint
Optionally you can use already predefined account:
email: elijah@company.com
password: Zaq12wsx!

### 5. Log in using /login endpoint and look up the weather in your town!
Don't forget to include the JWT in the Authorization header when calling protected endpoints

## API Endpoints
### Public (for demo purposes, no authentication required)
#### GET /realtime-weather
Returns current weather for Gliwice and Hamburg.

#### GET /forecast-weather?days={numberOfDays}
Returns forecast for Gliwice and Hamburg (up to 3 days).

### Authenticated (requires JWT token)
#### GET /registered/current?city=CityName
Gets current weather for a specified city.

#### GET /registered/forecast?city=CityName&days={numberOfDays}
Gets forecast for a specified city (1 to 3 days supported).

#### GET /registered/weather-favourite-cities-current
Gets current weather for all user's favorite cities.

#### GET /registered/weather-favourite-cities-forecast?days={numberOfDays}
Gets forecast for all favorite cities (1 to 3 days).

#### GET /registered/get-favourite-cities
Lists the current user's favorite cities.

#### POST /registered/add-favourite-city
Adds a new city to the current user's favorites. Accepts a body like: { "cityName": "CityName" }

#### DELETE /registered/delete-favourite-city?city=CityName
Removes a favorite city from the current user's profile.


## Technologies Used
- ASP.NET Core Web API

- Entity Framework Core

- SQLite

- Microsoft Identity for user management

- JWT (JSON Web Tokens)

- WeatherAPI (weather data source)



