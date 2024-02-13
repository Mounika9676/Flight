using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flight.Entity
{
    public class flights
    {
        [Key]
        public int FlightNumber { get; set; }
        public string? Airline { get; set; }
        public string? Departure { get; set; }
        public string? Arrival { get; set; }
        [Column(TypeName = "Date")]
        public DateTime DepartureDateTime { get; set; }
        [Column(TypeName = "Date")]
        public DateTime ArrivalDateTime { get; set; }
        public int TicketPrice { get; set; }

    }
}
