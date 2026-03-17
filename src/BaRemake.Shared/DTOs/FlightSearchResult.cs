using BaRemake.Shared.Enums;

namespace BaRemake.Shared.DTOs;

public class FlightSearchResult
{
    public List<FlightDto> OutboundFlights { get; set; } = new();
    public List<FlightDto> ReturnFlights { get; set; } = new();
    public FlightSearchRequest SearchRequest { get; set; } = null!;
}

public class FlightDto
{
    public int Id { get; set; }
    public string FlightNumber { get; set; } = string.Empty;
    public string OriginCode { get; set; } = string.Empty;
    public string OriginCity { get; set; } = string.Empty;
    public string OriginName { get; set; } = string.Empty;
    public string DestinationCode { get; set; } = string.Empty;
    public string DestinationCity { get; set; } = string.Empty;
    public string DestinationName { get; set; } = string.Empty;
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public int DurationMinutes { get; set; }
    public FlightStatus Status { get; set; }
    public string AircraftModel { get; set; } = string.Empty;

    // Pricing per class
    public decimal? EconomyPrice { get; set; }
    public decimal? BusinessPrice { get; set; }
    public decimal? FirstPrice { get; set; }

    // Availability
    public int AvailableEconomySeats { get; set; }
    public int AvailableBusinessSeats { get; set; }
    public int AvailableFirstSeats { get; set; }

    public string DurationDisplay => $"{DurationMinutes / 60}h {DurationMinutes % 60}m";

    public bool HasAvailableSeats(SeatClass cls) => cls switch
    {
        SeatClass.Economy => AvailableEconomySeats > 0,
        SeatClass.Business => AvailableBusinessSeats > 0,
        SeatClass.First => AvailableFirstSeats > 0,
        _ => false
    };

    public decimal? PriceForClass(SeatClass cls) => cls switch
    {
        SeatClass.Economy => EconomyPrice,
        SeatClass.Business => BusinessPrice,
        SeatClass.First => FirstPrice,
        _ => null
    };
}
