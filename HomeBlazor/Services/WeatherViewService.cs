using HomeBlazor.Data;
using HomeBlazor.Delegates;
using HomeBlazor.Models;

namespace HomeBlazor.Services
{
    public class WeatherViewService : IDisposable
    {
        private WeatherService weatherService;
        private readonly WeatherRepository repository;

        public Weather Current { get; private set; }
        public event WeatherUpdateEvent Updated;

        public WeatherViewService(WeatherService weatherService, WeatherRepository repository)
        {
            this.repository = repository;
            this.weatherService = weatherService;
            Current = weatherService.GetWeather();
            weatherService.WeatherChanged += WeatherService_WeatherChanged;
        }

        private void WeatherService_WeatherChanged(Weather newWeather)
        {
            Current = newWeather;
            Updated?.Invoke();
        }

        public ChartData<float, string> GetTemperatureChartData(DateTime startDate)
        {
            var records = repository.GetWhetherRecords(startDate);
            List<float> vertical = records.Select(x => x.Temperature).ToList();
            List<string> horizontal = records.Select(x => x.Created.ToString("HH:mm")).ToList();
            ChartData<float, string> chartData = new()
            {
                Vertical = vertical,
                Horizontal = horizontal
            };
            return chartData;
        }

        public void Dispose()
        {
            weatherService.WeatherChanged -= WeatherService_WeatherChanged;
        }
    }
}
