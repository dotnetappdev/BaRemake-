using System.ComponentModel.DataAnnotations;

namespace BaRemake.Shared.Models;

/// <summary>
/// An airline operator (BA, EasyJet, Ryanair, Virgin Atlantic, etc.)
/// Each flight belongs to an airline.
/// </summary>
public class Airline
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>IATA 2-letter code e.g. "BA", "U2", "FR"</summary>
    [Required, StringLength(3)]
    public string IATACode { get; set; } = string.Empty;

    /// <summary>ICAO 3-letter code e.g. "BAW", "EZY", "RYR"</summary>
    [StringLength(4)]
    public string? ICAOCode { get; set; }

    [StringLength(200)]
    public string? LogoUrl { get; set; }

    /// <summary>Brand hex colour e.g. "#002157" for BA</summary>
    [StringLength(7)]
    public string BrandColor { get; set; } = "#002157";

    [StringLength(7)]
    public string BrandColorSecondary { get; set; } = "#ffffff";

    [StringLength(100)]
    public string? Country { get; set; }

    [StringLength(100)]
    public string? Hub { get; set; }

    /// <summary>Budget, Low-cost, Full-service, Charter, Regional</summary>
    public AirlineType Type { get; set; } = AirlineType.FullService;

    public bool IsActive { get; set; } = true;

    // Luggage policy summary
    public string? CabinBagPolicy { get; set; }
    public string? HoldBagPolicy { get; set; }

    // Navigation
    public ICollection<Flight> Flights { get; set; } = new List<Flight>();
}

public enum AirlineType
{
    FullService = 1,
    LowCost = 2,
    Budget = 3,
    Charter = 4,
    Regional = 5,
    UltraLowCost = 6
}
