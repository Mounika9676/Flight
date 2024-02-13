using AutoMapper;
using Flight.DTO;
using Flight.Entity;

namespace Flight.Profiles
{
    public class FlightProfile : Profile
    {
        public FlightProfile()
        {
                CreateMap<flights, FlightDTO>();
                CreateMap<FlightDTO, flights>();
        }
    }
}

