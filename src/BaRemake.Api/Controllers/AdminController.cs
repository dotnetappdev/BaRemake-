using BaRemake.Data.Repositories;
using BaRemake.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaRemake.Api.Controllers;

[ApiController]
[Route("api/admin")]
[Authorize(Policy = "AdminOnly")]
public class AdminController : ControllerBase
{
    private readonly IFlightRepository _flights;
    private readonly IBookingRepository _bookings;

    public AdminController(IFlightRepository flights, IBookingRepository bookings)
    {
        _flights = flights;
        _bookings = bookings;
    }

    /// <summary>Admin dashboard statistics.</summary>
    [HttpGet("dashboard")]
    public async Task<ActionResult<AdminDashboardDto>> Dashboard() =>
        Ok(await _bookings.GetDashboardStatsAsync());

    /// <summary>List all flights for admin pricing management.</summary>
    [HttpGet("flights")]
    public async Task<IActionResult> GetFlights(
        [FromQuery] DateTime? from = null,
        [FromQuery] DateTime? to = null) =>
        Ok(await _flights.GetFlightsForAdminAsync(from, to));

    /// <summary>Update pricing for a flight.</summary>
    [HttpPut("flights/{id:int}/pricing")]
    public async Task<IActionResult> UpdatePricing(int id, [FromBody] AdminFlightPricingDto dto)
    {
        dto.FlightId = id;
        await _flights.UpdateFlightPricingAsync(dto);
        return Ok(new { message = "Pricing updated." });
    }

    /// <summary>Get all bookings for admin.</summary>
    [HttpGet("bookings")]
    public async Task<IActionResult> GetBookings() =>
        Ok(await _bookings.GetRecentBookingsAsync(100));
}
