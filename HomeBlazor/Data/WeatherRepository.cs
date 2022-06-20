using AutoMapper;
using HomeBlazor.Data.Models;
using HomeBlazor.Models;

namespace HomeBlazor.Data
{
    public class WeatherRepository : IDisposable
    {
        private readonly WeatherContext context;
        private readonly IMapper mapper;

        public WeatherRepository(WeatherContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<WeatherDbo> AddWeatherRecord(Weather weather)
        {
            WeatherDbo result = null;
            WeatherDbo record = mapper.Map<WeatherDbo>(weather);
            var addedRecord = await context.WeatherRecords.AddAsync(record);
            await context.SaveChangesAsync();
            result = addedRecord.Entity;
            return result;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public List<WeatherDbo> GetWhetherRecords(DateTime startDate)
        {
            List<WeatherDbo> result = context.WeatherRecords.Where(x => x.Created > startDate).ToList();
            return result;
        }
    }
}
