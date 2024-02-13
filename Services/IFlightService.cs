using Flight.Entity;

namespace Flight.Services
{
    public interface IFlightService
    {
        void AddFlight(flights flight);

        void UpdateFlight(flights flight);


        void DeleteFlight(int FlightNumber);


        flights GetFlightById(int FlightNumber);


        List<flights> GetAllFlights();

        List<flights> GetFlightByName(string search);
    }
}
