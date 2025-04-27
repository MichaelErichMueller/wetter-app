using HotChocolate;
using WeatherBackend.Models;
using WeatherBackend.Services;

namespace WeatherBackend.GraphQL
{
    public class WeatherQuery
    {
        public async Task<IEnumerable<WeatherData>> GetWeather(
            [Service] WeatherService weatherService, 
            string city)
        {
            return await weatherService.GetWeatherForecastAsync(city);
        }
    }
}
