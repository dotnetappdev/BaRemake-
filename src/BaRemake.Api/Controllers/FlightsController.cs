using BaRemake.Data.Repositories;
using BaRemake.Shared.DTOs;
using BaRemake.Shared.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaRemake.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlightsController : ControllerBase
{
    private readonly IFlightRepository _flights;

    public FlightsController(IFlightRepository flights) => _flights = flights;

    /// <summary>Search available flights.</summary>
    [HttpGet("search")]
    public async Task<ActionResult<FlightSearchResult>> Search(
        [FromQuery] string origin,
        [FromQuery] string destination,
        [FromQuery] DateTime date,
        [FromQuery] int passengers = 1,
        [FromQuery] SeatClass cabin = SeatClass.Economy,
        [FromQuery] TripType tripType = TripType.OneWay,
        [FromQuery] DateTime? returnDate = null)
    {
        if (string.IsNullOrWhiteSpace(origin) || string.IsNullOrWhiteSpace(destination))
            return BadRequest("Origin and destination are required.");

        var outbound = await _flights.SearchFlightsAsync(origin, destination, date, passengers, cabin);

        var result = new FlightSearchResult
        {
            OutboundFlights = outbound,
            SearchRequest = new FlightSearchRequest
            {
                OriginCode = origin,
                DestinationCode = destination,
                DepartureDate = date,
                Adults = passengers,
                CabinClass = cabin,
                TripType = tripType,
                ReturnDate = returnDate
            }
        };

        if (tripType == TripType.Return && returnDate.HasValue)
            result.ReturnFlights = await _flights.SearchFlightsAsync(
                destination, origin, returnDate.Value, passengers, cabin);

        return Ok(result);
    }

    /// <summary>Get a single flight by ID.</summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<FlightDto>> GetFlight(int id)
    {
        var flight = await _flights.GetFlightDtoByIdAsync(id);
        return flight == null ? NotFound() : Ok(flight);
    }

    /// <summary>Get interactive seat map for a flight.</summary>
    [HttpGet("{id:int}/seatmap")]
    public async Task<ActionResult<SeatMapDto>> GetSeatMap(int id)
    {
        try
        {
            var map = await _flights.GetSeatMapAsync(id);
            return Ok(map);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>Get all airports.</summary>
    [HttpGet("airports")]
    public async Task<IActionResult> GetAirports([FromQuery] string? search = null)
    {
        if (!string.IsNullOrWhiteSpace(search))
            return Ok(await _flights.SearchAirportsAsync(search));

        return Ok(await _flights.GetAirportsAsync());
    }
}
