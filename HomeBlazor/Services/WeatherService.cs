using HomeBlazor.Data;
using HomeBlazor.Delegates;
using HomeBlazor.Models;

namespace HomeBlazor.Services
{
    public class WeatherService
    {
        private static Weather LastWeather = new();
        private readonly IServiceProvider serviceProvider;

        public event WeatherEvent WeatherChanged;

        public WeatherService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task PushData(string jsonData, bool retain)
        {
            var obj = jsonData.Deserialize<Weather>();
            if(obj == null)
            {
                return;
            }
            LastWeather = obj;
            NotifyWeatherChanged();
            if(!retain)
            {
                await LogWeather(obj);
            }
        }

        private async Task LogWeather(Weather obj)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<WeatherRepository>();
                await repository.AddWeatherRecord(obj);
            }
        }

        public Weather GetWeather()
        {
            return LastWeather.Clone();
        }

        private void NotifyWeatherChanged()
        {
            WeatherChanged?.Invoke(LastWeather.Clone());
        }
    }
}
