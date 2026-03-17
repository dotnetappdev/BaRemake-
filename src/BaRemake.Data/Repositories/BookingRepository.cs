using BaRemake.Data.Repositories;
using BaRemake.Shared.DTOs;
using BaRemake.Shared.Enums;
using BaRemake.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BaRemake.Data.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IFlightRepository _flights;

    public BookingRepository(ApplicationDbContext db, IFlightRepository flights)
    {
        _db = db;
        _flights = flights;
    }

    public async Task<Booking> CreateBookingAsync(BookingSessionDto session, string userId)
    {
        await using var tx = await _db.Database.BeginTransactionAsync();
        try
        {
            var booking = new Booking
            {
                BookingReference = GenerateReference(),
                UserId = userId,
                Status = BookingStatus.Confirmed,
                TripType = session.SearchRequest.TripType,
                TotalPrice = session.TotalPrice,
                TaxAmount = session.TaxAmount,
                ContactEmail = session.Passengers.FirstOrDefault()?.FirstName + "@baremake.com",
                ConfirmedAt = DateTime.UtcNow
            };

            _db.Bookings.Add(booking);
            await _db.SaveChangesAsync(); // get booking.Id

            // Create passenger records for outbound flight
            if (session.SelectedOutboundFlight != null)
            {
                for (int i = 0; i < session.Passengers.Count; i++)
                {
                    var p = session.Passengers[i];
                    var seatId = i < session.SelectedOutboundSeatIds.Count
                        ? session.SelectedOutboundSeatIds[i] : (int?)null;

                    _db.BookingPassengers.Add(new BookingPassenger
                    {
                        BookingId = booking.Id,
                        FlightId = session.SelectedOutboundFlight.Id,
                        AircraftSeatId = seatId,
                        Title = p.Title,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        DateOfBirth = p.DateOfBirth,
                        PassportNumber = p.PassportNumber,
                        NationalityCode = p.NationalityCode,
                        IsLeadPassenger = p.IsLeadPassenger,
                        CabinBaggage = p.CabinBaggage,
                        HasCheckedBaggage = p.HasCheckedBaggage,
                        Class = session.SearchRequest.CabinClass,
                        Direction = "Outbound"
                    });

                    // Mark seat as occupied
                    if (seatId.HasValue)
                        await _flights.UpdateSeatAvailabilityAsync(
                            session.SelectedOutboundFlight.Id, seatId.Value, true);
                }
            }

            // Return flight passengers
            if (session.SelectedReturnFlight != null)
            {
                for (int i = 0; i < session.Passengers.Count; i++)
                {
                    var p = session.Passengers[i];
                    var seatId = i < session.SelectedReturnSeatIds.Count
                        ? session.SelectedReturnSeatIds[i] : (int?)null;

                    _db.BookingPassengers.Add(new BookingPassenger
                    {
                        BookingId = booking.Id,
                        FlightId = session.SelectedReturnFlight.Id,
                        AircraftSeatId = seatId,
                        Title = p.Title,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        DateOfBirth = p.DateOfBirth,
                        Class = session.SearchRequest.CabinClass,
                        Direction = "Return"
                    });

                    if (seatId.HasValue)
                        await _flights.UpdateSeatAvailabilityAsync(
                            session.SelectedReturnFlight.Id, seatId.Value, true);
                }
            }

            await _db.SaveChangesAsync();
            await tx.CommitAsync();
            return booking;
        }
        catch
        {
            await tx.RollbackAsync();
            throw;
        }
    }

    public async Task<List<Booking>> GetUserBookingsAsync(string userId) =>
        await _db.Bookings
            .Include(b => b.Passengers).ThenInclude(p => p.Flight)
                .ThenInclude(f => f.Route).ThenInclude(r => r.OriginAirport)
            .Include(b => b.Passengers).ThenInclude(p => p.Flight)
                .ThenInclude(f => f.Route).ThenInclude(r => r.DestinationAirport)
            .Include(b => b.Passengers).ThenInclude(p => p.AircraftSeat)
            .Where(b => b.UserId == userId)
            .OrderByDescending(b => b.CreatedAt)
            .ToListAsync();

    public async Task<Booking?> GetBookingByReferenceAsync(string reference) =>
        await _db.Bookings
            .Include(b => b.User)
            .Include(b => b.Passengers).ThenInclude(p => p.Flight)
                .ThenInclude(f => f.Route).ThenInclude(r => r.OriginAirport)
            .Include(b => b.Passengers).ThenInclude(p => p.Flight)
                .ThenInclude(f => f.Route).ThenInclude(r => r.DestinationAirport)
            .Include(b => b.Passengers).ThenInclude(p => p.AircraftSeat)
            .FirstOrDefaultAsync(b => b.BookingReference == reference.ToUpper());

    public async Task<Booking?> GetBookingByIdAsync(int id) =>
        await _db.Bookings
            .Include(b => b.User)
            .Include(b => b.Passengers).ThenInclude(p => p.Flight)
            .FirstOrDefaultAsync(b => b.Id == id);

    public async Task<bool> CancelBookingAsync(int bookingId, string userId)
    {
        var booking = await _db.Bookings
            .Include(b => b.Passengers)
            .FirstOrDefaultAsync(b => b.Id == bookingId && b.UserId == userId);

        if (booking == null || booking.Status == BookingStatus.Cancelled) return false;

        booking.Status = BookingStatus.Cancelled;
        booking.CancelledAt = DateTime.UtcNow;

        // Release seats
        foreach (var p in booking.Passengers.Where(p => p.AircraftSeatId.HasValue))
            await _flights.UpdateSeatAvailabilityAsync(p.FlightId, p.AircraftSeatId!.Value, false);

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<List<RecentBookingDto>> GetRecentBookingsAsync(int count = 20) =>
        await _db.Bookings
            .Include(b => b.User)
            .Include(b => b.Passengers).ThenInclude(p => p.Flight)
                .ThenInclude(f => f.Route).ThenInclude(r => r.OriginAirport)
            .Include(b => b.Passengers).ThenInclude(p => p.Flight)
                .ThenInclude(f => f.Route).ThenInclude(r => r.DestinationAirport)
            .OrderByDescending(b => b.CreatedAt)
            .Take(count)
            .Select(b => new RecentBookingDto
            {
                Reference = b.BookingReference,
                CustomerName = b.User.FirstName + " " + b.User.LastName,
                Route = b.Passengers.Any()
                    ? b.Passengers.First().Flight.Route.OriginAirport.IATACode + " → " +
                      b.Passengers.First().Flight.Route.DestinationAirport.IATACode
                    : "N/A",
                Amount = b.TotalPrice,
                Status = b.Status,
                CreatedAt = b.CreatedAt
            })
            .ToListAsync();

    public async Task<AdminDashboardDto> GetDashboardStatsAsync()
    {
        var today = DateTime.Today;
        var tomorrow = today.AddDays(1);

        var bookingsToday = await _db.Bookings
            .Where(b => b.CreatedAt >= today && b.CreatedAt < tomorrow)
            .ToListAsync();

        var flightsToday = await _db.Flights
            .Where(f => f.DepartureTime >= today && f.DepartureTime < tomorrow)
            .CountAsync();

        var upcomingFlights = await _flights.GetUpcomingFlightsAsync(5);
        var recentBookings = await GetRecentBookingsAsync(5);

        return new AdminDashboardDto
        {
            TotalFlightsToday = flightsToday,
            TotalBookingsToday = bookingsToday.Count,
            RevenueToday = bookingsToday.Where(b => b.Status != BookingStatus.Cancelled).Sum(b => b.TotalPrice),
            TotalPassengersToday = bookingsToday.Count * 1, // simplified
            PendingBookings = await _db.Bookings.CountAsync(b => b.Status == BookingStatus.Pending),
            CancelledBookings = await _db.Bookings.CountAsync(b =>
                b.CancelledAt >= today && b.CancelledAt < tomorrow),
            UpcomingFlights = upcomingFlights.Select(f => new FlightDto
            {
                Id = f.Id,
                FlightNumber = f.FlightNumber,
                DepartureTime = f.DepartureTime,
                ArrivalTime = f.ArrivalTime
            }).ToList(),
            RecentBookings = recentBookings
        };
    }

    private static string GenerateReference()
    {
        const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
        var rng = new Random();
        return new string(Enumerable.Range(0, 6).Select(_ => chars[rng.Next(chars.Length)]).ToArray());
    }
}
