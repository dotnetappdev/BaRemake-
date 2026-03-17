using BaRemake.Data.Repositories;
using BaRemake.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BaRemake.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BookingsController : ControllerBase
{
    private readonly IBookingRepository _bookings;

    public BookingsController(IBookingRepository bookings) => _bookings = bookings;

    /// <summary>Create a new booking.</summary>
    [HttpPost]
    public async Task<IActionResult> CreateBooking([FromBody] BookingSessionDto session)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        try
        {
            var booking = await _bookings.CreateBookingAsync(session, userId);
            return Ok(new { bookingReference = booking.BookingReference, bookingId = booking.Id });
        }
        catch (Exception ex)
        {
            return BadRequest($"Booking failed: {ex.Message}");
        }
    }

    /// <summary>Get all bookings for the current user.</summary>
    [HttpGet("my")]
    public async Task<IActionResult> GetMyBookings()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var bookings = await _bookings.GetUserBookingsAsync(userId);
        return Ok(bookings);
    }

    /// <summary>Get booking by reference.</summary>
    [HttpGet("{reference}")]
    public async Task<IActionResult> GetBooking(string reference)
    {
        var booking = await _bookings.GetBookingByReferenceAsync(reference.ToUpper());
        if (booking == null) return NotFound();

        // Customers can only view own bookings
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var isAdmin = User.IsInRole("Admin");
        if (!isAdmin && booking.UserId != userId) return Forbid();

        return Ok(booking);
    }

    /// <summary>Cancel a booking.</summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> CancelBooking(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var cancelled = await _bookings.CancelBookingAsync(id, userId);
        return cancelled ? Ok(new { message = "Booking cancelled." }) : NotFound();
    }
}
