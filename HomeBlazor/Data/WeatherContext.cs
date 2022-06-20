using HomeBlazor.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeBlazor.Data
{
    public class WeatherContext : DbContext
    {
        public DbSet<WeatherDbo> WeatherRecords { get; set; }

        public WeatherContext() : base()
        {
        }

        public WeatherContext(DbContextOptions<WeatherContext> options) : base(options)
        {
            Database.SetCommandTimeout(360000);
        }
    }
}
