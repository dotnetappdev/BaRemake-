using BaRemake.Shared.DTOs;
using BaRemake.Shared.Enums;
using BaRemake.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BaRemake.Data.Repositories;

public class FlightRepository : IFlightRepository
{
    private readonly ApplicationDbContext _db;

    public FlightRepository(ApplicationDbContext db) => _db = db;

    public async Task<List<FlightDto>> SearchFlightsAsync(
        string originCode, string destinationCode,
        DateTime date, int passengers, SeatClass cabinClass)
    {
        var dayStart = date.Date;
        var dayEnd = dayStart.AddDays(1);

        var flights = await _db.Flights
            .Include(f => f.Route)
                .ThenInclude(r => r.OriginAirport)
            .Include(f => f.Route)
                .ThenInclude(r => r.DestinationAirport)
            .Include(f => f.Aircraft)
            .Include(f => f.SeatPrices)
            .Where(f =>
                f.Route.OriginAirport.IATACode == originCode.ToUpper() &&
                f.Route.DestinationAirport.IATACode == destinationCode.ToUpper() &&
                f.DepartureTime >= dayStart &&
                f.DepartureTime < dayEnd &&
                f.Status != FlightStatus.Cancelled)
            .OrderBy(f => f.DepartureTime)
            .ToListAsync();

        return flights
            .Where(f => HasEnoughSeats(f, cabinClass, passengers))
            .Select(MapToDto)
            .ToList();
    }

    public async Task<FlightDto?> GetFlightDtoByIdAsync(int flightId)
    {
        var flight = await _db.Flights
            .Include(f => f.Route).ThenInclude(r => r.OriginAirport)
            .Include(f => f.Route).ThenInclude(r => r.DestinationAirport)
            .Include(f => f.Aircraft)
            .Include(f => f.SeatPrices)
            .FirstOrDefaultAsync(f => f.Id == flightId);

        return flight == null ? null : MapToDto(flight);
    }

    public async Task<Flight?> GetFlightByIdAsync(int flightId) =>
        await _db.Flights
            .Include(f => f.Route).ThenInclude(r => r.OriginAirport)
            .Include(f => f.Route).ThenInclude(r => r.DestinationAirport)
            .Include(f => f.Aircraft).ThenInclude(a => a.Seats)
            .Include(f => f.SeatPrices)
            .FirstOrDefaultAsync(f => f.Id == flightId);

    public async Task<SeatMapDto> GetSeatMapAsync(int flightId)
    {
        var flight = await _db.Flights
            .Include(f => f.Aircraft).ThenInclude(a => a.Seats)
            .Include(f => f.SeatPrices)
            .FirstOrDefaultAsync(f => f.Id == flightId)
            ?? throw new KeyNotFoundException($"Flight {flightId} not found");

        // Get occupied seats for this flight
        var occupiedSeatIds = await _db.BookingPassengers
            .Where(bp => bp.FlightId == flightId && bp.AircraftSeatId != null &&
                         bp.Booking.Status != BookingStatus.Cancelled)
            .Select(bp => bp.AircraftSeatId!.Value)
            .ToListAsync();

        var extraLegroomFee = flight.SeatPrices
            .FirstOrDefault(p => p.Class == SeatClass.Economy)?.ExtraLegroomFee ?? 0;
        var seatSelectionFee = flight.SeatPrices
            .FirstOrDefault(p => p.Class == SeatClass.Economy)?.SeatSelectionFee ?? 0;

        var rows = flight.Aircraft.Seats
            .GroupBy(s => s.Row)
            .OrderBy(g => g.Key)
            .Select(g => new SeatRowDto
            {
                RowNumber = g.Key,
                IsExitRow = g.Any(s => s.IsExitRow),
                IsExtraLegroom = g.Any(s => s.IsExtraLegroom),
                Seats = g.OrderBy(s => s.Column).Select(s => new SeatDto
                {
                    Id = s.Id,
                    SeatNumber = s.SeatNumber,
                    Row = s.Row,
                    Column = s.Column,
                    Class = s.Class,
                    Status = occupiedSeatIds.Contains(s.Id)
                        ? SeatStatus.Occupied
                        : SeatStatus.Available,
                    IsExtraLegroom = s.IsExtraLegroom,
                    IsExitRow = s.IsExitRow,
                    IsWindowSeat = s.IsWindowSeat,
                    IsAisleSeat = s.IsAisleSeat,
                    IsMiddleSeat = s.IsMiddleSeat,
                    SeatFee = s.IsExtraLegroom ? extraLegroomFee : seatSelectionFee
                }).ToList()
            }).ToList();

        return new SeatMapDto
        {
            FlightId = flightId,
            AircraftModel = flight.Aircraft.Model,
            SeatLayout = flight.Aircraft.SeatLayout,
            TotalRows = flight.Aircraft.TotalRows,
            Rows = rows
        };
    }

    public async Task<List<Airport>> GetAirportsAsync() =>
        await _db.Airports.Where(a => a.IsActive).OrderBy(a => a.City).ToListAsync();

    public async Task<List<Airport>> SearchAirportsAsync(string query)
    {
        query = query.ToLower();
        return await _db.Airports
            .Where(a => a.IsActive && (
                a.IATACode.ToLower().Contains(query) ||
                a.City.ToLower().Contains(query) ||
                a.Name.ToLower().Contains(query) ||
                a.Country.ToLower().Contains(query)))
            .OrderBy(a => a.City)
            .Take(10)
            .ToListAsync();
    }

    public async Task<List<Flight>> GetFlightsByDateAsync(DateTime date)
    {
        var start = date.Date;
        var end = start.AddDays(1);
        return await _db.Flights
            .Include(f => f.Route).ThenInclude(r => r.OriginAirport)
            .Include(f => f.Route).ThenInclude(r => r.DestinationAirport)
            .Include(f => f.SeatPrices)
            .Where(f => f.DepartureTime >= start && f.DepartureTime < end)
            .OrderBy(f => f.DepartureTime)
            .ToListAsync();
    }

    public async Task<List<Flight>> GetUpcomingFlightsAsync(int count = 20) =>
        await _db.Flights
            .Include(f => f.Route).ThenInclude(r => r.OriginAirport)
            .Include(f => f.Route).ThenInclude(r => r.DestinationAirport)
            .Where(f => f.DepartureTime > DateTime.UtcNow)
            .OrderBy(f => f.DepartureTime)
            .Take(count)
            .ToListAsync();

    public async Task UpdateFlightPricingAsync(AdminFlightPricingDto dto)
    {
        var prices = await _db.FlightSeatPrices
            .Where(p => p.FlightId == dto.FlightId)
            .ToListAsync();

        UpdatePrice(prices, SeatClass.Economy, dto.EconomyBasePrice,
            dto.EconomyDynamicMultiplier, dto.ExtraLegroomFee, dto.SeatSelectionFee, dto.BaggageFee);
        UpdatePrice(prices, SeatClass.Business, dto.BusinessBasePrice,
            dto.BusinessDynamicMultiplier, 0, 0, dto.BaggageFee);
        UpdatePrice(prices, SeatClass.First, dto.FirstBasePrice,
            1.0m, 0, 0, 0);

        await _db.SaveChangesAsync();
    }

    public async Task UpdateSeatAvailabilityAsync(int flightId, int seatId, bool isOccupied)
    {
        var flight = await _db.Flights.FindAsync(flightId);
        if (flight == null) return;
        var seat = await _db.AircraftSeats.FindAsync(seatId);
        if (seat == null) return;

        if (seat.Class == SeatClass.Economy)
            flight.AvailableEconomySeats += isOccupied ? -1 : 1;
        else if (seat.Class == SeatClass.Business)
            flight.AvailableBusinessSeats += isOccupied ? -1 : 1;

        await _db.SaveChangesAsync();
    }

    public async Task<List<AdminFlightPricingDto>> GetFlightsForAdminAsync(
        DateTime? from = null, DateTime? to = null)
    {
        var query = _db.Flights
            .Include(f => f.Route).ThenInclude(r => r.OriginAirport)
            .Include(f => f.Route).ThenInclude(r => r.DestinationAirport)
            .Include(f => f.SeatPrices)
            .AsQueryable();

        if (from.HasValue) query = query.Where(f => f.DepartureTime >= from.Value);
        if (to.HasValue) query = query.Where(f => f.DepartureTime <= to.Value);

        var flights = await query.OrderBy(f => f.DepartureTime).ToListAsync();

        return flights.Select(f =>
        {
            var eco = f.SeatPrices.FirstOrDefault(p => p.Class == SeatClass.Economy);
            var bus = f.SeatPrices.FirstOrDefault(p => p.Class == SeatClass.Business);
            return new AdminFlightPricingDto
            {
                FlightId = f.Id,
                FlightNumber = f.FlightNumber,
                DepartureTime = f.DepartureTime,
                Route = $"{f.Route.OriginAirport.IATACode} → {f.Route.DestinationAirport.IATACode}",
                EconomyBasePrice = eco?.BasePrice ?? 0,
                BusinessBasePrice = bus?.BasePrice ?? 0,
                FirstBasePrice = f.SeatPrices.FirstOrDefault(p => p.Class == SeatClass.First)?.BasePrice ?? 0,
                EconomyDynamicMultiplier = eco?.DynamicMultiplier ?? 1.0m,
                BusinessDynamicMultiplier = bus?.DynamicMultiplier ?? 1.0m,
                ExtraLegroomFee = eco?.ExtraLegroomFee ?? 0,
                SeatSelectionFee = eco?.SeatSelectionFee ?? 0,
                BaggageFee = eco?.BaggageFee ?? 0
            };
        }).ToList();
    }

    // ── Helpers ──────────────────────────────────────────────────────────────

    private static bool HasEnoughSeats(Flight f, SeatClass cls, int count) => cls switch
    {
        SeatClass.Economy => f.AvailableEconomySeats >= count,
        SeatClass.Business => f.AvailableBusinessSeats >= count,
        SeatClass.First => f.AvailableFirstSeats >= count,
        _ => false
    };

    private static FlightDto MapToDto(Flight f) => new()
    {
        Id = f.Id,
        FlightNumber = f.FlightNumber,
        OriginCode = f.Route.OriginAirport.IATACode,
        OriginCity = f.Route.OriginAirport.City,
        OriginName = f.Route.OriginAirport.Name,
        DestinationCode = f.Route.DestinationAirport.IATACode,
        DestinationCity = f.Route.DestinationAirport.City,
        DestinationName = f.Route.DestinationAirport.Name,
        DepartureTime = f.DepartureTime,
        ArrivalTime = f.ArrivalTime,
        DurationMinutes = f.Route.DurationMinutes,
        Status = f.Status,
        AircraftModel = f.Aircraft.Model,
        EconomyPrice = f.SeatPrices.FirstOrDefault(p => p.Class == SeatClass.Economy)?.EffectivePrice,
        BusinessPrice = f.SeatPrices.FirstOrDefault(p => p.Class == SeatClass.Business)?.EffectivePrice,
        FirstPrice = f.SeatPrices.FirstOrDefault(p => p.Class == SeatClass.First)?.EffectivePrice,
        AvailableEconomySeats = f.AvailableEconomySeats,
        AvailableBusinessSeats = f.AvailableBusinessSeats,
        AvailableFirstSeats = f.AvailableFirstSeats
    };

    private static void UpdatePrice(List<FlightSeatPrice> prices, SeatClass cls,
        decimal basePrice, decimal multiplier, decimal extraLegroom, decimal seatFee, decimal baggageFee)
    {
        var p = prices.FirstOrDefault(x => x.Class == cls);
        if (p == null) return;
        p.BasePrice = basePrice;
        p.DynamicMultiplier = multiplier;
        p.ExtraLegroomFee = extraLegroom;
        p.SeatSelectionFee = seatFee;
        p.BaggageFee = baggageFee;
        p.LastUpdated = DateTime.UtcNow;
    }
}
