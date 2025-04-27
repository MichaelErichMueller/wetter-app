using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using WeatherBackend.Models;

namespace WeatherBackend.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "5403c4bf7ee3bcb7b7fe1b66b106100d";

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<WeatherData>> GetWeatherForecastAsync(string city)
        {
            var url = $"https://api.openweathermap.org/data/2.5/forecast?q={city}&units=metric&appid={_apiKey}&lang=de";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(json);
            var dailyMap = new Dictionary<string, List<WeatherPeriod>>();

            foreach (var item in doc.RootElement.GetProperty("list").EnumerateArray())
            {
                var dateTimeStr = item.GetProperty("dt_txt").GetString();
                if (dateTimeStr == null) continue;

                var dateTime = DateTime.Parse(dateTimeStr);
                var dateKey = dateTime.ToString("yyyy-MM-dd");
                var hour = dateTime.Hour;
                string period = hour switch
                {
                    >= 0 and < 6 => "Nacht",
                    < 12 => "Morgen",
                    < 18 => "Mittag",
                    _ => "Abend"
                };

                var temperature = item.GetProperty("main").GetProperty("temp").GetDouble();
                var description = item.GetProperty("weather")[0].GetProperty("description").GetString() ?? "";
                var precipitation = item.TryGetProperty("pop", out var popVal) ? (int)(popVal.GetDouble() * 100) : 0;

                if (!dailyMap.ContainsKey(dateKey))
                    dailyMap[dateKey] = new List<WeatherPeriod>();
                if (!dailyMap[dateKey].Any(p => p.Period == period))
                {
                    dailyMap[dateKey].Add(new WeatherPeriod
                    {
                        Period = period,
                        Time = dateTime.ToString("HH:mm"),
                        Temperature = temperature,
                        Description = description,
                        PrecipitationProbability = precipitation
                    });
                }
            }
            var result = dailyMap.Select(day => new WeatherData
            {
                Date = DateTime.Parse(day.Key),
                Periods = day.Value
            });

            return result;
        }
    }
}
