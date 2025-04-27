namespace WeatherBackend.Models
{
    public class WeatherData
    {
        public DateTime Date { get; set; }
        public List<WeatherPeriod> Periods { get; set; } = new();
    }

    public class WeatherPeriod
    {
        public string Period { get; set; } = "";
        public string Time { get; set; } = "";
        public double Temperature { get; set; }
        public string Description { get; set; } = "";
        public int PrecipitationProbability { get; set; }
    }
}
