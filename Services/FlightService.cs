using Flight.Database;
using Flight.Entity;
using Flight.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Flight.Services
{
    public class FlightService : IFlightService
    {
        private readonly Mycontext _context;

        public FlightService(Mycontext context)
        {
            _context = context;
        }

       

        public void AddFlight(flights flight)
        {
            _context.Flights.Add(flight);
            _context.SaveChanges();
        }

       

        public void DeleteFlight(int flightNumber)
        {
            var flightToDelete = _context.Flights.FirstOrDefault(f => f.FlightNumber == flightNumber);
            if (flightToDelete != null)
            {
                _context.Flights.Remove(flightToDelete);
                _context.SaveChanges();
            }
        }

        

        public List<flights> GetAllFlights()
        {
            return _context.Flights.ToList();
        }
        public List<flights> GetFlightByName(string airline)
        {

            return _context.Flights.Where(f => f.Airline == airline).ToList();
        }


        public flights GetFlightById(int flightNumber)
        {
            return _context.Flights.Find(flightNumber);
        }





        public void UpdateFlight(flights flight)
        {
            if (flight != null)
            {
                _context.Flights.Update(flight);
                _context.SaveChanges();
            }
        }
    }
}
