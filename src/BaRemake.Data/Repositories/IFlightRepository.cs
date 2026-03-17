using BaRemake.Shared.DTOs;
using BaRemake.Shared.Enums;
using BaRemake.Shared.Models;

namespace BaRemake.Data.Repositories;

public interface IFlightRepository
{
    Task<List<FlightDto>> SearchFlightsAsync(string originCode, string destinationCode,
        DateTime date, int passengers, SeatClass cabinClass);

    Task<FlightDto?> GetFlightDtoByIdAsync(int flightId);
    Task<Flight?> GetFlightByIdAsync(int flightId);
    Task<SeatMapDto> GetSeatMapAsync(int flightId);
    Task<List<Airport>> GetAirportsAsync();
    Task<List<Airport>> SearchAirportsAsync(string query);
    Task<List<Flight>> GetFlightsByDateAsync(DateTime date);
    Task<List<Flight>> GetUpcomingFlightsAsync(int count = 20);
    Task UpdateFlightPricingAsync(AdminFlightPricingDto dto);
    Task UpdateSeatAvailabilityAsync(int flightId, int seatId, bool isOccupied);
    Task<List<AdminFlightPricingDto>> GetFlightsForAdminAsync(DateTime? from = null, DateTime? to = null);
}
