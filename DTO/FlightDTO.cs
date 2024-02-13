using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;

namespace Flight.DTO
{
    public class FlightDTO
    {
        public int FlightNumber { get; set; }
        public string? Airline { get; set; }
        public string? Departure { get; set; }
        public string? Arrival { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public int TicketPrice { get; set; }
    }
}
