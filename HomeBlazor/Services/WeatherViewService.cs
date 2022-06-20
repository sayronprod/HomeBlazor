using HomeBlazor.Data;
using HomeBlazor.Data.Models;
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

        public ChartData GetChartData(ChartParameter vertical, ChartParameter horizontal, DateTime startDate)
        {
            List<WeatherDbo> records = repository.GetWhetherRecords(startDate);
            List<float> verticalData = GetDataByTypeParameter(records, vertical).ToList();
            List<string> horizontalData = GetDataByTypeParameterForHorizontal(records, horizontal).ToList();
            ChartData result = new()
            {
                Vertical = verticalData,
                Horizontal = horizontalData
            };
            return result;
        }

        public void Dispose()
        {
            weatherService.WeatherChanged -= WeatherService_WeatherChanged;
        }

        private IEnumerable<float> GetDataByTypeParameter(List<WeatherDbo> records, ChartParameter parameter)
        {
            IEnumerable<float> result = null;
            switch (parameter)
            {
                case ChartParameter.Temperature:
                    result = records.Select(x => x.Temperature);
                    break;
                case ChartParameter.Pressure:
                    result = records.Select(x => x.Pressure);
                    break;
                default:
                    break;
            }
            return result;
        }

        private IEnumerable<string> GetDataByTypeParameterForHorizontal(List<WeatherDbo> records, ChartParameter parameter)
        {
            IEnumerable<string> result = null;
            {
                switch (parameter)
                {
                    case ChartParameter.Temperature:
                        result = records.Select(x => x.Temperature.ToString());
                        break;
                    case ChartParameter.Pressure:
                        result = records.Select(x => x.Pressure.ToString());
                        break;
                    case ChartParameter.Time:
                        result = records.Select(x => x.Created.ToString("HH:mm"));
                        break;
                    default:
                        break;
                }
            }
            return result;
        }
    }
}
