using AutoMapper;
using Flight.Database;
using Flight.DTO;
using Flight.Entity;
using Flight.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Flight.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;
        private readonly ILogger<FlightController> _logger;

        public FlightController(IFlightService flightService, IMapper mapper, ILogger<FlightController> logger)
        {
            _flightService = flightService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet, Route("GetAllFlights")]
        public IActionResult GetAllFlights()
        {
            try
            {
                List<flights> flights = _flightService.GetAllFlights();
                List<FlightDTO> flightsDTO = _mapper.Map<List<FlightDTO>>(flights);
                return StatusCode(200, flightsDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost, Route("AddFlight")]
        public IActionResult AddFlight(FlightDTO flightDTO)
        {
            try
            {
                flights flight = _mapper.Map<flights>(flightDTO);
                _flightService.AddFlight(flight);
                return StatusCode(200, flight);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet, Route("GetFlightById/{flightNumber}")]
        public IActionResult GetFlightById(int flightNumber)
        {
            try
            {
                flights flight = _flightService.GetFlightById(flightNumber);
                FlightDTO flightDTO = _mapper.Map<FlightDTO>(flight);
                if (flight != null)
                    return StatusCode(200, flightDTO);
                else
                    return StatusCode(404, new JsonResult("Invalid Flight Number"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut, Route("UpdateFlight")]
        public IActionResult UpdateFlight(FlightDTO flightDTO)
        {
            try
            {
                flights flight = _mapper.Map<flights>(flightDTO);
                _flightService.UpdateFlight(flight);
                return StatusCode(200, flight);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete, Route("DeleteFlight/{flightNumber}")]
        public IActionResult DeleteFlight(int flightNumber)
        {
            try
            {
                _flightService.DeleteFlight(flightNumber);
                return StatusCode(200, new JsonResult($"Flight with Number {flightNumber} is Deleted"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet, Route("GetFlightByName")]
        public IActionResult GetFlightByName(string airline)
        {
            try
            {
                List<flights> flights = _flightService.GetFlightByName(airline);
                List<FlightDTO> flightDTOs = _mapper.Map<List<FlightDTO>>(flights);
                if (flights.Count > 0)
                    return StatusCode(200, flightDTOs);
                else
                    return StatusCode(404, new JsonResult("No Flights found for the specified airline"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
