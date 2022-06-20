using AutoMapper;
using HomeBlazor.Data.Models;
using HomeBlazor.Models;

namespace HomeBlazor.Mapping
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Weather, WeatherDbo>().ReverseMap();
        }
    }
}
