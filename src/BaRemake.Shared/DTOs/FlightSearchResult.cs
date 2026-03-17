using BaRemake.Shared.Enums;

namespace BaRemake.Shared.DTOs;

public class FlightSearchResult
{
    public List<FlightDto> OutboundFlights { get; set; } = new();
    public List<FlightDto> ReturnFlights { get; set; } = new();
    public FlightSearchRequest SearchRequest { get; set; } = null!;

    // Available airlines in results (for filter chips)
    public List<AirlineFilterDto> Airlines =>
        OutboundFlights.Select(f => new AirlineFilterDto
        {
            Code = f.AirlineCode,
            Name = f.AirlineName,
            LogoUrl = f.AirlineLogoUrl,
            BrandColor = f.AirlineBrandColor
        })
        .DistinctBy(a => a.Code)
        .OrderBy(a => a.Name)
        .ToList();
}

public class AirlineFilterDto
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? LogoUrl { get; set; }
    public string BrandColor { get; set; } = "#002157";
}

public class FlightDto
{
    public int Id { get; set; }
    public string FlightNumber { get; set; } = string.Empty;

    // Airline
    public int AirlineId { get; set; }
    public string AirlineName { get; set; } = string.Empty;
    public string AirlineCode { get; set; } = string.Empty;
    public string? AirlineLogoUrl { get; set; }
    public string AirlineBrandColor { get; set; } = "#002157";
    public AirlineType AirlineType { get; set; }

    // Route
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

    public bool IsLowCost => AirlineType is AirlineType.LowCost or AirlineType.Budget or AirlineType.UltraLowCost;

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
