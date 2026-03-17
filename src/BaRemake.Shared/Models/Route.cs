using System.ComponentModel.DataAnnotations;

namespace BaRemake.Shared.Models;

public class Route
{
    public int Id { get; set; }

    public int OriginAirportId { get; set; }
    public Airport OriginAirport { get; set; } = null!;

    public int DestinationAirportId { get; set; }
    public Airport DestinationAirport { get; set; } = null!;

    public int DurationMinutes { get; set; }

    [StringLength(10)]
    public string RouteCode { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    // Navigation
    public ICollection<Flight> Flights { get; set; } = new List<Flight>();
}
